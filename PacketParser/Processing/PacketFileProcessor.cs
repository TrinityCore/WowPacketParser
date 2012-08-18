using System;
using System.Collections.Generic;
using System.IO;
using PacketParser.Processing;
using PacketParser.Misc;
using PacketParser.DataStructures;
using PacketParser.Enums;
using System.Linq;

namespace PacketParser.Processing
{
    public abstract class PacketFileProcessor
    {
        [ThreadStatic]
        public static PacketFileProcessor Current;

        public readonly string FileName;
        public readonly string LogPrefix;
        protected Dictionary<Type, IPacketProcessor> Processors = new Dictionary<Type, IPacketProcessor>();

        //protected Dictionary<Opcode, ProcessPacketEventHandler> ProcessPacketHandlers = new Dictionary<Opcode, ProcessPacketEventHandler>();
        protected ProcessPacketEventHandler ProcessAnyPacketHandler;

        //protected Dictionary<Opcode, ProcessedPacketEventHandler> ProcessedPacketHandlers = new Dictionary<Opcode, ProcessedPacketEventHandler>();
        protected ProcessedPacketEventHandler ProcessedAnyPacketHandler;

        //protected Dictionary<Opcode, ProcessDataEventHandler> ProcessDataHandlers = new Dictionary<Opcode, ProcessDataEventHandler>();
        protected ProcessDataEventHandler ProcessAnyDataHandler;

        protected ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler;

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

        public string GetHeader()
        {
            return "# TrinityCore - Packet Parser" + Environment.NewLine +
                   "# File name: " + Path.GetFileName(FileName) + Environment.NewLine +
                   "# Detected build: " + ClientVersion.Build + Environment.NewLine +
                   "# Parsing date: " + DateTime.Now.ToString() +
                   Environment.NewLine;
        }

        protected List<Type> GetAvailableProcessors()
        {
            return Utilities.GetClasses(typeof(IPacketProcessor));
        }

        protected virtual void InitProcessors()
        {
            var procss = GetAvailableProcessors();

            var processorObjects = new List<Tuple<Type, IPacketProcessor>>();
            var processorsDependingOn = new Dictionary<Type, List<Type>>();
            var processorsToInit = new LinkedList<Tuple<Type, IPacketProcessor>>();
            foreach (var p in procss)
            {
                if (p.IsAbstract || p.IsInterface)
                    continue;
                IPacketProcessor instance = (IPacketProcessor)Activator.CreateInstance(p);
                var deps = instance.DependsOn;
                if (deps != null)
                {
                    foreach (var dep in deps)
                    {
                        if (!processorsDependingOn.ContainsKey(dep))
                            processorsDependingOn[dep] = new List<Type>();
                        processorsDependingOn[dep].Add(p);
                    }
                }
                processorObjects.Add(new Tuple<Type, IPacketProcessor>(p, instance));
                //Console.WriteLine(p);
            }
            int counter = processorObjects.Count;
            while (counter > 0)
            {
                for(int j = 0; j < processorObjects.Count; ++j)
                {
                    var p = processorObjects[j];
                    if (p == null)
                        continue;
                    var t = p.Item1;
                    var i = p.Item2;
                    // noone wants to load this processor, skip
                    if (i.LoadOnDepend && !processorsDependingOn.ContainsKey(t))
                    {
                        processorObjects[j] = null;
                        --counter;
                        continue;
                    }
                    var deps = i.DependsOn;

                    /*if (deps == null)
                    {
                        processorsToInit.AddFirst(new Tuple<Type, IPacketProcessor>(t, i));
                        processorObjects[j] = null;
                        --counter;
                        continue;
                    }*/
                    bool valid = true;
                    // make sure all processors which are needed by this processor are already added
                    if (deps != null)
                    {
                        for(int k = 0; k < deps.Length; ++k)
                        {
                            bool found = false;
                            foreach (var l in processorsToInit)
                            {
                                if (l.Item1 == deps[k])
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                            continue;
                    }
                    // make sure none of the processors which are depending on this one are not added
                    if (processorsDependingOn.ContainsKey(t))
                    {
                        var depending = processorsDependingOn[t];
                        for(int k = 0; k < depending.Count; ++k)
                        {
                            bool found = false;
                            foreach (var l in processorsToInit)
                            {
                                if (l.Item1 == depending[k])
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found)
                            {
                                valid = false;
                                break;
                            }
                        }
                        if (!valid)
                            continue;
                    }
                    processorsToInit.AddLast(new Tuple<Type, IPacketProcessor>(t, i));
                    processorObjects[j] = null;
                    --counter;
                }
            }
            foreach (var p in processorsToInit)
            {
                var t = p.Item1;
                var i = p.Item2;
                if (i.Init(this))
                {
                    Processors[t] = i;
                    ProcessAnyPacketHandler += i.ProcessAnyPacketHandler;
                    ProcessAnyDataHandler += i.ProcessAnyDataHandler;
                    ProcessedAnyPacketHandler += i.ProcessedAnyPacketHandler;
                    ProcessedAnyDataNodeHandler += i.ProcessedAnyDataNodeHandler;
                }
            }
            if (ProcessAnyPacketHandler == null)
                ProcessAnyPacketHandler += Stub1;
            if (ProcessedAnyPacketHandler == null)
                ProcessedAnyPacketHandler += Stub2;
            if (ProcessAnyDataHandler == null)
                ProcessAnyDataHandler += Stub3;
            if (ProcessedAnyDataNodeHandler == null)
                ProcessedAnyDataNodeHandler += Stub4;
        }

        public void Stub1(Packet packet){}

        public void Stub2(Packet packet){}

        public void Stub3(string name, int? index, Object obj, Type t){}

        public void Stub4(string name, Object obj, Type t){}

        public T GetProcessor<T>() where T : IPacketProcessor
        {
            var type = typeof(T);
            if (Processors.ContainsKey(type))
                return (T)Processors[type];
            return default(T);
        }

        protected void FinishProcessors()
        {
            // finalize processors
            foreach (var procs in Processors)
            {
                procs.Value.Finish();
            }
        }

        public void ProcessData(Packet data)
        {
            var itr = data.GetTreeEnumerator();
            bool moveNext = itr.MoveNext();
            bool cont = moveNext;
            while (cont)
            {
                foreach (var i in itr.CurrentClosedNodes)
                {
                    ProcessedAnyDataNodeHandler(i.name, i.obj, i.type);
                    if (i.type == typeof(Packet))
                        ProcessedAnyPacketHandler((Packet)i.obj);
                }
                if (!moveNext)
                    break;
                
                if (itr.Type == typeof(Packet))
                {
                    Packet packet = (Packet)itr.Current;
                    ProcessAnyPacketHandler(packet);
                }

                ProcessAnyDataHandler(itr.Name, itr.Index, itr.Current, itr.Type);
                moveNext = itr.MoveNext();
            }
        }
    }
}
