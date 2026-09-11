using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Store.Objects
{
    [DBTableName("spell_areatrigger", TargetedDatabaseFlag.TillBattleForAzeroth)]
    [DBTableName("areatrigger_create_properties", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
    public sealed record AreaTriggerCreateProperties : WoWObject, IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabaseFlag.TillBattleForAzeroth, true)]
        [DBFieldName("Id", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, true)]
        public uint? AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, true)]
        public byte? IsCustom;

        [DBFieldName("AreaTriggerId")]
        public uint? AreaTriggerId;

        [DBFieldName("IsAreatriggerCustom", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
        public byte? IsAreatriggerCustom = 0;

        [DBFieldName("Flags", TargetedDatabaseFlag.Dragonflight | TargetedDatabaseFlag.TheWarWithin | TargetedDatabaseFlag.CataClassic)]
        public uint? FlagsLegacy;

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceMidnight)]
        public uint? Flags;

        [DBFieldName("MoveCurveId")]
        public int? MoveCurveId = 0;

        [DBFieldName("ScaleCurveId")]
        public int? ScaleCurveId = 0;

        [DBFieldName("MorphCurveId")]
        public int? MorphCurveId = 0;

        [DBFieldName("FacingCurveId")]
        public int? FacingCurveId = 0;

        [DBFieldName("AnimId")]
        public int? AnimId = ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772) ? -1 : 0;

        [DBFieldName("AnimKitId")]
        public int? AnimKitId = 0;

        [DBFieldName("DecalPropertiesId")]
        public uint? DecalPropertiesId = 0;

        [DBFieldName("SpellForVisuals", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, false, false, true)]
        public uint? SpellForVisuals;

        [DBFieldName("PositionalSoundKitId", TargetedDatabaseFlag.SinceMidnight)]
        public int? PositionalSoundKitId;

        [DBFieldName("TimeToTarget", TargetedDatabaseFlag.TillDragonflight)]
        public uint? TimeToTarget = 0;

        [DBFieldName("TimeToTargetScale")]
        public uint? TimeToTargetScale = 0;

        [DBFieldName("Speed", TargetedDatabaseFlag.SinceTheWarWithin)]
        public float? Speed;

        [DBFieldName("Shape", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public byte? Shape;

        [DBFieldName("ShapeData", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, 8, true)]
        public float?[] ShapeData = [0, 0, 0, 0, 0, 0, 0, 0];

        [DBFieldName("Roll", TargetedDatabaseFlag.SinceMidnight)]
        public float? Roll = 0;

        [DBFieldName("Pitch", TargetedDatabaseFlag.SinceMidnight)]
        public float? Pitch = 0;

        [DBFieldName("Yaw", TargetedDatabaseFlag.SinceMidnight)]
        public float? Yaw = 0;

        [DBFieldName("TargetRoll", TargetedDatabaseFlag.SinceMidnight, nullable: true)]
        public float? TargetRoll;

        [DBFieldName("TargetPitch", TargetedDatabaseFlag.SinceMidnight, nullable: true)]
        public float? TargetPitch;

        [DBFieldName("TargetYaw", TargetedDatabaseFlag.SinceMidnight, nullable: true)]
        public float? TargetYaw;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        // Will be inserted as comment
        public uint spellId = 0;

        public string CustomId;

        public Vector3? RollPitchYaw
        {
            get => Roll != null && Pitch != null && Yaw != null ? new Vector3(Roll.Value, Pitch.Value, Yaw.Value) : (Vector3?)null;
            set => (Roll, Pitch, Yaw) = (value?.X, value?.Y, value?.Z);
        }

        public Vector3? TargetRollPitchYaw
        {
            get => TargetRoll != null && TargetPitch != null && TargetYaw != null ? new Vector3(TargetRoll.Value, TargetPitch.Value, TargetYaw.Value) : (Vector3?)null;
            set => (TargetRoll, TargetPitch, TargetYaw) = (value?.X, value?.Y, value?.Z);
        }

        public bool AbsoluteOrientation
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.AbsoluteOrientation) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.HasAbsoluteOrientation,
                AreaTriggerCreatePropertiesFlags.AbsoluteOrientation);
        }

        public bool DynamicShape
        {
            get => FlagsLegacy?.HasAnyFlag(AreaTriggerCreatePropertiesLegacyFlags.HasDynamicShape) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.HasDynamicShape, null);
        }

        public bool Attached
        {
            get => FlagsLegacy?.HasAnyFlag(AreaTriggerCreatePropertiesLegacyFlags.HasAttached) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.HasAttached, null);
        }

        public bool FaceMovementDir
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.FaceMovementDir) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.FaceMovementDirection,
                AreaTriggerCreatePropertiesFlags.FaceMovementDir);
        }

        public bool FollowsTerrain
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.FollowsTerrain) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.FollowsTerrain,
                AreaTriggerCreatePropertiesFlags.FollowsTerrain);
        }

        public bool AlwaysExterior
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.AlwaysExterior) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.AlwaysExterior,
                AreaTriggerCreatePropertiesFlags.AlwaysExterior);
        }

        public bool UsesUnitRawFacing
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.UsesUnitRawFacing) ?? false;
            set => ModifyFlags(value, null, AreaTriggerCreatePropertiesFlags.UsesUnitRawFacing);
        }

        public bool VisualAnimIsDecay
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.VisualAnimIsDecay) ?? false;
            set => ModifyFlags(value, AreaTriggerCreatePropertiesLegacyFlags.VisualAnimIsDecay,
                AreaTriggerCreatePropertiesFlags.VisualAnimIsDecay);
        }

        public bool HeightIgnoresScale
        {
            get => Flags?.HasAnyFlag(AreaTriggerCreatePropertiesFlags.HeightIgnoresScale) ?? false;
            set => ModifyFlags(value, null, AreaTriggerCreatePropertiesFlags.HeightIgnoresScale);
        }

        public IAreaTriggerData AreaTriggerData;

        public AreaTriggerCreateProperties() : base()
        {
            AreaTriggerData = new AreaTriggerData(this);
        }

        public override void LoadValuesFromUpdateFields()
        {
            spellId             = (uint)(AreaTriggerData.SpellID ?? 0);
            SpellForVisuals     = (uint?)AreaTriggerData.SpellForVisuals;
            DecalPropertiesId   = AreaTriggerData.DecalPropertiesID;
            TimeToTarget        = AreaTriggerData.TimeToTarget;
            TimeToTargetScale   = AreaTriggerData.TimeToTargetScale;
            AreaTriggerCreatePropertiesId = GetAreaTriggerCreatePropertiesIdFromSpellId(spellId);
            IsCustom = 0;
            if (AreaTriggerCreatePropertiesId == null)
                IsCustom = 1;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772))
            {
                if (AreaTriggerData.VisualAnim != null)
                {
                    if (AreaTriggerData.VisualAnim.AnimationDataID != 0 && AreaTriggerData.VisualAnim.AnimationDataID != uint.MaxValue)
                        AnimId = (int?)AreaTriggerData.VisualAnim.AnimationDataID;

                    if (AreaTriggerData.VisualAnim.AnimKitID != 0)
                        AnimKitId = (int?)AreaTriggerData.VisualAnim.AnimKitID;

                    VisualAnimIsDecay = AreaTriggerData.VisualAnim.IsDecay == true;
                }
            }

            if (AreaTriggerData.PositionalSoundKitID != null)
                PositionalSoundKitId = (int)AreaTriggerData.PositionalSoundKitID;

            if (AreaTriggerData.MoveCurveId != null)
                MoveCurveId = (int)AreaTriggerData.MoveCurveId;

            if (AreaTriggerData.ScaleCurveId != null)
                ScaleCurveId = (int)AreaTriggerData.ScaleCurveId;

            if (AreaTriggerData.MorphCurveId != null)
                MorphCurveId = (int)AreaTriggerData.MorphCurveId;

            if (AreaTriggerData.FacingCurveId != null)
                FacingCurveId = (int)AreaTriggerData.FacingCurveId;

            if (AreaTriggerData.Sphere != null)
            {
                Shape = (byte)AreaTriggerType.Sphere;
                ShapeData = [AreaTriggerData.Sphere.Radius, AreaTriggerData.Sphere.RadiusTarget, 0, 0, 0, 0, 0, 0];
            }
            else if (AreaTriggerData.Box != null)
            {
                Shape = (byte)AreaTriggerType.Box;
                ShapeData = [AreaTriggerData.Box.Extents?.X, AreaTriggerData.Box.Extents?.Y, AreaTriggerData.Box.Extents?.Z,
                    AreaTriggerData.Box.ExtentsTarget?.X, AreaTriggerData.Box.ExtentsTarget?.Y, AreaTriggerData.Box.ExtentsTarget?.Z,
                    0, 0];
            }
            else if (AreaTriggerData.Polygon != null)
            {
                Shape = (byte)AreaTriggerType.Polygon;
                ShapeData = [AreaTriggerData.Polygon.Height, AreaTriggerData.Polygon.HeightTarget, 0, 0, 0, 0, 0, 0];
            }
            else if (AreaTriggerData.Cylinder != null)
            {
                Shape = (byte)AreaTriggerType.Cylinder;
                ShapeData = [AreaTriggerData.Cylinder.Radius, AreaTriggerData.Cylinder.RadiusTarget,
                    AreaTriggerData.Cylinder.Height, AreaTriggerData.Cylinder.HeightTarget,
                    AreaTriggerData.Cylinder.LocationZOffset, AreaTriggerData.Cylinder.LocationZOffsetTarget,
                    0, 0];
            }
            else if (AreaTriggerData.Disk != null)
            {
                Shape = (byte)AreaTriggerType.Disk;
                ShapeData = [AreaTriggerData.Disk.InnerRadius, AreaTriggerData.Disk.InnerRadiusTarget,
                    AreaTriggerData.Disk.OuterRadius, AreaTriggerData.Disk.OuterRadiusTarget,
                    AreaTriggerData.Disk.Height, AreaTriggerData.Disk.HeightTarget,
                    AreaTriggerData.Disk.LocationZOffset, AreaTriggerData.Disk.LocationZOffsetTarget];
            }
            else if (AreaTriggerData.BoundedPlane != null)
            {
                Shape = (byte)AreaTriggerType.BoundedPlane;
                ShapeData = [AreaTriggerData.BoundedPlane.ExtentsY, AreaTriggerData.BoundedPlane.ExtentsZ,
                    AreaTriggerData.BoundedPlane.ExtentsTargetY, AreaTriggerData.BoundedPlane.ExtentsTargetZ,
                    0, 0, 0, 0];
            }

            if (AreaTriggerData.RollPitchYaw != null)
            {
                Roll = AreaTriggerData.RollPitchYaw.Value.X;
                Pitch = AreaTriggerData.RollPitchYaw.Value.Y;
                Yaw = AreaTriggerData.RollPitchYaw.Value.Z;
                if (AreaTriggerData.TargetRollPitchYaw != null)
                {
                    TargetRoll = AreaTriggerData.TargetRollPitchYaw.Value.X;
                    TargetPitch = AreaTriggerData.TargetRollPitchYaw.Value.Y;
                    TargetYaw = AreaTriggerData.TargetRollPitchYaw.Value.Z;
                }
            }

            if (AreaTriggerData.HeightIgnoresScale != null)
                HeightIgnoresScale = AreaTriggerData.HeightIgnoresScale.Value;

            if (AreaTriggerData.Flags != null)
            {
                HeightIgnoresScale = (AreaTriggerData.Flags & 0x0001) != 0;
                AbsoluteOrientation = (AreaTriggerData.Flags & 0x0008) != 0;
                DynamicShape = (AreaTriggerData.Flags & 0x0010) != 0;
                Attached = (AreaTriggerData.Flags & 0x0020) != 0;
                FaceMovementDir = (AreaTriggerData.Flags & 0x0040) != 0;
                FollowsTerrain = (AreaTriggerData.Flags & 0x0080) != 0;
                UsesUnitRawFacing = (AreaTriggerData.Flags & 0x0100) != 0;
                AlwaysExterior = (AreaTriggerData.Flags & 0x0200) != 0;
            }
        }

        public static uint? GetAreaTriggerCreatePropertiesIdFromSpellId(uint spellId)
        {
            if (!Settings.UseDBC)
                return null;

            uint? areaTriggerCreatePropertiesId = null;

            for (uint idx = 0; idx < 32; idx++)
            {
                var tuple = Tuple.Create(spellId, idx);
                if (DBC.DBC.SpellEffectStores.TryGetValue(tuple, out var effect))
                {
                    if (effect.Effect == (uint)SpellEffects.SPELL_EFFECT_CREATE_AREATRIGGER ||
                        effect.Effect == (uint)SpellEffects.SPELL_EFFECT_183 ||
                        effect.EffectAura == (uint)AuraTypeLegion.SPELL_AURA_AREA_TRIGGER)
                    {
                        // If we already had a SPELL_EFFECT_CREATE_AREATRIGGER, spell has multiple areatrigger,
                        // so we can't deduce SpellMiscId, return null
                        if (areaTriggerCreatePropertiesId != null)
                            return null;

                        areaTriggerCreatePropertiesId = (uint)effect.EffectMiscValue[0];
                    }
                }
            }

            return areaTriggerCreatePropertiesId;
        }

        private void ModifyFlags(bool on, AreaTriggerCreatePropertiesLegacyFlags? flagsLegacy, AreaTriggerCreatePropertiesFlags? flags)
        {
            if (on)
            {
                if (flagsLegacy != null)
                    FlagsLegacy = (FlagsLegacy ?? 0) | (uint)flagsLegacy;
                if (flags != null)
                    Flags = (Flags ?? 0) | (uint)flags;
            }
            else
            {
                if (flagsLegacy != null)
                    FlagsLegacy &= ~(uint)flagsLegacy;
                if (flags != null)
                    Flags &= ~(uint)flags;
            }
        }
    }

    [DBTableName("spell_areatrigger", TargetedDatabaseFlag.TillBattleForAzeroth)]
    [DBTableName("areatrigger_create_properties", TargetedDatabaseFlag.SinceShadowlands)]
    public sealed record AreaTriggerCreatePropertiesCustom : IDataModel
    {
        [DBFieldName("SpellMiscId", TargetedDatabaseFlag.TillBattleForAzeroth, true, true)]
        [DBFieldName("Id", TargetedDatabaseFlag.SinceShadowlands, true, true)]
        public string AreaTriggerCreatePropertiesId;

        [DBFieldName("IsCustom", TargetedDatabaseFlag.SinceDragonflight, true)]
        public byte? IsCustom = 1;

        [DBFieldName("AreaTriggerId")]
        public uint? AreaTriggerId;

        [DBFieldName("IsAreatriggerCustom", TargetedDatabaseFlag.SinceDragonflight)]
        public byte? IsAreatriggerCustom;

        [DBFieldName("Flags", TargetedDatabaseFlag.Dragonflight | TargetedDatabaseFlag.TheWarWithin | TargetedDatabaseFlag.CataClassic)]
        public uint? FlagsLegacy;

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceMidnight)]
        public uint? Flags;

        [DBFieldName("MoveCurveId")]
        public int? MoveCurveId = 0;

        [DBFieldName("ScaleCurveId")]
        public int? ScaleCurveId = 0;

        [DBFieldName("MorphCurveId")]
        public int? MorphCurveId = 0;

        [DBFieldName("FacingCurveId")]
        public int? FacingCurveId = 0;

        [DBFieldName("AnimId")]
        public int? AnimId = ClientVersion.AddedInVersion(ClientVersionBuild.V9_1_5_40772) ? -1 : 0;

        [DBFieldName("AnimKitId")]
        public int? AnimKitId = 0;

        [DBFieldName("DecalPropertiesId")]
        public uint? DecalPropertiesId = 0;

        [DBFieldName("SpellForVisuals", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic, false, false, true)]
        public uint? SpellForVisuals;

        [DBFieldName("PositionalSoundKitId", TargetedDatabaseFlag.SinceMidnight)]
        public int? PositionalSoundKitId;

        [DBFieldName("TimeToTarget", TargetedDatabaseFlag.TillDragonflight)]
        public uint? TimeToTarget = 0;

        [DBFieldName("TimeToTargetScale")]
        public uint? TimeToTargetScale = 0;

        [DBFieldName("Speed", TargetedDatabaseFlag.SinceTheWarWithin)]
        public float? Speed;

        [DBFieldName("Shape", TargetedDatabaseFlag.SinceShadowlands)]
        public byte? Shape;

        [DBFieldName("ShapeData", TargetedDatabaseFlag.SinceShadowlands, 8, true)]
        public float?[] ShapeData = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
