using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_template")]
    public sealed class ConversationTemplate : WoWObject, IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("FirstLineID")]
        public uint? FirstLineID;

        [DBFieldName("LastLineEndTime")]
        public uint? LastLineEndTime;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public override void LoadValuesFromUpdateFields()
        {
            Id = UpdateFields.GetValue<ObjectField, uint>(ObjectField.OBJECT_FIELD_ENTRY);
            LastLineEndTime = UpdateFields.GetValue<ConversationField, uint>(ConversationField.CONVERSATION_LAST_LINE_END_TIME);

            var actorTemplates = new List<ConversationActorTemplate>();
            var actors = new List<UpdateField>();
            if (DynamicUpdateFields.TryGetValue(WowPacketParser.Enums.Version.UpdateFields.GetUpdateField(ConversationDynamicField.CONVERSATION_DYNAMIC_FIELD_ACTORS), out actors))
            {
                var ActorSize = 6;
                for (var i = 0; i + ActorSize <= actors.Count; i += ActorSize)
                {
                    var actor = new ConversationActorTemplate();
                    actor.Type = actors[i + 4].UInt32Value;

                    if (actor.Type == (uint)ActorType.WorldObjectActor)
                    {
                        ulong lowPart = Utilities.MAKE_PAIR64(actors[i + 0].UInt32Value, actors[i + 1].UInt32Value);
                        ulong highPart = Utilities.MAKE_PAIR64(actors[i + 2].UInt32Value, actors[i + 3].UInt32Value);
                        actor.Guid = new WowGuid128(lowPart, highPart);
                    }
                    if (actor.Type == (uint)ActorType.CreatureActor)
                    {
                        actor.Id = actors[i + 0].UInt32Value;
                        actor.CreatureId = actors[i + 1].UInt32Value;
                        actor.CreatureModelId = actors[i + 2].UInt32Value;

                        Storage.ConversationActorTemplates.Add(actor);
                    }

                    actorTemplates.Add(actor);
                }
            }

            var lines = new List<UpdateField>();
            if (DynamicUpdateFields.TryGetValue(WowPacketParser.Enums.Version.UpdateFields.GetUpdateField(ConversationDynamicField.CONVERSATION_DYNAMIC_FIELD_LINES), out lines))
            {
                var LineSize = 4;
                for (var i = 0; i + LineSize <= lines.Count; i += LineSize)
                {
                    var line = new ConversationLineTemplate();
                    line.Id = lines[i + 0].UInt32Value;
                    line.StartTime = lines[i + 1].UInt32Value;
                    line.UiCameraID = lines[i + 2].UInt32Value;
                    var part = lines[i + 3].UInt32Value;
                    var actorIdx = (byte)(part & 0xFF);
                    line.ActorIdx = actorIdx;
                    line.Flags = (byte)((part >> 8) & 0xFF);

                    if (i == 0)
                        FirstLineID = line.Id;

                    var actor = new ConversationActor();
                    actor.ConversationId = Id;
                    var actorTemplate = actorTemplates[actorIdx];
                    actor.ConversationActorId = actorTemplate.Id;
                    actor.Guid = actorTemplate.Guid;
                    actor.Idx = actorIdx;

                    Storage.ConversationLineTemplates.Add(line);
                    Storage.ConversationActors.Add(actor);
                }
            }
        }
    }
}
