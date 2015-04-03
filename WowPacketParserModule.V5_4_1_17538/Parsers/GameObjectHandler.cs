using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_1_17539.Parsers
{
    public static class GameObjectHandler
    {
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
                return;

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

            gameObject.QuestItems = new uint[packet.ReadByte("QuestItems Length")]; // correct?

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

        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            var guid = new byte[8];

            var entry = packet.ReadInt32("Entry");

            packet.StartBitStream(guid, 6, 3, 1, 2, 0, 7, 5, 4);
            packet.ParseBitStream(guid, 5, 0, 6, 7, 3, 4, 2, 1);

            packet.WriteGuid("GUID", guid);
        }
    }
}
