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
        public static void DumpSQL(string prefix, string fileName, string header)
        {
            var units = Storage.Objects.IsEmpty() ? null : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.Unit && obj.Key.GetHighType() != HighGuidType.Pet && !obj.Value.Item1.IsTemporarySpawn()).ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as Unit);
            var gameObjects = Storage.Objects.IsEmpty() ? null : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.GameObject).ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as GameObject);
            //var pets = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Unit && obj.Key.GetHighType() == HighGuidType.Pet).ToDictionary(obj => obj.Key, obj => obj.Value as Unit);
            //var players = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Player).ToDictionary(obj => obj.Key, obj => obj.Value as Player);
            //var items = Storage.Objects.Where(obj => obj.Value.Type == ObjectType.Item).ToDictionary(obj => obj.Key, obj => obj.Value as Item);

            foreach (var obj in Storage.Objects)
                obj.Value.Item1.LoadValuesFromUpdateFields();

            // Ewwwww...
            var build = ClientVersion.BuildInt;
            if (!Storage.GameObjectTemplates.IsEmpty())
                foreach (var obj in Storage.GameObjectTemplates)
                    obj.Value.Item1.WDBVerified = build;
            if (!Storage.NpcTexts.IsEmpty())
                foreach (var obj in Storage.NpcTexts)
                    obj.Value.Item1.WDBVerified = build;
            if (!Storage.PageTexts.IsEmpty())
                foreach (var obj in Storage.PageTexts)
                    obj.Value.Item1.WDBVerified = build;
            if (!Storage.UnitTemplates.IsEmpty())
                foreach (var obj in Storage.UnitTemplates)
                    obj.Value.Item1.WDBVerified = build;
            if (!Storage.QuestTemplates.IsEmpty())
                foreach (var obj in Storage.QuestTemplates)
                    obj.Value.Item1.WDBVerified = build;
            if (!Storage.ItemTemplates.IsEmpty())
                foreach (var obj in Storage.ItemTemplates)
                    obj.Value.Item1.WDBVerified = build;

            using (var store = new SQLFile(fileName))
            {
                // TODO: Rewrite this
                var i = 0;
                const int max = 27;
                Trace.WriteLine(string.Format("{0}/{1} - Write WDBTemplates.GameObject", ++i, max)); store.WriteData(WDBTemplates.GameObject());
                Trace.WriteLine(string.Format("{0}/{1} - Write Spawns.GameObject", ++i, max)); if (gameObjects != null) store.WriteData(Spawns.GameObject(gameObjects));
                Trace.WriteLine(string.Format("{0}/{1} - Write WDBTemplates.Quest", ++i, max)); store.WriteData(WDBTemplates.Quest());
                Trace.WriteLine(string.Format("{0}/{1} - Write QuestOffer.QuestPOI", ++i, max)); store.WriteData(QuestMisc.QuestPOI());
                Trace.WriteLine(string.Format("{0}/{1} - Write WDBTemplates.Npc", ++i, max)); store.WriteData(WDBTemplates.Npc());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.NpcTemplateNonWDB", ++i, max)); if (units != null) store.WriteData(UnitMisc.NpcTemplateNonWDB(units));
                Trace.WriteLine(string.Format("{0}/{1} - Write Miscellaneous.GameObjectTemplateNonWDB", ++i, max)); if (gameObjects != null) store.WriteData(Miscellaneous.GameobjectTemplateNonWDB(gameObjects));
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.Addon", ++i, max)); if (units != null) store.WriteData(UnitMisc.Addon(units));
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.ModelData", ++i, max)); if (units != null) store.WriteData(UnitMisc.ModelData(units));
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.SpellsX", ++i, max)); store.WriteData(UnitMisc.SpellsX());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.CreatureText", ++i, max)); store.WriteData(UnitMisc.CreatureText());
                Trace.WriteLine(string.Format("{0}/{1} - Write Spawns.Creature", ++i, max)); if (units != null) store.WriteData(Spawns.Creature(units));
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.NpcTrainer", ++i, max)); store.WriteData(UnitMisc.NpcTrainer());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.NpcVendor", ++i, max)); store.WriteData(UnitMisc.NpcVendor());
                Trace.WriteLine(string.Format("{0}/{1} - Write WDBTemplates.PageText", ++i, max)); store.WriteData(WDBTemplates.PageText());
                Trace.WriteLine(string.Format("{0}/{1} - Write WDBTemplates.NpcText", ++i, max)); store.WriteData(WDBTemplates.NpcText());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.Gossip", ++i, max)); store.WriteData(UnitMisc.Gossip());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.Loot", ++i, max)); store.WriteData(UnitMisc.Loot());
                Trace.WriteLine(string.Format("{0}/{1} - Write Miscellaneous.SniffData", ++i, max)); store.WriteData(Miscellaneous.SniffData());
                Trace.WriteLine(string.Format("{0}/{1} - Write Miscellaneous.StartInformation", ++i, max)); store.WriteData(Miscellaneous.StartInformation());
                Trace.WriteLine(string.Format("{0}/{1} - Write Miscellaneous.ObjectNames", ++i, max)); store.WriteData(Miscellaneous.ObjectNames());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.CreatureEquip", ++i, max)); if (units != null) store.WriteData(UnitMisc.CreatureEquip(units));
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.CreatureMovement", ++i, max)); if (units != null) store.WriteData(UnitMisc.CreatureMovement(units));
                Trace.WriteLine(string.Format("{0}/{1} - Write QuestOffer.QuestOffers", ++i, max)); store.WriteData(QuestMisc.QuestOffer());
                Trace.WriteLine(string.Format("{0}/{1} - Write QuestOffer.QuestRewards", ++i, max)); store.WriteData(QuestMisc.QuestReward());
                Trace.WriteLine(string.Format("{0}/{1} - Write WDBTemplates.Item", ++i, max)); store.WriteData(WDBTemplates.Item());
                Trace.WriteLine(string.Format("{0}/{1} - Write UnitMisc.PointsOfInterest", ++i, max)); store.WriteData(UnitMisc.PointsOfInterest());

                Trace.WriteLine(store.WriteToFile(header)
                                    ? String.Format("{0}: Saved file to '{1}'", prefix, fileName)
                                    : "No SQL files created -- empty.");
            }
        }
    }
}
