using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_2_17658.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.SMSG_SET_TIME_ZONE_INFORMATION)]
        public static void HandleSetTimeZoneInformation(Packet packet)
        {
            var len1 = packet.Translator.ReadBits(7);
            var len2 = packet.Translator.ReadBits(7);
            packet.Translator.ReadWoWString("Server Location", len2);
            packet.Translator.ReadWoWString("Server Location", len1);
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 2, 3, 5, 6, 4, 1, 7);
            packet.Translator.ParseBitStream(guid, 2, 0, 3, 7, 4, 5, 6, 1);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES)]
        public static void MultiplePackets(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 24);
            var lengths = new int[count];
            for (var i = 0; i < count; ++i)
                lengths[i] = (int)packet.Translator.ReadBits(5);

            for (var i = 0; i < count; ++i)
                packet.Translator.ReadWoWString("Addon", lengths[i], i);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var unk = packet.Translator.ReadBit("Unk Bit"); // Type
            var grade = packet.Translator.ReadSingle("Grade");
            var state = packet.Translator.ReadInt32E<WeatherState>("State");

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.Translator.ReadEntry("Area Trigger Id");
            packet.Translator.ReadBit("Unk bit1");
            packet.Translator.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            var sound = packet.Translator.ReadUInt32("Sound Id");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.Translator.ReadTime("Last Weekly Reset");
            packet.Translator.ReadInt32("Instance Difficulty ID");
            packet.Translator.ReadByte("Byte18");

            var bit30 = packet.Translator.ReadBit();
            var bit14 = packet.Translator.ReadBit();
            var bit20 = packet.Translator.ReadBit();
            var bit38 = packet.Translator.ReadBit();

            if (bit38)
                packet.Translator.ReadInt32("Int34");

            if (bit14)
                packet.Translator.ReadInt32("Int10");

            if (bit30)
                packet.Translator.ReadInt32("Int2C");

            if (bit20)
                packet.Translator.ReadInt32("Int1C");
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Unk Int8");
                packet.Translator.ReadInt32("Unk Int32");
            }
        }
    }
}
