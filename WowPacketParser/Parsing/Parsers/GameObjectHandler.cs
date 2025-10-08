using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class GameObjectHandler
    {
        [Parser(Opcode.CMSG_QUERY_GAME_OBJECT)]
        public static void HandleGameObjectQuery(Packet packet)
        {
            var entry = packet.ReadInt32<GOId>("Entry");
            var guid = packet.ReadGuid("GUID");

            if (guid.HasEntry() && (entry != guid.GetEntry()))
                packet.AddValue("Error", "Entry does not match calculated GUID entry");
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
                Entry = (uint)entry.Key,
                Type = packet.ReadInt32E<GameObjectType>("Type"),
                DisplayID = packet.ReadUInt32("Display ID")
            };
            var query = packet.Holder.QueryGameObjectResponse = new() { Entry = (uint)entry.Key, HasData = true};

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.ReadCString("Icon Name");
            gameObject.OpeningText = packet.ReadCString("Opening Text");
            gameObject.ClosingText = packet.ReadCString("Closing Text");

            gameObject.Data = new int?[ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596) ? 32 : 24];
            for (int i = 0; i < gameObject.Data.Length; i++)
                gameObject.Data[i] = packet.ReadInt32("Data", i);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056)) // not sure when it was added exactly - did not exist in 2.4.1 sniff
                gameObject.Size = query.Size = packet.ReadSingle("Size");

            gameObject.QuestItems = new uint?[ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192) ? 6 : 4];
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                for (int i = 0; i < gameObject.QuestItems.Length; i++)
                {
                    gameObject.QuestItems[i] = (uint)packet.ReadInt32<ItemId>("Quest Item", i);
                    query.Items.Add(gameObject.QuestItems[i].Value);
                }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                gameObject.RequiredLevel = query.RequiredLevel = packet.ReadInt32("RequiredLevel");

            packet.AddSniffData(StoreNameType.GameObject, entry.Key, "QUERY_RESPONSE");

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
            foreach (var data in gameObject.Data)
                query.Data.Add(data.Value);
        }

        [Parser(Opcode.SMSG_DESTRUCTIBLE_BUILDING_DAMAGE)]
        public static void HandleDestructibleBuildingDamage(Packet packet)
        {
            packet.ReadPackedGuid("GO GUID");
            packet.ReadPackedGuid("Vehicle GUID");
            packet.ReadPackedGuid("Player GUID");
            packet.ReadInt32("Damage");
            packet.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_GAMEOBJECT_DESPAWN_ANIM)]
        [Parser(Opcode.SMSG_PAGE_TEXT)]
        [Parser(Opcode.SMSG_GAME_OBJECT_RESET_STATE)]
        public static void HandleGOMisc(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            packet.ReadGuid("GUID");
        }
        
        [Parser(Opcode.CMSG_GAME_OBJ_USE)]
        public static void HandleGOUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject();
            use.GameObject = packet.ReadGuid("GUID");
        }
        
        [Parser(Opcode.CMSG_GAME_OBJ_REPORT_USE)]
        public static void HandleGOReportUse(Packet packet)
        {
            var use = packet.Holder.ClientUseGameObject = new PacketClientUseGameObject() { Report = true };
            use.GameObject = packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_CUSTOM_ANIM)]
        public static void HandleGOCustomAnim(Packet packet)
        {
            var customAnim = packet.Holder.GameObjectCustomAnim = new();
            customAnim.GameObject = packet.ReadGuid("GUID");
            customAnim.Anim = packet.ReadInt32("Anim");
        }

        [Parser(Opcode.SMSG_GAME_OBJECT_ACTIVATE_ANIM_KIT)] // 4.3.4
        public static void HandleGameObjectActivateAnimKit(Packet packet)
        {
            var guid = packet.StartBitStream(5, 1, 0, 4, 7, 2, 3, 6);
            packet.ParseBitStream(guid, 5, 1, 0, 3, 4, 6, 2, 7);
            packet.WriteGuid("Guid", guid);
            packet.ReadInt32("Anim");
        }
    }
}
