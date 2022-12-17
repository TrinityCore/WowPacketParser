﻿using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("conversation_template")]
    public sealed record ConversationTemplate : WoWObject, IDataModel
    {
        [DBFieldName("Id", true)]
        public uint? Id;

        [DBFieldName("FirstLineID")]
        public uint? FirstLineID;

        [DBFieldName("LastLineEndTime", TargetedDatabaseFlag.TillBattleForAzeroth)]
        public uint? LastLineEndTime;

        [DBFieldName("TextureKitId", TargetedDatabaseFlag.SinceBattleForAzeroth)]
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
                    Type = actors[i].Type,
                    NoActorObject = actors[i].NoActorObject > 0
                };

                if (actor.Type == (uint)ActorType.WorldObjectActor)
                {
                    actor.Id = actors[i].Id;
                    actor.Guid = actors[i].ActorGUID;
                }
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
                    Idx = line.ActorIdx,
                    CreatureId = actorTemplate.CreatureId,
                    CreatureDisplayInfoId = actorTemplate.CreatureModelId,
                    NoActorObject = actorTemplate.NoActorObject,
                    ActivePlayerObject = actorTemplate.Guid != null && actorTemplate.Guid.Low == 0xFFFFFFFFFFFFFFFF && actorTemplate.Guid.GetHighType() == HighGuidType.Player
                };

                Storage.ConversationLineTemplates.Add(line);
                Storage.ConversationActors.Add(actor);
            }
        }
    }
}
