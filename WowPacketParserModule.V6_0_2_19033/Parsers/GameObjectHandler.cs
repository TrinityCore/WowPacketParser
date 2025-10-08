using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Proto;
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
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            GameObjectTemplate gameObject = new GameObjectTemplate
            {
                Entry = (uint)entry.Key
            };
            var query = packet.Holder.QueryGameObjectResponse = new() { Entry = (uint)entry.Key };

            packet.ReadBit("Allow");

            int dataSize = packet.ReadInt32("DataSize");
            query.HasData = dataSize > 0;
            if (dataSize == 0)
                return;

            gameObject.Type = packet.ReadInt32E<GameObjectType>("Type");

            gameObject.DisplayID = packet.ReadUInt32("Display ID");

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.ReadCString("Icon Name");
            gameObject.OpeningText = packet.ReadCString("Opening Text");
            gameObject.ClosingText = packet.ReadCString("Closing Text");

            gameObject.Data = new int?[33];
            for (int i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.ReadInt32("Data", i);

            gameObject.Size = packet.ReadSingle("Size");

            byte questItemsCount = packet.ReadByte("QuestItemsCount");
            for (uint i = 0; i < questItemsCount; i++)
            {
                GameObjectTemplateQuestItem questItem = new GameObjectTemplateQuestItem
                {
                    GameObjectEntry = (uint)entry.Key,
                    Idx = i,
                    ItemId = packet.ReadUInt32<ItemId>("QuestItem", i)
                };

                query.Items.Add(questItem.ItemId.Value);
                Storage.GameObjectTemplateQuestItems.Add(questItem, packet.TimeSpan);
            }

            gameObject.RequiredLevel = query.RequiredLevel = packet.ReadInt32("RequiredLevel");

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
            foreach (var data in gameObject.Data)
                query.Data.Add(data.Value);
        }

        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        public static void HandleGoReportUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadPackedGuid128("GameObjectGUID");
            use.Report = true;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479) && ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_7_48676))
                packet.ReadBit("IsSoftInteract");
        }

        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGoUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadPackedGuid128("GameObjectGUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_2_46479) && ClientVersion.RemovedInVersion(ClientVersionBuild.V10_0_7_48676))
                packet.ReadBit("IsSoftInteract");
        }

        [Parser(Opcode.SMSG_PAGE_TEXT)]
        public static void HandleGoPageText(Packet packet)
        {
            packet.ReadPackedGuid128("GameObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGoCustomAnim(Packet packet)
        {
            var customAnim = packet.Holder.GameObjectCustomAnim = new();
            customAnim.GameObject = packet.ReadPackedGuid128("ObjectGUID");
            customAnim.Anim = packet.ReadInt32("CustomAnim");
            customAnim.PlayAsDespawn = packet.ReadBit("PlayAsDespawn");
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
            packet.ReadInt32<SpellId>("SpellID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_RESET_STATE)]
        public static void HandleGameObjectResetState(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_UI_LINK)]
        public static void HandleGameObjectUiLink(Packet packet)
        {
            packet.ReadPackedGuid128("GUID");
            packet.ReadInt32("UILink");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                packet.ReadInt32("UIItemInteractionID");
        }

        [Parser(Opcode.SMSG_FORCE_OBJECT_RELINK)]
        public static void HandleForceObjectRelink(Packet packet)
        {
            packet.ReadPackedGuid128("ObjectGUID");
        }
    }
}
