using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            packet.ReadInt32("Entry");
            packet.ReadPackedGuid128("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var gameObject = new GameObjectTemplate();

            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            packet.ReadBit("Allow");

            var dataSize = packet.ReadInt32("DataSize");
            if (dataSize == 0)
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

            gameObject.Data = new int[33];
            for (var i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.ReadInt32("Data", i);


            gameObject.Size = packet.ReadSingle("Size");

            gameObject.QuestItems = new uint[6];

            var length = packet.ReadByte("QuestItems Length");
            for (var i = 0; i < length; i++)
                gameObject.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);

            packet.ReadUInt32E<ClientType>("Expansion");

            Storage.GameObjectTemplates.Add((uint)entry.Key, gameObject, packet.TimeSpan);

            var objectName = new ObjectName
            {
                ObjectType = ObjectType.GameObject,
                Name = gameObject.Name
            };
            Storage.ObjectNames.Add((uint)entry.Key, objectName, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        [Parser(Opcode.SMSG_PAGE_TEXT)]
        public static void HandleGoMisc(Packet packet)
        {
            packet.ReadPackedGuid128("GameObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGoCustomAnim(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("CustomAnim");
            packet.ReadBit("PlayAsDespawn");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_DESPAWN)]
        public static void HandleGameObjectDespawn(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT)]
        public static void HandleGameObjectActivateAnimKit(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadInt32("AnimKitID");
            packet.ReadBit("Maintain");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL)]
        public static void HandleGameObjectPlaySpellVisual(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
            packet.ReadPackedGuid128("ActivatorGUID");
            packet.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.ReadPackedGuid128("Target");
            packet.ReadPackedGuid128("Caster");
            packet.ReadPackedGuid128("Owner");
            packet.ReadInt32("Damage");
            packet.ReadInt32("SpellID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_RESET_STATE)]
        public static void HandleGameObjectResetState(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }
    }
}
