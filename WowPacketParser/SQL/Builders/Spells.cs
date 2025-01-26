using System.Linq;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

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

            return SQLUtil.Compare(Storage.SpellTargetPositions, SQLDatabase.Get(Storage.SpellTargetPositions), t => t.EffectHelper);
        }

        [BuilderMethod]
        public static string SpellTargetMultiplePositions()
        {
            if (Storage.SpellTargetMultiplePositions.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.spell_target_position))
                return string.Empty;

            var rows = new RowList<SpellTargetPosition>();
            Dictionary<uint, uint> idCount = new Dictionary<uint, uint>();

            foreach (var spellTargetPosition in Storage.SpellTargetMultiplePositions.OrderBy(t => t.Key))
            {
                foreach (var spellTargetPositionValue in spellTargetPosition.Value)
                {
                    idCount.TryGetValue(spellTargetPosition.Key, out uint count);
                    var duplicatedRow = rows.Where(spellTargetPosition2 =>
                        spellTargetPosition2.Data.ID == spellTargetPosition.Key &&
                        spellTargetPosition2.Data.PositionX == spellTargetPositionValue.Item1.PositionX &&
                        spellTargetPosition2.Data.PositionY == spellTargetPositionValue.Item1.PositionY &&
                        spellTargetPosition2.Data.PositionZ == spellTargetPositionValue.Item1.PositionZ &&
                        spellTargetPosition2.Data.MapID == spellTargetPositionValue.Item1.MapID
                    ).Any();

                    if (duplicatedRow)
                        continue;

                    var row = new Row<SpellTargetPosition>
                    {
                        Data = new SpellTargetPosition
                        {
                            ID = spellTargetPosition.Key,
                            EffectIndex = spellTargetPositionValue.Item1.EffectIndex,
                            OrderIndex = "ORD_INDEX+" + count,
                            PositionX = spellTargetPositionValue.Item1.PositionX,
                            PositionY = spellTargetPositionValue.Item1.PositionY,
                            PositionZ = spellTargetPositionValue.Item1.PositionZ,
                            MapID = spellTargetPositionValue.Item1.MapID
                        },

                        Comment = spellTargetPositionValue.Item1.EffectHelper
                    };

                    idCount[spellTargetPosition.Key] = count + 1;
                    rows.Add(row);
                }
            }
            return new SQLInsert<SpellTargetPosition>(rows, false).Build();
        }
    }
}
