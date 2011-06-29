using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading.Loaders
{
    [Loader("kszack")]
    public sealed class KSnifferZackLoader : Loader
    {
        private static readonly Regex HeaderRegex = new Regex("^(?<stoc>{SERVER}|{CLIENT})\\sPacket:\\s\\((?<opcode>0x[0-9a-fA-F]*)\\)\\s(?<name>[A-Z0-9_]*)\\sPacketSize = (?<size>[0-9]*)\\sTimeStamp = (?<time>[0-9]*)");

        public KSnifferZackLoader(string file)
            : base(file)
        {
        }

        public override IEnumerable<Packet> ParseFile()
        {
            var reader = new StreamReader(new FileStream(FileToParse, FileMode.Open), true);
            var packets = new List<Packet>();

            try
            {
                reader.ReadLine();

                while (true)
                {
                    var line = reader.ReadLine();
                    if (line == null || !HeaderRegex.IsMatch(line))
                        break;

                    var match = HeaderRegex.Match(line);

                    var opcode = (Opcode)Convert.ToInt32(match.Groups["opcode"].Value, 16);
                    var length = Convert.ToInt32(match.Groups["size"].Value, 10);
                    var direction = (match.Groups["stoc"].Value == "{CLIENT}" ?
                        Direction.ClientToServer : Direction.ServerToClient);
                    var data = reader.ReadLine();

                    var tokens = data.Split(' ');
                    var arr = new byte[length];
                    for (var i = 0; i < length; i++)
                        arr[i] = byte.Parse(tokens[i], NumberStyles.HexNumber, null);

                    var packet = new Packet(arr, opcode, DateTime.Now, direction);
                    packets.Add(packet);

                    reader.ReadLine();
                }
            }
            catch (EndOfStreamException)
            {
            }

            return packets;
        }
    }
}
