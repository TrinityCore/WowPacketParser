using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        public static IEnumerable<Packet> Read(string fileName, string filters, string ignoreFilters, int packetNumberLow, int packetNumberHigh, int packetsToRead)
        {
            IEnumerable<Packet> packets = null;

            var packetNum = 0;
            var packetList = new List<Packet>();
            var appliedFilters = filters.Split(',');
            var appliedIgnoreFilters = ignoreFilters.Split(',');
            var packetsRead = 0;

            using (var bin = new BinaryReader(new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read), Encoding.ASCII))
            {
                while (bin.BaseStream.Position != bin.BaseStream.Length)
                {
                    Opcode opcode = 0;
                    var length = 0;
                    DateTime time = DateTime.Now;
                    Direction direction = 0;
                    byte[] data = {};

                    if (Path.GetExtension(fileName) == ".bin")
                    {
                        opcode = (Opcode)bin.ReadInt32();
                        length = bin.ReadInt32();
                        time = Utilities.GetDateTimeFromUnixTime(bin.ReadInt32());
                        direction = (Direction)bin.ReadChar();
                        data = bin.ReadBytes(length);
                    }
                    else if (Path.GetExtension(fileName) == ".pkt")
                    {
                        opcode = (Opcode)bin.ReadUInt16();
                        length = bin.ReadInt32();
                        direction = (Direction)bin.ReadByte();
                        time = Utilities.GetDateTimeFromUnixTime((int)bin.ReadInt64());
                        data = bin.ReadBytes(length);
                    }
                    else
                        throw new IOException("Invalid file type");

                    var num = packetNum++;

                    if (num < packetNumberLow)
                        continue;

                    var packet = new Packet(data, opcode, time, direction, num);

                    var add = true;
                    if (!string.IsNullOrEmpty(filters))
                    {
                        add = false;

                        foreach (var opc in appliedFilters)
                        {
                            if (opcode.ToString().Contains(opc))
                            {
                                add = true;
                                break;
                            }
                        }
                    }

                    if (add && !string.IsNullOrEmpty(ignoreFilters))
                    {
                        foreach (var opc in appliedIgnoreFilters)
                        {
                            if (opcode.ToString().Contains(opc))
                            {
                                add = false;
                                break;
                            }
                        }
                    }

                    if (add)
                    {
                        packetList.Add(packet);
                        if (packetsToRead > 0 && ++packetsRead == packetsToRead)
                            break;
                    }

                    if (packetNumberHigh > 0 && packetNum > packetNumberHigh)
                        break;
                }
                packets = packetList;
            }

            return packets;
        }
    }
}
