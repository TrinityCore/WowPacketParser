using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct QuestPOIQuery
    {
        public int MissingQuestCount;
        public fixed int MissingQuestPOIs[50];

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.Zero, ClientVersionBuild.V5_3_0_16981)]
        public static void HandleQuestPoiQuery(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.V5_3_0_16981, ClientVersionBuild.V5_4_0_17359)]
        public static void HandleQuestPoiQuery530(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.V5_4_0_17359, ClientVersionBuild.V5_4_2_17658)]
        public static void HandleQuestPoiQuery540(Packet packet)
        {
            var count = packet.ReadUInt32("Count");

            for (var i = 0; i < count; i++) // for (var i = 0; i < 50; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.V5_4_2_17658, ClientVersionBuild.V5_4_7_17898)]
        public static void HandleQuestPoiQuery542(Packet packet)
        {
            var count = packet.ReadUInt32("Count");


            for (var i = 0; i < count; i++) //for (var i = 0; i < 50; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);

            packet.ReadToEnd(); // Hack
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.V5_4_7_17898, ClientVersionBuild.V5_4_8_18291)]
        public static void HandleQuestPoiQuery547(Packet packet)
        {
            var quest = new int[50];
            for (var i = 0; i < 50; i++)
                quest[i] = packet.ReadInt32();

            var count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
                packet.AddValue("Quest ID", StoreGetters.GetName(StoreNameType.Quest, quest[i]));
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.V5_4_8_18291, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleQuestPoiQuery548(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ResetBitReader();

            for (var i = 0; i < count; i++)
                packet.ReadInt32<QuestId>("Quest ID", i);
        }

        [Parser(Opcode.CMSG_QUEST_POI_QUERY, ClientVersionBuild.V6_0_2_19033)]
        public static void HandleQuestPoiQuery602(Packet packet)
        {
            packet.ReadUInt32("MissingQuestCount");

            for (var i = 0; i < 50; i++)
                packet.ReadInt32<QuestId>("MissingQuestPOIs", i);
        }
    }
}
