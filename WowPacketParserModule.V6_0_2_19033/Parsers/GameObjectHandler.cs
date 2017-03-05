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
            packet.Translator.ReadInt32("Entry");
            packet.Translator.ReadPackedGuid128("GUID");
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            GameObjectTemplate gameObject = new GameObjectTemplate
            {
                Entry = (uint)entry.Key
            };

            packet.Translator.ReadBit("Allow");

            int dataSize = packet.Translator.ReadInt32("DataSize");
            if (dataSize == 0)
                return;

            gameObject.Type = packet.Translator.ReadInt32E<GameObjectType>("Type");

            gameObject.DisplayID = packet.Translator.ReadUInt32("Display ID");

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.Translator.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.Translator.ReadCString("Icon Name");
            gameObject.CastCaption = packet.Translator.ReadCString("Cast Caption");
            gameObject.UnkString = packet.Translator.ReadCString("Unk String");

            gameObject.Data = new int?[33];
            for (int i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.Translator.ReadInt32("Data", i);

            gameObject.Size = packet.Translator.ReadSingle("Size");

            byte questItemsCount = packet.Translator.ReadByte("QuestItemsCount");
            for (uint i = 0; i < questItemsCount; i++)
            {
                GameObjectTemplateQuestItem questItem = new GameObjectTemplateQuestItem
                {
                    GameObjectEntry = (uint)entry.Key,
                    Idx = i,
                    ItemId = packet.Translator.ReadUInt32<ItemId>("QuestItem", i)
                };

                Storage.GameObjectTemplateQuestItems.Add(questItem, packet.TimeSpan);
            }

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

        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        [Parser(Opcode.SMSG_PAGE_TEXT)]
        public static void HandleGoMisc(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("GameObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGoCustomAnim(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ObjectGUID");
            packet.Translator.ReadInt32("CustomAnim");
            packet.Translator.ReadBit("PlayAsDespawn");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_DESPAWN)]
        public static void HandleGameObjectDespawn(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT)]
        public static void HandleGameObjectActivateAnimKit(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ObjectGUID");
            packet.Translator.ReadInt32("AnimKitID");
            packet.Translator.ReadBit("Maintain");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_PLAY_SPELL_VISUAL)]
        public static void HandleGameObjectPlaySpellVisual(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ObjectGUID");
            packet.Translator.ReadPackedGuid128("ActivatorGUID");
            packet.Translator.ReadInt32("SpellVisualID");
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("Target");
            packet.Translator.ReadPackedGuid128("Caster");
            packet.Translator.ReadPackedGuid128("Owner");
            packet.Translator.ReadInt32("Damage");
            packet.Translator.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_RESET_STATE)]
        public static void HandleGameObjectResetState(Packet packet)
        {
            packet.Translator.ReadPackedGuid128("ObjectGUID");
        }
    }
}
