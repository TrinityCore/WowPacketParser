using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public sealed class Unit : WoWObject
    {
        public ICollection<Aura> Auras;

        public override bool IsTemporarySpawn()
        {
            // If our unit got any of the following update fields set,
            // it's probably a temporary spawn
            UpdateField uf;
            if (UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_SUMMONEDBY), out uf) ||
                UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_CREATED_BY_SPELL), out uf) ||
                UpdateFields.TryGetValue(Enums.Version.UpdateFields.GetUpdateField(UnitField.UNIT_FIELD_CREATEDBY), out uf))
                return uf.Int32Value != 0;

            return false;
        }
    }
}
