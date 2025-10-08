using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
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

            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 1, 7, 0, 3, 5, 4, 6, 2);
            packet.ParseBitStream(guid, 3, 6, 1, 2, 0, 7, 5, 4);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            GameObjectTemplate gameObject = new GameObjectTemplate
            {
                Entry = (uint)entry.Key
            };
            var query = packet.Holder.QueryGameObjectResponse = new() { Entry = (uint)entry.Key };

            int unk1 = packet.ReadInt32("Unk1 UInt32");
            query.HasData = unk1 > 0;
            if (unk1 == 0)
            {
                packet.ReadByte("Unk1 Byte");
                return;
            }

            gameObject.Type = packet.ReadInt32E<GameObjectType>("Type");
            gameObject.DisplayID = packet.ReadUInt32("Display ID");

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.ReadCString("Icon Name");
            gameObject.OpeningText = packet.ReadCString("Opening Text");
            gameObject.ClosingText = packet.ReadCString("Closing Text");

            gameObject.Data = new int?[32];
            for (int i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.ReadInt32("Data", i);


            gameObject.Size = packet.ReadSingle("Size");

            gameObject.QuestItems = new uint?[packet.ReadByte("QuestItems Length")];

            for (int i = 0; i < gameObject.QuestItems.Length; i++)
                gameObject.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            gameObject.RequiredLevel = packet.ReadInt32("RequiredLevel");

            packet.ReadByte("Unk1 Byte");

            Storage.GameObjectTemplates.Add(gameObject, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.GameObject,
                ID = entry.Key,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        public static void HandleGOReportUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject() { Report = true };
            var guid = new byte[8];

            packet.StartBitStream(guid, 7, 0, 3, 2, 1, 6, 5, 4);
            packet.ParseBitStream(guid, 1, 3, 5, 4, 6, 7, 2, 0);

            use.GameObject = packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 7, 6, 5, 1, 3, 2, 0);
            packet.ParseBitStream(guid, 4, 3, 2, 0, 5, 6, 1, 7);

            use.GameObject = packet.WriteGuid("Guid", guid);
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
            var customAnim = packet.Holder.GameObjectCustomAnim = new();
            var guid = new byte[8];

            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            bool hasAnim = !packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadXORByte(guid, 1);
            if (hasAnim)
                customAnim.Anim = packet.ReadInt32("Anim");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);

            customAnim.GameObject = packet.WriteGuid("Guid", guid);
        }
    }
}
