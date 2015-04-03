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
            var gameObject = new GameObjectTemplate();

            var unk1 = packet.ReadInt32("Unk1 UInt32");
            if (unk1 == 0)
            {
                packet.ReadEntry("Entry");
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

            gameObject.QuestItems = new uint[packet.ReadByte("QuestItems Length")]; // correct?

            for (var i = 0; i < gameObject.QuestItems.Length; i++)
                gameObject.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            packet.ReadUInt32E<ClientType>("Expansion");

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;
            packet.ReadByte("Unk Byte");

            Storage.GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGOCustomAnim(Packet packet)
        {
            var guid = new byte[8];
            packet.ReadBit("Unk bit");
            packet.StartBitStream(guid, 6, 3, 4);
            var hasAnim = !packet.ReadBit();
            packet.StartBitStream(guid, 5, 1, 2, 0, 7);
            packet.ResetBitReader();

            packet.ReadXORBytes(guid, 0, 2, 5, 7, 4, 3, 1);
            if (hasAnim)
                packet.ReadInt32("Anim");

            packet.ReadXORByte(guid, 6);
            packet.WriteGuid("GUID", guid);
        }
    }
}
