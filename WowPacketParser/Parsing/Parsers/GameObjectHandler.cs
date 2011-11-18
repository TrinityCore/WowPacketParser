using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;


namespace WowPacketParser.Parsing.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_GAMEOBJECT_QUERY)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            QueryHandler.ReadQueryHeader(ref packet);
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");

            if (entry.Value) // entry is masked
                return;

            var type = packet.ReadEnum<GameObjectType>("Type", TypeCode.Int32);
            var dispId = packet.ReadInt32("Display ID");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);

            var iconName = packet.ReadCString("Icon Name");
            var castCaption = packet.ReadCString("Cast Caption");
            var unkStr = packet.ReadCString("Unk String");

            var data = new int[ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333) ? 32 : 24];
            for (var i = 0; i < data.Length; i++)
                data[i] = packet.ReadInt32("Data", i);

            var size = 0f;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056)) // not sure when it was added exactly - did not exist in 2.4.1 sniff
                size = packet.ReadSingle("Size");

            var item = new int[ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                for (var i = 0; i < item.Length; i++)
                    item[i] = packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            if (ClientVersion.Build >= ClientVersionBuild.V4_2_0_14333)
                packet.ReadUInt32("Unknown UInt32");

            SQLStore.WriteData(SQLStore.GameObjects.GetCommand(entry.Key, type, dispId, name[0], iconName,
                castCaption, unkStr, data, size, item));
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.ReadPackedGuid("GO GUID");
            packet.ReadPackedGuid("Vehicle GUID:");
            packet.ReadPackedGuid("Player GUID");
            packet.ReadInt32("Damage");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM)]
        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
        [Parser(Opcode.SMSG_GAMEOBJECT_PAGETEXT)]
        [Parser(Opcode.SMSG_GAMEOBJECT_RESET_STATE)]
        public static void HandleGOMisc(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_CUSTOM_ANIM)]
        public static void HandleGOCustomAnim(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32("Anim");
        }
    }
}
