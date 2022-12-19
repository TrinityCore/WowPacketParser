using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
using WowPacketParser.SQL;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class NpcHandler
    {
        public static GossipMessageOption ReadGossipOptionsData(uint menuId, WowGuid npcGuid, Packet packet, params object[] idx)
        {
            GossipMessageOption gossipMessageOption = new();
            GossipMenuOption gossipOption = new GossipMenuOption
            {
                MenuID = menuId
            };

            gossipOption.OptionID = gossipMessageOption.OptionIndex = (uint)packet.ReadInt32("ClientOption", idx);
            gossipOption.OptionNpc = (GossipOptionNpc?)packet.ReadByte("OptionNPC", idx);
            gossipMessageOption.OptionNpc = (int)gossipOption.OptionNpc;
            gossipOption.BoxCoded = gossipMessageOption.BoxCoded = packet.ReadByte("OptionFlags", idx) != 0;
            gossipOption.BoxMoney = gossipMessageOption.BoxCost = (uint)packet.ReadInt32("OptionCost", idx);

            gossipOption.OptionText = gossipMessageOption.Text = packet.ReadCString("Text", idx);
            gossipMessageOption.BoxText = packet.ReadCString("Confirm", idx);

            if (!string.IsNullOrEmpty(gossipMessageOption.BoxText))
                gossipOption.BoxText = gossipMessageOption.BoxText;

            gossipOption.FillBroadcastTextIDs();

            if (Settings.TargetedDatabase < TargetedDatabase.Shadowlands)
                gossipOption.FillOptionType(npcGuid);

            Storage.GossipMenuOptions.Add((gossipOption.MenuID, gossipOption.OptionID), gossipOption, packet.TimeSpan);

            return gossipMessageOption;
        }

        public static GossipQuestOption ReadGossipQuestTextData(Packet packet, params object[] idx)
        {
            var gossipQuest = new GossipQuestOption();
            gossipQuest.QuestId = (uint)packet.ReadInt32("QuestID", idx);
            packet.ReadInt32("QuestType", idx);
            packet.ReadInt32("QuestLevel", idx);
            packet.ReadInt32("QuestFlags", idx);
            packet.ReadBool("Repeatable", idx);
            gossipQuest.Title = packet.ReadCString("QuestTitle", idx);

            return gossipQuest;
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GOSSIP_MESSAGE)]
        public static void HandleNpcGossip(Packet packet)
        {
            PacketGossipMessage packetGossip = packet.Holder.GossipMessage = new();
            GossipMenu gossip = new GossipMenu();

            WowGuid guid = packet.ReadGuid("GossipGUID");
            packetGossip.GossipSource = guid;

            gossip.ObjectType = guid.GetObjectType();
            gossip.ObjectEntry = guid.GetEntry();

            int menuId = packet.ReadInt32("GossipID");
            gossip.MenuID = packetGossip.MenuId = (uint)menuId;
            gossip.TextID = packetGossip.TextId = (uint)packet.ReadInt32("TextID");

            int optionsCount = packet.ReadInt32("GossipOptionsCount");
            for (int i = 0; i < optionsCount; ++i)
                packetGossip.Options.Add(ReadGossipOptionsData((uint)menuId, guid, packet, i, "GossipOptions"));

            int questsCount = packet.ReadInt32("GossipQuestsCount");
            for (int i = 0; i < questsCount; ++i)
                packetGossip.Quests.Add(ReadGossipQuestTextData(packet, i, "GossipQuests"));
        }
    };
}