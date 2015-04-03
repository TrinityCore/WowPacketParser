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
            var len1 = packet.ReadBits(7);
            var len2 = packet.ReadBits(7);
            packet.ReadWoWString("Server Location", len2);
            packet.ReadWoWString("Server Location", len1);
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(0, 2, 3, 5, 6, 4, 1, 7);
            packet.ParseBitStream(guid, 2, 0, 3, 7, 4, 5, 6, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_ADDON_REGISTERED_PREFIXES)]
        public static void MultiplePackets(Packet packet)
        {
            var count = packet.ReadBits("Count", 24);
            var lengths = new int[count];
            for (var i = 0; i < count; ++i)
                lengths[i] = (int)packet.ReadBits(5);

            for (var i = 0; i < count; ++i)
                packet.ReadWoWString("Addon", lengths[i], i);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var unk = packet.ReadBit("Unk Bit"); // Type
            var grade = packet.ReadSingle("Grade");
            var state = packet.ReadInt32E<WeatherState>("State");

            Storage.WeatherUpdates.Add(new WeatherUpdate
            {
                MapId = CoreParsers.MovementHandler.CurrentMapId,
                ZoneId = 0, // fixme
                State = state,
                Grade = grade,
                Unk = unk
            }, packet.TimeSpan);
        }

        [Parser(Opcode.CMSG_QUERY_WORLD_COUNTDOWN_TIMER)]
        public static void HandleQueryWorldCountdownTimer(Packet packet)
        {
            packet.ReadInt32("Int10");
        }

        [Parser(Opcode.CMSG_AREA_TRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            var entry = packet.ReadEntry("Area Trigger Id");
            packet.ReadBit("Unk bit1");
            packet.ReadBit("Unk bit2");

            packet.AddSniffData(StoreNameType.AreaTrigger, entry.Key, "AREATRIGGER");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = new byte[8];

            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            var sound = packet.ReadUInt32("Sound Id");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);

            Storage.Sounds.Add(sound, packet.TimeSpan);
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            packet.ReadTime("Last Weekly Reset");
            packet.ReadInt32("Instance Difficulty ID");
            packet.ReadByte("Byte18");

            var bit30 = packet.ReadBit();
            var bit14 = packet.ReadBit();
            var bit20 = packet.ReadBit();
            var bit38 = packet.ReadBit();

            if (bit38)
                packet.ReadInt32("Int34");

            if (bit14)
                packet.ReadInt32("Int10");

            if (bit30)
                packet.ReadInt32("Int2C");

            if (bit20)
                packet.ReadInt32("Int1C");
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadByte("Unk Int8");
                packet.ReadInt32("Unk Int32");
            }
        }
    }
}
