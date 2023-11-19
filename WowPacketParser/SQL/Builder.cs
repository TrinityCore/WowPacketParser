using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.SQL.Builders;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL
{
    public static class Builder
    {
        private static StoreNameType FromObjectType(ObjectType type)
        {
            switch (type)
            {
                case ObjectType.Item:
                    return StoreNameType.Item;
                case ObjectType.Unit:
                    return StoreNameType.Unit;
                case ObjectType.Player:
                    return StoreNameType.Player;
                case ObjectType.GameObject:
                    return StoreNameType.GameObject;
                case ObjectType.Map:
                    return StoreNameType.Map;
                case ObjectType.Object:
                case ObjectType.Container:
                case ObjectType.DynamicObject:
                case ObjectType.Corpse:
                case ObjectType.AreaTrigger:
                case ObjectType.SceneObject:
                case ObjectType.Conversation:
                    return StoreNameType.None;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Update SQLDatabase.NameStores with names from Storage.ObjectNames
        /// </summary>
        private static void LoadNames()
        {
            foreach (var objectName in Storage.ObjectNames)
            {
                if (objectName.Item1.ObjectType != null && objectName.Item1.ID != null)
                {
                    var type = objectName.Item1.ObjectType.Value;
                    Dictionary<int, string> names;
                    if (!SQLDatabase.NameStores.TryGetValue(type, out names))
                    {
                        names = new Dictionary<int, string>();
                        SQLDatabase.NameStores.Add(type, names);
                    }

                    if (!names.ContainsKey(objectName.Item1.ID.Value))
                        names.Add(objectName.Item1.ID.Value, objectName.Item1.Name);
                }
            }
        }

        public static void DumpFile(string prefix, string fileName, string header, List<MethodInfo> builderMethods, Dictionary<WowGuid, Unit> units, Dictionary<WowGuid, GameObject> gameObjects)
        {
            var startTime = DateTime.Now;
            using (var store = new SQLFile(fileName))
            {
                store.WriteData(UnitMisc.CreatureEquip(units)); // ensure this is run before spawns

                for (int i = 1; i <= builderMethods.Count; i++)
                {
                    var method = builderMethods[i - 1];
                    var attr = method.GetCustomAttribute<BuilderMethodAttribute>();

                    if (attr.CheckVersionMismatch)
                    {
                        if (!GetExpectedTargetDatabasesForExpansion(ClientVersion.Expansion).Contains(Settings.TargetedDatabase))
                        {
                            Trace.WriteLine(
                                $"{i}/{builderMethods.Count} - Error: Couldn't generate SQL output of {method.Name} since the targeted database and the sniff version don't match.");
                            continue;
                        }
                    }

                    var parameters = new List<object>();
                    if (attr.Units)
                        parameters.Add(units);

                    if (attr.Gameobjects)
                        parameters.Add(gameObjects);

                    Trace.WriteLine($"{i}/{builderMethods.Count} - Write {method.Name}");
                    try
                    {
                        store.WriteData(method.Invoke(null, parameters.ToArray()).ToString());
                    }
                    catch (TargetInvocationException e)
                    {
                        Trace.WriteLine($"{i}/{builderMethods.Count} - Error: Failed writing {method.Name}");
                        Trace.TraceError(e.InnerException?.ToString() ?? e.ToString());
                    }
                }

                Trace.WriteLine(store.WriteToFile(header)
                    ? $"{prefix}: Saved file to '{fileName}'"
                    : "No SQL files created -- empty.");
                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Trace.WriteLine($"Finished SQL file {fileName} in {span.ToFormattedString()}.");
            }
        }

        public static void DumpSQL(string prefix, string fileName, string header)
        {
            var startTime = DateTime.Now;

            LoadNames();

            var units = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, Unit>()                                                               // empty dict if there are no objects
                : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.Unit)
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as Unit);

            var gameObjects = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, GameObject>()                                                         // empty dict if there are no objects
                : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.GameObject)
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as GameObject);

            foreach (var obj in Storage.Objects)
                obj.Value.Item1.LoadValuesFromUpdateFields();

            var methods = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => type.GetCustomAttributes(typeof(BuilderClassAttribute), true).Length > 0)
                    .SelectMany(x => x.GetMethods());
            var allMethods = methods.Select(y => new { Method = y, Attributes = y.GetCustomAttributes().OfType<BuilderMethodAttribute>()}).Where(y => y.Attributes.Any()).ToList();

            if (Settings.SplitSQLFile)
            {
                fileName = System.IO.Path.ChangeExtension(fileName, null); // remove .sql

                var hotfixMethods = allMethods.Where(x => x.Attributes.First().Database == TargetSQLDatabase.Hotfixes).Select(x => x.Method).ToList();
                DumpFile(prefix, $"{fileName}_hotfixes.sql", header, hotfixMethods, units, gameObjects);

                var worldMethods = allMethods.Where(x => x.Attributes.First().Database == TargetSQLDatabase.World).Select(x => x.Method).ToList();
                DumpFile(prefix, $"{fileName}_world.sql", header, worldMethods, units, gameObjects);

                var wppMethods = allMethods.Where(x => x.Attributes.First().Database == TargetSQLDatabase.WPP).Select(x => x.Method).ToList();
                DumpFile(prefix, $"{fileName}_wpp.sql", header, wppMethods, units, gameObjects);
            }
            else
            {
                var builderMethods = allMethods.Select(x => x.Method).ToList();
                DumpFile(prefix, fileName, header, builderMethods, units, gameObjects);
            }
        }

        private static List<TargetedDatabase> GetExpectedTargetDatabasesForExpansion(ClientType expansion)
        {
            switch (expansion)
            {
                case ClientType.TheBurningCrusade:
                    return new List<TargetedDatabase> { TargetedDatabase.TheBurningCrusade };
                case ClientType.WrathOfTheLichKing:
                    return new List<TargetedDatabase> { TargetedDatabase.WrathOfTheLichKing };
                case ClientType.Cataclysm:
                    return new List<TargetedDatabase> { TargetedDatabase.Cataclysm };
                case ClientType.WarlordsOfDraenor:
                    return new List<TargetedDatabase> { TargetedDatabase.WarlordsOfDraenor };
                case ClientType.Legion:
                    return new List<TargetedDatabase> { TargetedDatabase.Legion };
                case ClientType.BattleForAzeroth: // == ClientType.Classic
                    return new List<TargetedDatabase> { TargetedDatabase.BattleForAzeroth, TargetedDatabase.Classic, TargetedDatabase.TheBurningCrusade };
                case ClientType.Shadowlands: // == ClientType.BurningCrusadeClassic
                    return new List<TargetedDatabase> { TargetedDatabase.Shadowlands, TargetedDatabase.Classic, TargetedDatabase.WotlkClassic, TargetedDatabase.TheBurningCrusade };
                case ClientType.Dragonflight:
                    return new List<TargetedDatabase> { TargetedDatabase.Dragonflight, TargetedDatabase.WotlkClassic };
                default:
                    return new List<TargetedDatabase>();
            }
        }
    }
}
