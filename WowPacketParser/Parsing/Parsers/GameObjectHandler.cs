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
            QueryHandler.ReadQueryHeader(packet);
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
            {
                name[i] = packet.ReadCString("Name " + i);
            }

            var iconName = packet.ReadCString("Icon Name");

            var castCaption = packet.ReadCString("Cast Caption");

            var unkStr = packet.ReadCString("Unk String");

            var data = new int[24];
            for (var i = 0; i < 24; i++)
            {
                data[i] = packet.ReadInt32("Data " + i);
            }

            var size = packet.ReadSingle("Size");

            var qItem = new int[6];
            for (var i = 0; i < 6; i++)
            {
                qItem[i] = packet.ReadInt32("Quest Item " + i);
            }

            SQLStore.WriteData(SQLStore.GameObjects.GetCommand(entry.Key, type, dispId, name[0], iconName,
                castCaption, unkStr, data, size, qItem));
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.ReadPackedGuid("GO GUID");

            packet.ReadPackedGuid("Vehicle GUID:");

            packet.ReadPackedGuid("Player GUID");

            packet.ReadInt32("Damage");

            Console.WriteLine("Spell ID: " + Extensions.SpellLine(packet.ReadInt32()));
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM)]
        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
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
