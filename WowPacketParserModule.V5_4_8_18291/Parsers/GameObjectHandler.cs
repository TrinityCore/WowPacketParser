using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            var guid = new byte[8];

            packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 5, 3, 6, 2, 7, 1, 0, 4);
            packet.ParseBitStream(guid, 1, 5, 3, 4, 6, 2, 7, 0);

            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            packet.ReadByte("Unk1 Byte");

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

            Storage.GameObjectTemplates.Add(gameObject, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.GameObject,
                ID = entry.Key,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
            query.Type = (uint)gameObject.Type.Value;
            query.Model = gameObject.DisplayID.Value;
            query.Name = gameObject.Name;
            query.IconName = gameObject.IconName;
            query.OpeningText = gameObject.OpeningText;
            query.Size = gameObject.Size.Value;
            query.RequiredLevel = gameObject.RequiredLevel.Value;
            foreach (var data in gameObject.Data)
                query.Data.Add(data.Value);
            foreach (var item in gameObject.QuestItems)
                query.Items.Add(item.Value);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 7, 5, 3, 6, 1, 2, 0);
            packet.ParseBitStream(guid, 7, 1, 6, 5, 0, 3, 2, 4);

            packet.WriteGuid("Guid", guid);
        }
    }
}
