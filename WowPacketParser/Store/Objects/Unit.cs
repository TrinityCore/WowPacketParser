using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects.UpdateFields;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Store.Objects
{
    public sealed class Unit : WoWObject
    {
        public List<Aura> Auras;

        public BlockingCollection<List<Aura>> AddedAuras = new BlockingCollection<List<Aura>>();

        public uint GossipId;

        public ushort? AIAnimKit;
        public ushort? MovementAnimKit;
        public ushort? MeleeAnimKit;

        // Fields from UPDATE_FIELDS
        public uint Bytes1;
        public UnitDynamicFlags? DynamicFlags;
        public UnitDynamicFlagsWOD? DynamicFlagsWod;
        public uint Bytes2;

        public IUnitData UnitData;

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
            return !UnitData.SummonedBy.IsEmpty() || !UnitData.CreatedBy.IsEmpty() || UnitData.CreatedBySpell != 0;
        }

        public override void LoadValuesFromUpdateFields()
        {
            Bytes1 = BitConverter.ToUInt32(new byte[] { UnitData.StandState, UnitData.PetTalentPoints, UnitData.VisFlags, UnitData.AnimTier }, 0);
            Bytes2 = BitConverter.ToUInt32(new byte[] { UnitData.SheatheState, UnitData.PvpFlags, UnitData.PetFlags, UnitData.ShapeshiftForm }, 0);
            if (ClientVersion.AddedInVersion(ClientType.WarlordsOfDraenor))
                DynamicFlagsWod = (UnitDynamicFlagsWOD)ObjectData.DynamicFlags;
            else
                DynamicFlags  = UpdateFields.GetEnum<UnitField, UnitDynamicFlags?>(UnitField.UNIT_DYNAMIC_FLAGS);
        }
    }
}
