using System;
using System.Collections.Generic;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Processing;
using PacketDumper.Enums;
using PacketDumper.Misc;
using PacketDumper.DataStructures;
using PacketParser.DataStructures;
using PacketParser.SQL;

namespace PacketDumper.Processing.SQLData
{
    public class StartInformationStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        // Start info (Race, Class)
        public readonly TimeSpanDictionary<Tuple<Race, Class>, StartAction> StartActions = new TimeSpanDictionary<Tuple<Race, Class>, StartAction>();
        public readonly TimeSpanDictionary<Tuple<Race, Class>, StartSpell> StartSpells = new TimeSpanDictionary<Tuple<Race, Class>, StartSpell>();
        public readonly TimeSpanDictionary<Tuple<Race, Class>, StartPosition> StartPositions = new TimeSpanDictionary<Tuple<Race, Class>, StartPosition>();
        public bool Init(PacketFileProcessor file)
        {
            return Settings.SQLOutput.HasFlag(SQLOutputFlags.StartInformation);
        }

        public void ProcessPacket(Packet packet)
        {
            if (packet.Status != ParsedStatus.Success)
                return;

            switch(Opcodes.GetOpcode(packet.Opcode))
            {
                case Opcode.SMSG_CHAR_ENUM:
                    {
                        var characters = packet.GetData().GetNode<IndexedTreeNode>("Characters");
                        foreach (var c in characters)
                        {
                            var character = c.Value;
                            if (character.GetNode<Boolean>("First Login"))
                            {
                                var race = character.GetNode<Race>("Race");
                                var clss = character.GetNode<Class>("Class");

                                var mapId = character.GetNode<Int32>("Map Id");
                                var zone = character.GetNode<UInt32>("Zone Id");
                                var pos = character.GetNode<Vector3>("Position");

                                var startPos = new StartPosition { Map = mapId, Position = pos, Zone = (int)zone };
                                StartPositions.Add(new Tuple<Race, Class>(race, clss), startPos, packet.TimeSpan);
                            }
                        }
                        break;
                    }
                case Opcode.SMSG_ACTION_BUTTONS:
                    {
                        Player character = PacketFileProcessor.Current.GetProcessor<SessionStore>().LoggedInCharacter;
                        if (character == null || !character.FirstLogin)
                            return;

                        var startAction = new StartAction
                        {
                            Actions = new List<PacketDumper.DataStructures.Action>()
                        };
                        var buttons = packet.GetData().GetNode<IndexedTreeNode>("Buttons");
                        foreach (var b in buttons)
                        {
                            var action = new PacketDumper.DataStructures.Action
                            {
                                Button = (uint)b.Key,
                                Id = b.Value.GetNode<uint>("Action"),
                                Type = b.Value.GetNode<ActionButtonType>("Type")
                            };

                            startAction.Actions.Add(action);
                        }
                        StartActions.Add(new Tuple<Race, Class>(character.Race, character.Class), startAction, packet.TimeSpan);
                        break;
                    }
                case Opcode.SMSG_INITIAL_SPELLS:
                    {
                        var spells = new List<uint>();
                        var buttons = packet.GetData().GetNode<IndexedTreeNode>("InitialSpells");
                        foreach (var s in spells)
                            spells.Add((uint)packet.GetNode<UInt32>("Spell ID"));

                        var startSpell = new StartSpell();
                        startSpell.Spells = spells;

                        Player character = PacketFileProcessor.Current.GetProcessor<SessionStore>().LoggedInCharacter;
                        if (character != null && character.FirstLogin)
                            StartSpells.Add(new Tuple<Race, Class>(character.Race, character.Class), startSpell, packet.TimeSpan);
                        break;
                    }

            }
        }

        public void Finish()
        {

        }
        public string Build()
        {
            var result = String.Empty;

            var names = PacketFileProcessor.Current.GetProcessor<NameStore>();
            if (!StartActions.IsEmpty())
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (KeyValuePair<Tuple<Race, Class>, Tuple<StartAction, TimeSpan?>> startActions in StartActions)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startActions.Key.Item1 + " - " + startActions.Key.Item2;
                    rows.Add(comment);

                    foreach (var action in startActions.Value.Item1.Actions)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("race", startActions.Key.Item1);
                        row.AddValue("class", startActions.Key.Item2);
                        row.AddValue("button", action.Button);
                        row.AddValue("action", action.Id);
                        row.AddValue("type", action.Type);
                        if (action.Type == ActionButtonType.Spell)
                            row.Comment = names.GetName(StoreNameType.Spell, (int)action.Id, false);
                        if (action.Type == ActionButtonType.Item)
                            row.Comment = names.GetName(StoreNameType.Item, (int)action.Id, false);

                        rows.Add(row);
                    }
                }

                result = new QueryBuilder.SQLInsert("playercreateinfo_action", rows, 2).Build();
            }

            if (!StartPositions.IsEmpty())
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (KeyValuePair<Tuple<Race, Class>, Tuple<StartPosition, TimeSpan?>> startPosition in StartPositions)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startPosition.Key.Item1 + " - " + startPosition.Key.Item2;
                    rows.Add(comment);

                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("race", startPosition.Key.Item1);
                    row.AddValue("class", startPosition.Key.Item2);
                    row.AddValue("map", startPosition.Value.Item1.Map);
                    row.AddValue("zone", startPosition.Value.Item1.Zone);
                    row.AddValue("position_x", startPosition.Value.Item1.Position.X);
                    row.AddValue("position_y", startPosition.Value.Item1.Position.Y);
                    row.AddValue("position_z", startPosition.Value.Item1.Position.Z);

                    row.Comment = names.GetName(StoreNameType.Map, startPosition.Value.Item1.Map, false) + " - " +
                                  names.GetName(StoreNameType.Zone, startPosition.Value.Item1.Zone, false);

                    rows.Add(row);
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo", rows, 2).Build();
            }

            if (!StartSpells.IsEmpty())
            {
                var rows = new List<QueryBuilder.SQLInsertRow>();
                foreach (var startSpells in StartSpells)
                {
                    var comment = new QueryBuilder.SQLInsertRow();
                    comment.HeaderComment = startSpells.Key.Item1 + " - " + startSpells.Key.Item2;
                    rows.Add(comment);

                    foreach (var spell in startSpells.Value.Item1.Spells)
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("race", startSpells.Key.Item1);
                        row.AddValue("class", startSpells.Key.Item2);
                        row.AddValue("Spell", spell);
                        row.AddValue("Note", names.GetName(StoreNameType.Spell, (int)spell, false));

                        rows.Add(row);
                    }
                }

                result += new QueryBuilder.SQLInsert("playercreateinfo_spell", rows, 2).Build();
            }

            return result;
        }
    }
}
