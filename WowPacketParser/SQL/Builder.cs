using System;
using System.Diagnostics;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL.Builders;

namespace WowPacketParser.SQL
{
    public static class Builder
    {
        public static void DumpSQL(string prefix, string fileName, SQLOutputFlags sqlOutput)
        {
            using (var store = new SQLStore(fileName))
            {
                if (sqlOutput.HasAnyFlag(SQLOutputFlags.GameObjectTemplate))
                    store.WriteData(WDBTemplates.GameObject());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.GameObjectSpawns))
                    store.WriteData(Spawns.GameObject());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.QuestTemplate))
                    store.WriteData(WDBTemplates.Quest());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.QuestPOI))
                    store.WriteData(QuestMisc.QuestPOI());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureTemplate))
                {
                    store.WriteData(WDBTemplates.Npc());
                    store.WriteData(UnitMisc.NpcTemplateNonWDB());
                }

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureSpawns))
                    store.WriteData(Spawns.Creature());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcTrainer))
                    store.WriteData(UnitMisc.NpcTrainer());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcVendor))
                    store.WriteData(UnitMisc.NpcVendor());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.NpcText))
                    store.WriteData(WDBTemplates.PageText());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.PageText))
                    store.WriteData(WDBTemplates.NpcText());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.Gossip))
                    store.WriteData(UnitMisc.Gossip());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.Loot))
                    store.WriteData(UnitMisc.Loot());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.SniffData | SQLOutputFlags.SniffDataOpcodes))
                    store.WriteData(Miscellaneous.SniffData());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.StartInformation))
                    store.WriteData(Miscellaneous.StartInformation());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.ObjectNames))
                    store.WriteData(Miscellaneous.ObjectNames());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureEquip))
                    store.WriteData(UnitMisc.CreatureEquip());

                if (sqlOutput.HasAnyFlag(SQLOutputFlags.CreatureMovement))
                    store.WriteData(UnitMisc.CreatureMovement());

                Trace.WriteLine(store.WriteToFile()
                                    ? String.Format("{0}: Saved file to '{1}'", prefix, fileName)
                                    : "No SQL files created -- empty.");
            }
        }
    }
}
