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

        public static void DumpDataAsText(Object data, StringBuilder output, string prefix)
        {
            var t = data.GetType();
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Single:
                    if (Settings.DebugReads)
                    {
                        byte[] bytes = BitConverter.GetBytes((Single)data);
                        output.AppendFormat("{0} (0x{1})\n", data, BitConverter.ToString(bytes));
                    }
                    else
                        output.AppendLine(data.ToString());
                    break;
                case TypeCode.Double:
                    if (Settings.DebugReads)
                    {
                        byte[] bytes = BitConverter.GetBytes((Double)data);
                        output.AppendFormat("{0} (0x{1})\n", data, BitConverter.ToString(bytes));
                    }
                    else
                        output.AppendLine(data.ToString());
                    break;
                case TypeCode.Byte:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((Byte)data).ToString("X2") + ")" : String.Empty));
                    break;
                case TypeCode.SByte:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((SByte)data).ToString("X2") + ")" : String.Empty));
                    break;
                case TypeCode.Int16:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((Int16)data).ToString("X4") + ")" : String.Empty));
                    break;
                case TypeCode.UInt16:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((UInt16)data).ToString("X4") + ")" : String.Empty));
                    break;
                case TypeCode.UInt32:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((UInt32)data).ToString("X8") + ")" : String.Empty));
                    break;
                case TypeCode.Int32:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((Int32)data).ToString("X8") + ")" : String.Empty));
                    break;
                case TypeCode.UInt64:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((UInt64)data).ToString("X16") + ")" : String.Empty));
                    break;
                case TypeCode.Int64:
                    output.AppendFormat("{0}{1}\n", data, (Settings.DebugReads ? " (0x" + ((Int64)data).ToString("X16") + ")" : String.Empty));
                    break;
                case TypeCode.DateTime:
                    output.AppendFormat("{0}{1}\n", (DateTime)data, (Settings.DebugReads ? " (0x" + ((DateTime)data).ToString("X4") + ")" : String.Empty));
                    break;
                default:
                    //else if (data.GetType() == typeof(enum))
                    //{
                    //     Writer.WriteLine("{0}{1}: {2} ({3}){4}", GetIndexString(values), name, data.Value, data.Key, (Settings.DebugReads ? " (0x" + data.Key.ToString("X4") + ")" : String.Empty));
                    //}
                    if (t == typeof(Guid))
                    {
                        //if (WriteToFile)
                            //WriteToFile = Filters.CheckFilter((Guid)data);
                        output.AppendLine(data.ToString());
                    }
                    else if (t == typeof(StoreEntry))
                    {
                        var val = (StoreEntry)data;
                        //if (WriteToFile)
                            //WriteToFile = Filters.CheckFilter(val._type, val._data);
                        output.AppendLine(data.ToString());
                    }
                    else if (t == typeof(Packet))
                    {
                        Packet packet = (Packet)data;
                        output.Append(String.Format("{0}: {1} (0x{2}) Length: {3} Time: {4} Number: {5}{6}",
                            packet.Direction, Opcodes.GetOpcodeName(packet.Opcode), packet.Opcode.ToString("X4"),
                            packet.Length, packet.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                            packet.Number, (packet.Parent != null) ? String.Format(" (subpacket of packet: opcode {0} (0x{1}), number {2} )", Opcodes.GetOpcodeName(packet.Parent.Opcode), packet.Parent.Opcode, packet.Parent.Number)  : String.Empty));

                        DumpDataAsText(packet.GetData(), output, prefix);

                        // errors
                        switch (packet.Status)
                        {
                            case ParsedStatus.Success:
                                break;
                            case ParsedStatus.WithErrors:
                                output.AppendLine(packet.ErrorMessage);
                                break;
                            case ParsedStatus.NotParsed:
                                output.AppendLine("Opcode not parsed");
                                output.AppendLine(packet.ToHex());
                                break;
                        }
                    }
                    else if (t == typeof(NameDict))
                    {
                        output.AppendLine();
                        var itr = ((NameDict)data).GetEnumerator();
                        string offset = prefix + ((t == typeof(IndexDict)) ? "\t" : String.Empty);
                        while (itr.MoveNext())
                        {
                            output.AppendFormat("{0}{1}: ",prefix, itr.Key);
                            DumpDataAsText(itr.Value, output, offset);
                        }
                        //output.AppendLine();
                    }
                    else if (t == typeof(IndexDict))
                    {
                        string offset = prefix + "\t";
                        output.AppendLine();
                        foreach (var itr in ((IndexDict)data))
                        {
                            output.AppendFormat("{0}[{1}]: ", prefix, itr.Key);
                            DumpDataAsText(itr.Value, output, offset);
                        }
                        //output.AppendLine();
                    }
                    else
                    {
                        output.AppendLine(data.ToString());
                    }
                    break;
            }
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
