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

            packet.Translator.ReadInt32("Entry");

            packet.Translator.StartBitStream(guid, 5, 3, 6, 2, 7, 1, 0, 4);
            packet.Translator.ParseBitStream(guid, 1, 5, 3, 4, 6, 2, 7, 0);

            packet.Translator.WriteGuid("GUID", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            packet.Translator.ReadByte("Unk1 Byte");

            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            GameObjectTemplate gameObject = new GameObjectTemplate
            {
                Entry = (uint)entry.Key
            };

            int unk1 = packet.Translator.ReadInt32("Unk1 UInt32");
            if (unk1 == 0)
            {
                packet.Translator.ReadByte("Unk1 Byte");
                return;
            }

            gameObject.Type = packet.Translator.ReadInt32E<GameObjectType>("Type");
            gameObject.DisplayID = packet.Translator.ReadUInt32("Display ID");

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.Translator.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.Translator.ReadCString("Icon Name");
            gameObject.CastCaption = packet.Translator.ReadCString("Cast Caption");
            gameObject.UnkString = packet.Translator.ReadCString("Unk String");

            gameObject.Data = new int?[32];
            for (int i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.Translator.ReadInt32("Data", i);


            gameObject.Size = packet.Translator.ReadSingle("Size");

            gameObject.QuestItems = new uint?[packet.Translator.ReadByte("QuestItems Length")];

            for (int i = 0; i < gameObject.QuestItems.Length; i++)
                gameObject.QuestItems[i] = (uint)packet.Translator.ReadInt32<ItemId>("Quest Item", i);

            gameObject.RequiredLevel = packet.Translator.ReadInt32("RequiredLevel");

            Storage.GameObjectTemplates.Add(gameObject, packet.TimeSpan);

            ObjectName objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                ID = entry.Key,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add(objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var guid = new byte[8];

            packet.Translator.StartBitStream(guid, 4, 7, 5, 3, 6, 1, 2, 0);
            packet.Translator.ParseBitStream(guid, 7, 1, 6, 5, 0, 3, 2, 4);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
