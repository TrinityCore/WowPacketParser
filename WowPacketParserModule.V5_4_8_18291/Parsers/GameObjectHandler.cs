using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class GameObjectHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var gameObject = new GameObjectTemplate();

            packet.ReadByte("Unk Byte");
            var entry = packet.ReadEntry("Entry");
            var Unk = packet.ReadInt32();

            gameObject.Type = packet.ReadEnum<GameObjectType>("Type", TypeCode.Int32);
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
                gameObject.QuestItems[i] = (uint)packet.ReadEntryWithName<Int32>(StoreNameType.Item, "Quest Item", i);

            packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            //var entry = packet.ReadEntry("Entry");
            //if (entry.Value) // entry is masked
            //    return;
            //packet.ReadByte("Unk Byte");

            Storage.GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                Name = gameObject.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
        public static void HandleGameObjectReportUse(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(4, 7, 5, 3, 6, 1, 2, 0);
            packet.ParseBitStream(GUID, 7, 1, 6, 5, 0, 3, 2, 4);
            packet.WriteGuid("GUID", GUID);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        public static void HandleGameObjectuse(Packet packet)
        {
            var GUID = new byte[8];
            GUID = packet.StartBitStream(6, 1, 3, 4, 0, 5, 7, 2);
            packet.ParseBitStream(GUID, 0, 1, 6, 2, 3, 4, 5, 7);
            packet.WriteGuid("GUID", GUID);
        }
		
    }
}
