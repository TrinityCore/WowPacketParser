using System;
using System.IO;
using System.Text;
using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class Profile
    {
        [BattlenetParser(ProfileClientCommand.ReadRequest)]
        public static void HandleReadRequest(BattlenetPacket packet)
        {
            packet.ReadSkip(21);
            packet.Read<ulong>("Id", 0, 64, "RecordAddress");
            packet.Read<uint>("Label", 0, 32, "RecordAddress");
            packet.Read<uint>("RequestId", 0, 32);
            if (packet.ReadBoolean())
            {
                var profiles = packet.Read<int>(0, 4);
                for (var i = 0; i < profiles; ++i)
                    packet.Read<ulong>("ProfileId", 0, 64, "Specification", "Reader");
            }

            var selection = packet.Read<int>(0, 3);
            switch (selection)
            {
                case 0:
                    packet.ReadByteArray("PrefixPath", 0, 6, "All");
                    break;
                case 1:
                    packet.ReadByteArray("PrefixPath", 0, 6, "Slice");
                    if (packet.ReadBoolean())
                        packet.Read<ulong>("SliceStart", long.MinValue, 64, "Slice");
                    if (packet.ReadBoolean())
                        packet.Read<ulong>("SliceEnd", long.MinValue, 64, "Slice");
                    if (packet.ReadBoolean())
                        packet.Read<uint>("RowLimit", 0, 32, "Slice");
                    if (packet.ReadBoolean())
                        packet.ReadBoolean("SingleDepth", "Slice");
                    break;
                case 2:
                    packet.ReadByteArray("PrefixPath", 0, 6, "Random");
                    var indices = packet.Read<int>(0, 7);
                    for (var i = 0; i < indices; ++i)
                        packet.Read<ulong>("Index", 0, 64, "Random", "Indices");
                    break;
                case 3:
                    var paths = packet.Read<int>(0, 5);
                    for (var i = 0; i < paths; ++i)
                        packet.ReadByteArray("PrefixPath", 0, 6, "MultiPath", "Paths");
                    break;
                case 4:
                    packet.ReadByteArray("PrefixPath", 0, 6, "Range");
                    if (packet.ReadBoolean())
                        packet.Read<ulong>("RangeStart", long.MinValue, 64, "Range");
                    if (packet.ReadBoolean())
                        packet.Read<ulong>("RangeEnd", long.MinValue, 64, "Range");
                    if (packet.ReadBoolean())
                        packet.Read<uint>("RowLimit", 0, 32, "Range");
                    if (packet.ReadBoolean())
                        packet.ReadBoolean("SingleDepth", "Range");
                    break;
            }
        }

        private static long UnpackInt(BinaryReader reader)
        {
            var firstByte = reader.ReadByte();
            var lengthOfLength = 1;
            for (var b = firstByte; (b & 0x80) != 0; b >>= 1)
                ++lengthOfLength;

            if (lengthOfLength != 9)
                firstByte &= (byte)(0xFF >> lengthOfLength);

            long length = firstByte;
            if (lengthOfLength > 1)
            {
                do
                {
                    length <<= 8;
                    length |= reader.ReadByte();
                } while (lengthOfLength > 0);
            }

            return length;
        }

        [BattlenetParser(ProfileServerCommand.ReadResponse)]
        public static void HandleReadResponse(BattlenetPacket packet)
        {
            packet.Read<uint>("RequestId", 0, 32);
            var type = packet.Read<int>(0, 2);
            switch (type)
            {
                case 0:
                    packet.Read<uint>("NumPackets", 0, 32, "Start");
                    packet.Read<uint>("Type", 0, 32, "Start");
                    break;
                case 1:
                    var raw = packet.ReadBytes(packet.Read<int>(0, 14));
                    using (var reader = new BinaryReader(new MemoryStream(raw)))
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            var pathLength = UnpackInt(reader);
                            packet.Stream.AddValue("Path", Convert.ToHexString(reader.ReadBytes((int)pathLength)));
                            var fieldType = reader.ReadByte();
                            switch (fieldType)
                            {
                                case 1: //int
                                    var encodedInt = UnpackInt(reader);
                                    var decodedInt = 0L;
                                    var p1 = (encodedInt & 0xFFFFFFFF) >> 1;
                                    var p2 = ((encodedInt >> 32) & 0xFFFFFFFF) >> 1;
                                    if ((encodedInt & 1) != 0)
                                        decodedInt = ~p1 | (~p2 << 32);
                                    else
                                        decodedInt = p1 | (p2 << 32);
                                    packet.Stream.AddValue("Int", decodedInt);
                                    break;
                                case 2:
                                    packet.Stream.AddValue("Float", reader.ReadDouble());
                                    break;
                                case 3:
                                    var binaryLength = UnpackInt(reader);
                                    packet.Stream.AddValue("Binary", Convert.ToHexString(reader.ReadBytes((int)binaryLength)));
                                    break;
                                case 4:
                                    var stringLength = UnpackInt(reader);
                                    packet.Stream.AddValue("String", Encoding.UTF8.GetString(reader.ReadBytes((int)stringLength)));
                                    break;
                                case 5: //pointer
                                    packet.Stream.AddValue("Unk", UnpackInt(reader), "Pointer");
                                    packet.Stream.AddValue("Label", UnpackInt(reader), "Pointer");
                                    packet.Stream.AddValue("Id", UnpackInt(reader), "Pointer");
                                    break;
                                case 6: //cache handle
                                    var handleLength = UnpackInt(reader);
                                    if (handleLength == 40)
                                        packet.Stream.AddValue("CacheHandle", Convert.ToHexString(reader.ReadBytes(40)));
                                    break;
                            }
                        }
                    }
                    break;
                case 2:
                    packet.Read<ushort>("Failure", 0, 16);
                    break;
            }
        }

        [BattlenetParser(ProfileServerCommand.SettingsAvailable)]
        public static void HandleSettingsAvailable(BattlenetPacket packet)
        {
            packet.ReadSkip(5);
            packet.ReadByteArray("Path", 0, 6);
            packet.ReadSkip(21);
            packet.Read<ulong>("Id", 0, 64, "Address");
            packet.Read<uint>("Label", 0, 32, "Address");
            packet.Read<SettingsType>("Type", 1, 2);
        }
    }
}
