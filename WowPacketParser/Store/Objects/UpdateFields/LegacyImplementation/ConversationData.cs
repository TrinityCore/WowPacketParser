using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class ConversationData : IConversationData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;
        private Dictionary<int, List<UpdateField>> DynamicUpdateFields => Object.DynamicUpdateFields;

        public ConversationData(WoWObject obj)
        {
            Object = obj;
        }

        public IConversationLine[] Lines
        {
            get
            {
                var lines = DynamicUpdateFields.GetValue<ConversationDynamicField, uint>(ConversationDynamicField.CONVERSATION_DYNAMIC_FIELD_LINES).ToArray();
                var LineSize = 4;
                var structuredLines = new IConversationLine[lines.Length / LineSize];
                for (var i = 0; i + LineSize <= lines.Length; i += LineSize)
                {
                    var line = new Line();
                    line.ConversationLineID = (int)lines[i + 0];
                    line.StartTime = lines[i + 1];
                    line.UiCameraID = (int)lines[i + 2];
                    var part = lines[i + 3];
                    var actorIdx = (byte)(part & 0xFF);
                    line.ActorIndex = actorIdx;
                    line.Flags = (byte)((part >> 8) & 0xFF);
                    structuredLines[i / LineSize] = line;
                }
                return structuredLines;
            }
        }

        public int? LastLineEndTime => UpdateFields.GetValue<ConversationField, int?>(ConversationField.CONVERSATION_LAST_LINE_END_TIME);

        public DynamicUpdateField<IConversationActor> Actors
        {
            get
            {
                var updateField = new DynamicUpdateField<IConversationActor>();
                var actors = DynamicUpdateFields.GetValue<ConversationDynamicField, uint>(ConversationDynamicField.CONVERSATION_DYNAMIC_FIELD_ACTORS).ToList();
                var ActorSize = 6;
                updateField.Resize((uint)(actors.Count / ActorSize));

                for (var i = 0; i + ActorSize <= actors.Count; i += ActorSize)
                {
                    var actor = new Actor();
                    actor.Type = actors[i + 4];

                    if (actor.Type == (uint)ActorType.WorldObjectActor)
                    {
                        ulong lowPart = Utilities.MAKE_PAIR64(actors[i + 0], actors[i + 1]);
                        ulong highPart = Utilities.MAKE_PAIR64(actors[i + 2], actors[i + 3]);
                        actor.ActorGUID = new WowGuid128(lowPart, highPart);
                    }
                    if (actor.Type == (uint)ActorType.CreatureActor)
                    {
                        if (ClientVersion.AddedInVersion(ClientVersionBuild.V8_0_1_27101))
                        {
                            actor.CreatureID = actors[i + 0];
                            actor.CreatureDisplayInfoID = actors[i + 1];
                        }
                        else
                        {
                            actor.Id = (int)actors[i + 0];
                            actor.CreatureID = actors[i + 1];
                            actor.CreatureDisplayInfoID = actors[i + 2];
                        }
                    }

                    updateField[i / ActorSize] = actor;
                }

                return updateField;
            }
        }

        public class Line : IConversationLine
        {
            public int ConversationLineID { get; set; }
            public uint StartTime { get; set; }
            public int UiCameraID { get; set; }
            public byte ActorIndex { get; set; }
            public byte Flags { get; set; }
            public byte ChatType { get; set; }
        }

        public class Actor : IConversationActor
        {
            public int Id { get; set; }
            public uint CreatureID { get; set; }
            public uint CreatureDisplayInfoID { get; set; }
            public WowGuid ActorGUID { get; set; }
            public uint Type { get; set; }
            public uint NoActorObject { get; set; }
        }
    }
}
