using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

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

        [DBFieldName("TextureKitId", TargetedDatabase.BattleForAzeroth)]
        public uint? TextureKitId;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        public IConversationData ConversationData;

        public ConversationTemplate() : base()
        {
            ConversationData = new ConversationData(this);
        }

        public override void LoadValuesFromUpdateFields()
        {
            Id = (uint)ObjectData.EntryID;
            LastLineEndTime = (uint)ConversationData.LastLineEndTime;

            var actorTemplates = new List<ConversationActorTemplate>();
            var actors = ConversationData.Actors;
            for (var i = 0; i < actors.Count; ++i)
            {
                var actor = new ConversationActorTemplate
                {
                    Type = actors[i].Type
                };

                if (actor.Type == (uint)ActorType.WorldObjectActor)
                    actor.Guid = actors[i].ActorGUID;
                else if (actor.Type == (uint)ActorType.CreatureActor)
                {
                    actor.Id = actors[i].Id;
                    actor.CreatureId = actors[i].CreatureID;
                    actor.CreatureModelId = actors[i].CreatureDisplayInfoID;

                    Storage.ConversationActorTemplates.Add(actor);
                }

                actorTemplates.Add(actor);
            }

            var lines = ConversationData.Lines;
            for (var i = 0; i < lines.Length; ++i)
            {
                var line = new ConversationLineTemplate
                {
                    Id = (uint)lines[i].ConversationLineID,
                    StartTime = lines[i].StartTime,
                    UiCameraID = (uint)lines[i].UiCameraID,
                    ActorIdx = lines[i].ActorIndex,
                    Flags = lines[i].Flags
                };

                if (i == 0)
                    FirstLineID = line.Id;

                var actorTemplate = actorTemplates[line.ActorIdx.Value];
                var actor = new ConversationActor
                {
                    ConversationId = Id,
                    ConversationActorId = actorTemplate.Id,
                    Guid = actorTemplate.Guid,
                    Idx = line.ActorIdx
                };

                Storage.ConversationLineTemplates.Add(line);
                Storage.ConversationActors.Add(actor);
            }
        }
    }
}
