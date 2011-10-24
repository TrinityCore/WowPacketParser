using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL;


namespace WowPacketParser.Parsing.Parsers
{
    public static class ItemHandler
    {
        [Parser(Opcode.CMSG_ITEM_QUERY_SINGLE)]
        public static void HandleItemQuerySingle(Packet packet)
        {
            var entry = packet.ReadInt32();
            Console.WriteLine("Entry: " + entry);
        }

        [Parser(Opcode.SMSG_ITEM_QUERY_SINGLE_RESPONSE)]
        public static void HandleItemQueryResponse(Packet packet)
        {
            var entry = packet.ReadEntry();
            Console.WriteLine("Entry: " + entry.Key);

            if (entry.Value)
                return;

            var iClass = (ItemClass)packet.ReadInt32();
            Console.WriteLine("Class: " + iClass);

            var subClass = packet.ReadInt32();
            Console.WriteLine("Sub Class: " + subClass);

            var unk0 = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk0);

            var name = new string[4];
            for (var i = 0; i < 4; i++)
            {
                name[i] = packet.ReadCString();
                Console.WriteLine("Name " + i + ": " + name[i]);
            }

            var dispId = packet.ReadInt32();
            Console.WriteLine("Display ID: " + dispId);

            var quality = (ItemQuality)packet.ReadInt32();
            Console.WriteLine("Quality: " + quality);

            var flags = (ItemFlag)packet.ReadInt32();
            Console.WriteLine("Flags: " + flags);

            var flags2 = ItemFlagExtra.None;
            if (ClientVersion.Version >= ClientVersionBuild.V3_2_0_10192)
                flags2 = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);

            var buyPrice = packet.ReadInt32();
            Console.WriteLine("Buy Price: " + buyPrice);

            var sellPrice = packet.ReadInt32();
            Console.WriteLine("Sell Price: " + sellPrice);

            var invType = (InventoryType)packet.ReadInt32();
            Console.WriteLine("Inventory Type: " + invType);

            var allowClass = (ClassMask)packet.ReadInt32();
            Console.WriteLine("Allowed Classes: " + allowClass);

            var allowRace = (RaceMask)packet.ReadInt32();
            Console.WriteLine("Allowed Races: " + allowRace);

            var itemLvl = packet.ReadInt32();
            Console.WriteLine("Item Level: " + itemLvl);

            var reqLvl = packet.ReadInt32();
            Console.WriteLine("Required Level: " + reqLvl);

            var reqSkill = packet.ReadInt32();
            Console.WriteLine("Required Skill ID: " + reqSkill);

            var reqSkLvl = packet.ReadInt32();
            Console.WriteLine("Required Skill Level: " + reqSkLvl);

            var reqSpell = packet.ReadInt32();
            Console.WriteLine("Required Spell: " + Extensions.SpellLine(reqSpell));

            var reqHonor = packet.ReadInt32();
            Console.WriteLine("Required Honor Rank: " + reqHonor);

            var reqCity = packet.ReadInt32();
            Console.WriteLine("Required City Rank: " + reqCity);

            var reqRepFaction = packet.ReadInt32();
            Console.WriteLine("Required Rep Faction: " + reqRepFaction);

            var reqRepValue = packet.ReadInt32();
            Console.WriteLine("Required Rep Value: " + reqRepValue);

            var maxCount = packet.ReadInt32();
            Console.WriteLine("Max Count: " + maxCount);

            var stacks = packet.ReadInt32();
            Console.WriteLine("Max Stack Size: " + stacks);

            var contSlots = packet.ReadInt32();
            Console.WriteLine("Container Slots: " + contSlots);

            int statsCount;
            if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
                statsCount = packet.ReadInt32("Stats Count");
            else
                statsCount = 10;

            var statType = new ItemModType[statsCount];
            var statVal = new int[statsCount];
            for (var i = 0; i < statsCount; i++)
            {
                statType[i] = packet.ReadEnum<ItemModType>("Stat Type", TypeCode.Int32, i);
                statVal[i] = packet.ReadInt32("Stat Value", i);
            }

            var ssdId = 0;
            var ssdVal = 0;
            if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
            {
                ssdId = packet.ReadInt32("SSD ID");
                ssdVal = packet.ReadInt32("SSD Value");
            }

            int dmgCount;
            if (ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767)
                dmgCount = 2;
            else
                dmgCount = 5;

            var dmgMin = new float[dmgCount];
            var dmgMax = new float[dmgCount];
            var dmgType = new DamageType[dmgCount];
            for (var i = 0; i < dmgCount; i++)
            {
                dmgMin[i] = packet.ReadSingle("Damage Min", i);
                dmgMax[i] = packet.ReadSingle("Damage Max", i);
                dmgType[i] = packet.ReadEnum<DamageType>("Damage Type", TypeCode.Int32, i);
            }

            var resistance = new int[7];
            for (var i = 0; i < 7; i++)
            {
                resistance[i] = packet.ReadInt32();
                Console.WriteLine((DamageType)i + " Resistance: " + resistance[i]);
            }

            var delay = packet.ReadInt32();
            Console.WriteLine("Delay: " + delay);

            var ammoType = (AmmoType)packet.ReadInt32();
            Console.WriteLine("Ammo Type: " + ammoType);

            var rangedMod = packet.ReadSingle();
            Console.WriteLine("Ranged Mod: " + rangedMod);

            var spellId = new int[5];
            var spellTrigger = new ItemSpellTriggerType[5];
            var spellCharges = new int[5];
            var spellCooldown = new int[5];
            var spellCategory = new int[5];
            var spellCatCooldown = new int[5];
            for (var i = 0; i < 5; i++)
            {
                spellId[i] = packet.ReadInt32();
                Console.WriteLine("Triggered Spell ID " + i + ": " + Extensions.SpellLine(spellId[i]));

                spellTrigger[i] = (ItemSpellTriggerType)packet.ReadInt32();
                Console.WriteLine("Triggered Spell Type " + i + ": " + spellTrigger[i]);

                spellCharges[i] = packet.ReadInt32();
                Console.WriteLine("Triggered Spell Charges " + i + ": " + spellCharges[i]);

                spellCooldown[i] = packet.ReadInt32();
                Console.WriteLine("Triggered Spell Cooldown " + i + ": " + spellCooldown[i]);

                spellCategory[i] = packet.ReadInt32();
                Console.WriteLine("Triggered Spell Category " + i + ": " + spellCategory[i]);

                spellCatCooldown[i] = packet.ReadInt32();
                Console.WriteLine("Triggered Spell Category Cooldown " + i + ": " + spellCatCooldown[i]);
            }

            var binding = (ItemBonding)packet.ReadInt32();
            Console.WriteLine("Bonding: " + binding);

            var description = packet.ReadCString();
            Console.WriteLine("Description: " + description);

            var pageText = packet.ReadInt32();
            Console.WriteLine("Page Text: " + pageText);

            var langId = (Language)packet.ReadInt32();
            Console.WriteLine("Language ID: " + langId);

            var pageMat = (PageMaterial)packet.ReadInt32();
            Console.WriteLine("Page Material: " + pageMat);

            var startQuest = packet.ReadInt32();
            Console.WriteLine("Start Quest: " + startQuest);

            var lockId = packet.ReadInt32();
            Console.WriteLine("Lock ID: " + lockId);

            var material = (Material)packet.ReadInt32();
            Console.WriteLine("Material: " + material);

            var sheath = (SheathType)packet.ReadInt32();
            Console.WriteLine("Sheath Type: " + sheath);

            var randomProp = packet.ReadInt32();
            Console.WriteLine("Random Property: " + randomProp);

            var randomSuffix = packet.ReadInt32();
            Console.WriteLine("Random Suffix: " + randomSuffix);

            var block = packet.ReadInt32();
            Console.WriteLine("Block: " + block);

            var itemSet = packet.ReadInt32();
            Console.WriteLine("Item Set: " + itemSet);

            var maxDura = packet.ReadInt32();
            Console.WriteLine("Max Durability: " + maxDura);

            var area = packet.ReadInt32();
            Console.WriteLine("Area: " + area);

            // In this single (?) case, map 0 means no map
            var map = packet.ReadInt32();
            Console.WriteLine("Map ID: " + (map != 0 ? Extensions.MapLine(map) : map + " (No map)"));

            var bagFamily = (BagFamilyMask)packet.ReadInt32();
            Console.WriteLine("Bag Family: " + bagFamily);

            var totemCat = (TotemCategory)packet.ReadInt32();
            Console.WriteLine("Totem Category: " + totemCat);

            var socketColor = new ItemSocketColor[3];
            var socketItem = new int[3];
            for (var i = 0; i < 3; i++)
            {
                socketColor[i] = packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);
                socketItem[i] = packet.ReadInt32("Socket Item", i);
            }

            var socketBonus = packet.ReadInt32();
            Console.WriteLine("Socket Bonus: " + socketBonus);

            var gemProps = packet.ReadInt32();
            Console.WriteLine("Gem Properties: " + gemProps);

            var reqDisEnchSkill = packet.ReadInt32();
            Console.WriteLine("Required Disenchant Skill: " + reqDisEnchSkill);

            var armorDmgMod = packet.ReadSingle();
            Console.WriteLine("Armor Damage Modifier: " + armorDmgMod);

            var duration = 0;
            if (ClientVersion.Version >= ClientVersionBuild.V2_4_2_8209)
                duration = packet.ReadInt32("Duration");

            var limitCategory = 0;
            if (ClientVersion.Version > ClientVersionBuild.V2_4_3_8606)
                limitCategory = packet.ReadInt32("Limit Category");

            var holidayId = Holiday.None;
            if (ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767)
                holidayId = packet.ReadEnum<Holiday>("Holiday", TypeCode.Int32);

            SQLStore.WriteData(SQLStore.Items.GetCommand(entry.Key, iClass, subClass, unk0, name, dispId,
                quality, flags, flags2, buyPrice, sellPrice, invType, allowClass, allowRace, itemLvl,
                reqLvl, reqSkill, reqSkLvl, reqSpell, reqHonor, reqCity, reqRepFaction, reqRepValue,
                maxCount, stacks, contSlots, statsCount, statType, statVal, ssdId, ssdVal, dmgMin, dmgMax,
                dmgType, resistance, delay, ammoType, rangedMod, spellId, spellTrigger, spellCharges,
                spellCooldown, spellCategory, spellCatCooldown, binding, description, pageText, langId,
                pageMat, startQuest, lockId, material, sheath, randomProp, randomSuffix, block, itemSet,
                maxDura, area, map, bagFamily, totemCat, socketColor, socketItem, socketBonus, gemProps,
                reqDisEnchSkill, armorDmgMod, duration, limitCategory, holidayId));
        }

        [Parser(Opcode.SMSG_UPDATE_ITEM_ENCHANTMENTS)]
        public static void HandleUpdateItemEnchantments(Packet packet)
        {
            for (var i = 0; i < 4; i++)
            {
                var enchId = packet.ReadInt32();
                Console.WriteLine("Aura ID " + i + ": " + enchId);
            }
        }

        [Parser(Opcode.SMSG_SET_PROFICIENCY)]
        public static void HandleSetProficency(Packet packet)
        {
            packet.ReadEnum<ItemClass>("Class", TypeCode.Byte);

            packet.ReadInt32("Mask");
        }

        [Parser(Opcode.SMSG_ITEM_COOLDOWN)]
        public static void HandleItemCooldown(Packet packet)
        {
            packet.ReadGuid("GUID");
            Console.WriteLine("Spell ID " + Extensions.SpellLine(packet.ReadInt32()));
        }
    }
}
