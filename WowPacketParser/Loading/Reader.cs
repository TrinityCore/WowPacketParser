using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public static class Reader
    {
        public static IEnumerable<Packet> Read(string file, string filters, string ignoreFilters, int packetNumberLow, int packetNumberHigh, int packetsToRead)
        {
            IEnumerable<Packet> packets = null;

            try
            {
                var packetNum = 0;
                var packetList = new List<Packet>();
                var bin = new BinaryReader(new FileStream(file, FileMode.Open));
                var appliedFilters = filters.Split(',');
                var appliedIgnoreFilters = ignoreFilters.Split(',');
                var packetsRead = 0;

                while (bin.BaseStream.Position != bin.BaseStream.Length)
                {
                    var opcode = (Opcode)bin.ReadInt32();
                    var length = bin.ReadInt32();
                    var time = Utilities.GetDateTimeFromUnixTime(bin.ReadInt32());
                    var direction = (Direction)bin.ReadChar();
                    var data = bin.ReadBytes(length);
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
                            else
                            {
                                var opcodeString = "0x" + ((int)opcode).ToString("X4");
                                if (opcodeString.Contains(opc))
                                {
                                    add = true;
                                    break;
                                }
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
                            else
                            {
                                var opcodeString = "0x" + ((int)opcode).ToString("X4");
                                if (opcodeString.Contains(opc))
                                {
                                    add = false;
                                    break;
                                }
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
            catch (Exception) { }

            return packets;
        }
    }
}
