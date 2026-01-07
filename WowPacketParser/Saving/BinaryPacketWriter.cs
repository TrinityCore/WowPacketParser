using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;

namespace WowPacketParser.Saving
{
    public static class BinaryPacketWriter
    {
        public static void Write(string fileName, FileMode fileMode, IEnumerable<Packet> packets)
        {
            using var fileStream = new FileStream(fileName, fileMode, FileAccess.Write, FileShare.None);
            using var writer = new BinaryWriter(fileStream, Encoding.ASCII);

            writer.Write(['P', 'K', 'T']);
            writer.Write((ushort)BinaryPacketReader.PktVersion.V3_1);
            writer.Write((byte)0x15);                                                   // sniffer id (pretend to be ymir)
            writer.Write(ClientVersion.BuildInt);
            writer.Write(Encoding.ASCII.GetBytes(ClientLocale.ClientLocaleString));
            writer.Write(new byte[40]);                                                 // session key
            writer.Write((uint)0);                                                      // dump start timestamp
            writer.Write((uint)0);                                                      // dump start TickCount

            uint optionalHeaderLength = 2;
            writer.Write(optionalHeaderLength);                                         // optional header length
            writer.Write((ushort)0x0100);                                               // sniffer version

            foreach (var packet in packets)
            {
                writer.Write(packet.Direction == Direction.ClientToServer ? 0x47534d43u : 0x47534d53u);
                writer.Write(packet.ConnectionIndex);
                writer.Write((uint)0);                                                          // packet TickCount
                writer.Write((uint)8);                                                          // size of optional data
                writer.Write((uint)packet.Length + 4);                                          // size of packet data
                writer.Write((double)((DateTimeOffset)packet.Time).ToUnixTimeMilliseconds());   // optional data
                writer.Write(packet.Opcode);                                                    // opcode
                writer.Write(packet.GetStream(0));                                  // data
            }
        }
    }
}
