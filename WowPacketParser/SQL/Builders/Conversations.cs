using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Conversations
    {
        [BuilderMethod]
        public static string ConversationActorsData()
        {
            if (Storage.ConversationActors.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.conversation_actors))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.ConversationActors);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.ConversationActors.OrderBy(x => x.Item1.ConversationId).ToArray() : Storage.ConversationActors.ToArray(), templateDb, x => x.Guid != null ? x.Guid.ToString() : string.Empty);
        }

        [BuilderMethod]
        public static string ConversationActorTemplatesData()
        {
            if (Storage.ConversationActorTemplates.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.conversation_actor_template))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.ConversationActorTemplates);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.ConversationActorTemplates.OrderBy(x => x.Item1.Id).ToArray() : Storage.ConversationActorTemplates.ToArray(), templateDb, x => string.Empty);
        }

        [BuilderMethod]
        public static string ConversationLineTemplatesData()
        {
            if (Storage.ConversationLineTemplates.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.conversation_line_template))
                return string.Empty;

            var templateDb = SQLDatabase.Get(Storage.ConversationLineTemplates);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? Storage.ConversationLineTemplates.OrderBy(x => x.Item1.Id).ToArray() : Storage.ConversationLineTemplates.ToArray(), templateDb, x => string.Empty);
        }

        [BuilderMethod]
        public static string BroadcastTextDurationServerside()
        {
            if (Storage.BroadcastTextDurationHotfixesServerside.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.conversation_line_template))
                return string.Empty;

            var hotfixes = SQLDatabase.Get(Storage.BroadcastTextDurationHotfixesServerside, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.BroadcastTextDurationHotfixesServerside, hotfixes, StoreNameType.None);
        }

        [BuilderMethod]
        public static string ConversationTemplateData()
        {
            var conversations = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, ConversationTemplate>()                                       // empty dict if there are no objects
                : Storage.Objects.Where(
                    obj =>
                        obj.Value.Item1.Type == ObjectType.Conversation)
                    .OrderBy(pair => pair.Value.Item2)                                                  // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as ConversationTemplate);

            if (conversations.Count == 0)
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.conversation_template))
                return string.Empty;

            var conversationsData = new DataBag<ConversationTemplate>();

            foreach (var conversation in conversations)
            {
                conversationsData.Add(conversation.Value);
            }

            var templateDb = SQLDatabase.Get(conversationsData);

            return SQLUtil.Compare(Settings.SQLOrderByKey ? conversationsData.OrderBy(x => x.Item1.Id).ToArray() : conversationsData.ToArray(), templateDb, x => string.Empty);
        }
    }
}
