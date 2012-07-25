using System;
using System.Collections.Generic;
using System.IO;
using PacketParser.Processing;
using PacketParser.Misc;
using PacketParser.DataStructures;

namespace PacketParser.Processing
{
    public class PacketFileProcessor
    {
        [ThreadStatic]
        public static PacketFileProcessor Current;

        public readonly string FileName;
        public readonly string LogPrefix;
        public Dictionary<Type, IPacketProcessor> Processors = new Dictionary<Type, IPacketProcessor>();

        public PacketFileProcessor(string fileName, Tuple<int, int> number = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("fileName cannot be null, empty or whitespace.", "fileName");

            FileName = fileName;

            if (number == null)
                LogPrefix = string.Format("[{0}]", Path.GetFileName(fileName));
            else
                LogPrefix = string.Format("[{0}/{1} {2}]", number.Item1, number.Item2, Path.GetFileName(fileName));
        }

        public T GetProcessor<T>() where T:IPacketProcessor
        {
            var type = typeof(T);
            if (Processors.ContainsKey(type))
                return (T)Processors[type];
            return default(T);
        }

        public string GetHeader()
        {
            return "# TrinityCore - Packet Parser" + Environment.NewLine +
                   "# File name: " + Path.GetFileName(FileName) + Environment.NewLine +
                   "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                   "# Parsing date: " + DateTime.Now.ToString() +
                   Environment.NewLine;
        }

        public virtual void InitProcessors()
        {
            var procss = Utilities.GetClasses(typeof(IPacketProcessor));
            foreach (var p in procss)
            {
                if (p.IsAbstract || p.IsInterface)
                    continue;
                IPacketProcessor instance = (IPacketProcessor)Activator.CreateInstance(p);
                if (instance.Init(this))
                    Processors[p] = instance;
            }
        }

        public void ProcessData(Packet data)
        {
            var itr = data.GetTreeEnumerator();
            bool moveNext = itr.MoveNext();
            bool cont = moveNext;
            while (cont)
            {
                foreach (var p in Processors)
                {
                    var proc = p.Value;
                    foreach (var i in itr.CurrentClosedNodes)
                    {
                        if (i.type == typeof(Packet))
                            proc.ProcessedPacket(i.obj as Packet);
                    }
                }
                if (!moveNext)
                    break;
                foreach (var p in Processors)
                {
                    var proc = p.Value;
                    if (itr.Type == typeof(Packet))
                    {
                        Packet packet = (Packet)itr.Current;
                        proc.ProcessPacket(packet);
                    }
                    proc.ProcessData(itr.Name, itr.Index, itr.Current, itr.Type, itr);
                }
                moveNext = itr.MoveNext();
            }
        }
    }
}
