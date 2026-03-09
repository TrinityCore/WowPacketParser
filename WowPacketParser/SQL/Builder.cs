using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing.Proto;
using WowPacketParser.Proto;
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

        public static void DumpFile(IReadOnlyList<Packets> packets, string prefix, string fileName, string header, List<Type> protoBuilders, List<MethodInfo> builderMethods, Dictionary<WowGuid, Unit> units, Dictionary<WowGuid, GameObject> gameObjects)
        {
            var startTime = DateTime.Now;
            using (var store = new SQLFile(fileName))
            {
                store.WriteData(UnitMisc.CreatureEquip(units)); // ensure this is run before spawns

                var currentBuilder = 0;
                var totalCount = protoBuilders.Count + builderMethods.Count;

                foreach (var builderType in protoBuilders)
                {
                    currentBuilder++;
                    var builder = (IProtoQueryBuilder)Activator.CreateInstance(builderType);

                    if (!builder.IsEnabled())
                        continue;

                    Trace.WriteLine($"{currentBuilder}/{totalCount} - Write {builderType}");
                    try
                    {
                        store.WriteData(builder.Process(packets));
                    }
                    catch (TargetInvocationException e)
                    {
                        Trace.WriteLine($"{currentBuilder}/{totalCount} - Error: Failed writing {builderType}");
                        Trace.TraceError(e.InnerException?.ToString() ?? e.ToString());
                    }
                }

                foreach (var method in builderMethods)
                {
                    currentBuilder++;
                    var attr = method.GetCustomAttribute<BuilderMethodAttribute>();

                    if (attr.CheckVersionMismatch)
                    {
                        if (!GetExpectedTargetDatabasesForExpansion(ClientVersion.Expansion).Contains(Settings.TargetedDatabase))
                        {
                            Trace.WriteLine(
                                $"{currentBuilder}/{totalCount} - Error: Couldn't generate SQL output of {method.Name} since the targeted database and the sniff version don't match.");
                            continue;
                        }
                    }

                    var parameters = new List<object>();
                    if (attr.Units)
                        parameters.Add(units);

                    if (attr.Gameobjects)
                        parameters.Add(gameObjects);

                    Trace.WriteLine($"{currentBuilder}/{totalCount} - Write {method.Name}");
                    try
                    {
                        store.WriteData(method.Invoke(null, parameters.ToArray()).ToString());
                    }
                    catch (TargetInvocationException e)
                    {
                        Trace.WriteLine($"{currentBuilder}/{totalCount} - Error: Failed writing {method.Name}");
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

        public static void DumpSQL(IReadOnlyList<Packets> packets, string prefix, string fileName, string header)
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

            var allProtoBuilders = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Select(type => (Type: type, Attributes: type.GetCustomAttributes(typeof(ProtoBuilderClassAttribute), true).OfType<ProtoBuilderClassAttribute>().ToList()))
                .Where(pair => pair.Attributes.Count > 0)
                .ToList();

            if (Settings.SplitSQLFile)
            {
                fileName = System.IO.Path.ChangeExtension(fileName, null); // remove .sql

                var hotfixMethods = allMethods.Where(x => x.Attributes.First().Database == TargetSQLDatabase.Hotfixes).Select(x => x.Method).ToList();
                var hotfixProtoBuilders = allProtoBuilders.Where(x => x.Attributes.First().Database == TargetSQLDatabase.Hotfixes).Select(x => x.Type).ToList();
                DumpFile(packets, prefix, $"{fileName}_hotfixes.sql", header, hotfixProtoBuilders, hotfixMethods, units, gameObjects);

                var worldMethods = allMethods.Where(x => x.Attributes.First().Database == TargetSQLDatabase.World).Select(x => x.Method).ToList();
                var worldProtoBuilders = allProtoBuilders.Where(x => x.Attributes.First().Database == TargetSQLDatabase.World).Select(x => x.Type).ToList();
                DumpFile(packets, prefix, $"{fileName}_world.sql", header, worldProtoBuilders, worldMethods, units, gameObjects);

                var wppMethods = allMethods.Where(x => x.Attributes.First().Database == TargetSQLDatabase.WPP).Select(x => x.Method).ToList();
                var wppProtoBuilders = allProtoBuilders.Where(x => x.Attributes.First().Database == TargetSQLDatabase.WPP).Select(x => x.Type).ToList();
                DumpFile(packets, prefix, $"{fileName}_wpp.sql", header, wppProtoBuilders, wppMethods, units, gameObjects);
            }
            else
            {
                var protoBuilderTypes = allProtoBuilders.Select(x => x.Type).ToList();
                var builderMethods = allMethods.Select(x => x.Method).ToList();
                DumpFile(packets, prefix, fileName, header, protoBuilderTypes, builderMethods, units, gameObjects);
            }
        }

        private static List<TargetedDatabase> GetExpectedTargetDatabasesForExpansion(ClientType expansion)
        {
            switch (expansion)
            {
                case ClientType.TheBurningCrusade:
                    return [TargetedDatabase.TheBurningCrusade];
                case ClientType.WrathOfTheLichKing:
                    return [TargetedDatabase.WrathOfTheLichKing];
                case ClientType.Cataclysm:
                    return [TargetedDatabase.Cataclysm];
                case ClientType.WarlordsOfDraenor:
                    return [TargetedDatabase.WarlordsOfDraenor];
                case ClientType.Legion:
                    return [TargetedDatabase.Legion];
                case ClientType.BattleForAzeroth: // == ClientType.Classic
                    return [TargetedDatabase.BattleForAzeroth, TargetedDatabase.Classic, TargetedDatabase.TheBurningCrusade];
                case ClientType.Shadowlands: // == ClientType.BurningCrusadeClassic
                    return [TargetedDatabase.Shadowlands, TargetedDatabase.Classic, TargetedDatabase.WotlkClassic, TargetedDatabase.TheBurningCrusade];
                case ClientType.Dragonflight:
                    return [TargetedDatabase.Dragonflight, TargetedDatabase.WotlkClassic, TargetedDatabase.CataClassic];
                case ClientType.TheWarWithin:
                    return [TargetedDatabase.TheWarWithin, TargetedDatabase.CataClassic];
                case ClientType.Midnight:
                    return [TargetedDatabase.Midnight];
                default:
                    return [];
            }
        }
    }
}
