using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;
using WowPacketParser.Misc;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using System.Diagnostics;
using System.Globalization;
namespace WowPacketParser.Loading
{
    public class IzidorPacketReader : IPacketReader
    {
        public TextReader _tr;
        public IzidorPacketReader(string fileName)
        {
            _tr = new StreamReader(fileName);
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
                time = Utilities.GetDateTimeFromUnixTime(UInt32.Parse(data[2])); //data[2] is not a real unit timestamp, it's a tickcount or something
            byte direction = (byte)(data[4] == "StoC" ? 1 : 0);
            ushort opcode;
            // a hack - need to create a different way for getting versions
            if (ClientVersion.Build == ClientVersionBuild.Zero)
                ClientVersion.SetVersion(time);
            if (!UInt16.TryParse(data[6], out opcode))
            {
                Opcode opc;
                // sometimes opcode name is written directly..
                if (!Enum.TryParse<Opcode>(data[6], out opc))
                {
                    Trace.WriteLine("IzidorPacketReader: Opcode name {0} not found", data[6]);
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
            return ClientVersionBuild.Zero;
        }

        public void Dispose()
        {
            _tr.Dispose();
        }
    }
}
