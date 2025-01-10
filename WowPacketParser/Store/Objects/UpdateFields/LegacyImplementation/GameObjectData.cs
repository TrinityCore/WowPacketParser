using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation
{
    public class GameObjectData : IGameObjectData
    {
        private WoWObject Object { get; }
        private Dictionary<int, UpdateField> UpdateFields => Object.UpdateFields;

        public GameObjectData(WoWObject obj)
        {
            Object = obj;
        }

        public WowGuid CreatedBy
        {
            get
            {
                if (!ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                {
                    var parts = UpdateFields.GetArray<GameObjectField, uint>(GameObjectField.GAMEOBJECT_FIELD_CREATED_BY, 2);
                    return new WowGuid64(Utilities.MAKE_PAIR64(parts[0], parts[1]));
                }
                else
                {
                    var parts = UpdateFields.GetArray<GameObjectField, uint>(GameObjectField.GAMEOBJECT_FIELD_CREATED_BY, 4);
                    return new WowGuid128(Utilities.MAKE_PAIR64(parts[0], parts[1]), Utilities.MAKE_PAIR64(parts[2], parts[3]));
                }
            }
        }

        public uint? Flags => UpdateFields.GetValue<GameObjectField, uint>(GameObjectField.GAMEOBJECT_FLAGS);

        public Quaternion? ParentRotation
        {
            get
            {
                var parts = UpdateFields.GetArray<GameObjectField, float?>(GameObjectField.GAMEOBJECT_PARENTROTATION, 4);
                return new Quaternion(parts[0].GetValueOrDefault(0.0f), parts[1].GetValueOrDefault(0.0f),
                    parts[2].GetValueOrDefault(0.0f), parts[3].GetValueOrDefault(1.0f));
            }
        }

        public int? FactionTemplate => UpdateFields.GetValue<GameObjectField, int?>(GameObjectField.GAMEOBJECT_FACTION);

        public sbyte? State => (sbyte)(UpdateFields.GetValue<GameObjectField, int>(GameObjectField.GAMEOBJECT_BYTES_1) & 0x000000FF);

        public sbyte? TypeID => (sbyte)((UpdateFields.GetValue<GameObjectField, int>(GameObjectField.GAMEOBJECT_BYTES_1) & 0x0000FF00) >> 8);

        public byte? PercentHealth => (byte)((UpdateFields.GetValue<GameObjectField, uint>(GameObjectField.GAMEOBJECT_BYTES_1) & 0xFF000000) >> 24);

        public int? DisplayID => UpdateFields.GetValue<GameObjectField, int?>(GameObjectField.GAMEOBJECT_DISPLAYID);

        public uint? ArtKit => UpdateFields.GetValue<GameObjectField, uint?>(GameObjectField.GAMEOBJECT_ARTKIT);

        public int? Level => UpdateFields.GetValue<GameObjectField, int?>(GameObjectField.GAMEOBJECT_LEVEL);

        public uint? SpellVisualID => UpdateFields.GetValue<GameObjectField, uint?>(GameObjectField.GAMEOBJECT_SPELL_VISUAL_ID);

        public uint? StateSpellVisualID => UpdateFields.GetValue<GameObjectField, uint?>(GameObjectField.GAMEOBJECT_STATE_SPELL_VISUAL_ID);

        public uint? SpawnTrackingStateAnimID => UpdateFields.GetValue<GameObjectField, uint?>(GameObjectField.GAMEOBJECT_STATE_ANIM_ID);

        public uint? SpawnTrackingStateAnimKitID => UpdateFields.GetValue<GameObjectField, uint?>(GameObjectField.GAMEOBJECT_STATE_ANIM_KIT_ID);

        public uint? StateWorldEffectsQuestObjectiveID => null;

        public uint?[] StateWorldEffectIDs => UpdateFields.GetValue<GameObjectField, uint?[]>(GameObjectField.GAMEOBJECT_STATE_WORLD_EFFECT_ID);


    }
}
