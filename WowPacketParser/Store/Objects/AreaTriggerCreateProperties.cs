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

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceDragonflight | TargetedDatabaseFlag.CataClassic)]
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

        [DBFieldName("TimeToTarget", TargetedDatabaseFlag.TillDragonflight)]
        public uint? TimeToTarget = 0;

        [DBFieldName("TimeToTargetScale")]
        public uint? TimeToTargetScale = 0;

        [DBFieldName("Speed", TargetedDatabaseFlag.SinceTheWarWithin)]
        public float? Speed;

        [DBFieldName("Shape", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic)]
        public byte? Shape;

        [DBFieldName("ShapeData", TargetedDatabaseFlag.SinceShadowlands | TargetedDatabaseFlag.CataClassic, 8, true)]
        public float?[] ShapeData = { 0, 0, 0, 0, 0, 0, 0, 0 };

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;

        // Will be inserted as comment
        public uint spellId = 0;

        public string CustomId;

        public IAreaTriggerData AreaTriggerData;

        public AreaTriggerCreateProperties() : base()
        {
            AreaTriggerData = new AreaTriggerData(this);
        }

        public override void LoadValuesFromUpdateFields()
        {
            spellId             = (uint)AreaTriggerData.SpellID;
            SpellForVisuals     = (uint)AreaTriggerData.SpellForVisuals;
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
                        AnimId = (int)AreaTriggerData.VisualAnim.AnimationDataID;

                    if (AreaTriggerData.VisualAnim.AnimKitID != 0)
                        AnimKitId = (int)AreaTriggerData.VisualAnim.AnimKitID;

                    if (AreaTriggerData.VisualAnim.IsDecay == true)
                        Flags |= (uint)AreaTriggerCreatePropertiesFlags.VisualAnimIsDecay;
                }
            }

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
                ShapeData = [AreaTriggerData.Box.Extents.X, AreaTriggerData.Box.Extents.Y, AreaTriggerData.Box.Extents.Z,
                    AreaTriggerData.Box.ExtentsTarget.X, AreaTriggerData.Box.ExtentsTarget.Y, AreaTriggerData.Box.ExtentsTarget.Z,
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
                ShapeData = [AreaTriggerData.BoundedPlane.Extents.X, AreaTriggerData.BoundedPlane.Extents.Y,
                    AreaTriggerData.BoundedPlane.ExtentsTarget.X, AreaTriggerData.BoundedPlane.ExtentsTarget.Y,
                    0, 0, 0, 0];
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

        [DBFieldName("Flags", TargetedDatabaseFlag.SinceDragonflight)]
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
