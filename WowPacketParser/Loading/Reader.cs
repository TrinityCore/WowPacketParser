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
        public static IEnumerable<Packet> Read(string file, string filters, string ignoreFilters, int packetsToRead)
        {
            IEnumerable<Packet> packets = null;

            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            foreach (var type in types)
            {
                if (!type.IsSealed)
                    continue;

                if (!type.IsPublic)
                    continue;

                try
                {
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

                        var packet = new Packet(data, opcode, time, direction);

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
                                bin.BaseStream.Position = bin.BaseStream.Length;
                        }
                    }
                    packets = packetList;
                }
                catch (Exception) {}
            }

            return packets;
        }
    }
}
