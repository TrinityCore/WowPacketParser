using System;
using System.IO;
using PacketParser.Misc;
using PacketParser.Enums;
using PacketParser.Enums.Version;
using System.Diagnostics;
using System.Globalization;
using PacketParser.DataStructures;

namespace PacketParser.Loading
{
    public class IzidorPacketReader : IPacketReader
    {
        public StreamReader _tr;
        public DateTime _baseDate;
        private ClientVersionBuild _build;

        public IzidorPacketReader(string fileName)
        {
            _tr = new StreamReader(fileName);
            _baseDate = File.GetLastWriteTimeUtc(fileName);

            DateTime lastWriteTimeUtc = File.GetLastWriteTimeUtc(fileName);
            _build = ClientVersion.GetVersion(lastWriteTimeUtc);
        }

        public bool CanRead()
        {
            return _tr.Peek() != -1;
        }

        public Packet Read(int number, string fileName)
        {
            string line = _tr.ReadLine();
            string[] data = line.Split('<', '>', '"');

            DateTime time;
            //24/04/2010 18:03:47
            string pattern = "dd/MM/yyyy HH:mm:ss";
            if (!DateTime.TryParseExact(data[2], pattern, null, DateTimeStyles.None, out time))
            {
                //data[2] is not a real unit timestamp, it's a tickcount or something
                time = _baseDate.AddMilliseconds(UInt32.Parse(data[2]));
            }
            byte direction = (byte)(data[4] == "StoC" ? 1 : 0);
            ushort opcode;
            if (!UInt16.TryParse(data[6], out opcode))
            {
                Opcode opc;
                // sometimes opcode name is written directly..
                if (!Enum.TryParse<Opcode>(data[6], out opc))
                {
                    Trace.WriteLine(String.Format("IzidorPacketReader: Opcode name {0} not found", data[6]));
                    // name not found in our storage - return
                    return null;
                }
                opcode = (ushort)Opcodes.GetOpcode(opc);
            }
            string directdata = data[8];
            byte[] byteData = ParseHex(directdata);
            return new Packet(byteData, opcode, time, (Direction)direction, number, fileName);
        }

        public static byte[] ParseHex(string hex)
        {
            int offset = hex.StartsWith("0x") ? 2 : 0;
            if ((hex.Length % 2) != 0)
            {
                throw new ArgumentException("Invalid length: " + hex.Length);
            }
            byte[] ret = new byte[(hex.Length - offset) / 2];

            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = (byte)((ParseNybble(hex[offset]) << 4)
                              | ParseNybble(hex[offset + 1]));
                offset += 2;
            }
            return ret;
        }

        static int ParseNybble(char c)
        {
            if (c >= '0' && c <= '9')
            {
                return c - '0';
            }
            if (c >= 'A' && c <= 'F')
            {
                return c - 'A' + 10;
            }
            if (c >= 'a' && c <= 'f')
            {
                return c - 'a' + 10;
            }
            throw new ArgumentException("Invalid hex digit: " + c);
        }


        public ClientVersionBuild GetBuild()
        {
            return _build;
        }

        public void Dispose()
        {
            _tr.Dispose();
        }

        public uint GetProgress()
        {
            if (_tr.BaseStream.Length != 0)
                return (uint)(_tr.BaseStream.Position*100 / _tr.BaseStream.Length);
            return 100;
        }
    }
}
