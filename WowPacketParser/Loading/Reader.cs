using System;
using System.Collections.Generic;
using System.Reflection;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        public static IEnumerable<Packet> Read(string loader, string file)
        {
            IEnumerable<Packet> packets = null;
            var res = false;

            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsSealed)
                    continue;

                if (!type.IsPublic)
                    continue;

                if (type.BaseType != typeof(Loader))
                    continue;

                var attrs = (LoaderAttribute[])type.GetCustomAttributes(typeof(LoaderAttribute), false);

                if (attrs.Length <= 0)
                    continue;

                foreach (var attr in attrs)
                {
                    if (attr.Name != loader)
                        continue;

                    var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

                    try
                    {
                        var obj = (Loader)ctors[0].Invoke(new[] { file });

                        packets = obj.ParseFile();
                    }
                    catch (Exception)
                    {
                    }

                    res = true;
                    break;
                }

                if (res)
                    break;
            }

            if (!res)
                Console.WriteLine("No such loader.");

            return packets;
        }
    }
}
