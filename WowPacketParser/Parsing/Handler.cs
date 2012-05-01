using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using System.Text;
using System.Collections.Specialized;
using System;
using Guid = WowPacketParser.Misc.Guid;
using System.IO;

namespace WowPacketParser.Parsing
{
    //using NameDict = Dictionary<string, Object>;
    //1using IndexDict = Dictionary<int, Dictionary<string, Object>>;
    using NameDict = OrderedDictionary;
    using IndexDict = Dictionary<int, OrderedDictionary>;

    public static class Handler
    {
        private static Dictionary<int, Action<Packet>> LoadHandlers()
        {
            var handlers = new Dictionary<int, Action<Packet>>(1000);

            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                //if (type.Namespace != "WowPacketParser.Parsing.Parsers")
                //    continue;

                if (!type.IsAbstract)
                    continue;

                if (!type.IsPublic)
                    continue;

                var methods = type.GetMethods();

                foreach (var method in methods)
                {
                    if (!method.IsPublic)
                        continue;

                    var attrs = (ParserAttribute[])method.GetCustomAttributes(typeof(ParserAttribute),
                        false);

                    if (attrs.Length <= 0)
                        continue;

                    var parms = method.GetParameters();

                    if (parms.Length <= 0)
                        continue;

                    if (parms[0].ParameterType != typeof(Packet))
                        continue;

                    foreach (var attr in attrs)
                    {
                        var opc = attr.Opcode;
                        if (opc == 0)
                            continue;

                        var del = (Action<Packet>)Delegate.CreateDelegate(typeof(Action<Packet>), method);

                        if (handlers.ContainsKey(opc))
                        {
                            Trace.WriteLine(string.Format("Error: (Build: {0}) tried to overwrite delegate for opcode {1} ({2}); new handler: {3}; old handler: {4}",
                                ClientVersion.Build, opc, Opcodes.GetOpcodeName(opc), del.Method, handlers[opc].Method));
                            continue;
                        }

                        handlers[opc] = del;
                    }
                }
            }

            return handlers;
        }
        private static readonly Dictionary<int, Action<Packet>> Handlers = LoadHandlers();

        public static void WriteToFile(IEnumerable<Packet> packets, string file)
        {
            File.Delete(file);
            using (var writer = new StreamWriter(file, true))
            {
                foreach (var packet in packets)
                    writer.WriteLine(DumpAsText(packet));

                writer.Flush();
            }
        }

        public static string DumpAsText(Packet packet)
        {
            StringBuilder output = new StringBuilder();
            DumpDataAsText(packet, output, "");
            return output.ToString();
        }

        public static void DumpDataAsText(Object d, StringBuilder output, string prefix)
        {
            try
            {
                dynamic data = d;
                switch (Type.GetTypeCode(d.GetType()))
                {
                    case TypeCode.Single:
                    case TypeCode.Double:
                        if (Settings.DebugReads)
                        {
                            byte[] bytes = BitConverter.GetBytes(data);
                            output.AppendFormat("{0} (0x{1})", data, BitConverter.ToString(bytes));
                        }
                        else
                            output.Append(data);
                        output.AppendLine();
                        break;
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                        output.AppendFormat("{0}{1}", data, (Settings.DebugReads ? " (0x" + data.ToString("X2") + ")" : String.Empty));
                        output.AppendLine();
                        break;
                    case TypeCode.Int16:
                    case TypeCode.UInt16:
                        output.AppendFormat("{0}{1}", data, (Settings.DebugReads ? " (0x" + data.ToString("X4") + ")" : String.Empty));
                        output.AppendLine();
                        break;
                    case TypeCode.UInt32:
                    case TypeCode.Int32:
                        output.AppendFormat("{0}{1}", data, (Settings.DebugReads ? " (0x" + data.ToString("X8") + ")" : String.Empty));
                        output.AppendLine();
                        break;
                    case TypeCode.UInt64:
                    case TypeCode.Int64:
                        output.AppendFormat("{0}{1}", data, (Settings.DebugReads ? " (0x" + data.ToString("X16") + ")" : String.Empty));
                        output.AppendLine();
                        break;
                    case TypeCode.DateTime:
                        output.AppendFormat("{0}{1}", (DateTime)data, (Settings.DebugReads ? " (0x" + data.ToString("X4") + ")" : String.Empty));
                        output.AppendLine();
                        break;
                    default:
                        //else if (data.GetType() == typeof(enum))
                        //{
                        //     Writer.WriteLine("{0}{1}: {2} ({3}){4}", GetIndexString(values), name, data.Value, data.Key, (Settings.DebugReads ? " (0x" + data.Key.ToString("X4") + ")" : String.Empty));
                        //}
                        if (data.GetType() == typeof(Guid))
                        {
                            //if (WriteToFile)
                                //WriteToFile = Filters.CheckFilter((Guid)data);
                            output.Append(data);
                            output.AppendLine();
                        }
                        else if (data.GetType() == typeof(StoreEntry))
                        {
                            var val = (StoreEntry)data;
                            //if (WriteToFile)
                                //WriteToFile = Filters.CheckFilter(val._type, val._data);
                            output.Append(data);
                            output.AppendLine();
                        }
                        else if (data.GetType() == typeof(Packet))
                        {
                            Packet packet = (Packet)data;
                            output.Append(String.Format("{0}: {1} (0x{2}) Length: {3} Time: {4} Number: {5}{6}",
                                packet.Direction, Opcodes.GetOpcodeName(packet.Opcode), packet.Opcode.ToString("X4"),
                                packet.Length, packet.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                                packet.Number, (packet.Parent != null) ? String.Format(" (subpacket of packet: opcode {0} (0x{1}), number {2} )", Opcodes.GetOpcodeName(packet.Parent.Opcode), packet.Parent.Opcode, packet.Parent.Number)  : String.Empty));

                            DumpDataAsText(packet.GetData(), output, prefix);

                            // unread packet data
                            if (packet.Status != ParsedStatus.Success)
                            {
                                if (packet.Status == ParsedStatus.WithErrors)
                                {
                                    output.AppendLine(packet.ErrorMessage);
                                }
                                //output.AppendLine(packet.ToHex());
                            }
                        }
                        else if (data.GetType() == typeof(NameDict))
                        {
                            output.AppendLine();
                            var itr = ((NameDict)data).GetEnumerator();
                            while (itr.MoveNext())
                            {
                                output.AppendFormat("{0}{1}: ",prefix, itr.Key);
                                string offset = (data.GetType() == typeof(IndexDict)) ? "\t" : String.Empty;
                                DumpDataAsText(itr.Value, output, prefix + offset);
                            }
                            //output.AppendLine();
                        }
                        else if (data.GetType() == typeof(IndexDict))
                        {
                            string offset = "\t";
                            output.AppendLine();
                            foreach (var itr in ((IndexDict)data))
                            {
                                output.AppendFormat("{0}[{1}]: ", prefix, itr.Key);
                                DumpDataAsText(itr.Value, output, prefix + offset);
                            }
                            //output.AppendLine();
                        }
                        else
                        {
                            output.Append(data);
                            output.AppendLine();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().ToString() + "\n" + ex.Message + "\n" + ex.StackTrace + "\n");
            }
            /*bool needindex = false;
            UInt32 index = 0;
            // dump data
            for (var itr = data.First; itr != null; itr = itr.Next)
            {
                if (itr.Next != null)
                {
                    if (needindex)
                    {
                        if (itr.Next.Value.name != itr.Value.name)
                            needindex = false; 
                    }
                    else
                    {
                        if (itr.Next.Value.name == itr.Value.name)
                        {
                            needindex = true;
                            index = 0;
                        }
                    }
                }
                var d = itr.Value;
                string suffix = "";
                if (needindex)
                {
                    suffix = "[" + index + "]";
                    ++index;
                }
                if (d.item is LinkedList<PacketData>)
                {
                    if (((LinkedList<PacketData>)d.item).Count != 0)
                    {
                        output.AppendLine(prefix + d.name + suffix + ":");
                        DumpData(packet, (LinkedList<PacketData>)d.item, output, prefix + "  ");
                    }
                }
                else
                    output.AppendLine(prefix + d.name + suffix + ": " + d.item);
            }*/
        }

        public static void Parse(Packet packet, bool checkLength = true)
        {
            var opcode = packet.Opcode;

            if (opcode == 0)
                return;

            Action<Packet> handler;
            if (Handlers.TryGetValue(opcode, out handler))
            {
                try
                {
                    handler(packet);

                    if (!checkLength || packet.Position == packet.Length)
                        packet.Status = ParsedStatus.Success;
                    else
                    {
                        var pos = packet.Position;
                        var len = packet.Length;
                        packet.ErrorMessage = String.Format("Packet not fully read! Current position is {0}, length is {1}, and diff is {2}.", pos, len, len - pos);

                        packet.Status = ParsedStatus.WithErrors;
                    }
                }
                catch (Exception ex)
                {
                    packet.ErrorMessage = ex.GetType().ToString() + "\n" + ex.Message + "\n" + ex.StackTrace;

                    packet.Status = ParsedStatus.WithErrors;
                }
            }
            else
            {
                packet.Status = ParsedStatus.NotParsed;
            }

            var data = packet.Status == ParsedStatus.Success ? Opcodes.GetOpcodeName(packet.Opcode) : packet.Status.ToString();
            packet.AddSniffData(StoreNameType.Opcode, packet.Opcode, data);
        }
    }
}
