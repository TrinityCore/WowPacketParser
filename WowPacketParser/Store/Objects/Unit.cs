using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Store.Objects
{
    public sealed record Unit : WoWObject
    {
        public List<Aura> Auras;

        public BlockingCollection<List<Aura>> AddedAuras = new BlockingCollection<List<Aura>>();

        public ushort? AIAnimKit;
        public ushort? MovementAnimKit;
        public ushort? MeleeAnimKit;

        // Fields from UPDATE_FIELDS
        public UnitDynamicFlags? DynamicFlags;
        public UnitDynamicFlagsWOD? DynamicFlagsWod;

        public IUnitData UnitData;
        public bool ExistingDatabaseSpawn { get; set; }

        public Unit() : base()
        {
            UnitData = new UnitData(this);
        }

        public override bool IsTemporarySpawn()
        {
            if (ForceTemporarySpawn)
                return true;

            // If our unit got any of the following update fields set,
            // it's probably a temporary spawn
            return !UnitData.SummonedBy.IsEmpty() || !UnitData.CreatedBy.IsEmpty() || UnitData.CreatedBySpell != 0 || !UnitData.DemonCreator.IsEmpty();
        }

        public override void LoadValuesFromUpdateFields()
        {
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                DynamicFlagsWod = (UnitDynamicFlagsWOD)ObjectData.DynamicFlags;
            else
                DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);
        }

        public byte? VisibilityDistanceType
        {
            get
            {
                if (!UnitData.Flags2.HasValue)
                    return null;
                if (((UnitFlags2)UnitData.Flags2).HasFlag(UnitFlags2.InfiniteAOI))
                    return 5;
                if (((UnitFlags2)UnitData.Flags2).HasFlag(UnitFlags2.GiganticAOI))
                    return 4;
                if (((UnitFlags2)UnitData.Flags2).HasFlag(UnitFlags2.LargeAOI))
                    return 3;
                return 0;
            }
        }

        public CreatureEquipment GetEquipment()
        {
            var equipment = UnitData.VirtualItems;
            if (equipment.Length != 3)
                return null;

            if (equipment[0].ItemID == 0 && equipment[1].ItemID == 0 && equipment[2].ItemID == 0)
                return null;

            return new CreatureEquipment
            {
                CreatureID = Guid.GetEntry(),
                ItemID1 = (uint)equipment[0].ItemID,
                ItemID2 = (uint)equipment[1].ItemID,
                ItemID3 = (uint)equipment[2].ItemID,

                AppearanceModID1 = equipment[0].ItemAppearanceModID,
                AppearanceModID2 = equipment[1].ItemAppearanceModID,
                AppearanceModID3 = equipment[2].ItemAppearanceModID,

                ItemVisual1 = equipment[0].ItemVisual,
                ItemVisual2 = equipment[1].ItemVisual,
                ItemVisual3 = equipment[2].ItemVisual
            };
        }

        public override bool IsExistingSpawn()
        {
            return ExistingDatabaseSpawn;
        }
    }
}
