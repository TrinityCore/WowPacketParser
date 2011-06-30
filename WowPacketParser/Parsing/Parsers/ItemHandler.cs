using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL.Store;

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

            var flags2 = (ItemFlagExtra)packet.ReadInt32();
            Console.WriteLine("Extra Flags: " + flags2);

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
            Console.WriteLine("Required Spell: " + reqSpell);

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

            var statsCount = packet.ReadInt32();
            Console.WriteLine("Stats Count: " + statsCount);

            var type = new ItemModType[statsCount];
            var value = new int[statsCount];
            for (var i = 0; i < statsCount; i++)
            {
                type[i] = (ItemModType)packet.ReadInt32();
                Console.WriteLine("Stat Type " + i + ": " + type[i]);

                value[i] = packet.ReadInt32();
                Console.WriteLine("Stat Value " + i + ": " + value[i]);
            }

            var ssdId = packet.ReadInt32();
            Console.WriteLine("SSD ID: " + ssdId);

            var ssdVal = packet.ReadInt32();
            Console.WriteLine("SSD Value: " + ssdVal);

            var dmgMin = new float[2];
            var dmgMax = new float[2];
            var dmgType = new DamageType[2];
            for (var i = 0; i < 2; i++)
            {
                dmgMin[i] = packet.ReadSingle();
                Console.WriteLine("Damage Min " + i + ": " + dmgMin[i]);

                dmgMax[i] = packet.ReadSingle();
                Console.WriteLine("Damage Max " + i + ": " + dmgMax[i]);

                dmgType[i] = (DamageType)packet.ReadInt32();
                Console.WriteLine("Damage Type " + i + ": " + dmgType[i]);
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
                Console.WriteLine("Triggered Spell ID " + i + ": " + spellId[i]);

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

            var map = packet.ReadInt32();
            Console.WriteLine("Map: " + map);

            var bagFamily = (BagFamilyMask)packet.ReadInt32();
            Console.WriteLine("Bag Family: " + bagFamily);

            var totemCat = (TotemCategory)packet.ReadInt32();
            Console.WriteLine("Totem Category: " + totemCat);

            var color = new ItemSocketColor[3];
            var content = new int[3];
            for (var i = 0; i < 3; i++)
            {
                color[i] = (ItemSocketColor)packet.ReadInt32();
                Console.WriteLine("Socket Color " + i + ": " + color[i]);

                content[i] = packet.ReadInt32();
                Console.WriteLine("Socket Item " + i + ": " + content[i]);
            }

            var socketBonus = packet.ReadInt32();
            Console.WriteLine("Socket Bonus: " + socketBonus);

            var gemProps = packet.ReadInt32();
            Console.WriteLine("Gem Properties: " + gemProps);

            var reqDisEnchSkill = packet.ReadInt32();
            Console.WriteLine("Required Disenchant Skill: " + reqDisEnchSkill);

            var armorDmgMod = packet.ReadSingle();
            Console.WriteLine("Armor Damage Modifier: " + armorDmgMod);

            var duration = packet.ReadInt32();
            Console.WriteLine("Duration: " + duration);

            var limitCategory = packet.ReadInt32();
            Console.WriteLine("Limit Category: " + limitCategory);

            var holidayId = (Holiday)packet.ReadInt32();
            Console.WriteLine("Holiday: " + holidayId);

            Store.WriteData(Store.Items.GetCommand(entry.Key, iClass, subClass, unk0, name, dispId,
                quality, flags, flags2, buyPrice, sellPrice, invType, allowClass, allowRace, itemLvl,
                reqLvl, reqSkill, reqSkLvl, reqSpell, reqHonor, reqCity, reqRepFaction, reqRepValue,
                maxCount, stacks, contSlots, statsCount, type, value, ssdId, ssdVal, dmgMin, dmgMax,
                dmgType, resistance, delay, ammoType, rangedMod, spellId, spellTrigger, spellCharges,
                spellCooldown, spellCategory, spellCatCooldown, binding, description, pageText, langId,
                pageMat, startQuest, lockId, material, sheath, randomProp, randomSuffix, block, itemSet,
                maxDura, area, map, bagFamily, totemCat, color, content, socketBonus, gemProps,
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
    }
}
