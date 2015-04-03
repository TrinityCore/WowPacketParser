using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_17898.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            var guid = new byte[8];

            var entry = packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 1, 7, 0, 3, 5, 4, 6, 2);
            packet.ParseBitStream(guid, 3, 6, 1, 2, 0, 7, 5, 4);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var gameObject = new GameObjectTemplate();
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            var unk1 = packet.ReadInt32("Unk1 UInt32");
            if (unk1 == 0)
            {
                packet.ReadByte("Unk1 Byte");
                return;
            }

            gameObject.Type = packet.ReadInt32E<GameObjectType>("Type");
            gameObject.DisplayId = packet.ReadUInt32("Display ID");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.ReadCString("Icon Name");
            gameObject.CastCaption = packet.ReadCString("Cast Caption");
            gameObject.UnkString = packet.ReadCString("Unk String");

            gameObject.Data = new int[32];
            for (var i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.ReadInt32("Data", i);


            gameObject.Size = packet.ReadSingle("Size");

            gameObject.QuestItems = new uint[packet.ReadByte("QuestItems Length")];

            for (var i = 0; i < gameObject.QuestItems.Length; i++)
                gameObject.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            packet.ReadUInt32E<ClientType>("Expansion");

            packet.ReadByte("Unk1 Byte");

            Storage.GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        public static void HandleGOReportUse(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 0, 3, 2, 1, 6, 5, 4);
            packet.ParseBitStream(guid, 1, 3, 5, 4, 6, 7, 2, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 7, 6, 5, 1, 3, 2, 0);
            packet.ParseBitStream(guid, 4, 3, 2, 0, 5, 6, 1, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM)]
        public static void HandleGODespawnAnim(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 1, 2, 0, 6, 4, 5, 3, 7);
            packet.ParseBitStream(guid, 0, 5, 1, 7, 2, 3, 4, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGOCustomAnim(Packet packet)
        {
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasAnim = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var bit20 = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadXORByte(guid, 1);
            if (hasAnim)
                packet.ReadInt32("Anim");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            packet.WriteGuid("Guid", guid);
        }
    }
}
