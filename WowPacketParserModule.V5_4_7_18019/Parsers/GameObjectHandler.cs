using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_GAMEOBJECT_QUERY)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            var entry = packet.ReadUInt32("Entry");

            var guid = packet.StartBitStream(1, 7, 0, 3, 5, 4, 6, 2);
            packet.ParseBitStream(guid, 3, 6, 1, 2, 0, 7, 5, 4);

            packet.WriteGuid("GameObject Guid", guid);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
        public static void HandleGOReportUse(Packet packet)
        {
            var guid = packet.StartBitStream(7, 0, 3, 2, 1, 6, 5, 4);
            packet.ParseBitStream(guid, 1, 3, 5, 4, 6, 7, 2, 0);

            packet.WriteGuid("GameObject Guid", guid);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var guid = packet.StartBitStream(4, 7, 6, 5, 1, 3, 2, 0);
            packet.ParseBitStream(guid, 4, 3, 2, 0, 5, 6, 1, 7);

            packet.WriteGuid("GameObject Guid", guid);
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var gameObject = new GameObjectTemplate();

            var entry = packet.ReadEntry("Entry");
            var dataSize = packet.ReadUInt32("Unk 1"); // Maybe data size?

            if (dataSize > 0)
            {
                gameObject.Type = packet.ReadEnum<GameObjectType>("Type", TypeCode.Int32);
                gameObject.DisplayId = packet.ReadUInt32("Display Id");

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

                gameObject.QuestItems = new uint[packet.ReadByte("Quest Item Count")];

                for (var i = 0; i < gameObject.QuestItems.Length; i++)
                    gameObject.QuestItems[i] = (uint)packet.ReadEntry<Int32>(StoreNameType.Item, "Quest Item", i);

                packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);
            }

            packet.ReadByte("Unk Byte"); // Mostly seen 128.

            Storage.GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                Name = gameObject.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }
    }
}
