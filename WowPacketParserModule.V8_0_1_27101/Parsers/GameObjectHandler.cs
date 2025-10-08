using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V8_0_1_27101.Parsers
{
    public static class GameObjectHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_QUERY_GAME_OBJECT_RESPONSE)]
        public static void HandleGameObjectQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry("Entry");
            if (entry.Value) // entry is masked
                return;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_3_0_33062))
                packet.ReadPackedGuid128("GUID");

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

            gameObject.DisplayID = (uint)packet.ReadInt32("Display ID");

            var name = new string[4];
            for (int i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);
            gameObject.Name = name[0];

            gameObject.IconName = packet.ReadCString("Icon Name");
            gameObject.OpeningText = packet.ReadCString("Opening Text");
            gameObject.ClosingText = packet.ReadCString("Closing Text");

            gameObject.Data = new int?[34];
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
                    ItemId = (uint)packet.ReadInt32<ItemId>("QuestItem", i)
                };

                query.Items.Add(questItem.ItemId.Value);
                Storage.GameObjectTemplateQuestItems.Add(questItem, packet.TimeSpan);
            }

            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                gameObject.ContentTuningId = query.ContentTuningId = packet.ReadInt32("ContentTuningId");
            else
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
    }
}
