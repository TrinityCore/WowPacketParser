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
    public static class TextBuilder
    {
        static Packet lastPacket;
        static string cache;

        public static string Build(Packet packet, bool withSubPackets = false)
        {
            if (packet == lastPacket)
                return cache;

            StringBuilder output = DumpDataAsText(packet, withSubPackets);
            cache = output.ToString();
            lastPacket = packet;
            return cache;
        }

        public static StringBuilder DumpDataAsText(Packet mainPacket, bool withSubPackets)
        {
            StringBuilder output = new StringBuilder();
            var iter = mainPacket.GetTreeEnumerator();
            bool moveOn = iter.MoveNext();
            StringBuilder align = new StringBuilder(10);
            while (moveOn)
            {
                foreach (var val in iter.CurrentClosedNodes)
                {
                    if (val.type == typeof(Packet))
                    {
                        var pac = val.obj as Packet;
                        // errors
                        switch (pac.Status)
                        {
                            case ParsedStatus.Success:
                                break;
                            case ParsedStatus.WithErrors:
                                output.Append(align);
                                output.AppendLine(pac.ErrorMessage);
                                break;
                            case ParsedStatus.NotParsed:
                                output.Append(align);
                                output.AppendLine("Opcode not parsed");
                                output.Append(align);
                                output.AppendLine(pac.ToHex());
                                break;
                        }
                    }
                    else if (val.type == typeof(NamedTreeNode))
                    {
                        align.Remove(align.Length - 1, 1);
                    }
                }
                var t = iter.Type;
                var data = iter.Current;

                switch (Type.GetTypeCode(t))
                {
                    case TypeCode.Single:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            byte[] bytes = BitConverter.GetBytes((Single)data);
                            output.Append(" (0x");
                            output.Append(BitConverter.ToString(bytes));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.Double:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            byte[] bytes = BitConverter.GetBytes((Double)data);
                            output.Append(" (0x");
                            output.Append(BitConverter.ToString(bytes));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.Byte:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((Byte)data).ToString("X2"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.SByte:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((SByte)data).ToString("X2"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.Int16:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((Int16)data).ToString("X4"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.UInt16:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((UInt16)data).ToString("X4"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.UInt32:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((UInt32)data).ToString("X8"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.Int32:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((Int32)data).ToString("X8"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.UInt64:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((UInt64)data).ToString("X16"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.Int64:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((Int64)data).ToString("X16"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    case TypeCode.DateTime:
                        output.Append(align);
                        output.Append(iter.Name);
                        output.Append(": ");
                        output.Append(data);
                        if (Settings.DebugReads)
                        {
                            output.Append(" (0x");
                            output.Append(((DateTime)data).ToString("X4"));
                            output.AppendLine(")");
                        }
                        else
                        {
                            output.AppendLine();
                        }
                        break;
                    default:
                        //else if (data.GetType() == typeof(enum))
                        //{
                        //     Writer.WriteLine("{0}{1}: {2} ({3}){4}", GetIndexString(values), name, data.Value, data.Key, (Settings.DebugReads ? " (0x" + data.Key.ToString("X4") + ")" : String.Empty));
                        //}
                        if (t == typeof(Packet))
                        {
                            Packet packet = data as Packet;
                            if (packet.SubPacket && !withSubPackets)
                            {
                                moveOn = iter.MoveOver();
                                continue;
                            }

                            output.Append(align);
                            output.Append(packet.Direction);
                            output.Append(": ");
                            output.Append(Opcodes.GetOpcodeName(packet.Opcode));
                            output.Append(" (0x");
                            output.Append(packet.Opcode.ToString("X4"));
                            output.Append(") Length: ");
                            output.Append(packet.Length);
                            output.Append(" Time: ");
                            output.Append(packet.Time.ToString("MM/dd/yyyy HH:mm:ss.fff"));
                            output.Append(" Number: ");
                            output.Append(packet.Number);
                            if (packet.SubPacket)
                            {
                                output.Append(" (subpacket of packet: opcode ");
                                output.Append(Opcodes.GetOpcodeName(packet.ParentOpcode));
                                output.Append(" (0x");
                                output.Append(packet.ParentOpcode.ToString("X4"));
                                output.Append(") )");
                            }
                            output.AppendLine();
                        }
                        else if (t == typeof(NamedTreeNode))
                        {
                            if (iter.Index != null)
                            {
                                output.Append(align);
                                output.Append("[");
                                output.Append(iter.Index);
                                output.AppendLine("]:");
                            }
                            else
                            {
                                output.Append(align);
                                output.Append(iter.Name);
                                output.AppendLine(":");
                            }
                            align.Append("\t");
                        }
                        else if (t == typeof(IndexedTreeNode))
                        {
                            output.Append(align);
                            output.Append(iter.Name);
                            output.AppendLine(":");
                        }
                        else
                        {
                            output.Append(align);
                            output.Append(iter.Name);
                            output.Append(": ");
                            output.Append(data);
                            output.AppendLine();
                        }
                        break;
                }
                moveOn = iter.MoveNext();
            }
            return output;
        }
    }
}
