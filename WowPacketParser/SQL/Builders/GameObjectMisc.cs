﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class GameObjectMisc
    {
        [BuilderMethod(true, Gameobjects = true)]
        public static string GameObjectTemplateAddon(Dictionary<WowGuid, GameObject> gameobjects)
        {
            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.gameobject_template_addon))
                return string.Empty;

            if (gameobjects.Count == 0)
                return string.Empty;

            var addons = new DataBag<GameObjectTemplateAddon>();
            foreach (var obj in gameobjects)
            {
                var goT = Storage.GameObjectTemplates.FirstOrDefault(p => p.Item1.Entry == obj.Key.GetEntry());
                if (goT == null)
                    continue;

                var go = obj.Value;
                if (Settings.AreaFilters.Length > 0)
                    if (!(go.Area.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.AreaFilters)))
                        continue;

                if (Settings.MapFilters.Length > 0)
                    if (!(go.Map.ToString(CultureInfo.InvariantCulture).MatchesFilters(Settings.MapFilters)))
                        continue;

                var addon = new GameObjectTemplateAddon
                {
                    Entry = obj.Key.GetEntry()
                };

                HashSet<uint> playerFactions = new HashSet<uint> { 1, 2, 3, 4, 5, 6, 115, 116, 1610, 1629, 2203, 2204 };
                addon.Faction = go.Faction.GetValueOrDefault(0);
                if (playerFactions.Contains(go.Faction.GetValueOrDefault()))
                    addon.Faction = 0;

                addon.Flags = go.Flags.GetValueOrDefault(GameObjectFlag.None);
                addon.Flags &= ~GameObjectFlag.Triggered;
                addon.Flags &= ~GameObjectFlag.Damaged;
                addon.Flags &= ~GameObjectFlag.Destroyed;

                if (addons.ContainsKey(addon))
                    continue;

                if (addon.Flags == GameObjectFlag.None && addon.Faction == 0)
                    continue;

                addons.Add(addon);
            }

            var addonsDb = SQLDatabase.Get(addons);
            return SQLUtil.Compare(addons, addonsDb, StoreNameType.GameObject);
        }
    }
}
