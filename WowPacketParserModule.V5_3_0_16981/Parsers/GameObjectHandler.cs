using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_3_0_16981.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            packet.ReadInt32("Entry");

            var guid = packet.StartBitStream(1, 2, 7, 6, 4, 3, 0, 5);
            packet.ParseBitStream(guid, 6, 7, 3, 4, 0, 2, 5, 1);
            packet.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            GameObjectTemplate gameObject = new GameObjectTemplate();

            int unk1 = packet.ReadInt32("Unk1 UInt32");
            if (unk1 == 0)
            {
                var goEntry = packet.ReadEntry("Entry").Key;
                packet.ReadByte("Unk1 Byte");
                packet.Holder.QueryGameObjectResponse = new() { Entry = (uint)goEntry, HasData = false };
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

            gameObject.QuestItems = new uint?[packet.ReadByte("QuestItems Length")]; // correct?

            for (int i = 0; i < gameObject.QuestItems.Length; i++)
                gameObject.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            gameObject.RequiredLevel = packet.ReadInt32("RequiredLevel");

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            gameObject.Entry = (uint)entry.Key;

            packet.ReadByte("Unk Byte");

            Storage.GameObjectTemplates.Add(gameObject, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = StoreNameType.GameObject,
                ID = entry.Key,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
            var query = packet.Holder.QueryGameObjectResponse = new() { Entry = (uint)entry.Key, HasData = true };
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

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGOCustomAnim(Packet packet)
        {
            var customAnim = packet.Holder.GameObjectCustomAnim = new();
            
            var guid = new byte[8];
            packet.ReadBit("Unk bit");
            packet.StartBitStream(guid, 6, 3, 4);
            var hasAnim = !packet.ReadBit();
            packet.StartBitStream(guid, 5, 1, 2, 0, 7);
            packet.ResetBitReader();

            packet.ReadXORBytes(guid, 0, 2, 5, 7, 4, 3, 1);
            if (hasAnim)
                customAnim.Anim = packet.ReadInt32("Anim");

            packet.ReadXORByte(guid, 6);
            customAnim.GameObject = packet.WriteGuid("GUID", guid);
        }
    }
}
