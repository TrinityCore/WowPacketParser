using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using System.Collections.Specialized;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Processing
{
    //using NameDict = Dictionary<string, Object>;
    //1using IndexDict = Dictionary<int, Dictionary<string, Object>>;
    using NameDict = OrderedDictionary;
    using IndexDict = Dictionary<int, OrderedDictionary>;

    public static class TextBuilder
    {
        static Packet lastPacket;
        static string cache;

        public static string Build(Packet packet)
        {
            if (packet == lastPacket)
                return cache;

            StringBuilder output = new StringBuilder();
            DumpDataAsText(packet, output, "");
            cache = output.ToString();
            lastPacket = packet;
            return cache;
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
                    if (t == typeof(Packet))
                    {
                        Packet packet = (Packet)data;
                        output.Append(String.Format("{0}: {1} (0x{2}) Length: {3} Time: {4} Number: {5}{6}",
                            packet.Direction, Opcodes.GetOpcodeName(packet.Opcode), packet.Opcode.ToString("X4"),
                            packet.Length, packet.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"),
                            packet.Number, (packet.SubPacket) ? String.Format(" (subpacket of packet: opcode {0} (0x{1}) )", Opcodes.GetOpcodeName(packet.ParentOpcode), packet.ParentOpcode) : String.Empty));

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
                            output.AppendFormat("{0}{1}: ", prefix, itr.Key);
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
    }
}
