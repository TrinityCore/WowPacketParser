using System;
using System.Diagnostics;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL.Builders;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL
{
    public static class Builder
    {
        public static void DumpSQL(string prefix, string fileName, SQLOutputFlags sqlOutput)
        {
            var units = Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.Unit && obj.Key.GetHighType() != HighGuidType.Pet).ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as Unit);
            var gameObjects = Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.GameObject).ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as GameObject);
            //var pets = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Unit && obj.Key.GetHighType() == HighGuidType.Pet).ToDictionary(obj => obj.Key, obj => obj.Value as Unit);
            //var players = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Player).ToDictionary(obj => obj.Key, obj => obj.Value as Player);
            //var items = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Item).ToDictionary(obj => obj.Key, obj => obj.Value as Item);

            foreach (var unit in units)
                unit.Value.LoadValuesFromUpdateFields();

            using (var store = new SQLFile(fileName))
            {
                Trace.WriteLine("01/22 - Write WDBTemplates.GameObject"); store.WriteData(WDBTemplates.GameObject());
                Trace.WriteLine("02/22 - Write Spawns.GameObject"); store.WriteData(Spawns.GameObject(gameObjects));
                Trace.WriteLine("03/22 - Write WDBTemplates.Quest"); store.WriteData(WDBTemplates.Quest());
                Trace.WriteLine("04/22 - Write QuestMisc.QuestPOI"); store.WriteData(QuestMisc.QuestPOI());
                Trace.WriteLine("05/22 - Write WDBTemplates.Npc"); store.WriteData(WDBTemplates.Npc());
                Trace.WriteLine("06/22 - Write UnitMisc.NpcTemplateNonWDB"); store.WriteData(UnitMisc.NpcTemplateNonWDB(units));
                Trace.WriteLine("07/22 - Write UnitMisc.Addon"); store.WriteData(UnitMisc.Addon(units));
                Trace.WriteLine("08/22 - Write UnitMisc.ModelData"); store.WriteData(UnitMisc.ModelData(units));
                Trace.WriteLine("09/22 - Write UnitMisc.SpellsX"); store.WriteData(UnitMisc.SpellsX());
                Trace.WriteLine("10/22 - Write UnitMisc.CreatureText"); store.WriteData(UnitMisc.CreatureText());
                Trace.WriteLine("11/22 - Write Spawns.Creature"); store.WriteData(Spawns.Creature(units));
                Trace.WriteLine("12/22 - Write UnitMisc.NpcTrainer"); store.WriteData(UnitMisc.NpcTrainer());
                Trace.WriteLine("13/22 - Write UnitMisc.NpcVendor"); store.WriteData(UnitMisc.NpcVendor());
                Trace.WriteLine("14/22 - Write WDBTemplates.PageText"); store.WriteData(WDBTemplates.PageText());
                Trace.WriteLine("15/22 - Write WDBTemplates.NpcText"); store.WriteData(WDBTemplates.NpcText());
                Trace.WriteLine("16/22 - Write UnitMisc.Gossip"); store.WriteData(UnitMisc.Gossip());
                Trace.WriteLine("17/22 - Write UnitMisc.Loot"); store.WriteData(UnitMisc.Loot());
                Trace.WriteLine("18/22 - Write Miscellaneous.SniffData"); store.WriteData(Miscellaneous.SniffData());
                Trace.WriteLine("19/22 - Write Miscellaneous.StartInformation"); store.WriteData(Miscellaneous.StartInformation());
                Trace.WriteLine("20/22 - Write Miscellaneous.ObjectNames"); store.WriteData(Miscellaneous.ObjectNames());
                Trace.WriteLine("21/22 - Write UnitMisc.CreatureEquip"); store.WriteData(UnitMisc.CreatureEquip(units));
                Trace.WriteLine("22/22 - Write UnitMisc.CreatureMovement"); store.WriteData(UnitMisc.CreatureMovement(units));

                Trace.WriteLine(store.WriteToFile()
                                    ? String.Format("{0}: Saved file to '{1}'", prefix, fileName)
                                    : "No SQL files created -- empty.");
            }
        }
    }
}
