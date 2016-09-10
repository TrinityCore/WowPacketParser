using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class Spells
    {
        [BuilderMethod]
        public static string SpellTargetPosition()
        {
            if (Storage.SpellTargetPositions.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.spell_target_position))
                return string.Empty;

            var offerDb = SQLDatabase.Get(Storage.SpellTargetPositions);

            return SQLUtil.Compare(Storage.SpellTargetPositions, offerDb, StoreNameType.Spell);
        }
    }
}
