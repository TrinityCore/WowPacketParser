using System;
using System.Diagnostics;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.SQL.Builders;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL
{
    public static class Builder
    {
        public static void DumpSQL(string prefix, string fileName, string header)
        {
            var units = Storage.Objects.IsEmpty() ? null : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.Unit && obj.Key.GetHighType() != HighGuidType.Pet).ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as Unit);
            var gameObjects = Storage.Objects.IsEmpty() ? null : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.GameObject).ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as GameObject);
            //var pets = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Unit && obj.Key.GetHighType() == HighGuidType.Pet).ToDictionary(obj => obj.Key, obj => obj.Value as Unit);
            //var players = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Player).ToDictionary(obj => obj.Key, obj => obj.Value as Player);
            //var items = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Item).ToDictionary(obj => obj.Key, obj => obj.Value as Item);

            if (units != null)
                foreach (var unit in units)
                    unit.Value.LoadValuesFromUpdateFields();

            using (var store = new SQLFile(fileName))
            {
                Trace.WriteLine("01/24 - Write WDBTemplates.GameObject"); store.WriteData(WDBTemplates.GameObject());
                Trace.WriteLine("02/24 - Write Spawns.GameObject"); if (gameObjects != null) store.WriteData(Spawns.GameObject(gameObjects));
                Trace.WriteLine("03/24 - Write WDBTemplates.Quest"); store.WriteData(WDBTemplates.Quest());
                Trace.WriteLine("04/24 - Write QuestOffer.QuestPOI"); store.WriteData(QuestMisc.QuestPOI());
                Trace.WriteLine("05/24 - Write WDBTemplates.Npc"); store.WriteData(WDBTemplates.Npc());
                Trace.WriteLine("06/24 - Write UnitMisc.NpcTemplateNonWDB"); if (units != null) store.WriteData(UnitMisc.NpcTemplateNonWDB(units));
                Trace.WriteLine("07/24 - Write UnitMisc.Addon"); if (units != null) store.WriteData(UnitMisc.Addon(units));
                Trace.WriteLine("08/24 - Write UnitMisc.ModelData"); if (units != null) store.WriteData(UnitMisc.ModelData(units));
                Trace.WriteLine("09/24 - Write UnitMisc.SpellsX"); store.WriteData(UnitMisc.SpellsX());
                Trace.WriteLine("10/24 - Write UnitMisc.CreatureText"); store.WriteData(UnitMisc.CreatureText());
                Trace.WriteLine("11/24 - Write Spawns.Creature"); if (units != null) store.WriteData(Spawns.Creature(units));
                Trace.WriteLine("12/24 - Write UnitMisc.NpcTrainer"); store.WriteData(UnitMisc.NpcTrainer());
                Trace.WriteLine("13/24 - Write UnitMisc.NpcVendor"); store.WriteData(UnitMisc.NpcVendor());
                Trace.WriteLine("14/24 - Write WDBTemplates.PageText"); store.WriteData(WDBTemplates.PageText());
                Trace.WriteLine("15/24 - Write WDBTemplates.NpcText"); store.WriteData(WDBTemplates.NpcText());
                Trace.WriteLine("16/24 - Write UnitMisc.Gossip"); store.WriteData(UnitMisc.Gossip());
                Trace.WriteLine("17/24 - Write UnitMisc.Loot"); store.WriteData(UnitMisc.Loot());
                Trace.WriteLine("18/24 - Write Miscellaneous.SniffData"); store.WriteData(Miscellaneous.SniffData());
                Trace.WriteLine("19/24 - Write Miscellaneous.StartInformation"); store.WriteData(Miscellaneous.StartInformation());
                Trace.WriteLine("20/24 - Write Miscellaneous.ObjectNames"); store.WriteData(Miscellaneous.ObjectNames());
                Trace.WriteLine("21/24 - Write UnitMisc.CreatureEquip"); if (units != null) store.WriteData(UnitMisc.CreatureEquip(units));
                Trace.WriteLine("22/24 - Write UnitMisc.CreatureMovement"); if (units != null) store.WriteData(UnitMisc.CreatureMovement(units));
                Trace.WriteLine("23/24 - Write QuestOffer.QuestOffers"); store.WriteData(QuestMisc.QuestOffer());
                Trace.WriteLine("24/24 - Write QuestOffer.QuestRewards"); store.WriteData(QuestMisc.QuestReward());

                Trace.WriteLine(store.WriteToFile(header)
                                    ? String.Format("{0}: Saved file to '{1}'", prefix, fileName)
                                    : "No SQL files created -- empty.");
            }
        }
    }
}
