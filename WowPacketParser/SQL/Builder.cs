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
        public static void DumpSQL(string prefix, string fileName, string header)
        {
            var startTime = DateTime.Now;

            var units = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, Unit>()                                                               // empty dict if there are no objects
                : Storage.Objects.Where(
                    obj =>
                        obj.Value.Item1.Type == ObjectType.Unit && obj.Key.GetHighType() != HighGuidType.Pet && // remove pets
                        !obj.Value.Item1.IsTemporarySpawn())                                                    // remove temporary spawns
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as Unit);

            var gameObjects = Storage.Objects.IsEmpty()
                ? new Dictionary<WowGuid, GameObject>()                                                         // empty dict if there are no objects
                : Storage.Objects.Where(obj => obj.Value.Item1.Type == ObjectType.GameObject)
                    .OrderBy(pair => pair.Value.Item2)                                                          // order by spawn time
                    .ToDictionary(obj => obj.Key, obj => obj.Value.Item1 as GameObject);

            foreach (var obj in Storage.Objects)
                obj.Value.Item1.LoadValuesFromUpdateFields();

            using (var store = new SQLFile(fileName))
            {
                var builderMethods = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .Where(type => type.GetCustomAttributes(typeof (BuilderClassAttribute), true).Length > 0)
                    .SelectMany(x => x.GetMethods())
                    .Where(y => y.GetCustomAttributes().OfType<BuilderMethodAttribute>().Any())
                    .ToList();

                var i = 0;
                foreach (var method in builderMethods)
                {
                    var attr = method.GetCustomAttribute<BuilderMethodAttribute>();
                    var parameters = new List<object>();
                    if (attr.Units)
                        parameters.Add(units);

                    if (attr.Gameobjects)
                        parameters.Add(gameObjects);

                    Trace.WriteLine(string.Format("{0}/{1} - Write {2}", ++i, builderMethods.Count, method.Name));
                    store.WriteData(method.Invoke(null, parameters.ToArray()).ToString());
                }

                Trace.WriteLine(store.WriteToFile(header)
                    ? String.Format("{0}: Saved file to '{1}'", prefix, fileName)
                    : "No SQL files created -- empty.");
                var endTime = DateTime.Now;
                var span = endTime.Subtract(startTime);
                Trace.WriteLine(String.Format("Finished SQL file in {0}.", span.ToFormattedString()));
            }
        }
    }
}
