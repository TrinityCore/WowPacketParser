using System;
using System.Text;
using System.Collections.Generic;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using PacketParser.Misc;
using PacketParser.DataStructures;

namespace PacketParser.Processing
{
    public class TextBuilder : IPacketProcessor
    {
        public bool LoadOnDepend { get { return true; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return ProcessPacket; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return ProcessedPacket; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return ProcessData; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return ProcessedDataNode; } }

        private StringBuilder output;
        private StringBuilder align;
        private Stack<Tuple<StringBuilder, StringBuilder>> packets;

        public string LastPacket;
        public bool WithDebugValues = ParserSettings.ReadDebugValues;

        public bool Init(PacketFileProcessor p) 
        {
            packets = new Stack<Tuple<StringBuilder, StringBuilder>>();
            return true;
        }

        public void ProcessPacket(Packet packet) 
        {
            if (packet.SubPacket)
                packets.Push(new Tuple<StringBuilder, StringBuilder>(output, align));
            output = new StringBuilder();
            align = new StringBuilder(10);
        }

        public void ProcessedPacket(Packet packet) 
        {
            LastPacket = output.ToString();
            if (packet.SubPacket)
            {
                var t = packets.Pop();
                output = t.Item1;
                align = t.Item2;
            }
            else
            {
                output = null;
                align = null;
            }
        }

        public void Finish()
        {
            packets = null;
            LastPacket = null;
        }

        public void ProcessedDataNode(string name, Object data, Type t)
        {
            if (t == typeof(Packet))
            {
                var pac = data as Packet;
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
            else if (t == typeof(NamedTreeNode))
            {
                align.Remove(align.Length - 1, 1);
            }
        }

        public void ProcessData(string name, int? index, Object data, Type t) 
        {
            if (!(data is ITextOutputDisabled))
            {
                switch (Type.GetTypeCode(t))
                {
                    case TypeCode.Single:
                        output.Append(align);
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        output.Append(name);
                        output.Append(": ");
                        output.Append(data);
                        if (WithDebugValues)
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
                        /*if (data.GetType() == typeof(StoreEnum))
                        {
                            output.Append(align);
                            output.Append(name);
                            output.Append(": ");
                            output.Append(data);
                            output.Append(" (");
                            output.Append(((StoreEnum)data).rawVal);
                            if (WithDebugValues)
                            {
                                output.Append(" (0x");
                                output.Append(((StoreEnum)data).rawVal.ToString("X4"));
                                output.Append(")");
                            }
                            output.AppendLine(")");
                        }*/
                        if (t == typeof(Packet))
                        {
                        }
                        else if (t == typeof(NamedTreeNode))
                        {
                            if (index != null)
                            {
                                output.Append(align);
                                output.Append("[");
                                output.Append((int)index);
                                output.AppendLine("]:");
                            }
                            else
                            {
                                output.Append(align);
                                output.Append(name);
                                output.AppendLine(":");
                            }
                            align.Append("\t");
                        }
                        else if (t == typeof(IndexedTreeNode))
                        {
                            output.Append(align);
                            output.Append(name);
                            output.AppendLine(":");
                        }
                        else
                        {
                            output.Append(align);
                            output.Append(name);
                            output.Append(": ");
                            output.Append(data);
                            output.AppendLine();
                        }
                        break;
                }
            }
        }
    }
}
