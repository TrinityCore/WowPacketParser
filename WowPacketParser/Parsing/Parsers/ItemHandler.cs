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
            var entry = packet.ReadEntry("Entry");
            if (entry.Value)
                return;

            var iClass = packet.ReadEnum<ItemClass>("Class", TypeCode.Int32);

            var subClass = packet.ReadInt32("Sub Class");

            var unk0 = packet.ReadInt32("Unk Int32");

            var name = new string[4];
            for (var i = 0; i < 4; i++)
                name[i] = packet.ReadCString("Name", i);

            var dispId = packet.ReadInt32("Display ID");

            var quality = packet.ReadEnum<ItemQuality>("Quality", TypeCode.Int32);

            var flags = packet.ReadEnum<ItemFlag>("Flags", TypeCode.Int32);

            var flags2 = ItemFlagExtra.None;
            if (ClientVersion.Version >= ClientVersionBuild.V3_2_0_10192)
                flags2 = packet.ReadEnum<ItemFlagExtra>("Extra Flags", TypeCode.Int32);

            var buyPrice = packet.ReadInt32("Buy Price");

            var sellPrice = packet.ReadInt32("Sell Price");

            var invType = packet.ReadEnum<InventoryType>("Inventory Type", TypeCode.Int32);

            var allowClass = packet.ReadEnum<ClassMask>("Allowed Classes", TypeCode.Int32);

            var allowRace = packet.ReadEnum<RaceMask>("Allowed Races", TypeCode.Int32);

            var itemLvl = packet.ReadInt32("Item Level");

            var reqLvl = packet.ReadInt32("Required Level");

            var reqSkill = packet.ReadInt32("Required Skill ID");

            var reqSkLvl = packet.ReadInt32("Required Skill Level");

            var reqSpell = packet.ReadInt32();
            Console.WriteLine("Required Spell: " + Extensions.SpellLine(reqSpell));

            var reqHonor = packet.ReadInt32("Required Honor Rank");

            var reqCity = packet.ReadInt32("Required City Rank");

            var reqRepFaction = packet.ReadInt32("Required Rep Faction");

            var reqRepValue = packet.ReadInt32("Required Rep Value");

            var maxCount = packet.ReadInt32("Max Count");

            var stacks = packet.ReadInt32("Max Stack Size");

            var contSlots = packet.ReadInt32("Container Slots");

            int statsCount = ClientVersion.Version > ClientVersionBuild.V2_4_3_8606 ? packet.ReadInt32("Stats Count") : 10;

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

            int dmgCount = ClientVersion.Version >= ClientVersionBuild.V3_1_0_9767 ? 2 : 5;

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

            var delay = packet.ReadInt32("Delay");

            var ammoType = packet.ReadEnum<AmmoType>("Ammo Type", TypeCode.Int32);

            var rangedMod = packet.ReadSingle("Ranged Mod");

            var spellId = new int[5];
            var spellTrigger = new ItemSpellTriggerType[5];
            var spellCharges = new int[5];
            var spellCooldown = new int[5];
            var spellCategory = new int[5];
            var spellCatCooldown = new int[5];
            for (var i = 0; i < 5; i++)
            {
                spellId[i] = packet.ReadInt32();
                Console.WriteLine("[" + i + "] Triggered Spell ID: " + Extensions.SpellLine(spellId[i]));

                spellTrigger[i] = packet.ReadEnum<ItemSpellTriggerType>("Trigger Spell Type", TypeCode.Int32, i);
                spellCharges[i] = packet.ReadInt32("Triggered Spell Charges", i);
                spellCooldown[i] = packet.ReadInt32("Triggered Spell Cooldown", i);
                spellCategory[i] = packet.ReadInt32("Triggered Spell Category", i);
                spellCatCooldown[i] = packet.ReadInt32("Triggered Spell Category Cooldown", i);
            }

            var binding = packet.ReadEnum<ItemBonding>("Bonding", TypeCode.Int32);

            var description = packet.ReadCString();

            var pageText = packet.ReadInt32("Page Text");

            var langId = packet.ReadEnum<Language>("Language", TypeCode.Int32);

            var pageMat = packet.ReadEnum<PageMaterial>("Page Material", TypeCode.Int32);

            var startQuest = packet.ReadInt32("Start Quest");

            var lockId = packet.ReadInt32("Lock ID");

            var material = packet.ReadEnum<Material>("Material", TypeCode.Int32);

            var sheath = packet.ReadEnum<SheathType>("Sheath Type", TypeCode.Int32);

            var randomProp = packet.ReadInt32("Random Property");

            var randomSuffix = packet.ReadInt32("Random Suffix");

            var block = packet.ReadInt32("Block");

            var itemSet = packet.ReadInt32("Item Set");

            var maxDura = packet.ReadInt32("Max Durability");

            var area = packet.ReadInt32("Area");

            // In this single (?) case, map 0 means no map
            var map = packet.ReadInt32();
            Console.WriteLine("Map ID: " + (map != 0 ? Extensions.MapLine(map) : map + " (No map)"));

            var bagFamily = packet.ReadEnum<BagFamilyMask>("Bag Family", TypeCode.Int32);

            var totemCat = packet.ReadEnum<TotemCategory>("Totem Category", TypeCode.Int32);

            var socketColor = new ItemSocketColor[3];
            var socketItem = new int[3];
            for (var i = 0; i < 3; i++)
            {
                socketColor[i] = packet.ReadEnum<ItemSocketColor>("Socket Color", TypeCode.Int32, i);
                socketItem[i] = packet.ReadInt32("Socket Item", i);
            }

            var socketBonus = packet.ReadInt32("Socket Bonus");

            var gemProps = packet.ReadInt32("Gem Properties");

            var reqDisEnchSkill = packet.ReadInt32("Required Disenchant Skill");

            var armorDmgMod = packet.ReadSingle("Armor Damage Modifier");

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
