using System;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using Guid = PacketParser.DataStructures.Guid;
using System.Collections.Generic;
using PacketParser.Misc;
using PacketDumper.Misc;
using PacketParser.SQL;
using PacketParser.DataStructures;

namespace PacketDumper.Processing.SQLData
{
    public class CreatureTextStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        // `creature_text`
        public readonly TimeSpanMultiDictionary<uint, CreatureText> CreatureTexts = new TimeSpanMultiDictionary<uint, CreatureText>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.CreatureTemplate);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            if (Opcode.SMSG_MESSAGECHAT == Opcodes.GetOpcode(packet.Opcode))
            {
                var guid = packet.GetData().GetNode<Guid>("GUID");
                if (guid.GetObjectType() != ObjectType.Unit)
                    return;

                CreatureTexts.Add(guid.GetEntry(), packet.GetData().GetNode<CreatureText>("CreatureTextObject"), packet.TimeSpan);
            }
        }

        public void Finish()
        {

        }

        public string Build()
        {
            if (CreatureTexts.IsEmpty())
                return string.Empty;

            // For each sound and emote, if the time they were send is in the +1/-1 seconds range of
            // our texts, add that sound and emote to our Storage.CreatureTexts
            var Sounds = PacketFileProcessor.Current.GetProcessor<NpcSoundStore>().Sounds;
            var Emotes = PacketFileProcessor.Current.GetProcessor<NpcEmoteStore>().Emotes;

            foreach (KeyValuePair<uint, ICollection<Tuple<CreatureText, TimeSpan?>>> text in CreatureTexts)
            {
                // For each text
                foreach (Tuple<CreatureText, TimeSpan?> textValue in text.Value)
                {
                    // For each emote
                    foreach (KeyValuePair<Guid, ICollection<Tuple<EmoteType, TimeSpan?>>> emote in Emotes)
                    {
                        // Emote packets always have a sender (guid);
                        // skip this one if it was sent by a different creature
                        if (emote.Key.GetEntry() != text.Key)
                            continue;

                        foreach (Tuple<EmoteType, TimeSpan?> emoteValue in emote.Value)
                        {
                            var timeSpan = textValue.Item2 - emoteValue.Item2;
                            if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                                textValue.Item1.Emote = emoteValue.Item1;
                        }
                    }

                    // For each sound
                    foreach (Tuple<uint, TimeSpan?> sound in Sounds)
                    {
                        var timeSpan = textValue.Item2 - sound.Item2;
                        if (timeSpan != null && timeSpan.Value.Duration() <= TimeSpan.FromSeconds(1))
                            textValue.Item1.Sound = sound.Item1;
                    }
                }
            }

            /* DB comparer not implemented yet
            var entries = Storage.CreatureTexts.Keys.ToList();
            var creatureTextDb = SQLDatabase.GetDict<uint, CreatureText>(entries);
            */

            const string tableName = "creature_text";

            var rows = new List<QueryBuilder.SQLInsertRow>();
            foreach (KeyValuePair<uint, ICollection<Tuple<CreatureText, TimeSpan?>>> text in CreatureTexts)
            {
                foreach (Tuple<CreatureText, TimeSpan?> textValue in text.Value)
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("entry", text.Key);
                    row.AddValue("groupid", "x", false, true);
                    row.AddValue("id", "x", false, true);
                    row.AddValue("text", textValue.Item1.Text);
                    row.AddValue("type", textValue.Item1.Type);
                    row.AddValue("language", textValue.Item1.Language);
                    row.AddValue("probability", 100.0);
                    row.AddValue("emote", textValue.Item1.Emote);
                    row.AddValue("duration", 0);
                    row.AddValue("sound", textValue.Item1.Sound);
                    row.AddValue("comment", textValue.Item1.Comment);

                    rows.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert(tableName, rows, 1, false).Build();
        }
    }
}
