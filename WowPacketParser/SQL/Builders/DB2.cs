using System;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class DB2
    {
        [BuilderMethod]
        public static string BroadcastText()
        {
            if (Storage.BroadcastTexts.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.broadcast_text))
               return string.Empty;

            var entries = Storage.BroadcastTexts.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, BroadcastText>(entries, "Id", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.BroadcastTexts, templatesDb, StoreNameType.BroadcastText, "ID");
        }

        [BuilderMethod]
        public static string CurvePoint()
        {
            if (Storage.CurvePoints.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.curve_point))
                return string.Empty;

            var entries = Storage.CurvePoints.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, CurvePoint>(entries, "ID", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.CurvePoints, templatesDb, StoreNameType.None, "ID");
        }

        public static string Holidays() { throw new NotImplementedException(); }
        public static string ItemAppearance() { throw new NotImplementedException(); }
        public static string ItemBonus() { throw new NotImplementedException(); }
        public static string ItemBonusTreeNode() { throw new NotImplementedException(); }
        public static string ItemCurrencyCost() { throw new NotImplementedException(); }
        public static string ItemEffect() { throw new NotImplementedException(); }
        public static string ItemExtendedCost() { throw new NotImplementedException(); }
        public static string ItemModifiedAppearance() { throw new NotImplementedException(); }
        public static string ItemSparse() { throw new NotImplementedException(); }
        public static string Item() { throw new NotImplementedException(); }
        public static string ItemXBonusTree() { throw new NotImplementedException(); }
        public static string KeyChain() { throw new NotImplementedException(); }
        public static string Mount() { throw new NotImplementedException(); }
        public static string OverrideSpellData() { throw new NotImplementedException(); }
        public static string PhaseXPhaseGroup() { throw new NotImplementedException(); }
        public static string SpellAuraRestrictions() { throw new NotImplementedException(); }
        public static string SpellCastingRequirements() { throw new NotImplementedException(); }
        public static string SpellClassOptions() { throw new NotImplementedException(); }
        public static string SpellLearnSpell() { throw new NotImplementedException(); }

        [BuilderMethod]
        public static string SpellMisc()
        {
            if (Storage.SpellMiscs.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.spell_misc))
                return string.Empty;

            var entries = Storage.SpellMiscs.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, SpellMisc>(entries, "Id", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.SpellMiscs, templatesDb, StoreNameType.None);
        }

        public static string SpellPower() { throw new NotImplementedException(); }
        public static string SpellReagents() { throw new NotImplementedException(); }
        public static string SpellRuneCost() { throw new NotImplementedException(); }
        public static string SpellTotems() { throw new NotImplementedException(); }
        public static string TaxiNodes() { throw new NotImplementedException(); }
        public static string TaxiPathNode() { throw new NotImplementedException(); }
        public static string TaxiPath() { throw new NotImplementedException(); }

        [BuilderMethod]
        public static string CreatureDifficulty()
        {
            if (Storage.CreatureDifficultys.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.creature_template))
                return String.Empty;

            var rowsIns = new List<QueryBuilder.SQLInsertRow>();
            var rowsUpd = new List<QueryBuilder.SQLUpdateRow>();
            foreach (var creature in Storage.CreatureDifficultys)
            {
                if (SQLDatabase.CreatureDifficultyStores != null)
                {
                    if (SQLDatabase.CreatureDifficultyStores.ContainsKey(creature.Key))
                    {
                        foreach (var creatureDiff in SQLDatabase.CreatureDifficultyStores)
                        {
                            var row = new QueryBuilder.SQLUpdateRow();

                            if (!Utilities.EqualValues(creature.Key, creatureDiff.Key))
                                continue;

                            if (!Utilities.EqualValues(creatureDiff.Value.CreatureID, creature.Value.Item1.CreatureID))
                                row.AddValue("CreatureID", creature.Value.Item1.CreatureID);

                            if (!Utilities.EqualValues(creatureDiff.Value.FactionID, creature.Value.Item1.FactionID))
                                row.AddValue("FactionID", creature.Value.Item1.FactionID);

                            if (!Utilities.EqualValues(creatureDiff.Value.Expansion, creature.Value.Item1.Expansion))
                                row.AddValue("Expansion", creature.Value.Item1.Expansion);

                            if (!Utilities.EqualValues(creatureDiff.Value.MinLevel, creature.Value.Item1.MinLevel))
                                row.AddValue("MinLevel", creature.Value.Item1.MinLevel);

                            if (!Utilities.EqualValues(creatureDiff.Value.MaxLevel, creature.Value.Item1.MaxLevel))
                                row.AddValue("MaxLevel", creature.Value.Item1.MaxLevel);

                            for (int i = 0; i < 5; i++)
                                if (!Utilities.EqualValues(creatureDiff.Value.Flags[i], creature.Value.Item1.Flags[i]))
                                    row.AddValue("Flags" + (i + 1), creature.Value.Item1.Flags[i]);

                            if (!Utilities.EqualValues(creatureDiff.Value.VerifiedBuild, creature.Value.Item1.VerifiedBuild))
                                row.AddValue("VerifiedBuild", creature.Value.Item1.VerifiedBuild);

                            row.AddWhere("Id", creature.Key);

                            row.Table = "creature_difficulty";

                            if (row.ValueCount != 0)
                                rowsUpd.Add(row);
                        }
                    }
                    else // insert
                    {
                        var row = new QueryBuilder.SQLInsertRow();

                        row.AddValue("ID", creature.Key);
                        row.AddValue("CreatureID", creature.Value.Item1.CreatureID);

                        row.AddValue("FactionID", creature.Value.Item1.FactionID);
                        row.AddValue("Expansion", creature.Value.Item1.Expansion);

                        row.AddValue("MinLevel", creature.Value.Item1.MinLevel);
                        row.AddValue("MaxLevel", creature.Value.Item1.MaxLevel);

                        for (int i = 0; i < 5; i++)
                            row.AddValue("Flags" + (i + 1), creature.Value.Item1.Flags[i]);

                        row.AddValue("VerifiedBuild", creature.Value.Item1.VerifiedBuild);

                        rowsIns.Add(row);
                    }
                }
                else // insert
                {
                    var row = new QueryBuilder.SQLInsertRow();

                    row.AddValue("ID", creature.Key);
                    row.AddValue("CreatureID", creature.Value.Item1.CreatureID);

                    row.AddValue("FactionID", creature.Value.Item1.FactionID);
                    row.AddValue("Expansion", creature.Value.Item1.Expansion);

                    row.AddValue("MinLevel", creature.Value.Item1.MinLevel);
                    row.AddValue("MaxLevel", creature.Value.Item1.MaxLevel);

                    for (int i = 0; i < 5; i++)
                        row.AddValue("Flags" + (i + 1), creature.Value.Item1.Flags[i]);

                    row.AddValue("VerifiedBuild", creature.Value.Item1.VerifiedBuild);

                    rowsIns.Add(row);
                }
            }

            return new QueryBuilder.SQLInsert("creature_difficulty", rowsIns).Build() +
                new QueryBuilder.SQLUpdate(rowsUpd).Build();
        }

        [BuilderMethod]
        public static string GameObjectDB2()
        {
            if (Storage.GameObjectTemplateDB2s.IsEmpty())
                return String.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template))
                return string.Empty;

            var entries = Storage.GameObjectTemplateDB2s.Keys();
            var templatesDb = SQLDatabase.GetDict<uint, GameObjectTemplateDB2>(entries, "Id", Settings.HotfixesDatabase);

            return SQLUtil.CompareDicts(Storage.GameObjectTemplateDB2s, templatesDb, StoreNameType.GameObject);
        }
    }
}
