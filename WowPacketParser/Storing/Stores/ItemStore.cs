using WowPacketParser.Enums;

namespace WowPacketParser.Storing.Stores
{
    public sealed class ItemStore
    {
        public string GetCommand(int entry, ItemClass iClass, int subClass, int unk0, string[] name,
            int dispId, ItemQuality quality, ItemFlag flags, ItemFlagExtra flags2, int buyPrice,
            int sellPrice, InventoryType invType, ClassMask allowClass, RaceMask allowRace, int itemLvl,
            int reqLvl, int reqSkill, int reqSkLvl, int reqSpell, int reqHonor, int reqCity,
            int reqRepFaction, int reqRepValue, int maxCount, int stacks, int contSlots, int statsCount,
            ItemModType[] statType, int[] statValue, int ssdId, int ssdVal, float[] dmgMin,
            float[] dmgMax, DamageType[] dmgType, int[] resistance, int delay, AmmoType ammoType,
            float rangedMod, int[] spellId, ItemSpellTriggerType[] spellTrigger, int[] spellCharges,
            int[] spellCooldown, int[] spellCategory, int[] spellCatCooldown, ItemBonding binding,
            string description, int pageText, Language langId, PageMaterial pageMat, int startQuest,
            int lockId, Material material, SheathType sheath, int randomProp, int randomSuffix,
            int block, int itemSet, int maxDura, int area, int map, BagFamilyMask bagFamily,
            TotemCategory totemCat, ItemSocketColor[] color, int[] content, int socketBonus,
            int gemProps, int reqDisEnchSkill, float armorDmgMod, int duration, int limitCategory,
            Holiday holidayId)
        {
            var builder = new CommandBuilder("item_template");

            builder.AddColumnValue("entry", entry);
            builder.AddColumnValue("class", (int)iClass);
            builder.AddColumnValue("subclass", subClass);
            builder.AddColumnValue("unk0", unk0);
            builder.AddColumnValue("name", name[0]);
            builder.AddColumnValue("displayid", dispId);
            builder.AddColumnValue("Quality", (int)quality);
            builder.AddColumnValue("Flags", (int)flags);
            builder.AddColumnValue("BuyCount", 1);
            builder.AddColumnValue("BuyPrice", buyPrice);
            builder.AddColumnValue("SellPrice", sellPrice);
            builder.AddColumnValue("InventoryType", (int)invType);
            builder.AddColumnValue("AllowableClass", (int)allowClass);
            builder.AddColumnValue("AllowableRace", (int)allowRace);
            builder.AddColumnValue("ItemLevel", itemLvl);
            builder.AddColumnValue("RequiredLevel", reqLvl);
            builder.AddColumnValue("RequiredSkill", reqSkill);
            builder.AddColumnValue("RequiredSkillRank", reqSkLvl);
            builder.AddColumnValue("requiredspell", reqSpell);
            builder.AddColumnValue("requiredhonorrank", reqHonor);
            builder.AddColumnValue("RequiredCityRank", reqCity);
            builder.AddColumnValue("RequiredReputationFaction", reqRepFaction);
            builder.AddColumnValue("RequiredReputationRank", reqRepValue);
            builder.AddColumnValue("maxcount", maxCount);

            var stackVal = stacks;
            if (Store.Format == SqlFormat.Mangos && stacks > 1000)
                stackVal = -1;

            builder.AddColumnValue("stackable", stackVal);
            builder.AddColumnValue("ContainerSlots", contSlots);
            builder.AddColumnValue("StatsCount", statsCount);

            for (var i = 0; i < 10; i++)
            {
                int type;
                int value;

                if (i > statsCount - 1)
                {
                    type = 0;
                    value = 0;
                }
                else
                {
                    type = (int)statType[i];
                    value = statValue[i];
                }

                builder.AddColumnValue("stat_type" + (i + 1), type);
                builder.AddColumnValue("stat_value" + (i + 1), value);
            }

            builder.AddColumnValue("ScalingStatDistribution", ssdId);
            builder.AddColumnValue("ScalingStatValue", ssdVal);

            for (var i = 0; i < 2; i++)
            {
                builder.AddColumnValue("dmg_min" + (i + 1), dmgMin[i]);
                builder.AddColumnValue("dmg_max" + (i + 1), dmgMax[i]);
                builder.AddColumnValue("dmg_type" + (i + 1), (int)dmgType[i]);
            }

            builder.AddColumnValue("armor", resistance[0]);
            builder.AddColumnValue("holy_res", resistance[1]);
            builder.AddColumnValue("fire_res", resistance[2]);
            builder.AddColumnValue("nature_res", resistance[3]);
            builder.AddColumnValue("frost_res", resistance[4]);
            builder.AddColumnValue("shadow_res", resistance[5]);
            builder.AddColumnValue("arcane_res", resistance[6]);
            builder.AddColumnValue("delay", delay);
            builder.AddColumnValue("ammo_type", (int)ammoType);
            builder.AddColumnValue("RangedModRange", rangedMod);

            for (var i = 0; i < 5; i++)
            {
                builder.AddColumnValue("spellid_" + (i + 1), spellId[i]);
                builder.AddColumnValue("spelltrigger_" + (i + 1), (int)spellTrigger[i]);
                builder.AddColumnValue("spellcharges_" + (i + 1), spellCharges[i]);
                builder.AddColumnValue("spellppmRate_" + (i + 1), 0);
                builder.AddColumnValue("spellcooldown_" + (i + 1), spellCooldown[i]);
                builder.AddColumnValue("spellcategory_" + (i + 1), spellCategory[i]);
                builder.AddColumnValue("spellcategorycooldown_" + (i + 1), spellCatCooldown[i]);
            }

            builder.AddColumnValue("bonding", (int)binding);
            builder.AddColumnValue("description", description);
            builder.AddColumnValue("PageText", pageText);
            builder.AddColumnValue("LanguageID", (int)langId);
            builder.AddColumnValue("PageMaterial", (int)pageMat);
            builder.AddColumnValue("startquest", startQuest);
            builder.AddColumnValue("lockid", lockId);
            builder.AddColumnValue("Material", (int)material);
            builder.AddColumnValue("sheath", (int)sheath);
            builder.AddColumnValue("RandomProperty", randomProp);
            builder.AddColumnValue("RandomSuffix", randomSuffix);
            builder.AddColumnValue("block", block);
            builder.AddColumnValue("itemset", itemSet);
            builder.AddColumnValue("MaxDurability", maxDura);
            builder.AddColumnValue("area", area);
            builder.AddColumnValue("map", map);
            builder.AddColumnValue("BagFamily", (int)bagFamily);
            builder.AddColumnValue("TotemCategory", (int)totemCat);

            for (var i = 0; i < 3; i++)
            {
                builder.AddColumnValue("socketColor_" + (i + 1), (int)color[i]);
                builder.AddColumnValue("socketContent_" + (i + 1), content[i]);
            }

            builder.AddColumnValue("socketBonus", socketBonus);
            builder.AddColumnValue("GemProperties", gemProps);
            builder.AddColumnValue("RequiredDisenchantSkill", reqDisEnchSkill);
            builder.AddColumnValue("ArmorDamageModifier", armorDmgMod);
            builder.AddColumnValue("Duration", duration);
            builder.AddColumnValue("ItemLimitCategory", limitCategory);
            builder.AddColumnValue("HolidayID", (int)holidayId);
            builder.AddColumnValue("ScriptName", string.Empty);
            builder.AddColumnValue("DisenchantID", 0);
            builder.AddColumnValue("FoodType", 0);
            builder.AddColumnValue("minMoneyLoot", 0);
            builder.AddColumnValue("maxMoneyLoot", 0);
            builder.AddColumnValue("NonConsumable", 0);

            return builder.BuildInsert(true);
        }
    }
}
