using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_GAMEOBJECT_QUERY)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_GAMEOBJECT_QUERY_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var gameObject = new GameObjectTemplate();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            packet.ReadBit("Unk1 Byte");

            var unk1 = packet.ReadInt32("Unk1 UInt32");
            if (unk1 == 0)
                return;

            gameObject.Type = packet.ReadEnum<GameObjectType>("Type", TypeCode.Int32);

            gameObject.DisplayId = packet.ReadUInt32("Display ID");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.ReadCString("Icon Name");
            gameObject.CastCaption = packet.ReadCString("Cast Caption");
            gameObject.UnkString = packet.ReadCString("Unk String");

            gameObject.Data = new int[33];
            for (var i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.ReadInt32("Data", i);


            gameObject.Size = packet.ReadSingle("Size");

            gameObject.QuestItems = new uint[packet.ReadByte("QuestItems Length")];

            for (var i = 0; i < gameObject.QuestItems.Length; i++)
                gameObject.QuestItems[i] = (uint)packet.ReadEntry<Int32>(StoreNameType.Item, "Quest Item", i);

            packet.ReadEnum<ClientType>("Expansion", TypeCode.UInt32);

            Storage.GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                Name = gameObject.Name,
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GAMEOBJ_REPORT_USE)]
        [Parser(Opcode.CMSG_GAMEOBJ_USE)]
        public static void HandleGoUse(Packet packet)
        {
            packet.ReadPackedGuid128("Guid");
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_CUSTOM_ANIM)]
        public static void HandleGoCustomAnim(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("CustomAnim");
            packet.ReadBit("PlayAsDespawn");
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_DESPAWN)]
        public static void HandleGameObjectDespawn(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT)]
        public static void HandleGoActivateAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("AnimKitID");
            packet.ReadBit("Maintain");
        }
    }
}
