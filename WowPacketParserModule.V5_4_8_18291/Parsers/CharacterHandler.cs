using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CharacterHandler
    {
        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            var guid = new byte[8];

            packet.StartBitStream(guid, 4, 6, 7, 5, 2, 3, 0, 1);

            var count = packet.ReadBits("Count", 21);

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);

            for (var i = 0; i < count; i++)
            {
                packet.ReadByteE<PowerType>("Power type"); // Actually powertype for class
                packet.ReadInt32("Value");
            }

            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.ReadBits("Count", 21);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                hasWeekCount[i] = packet.ReadBit();     // +28
                flags[i] = packet.ReadBits(5);          // +32
                hasWeekCap[i] = packet.ReadBit();       // +20
                hasSeasonTotal[i] = packet.ReadBit();   // +12
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i); // +32

                if (hasSeasonTotal[i])
                    packet.ReadUInt32("Season total earned", i);    // +12

                packet.ReadUInt32("Currency id", i);    // +5

                if (hasWeekCount[i])
                    packet.ReadUInt32("Weekly count", i);    // +28

                packet.ReadUInt32("Currency count", i);    // +4

                if (hasWeekCap[i])
                    packet.ReadUInt32("Weekly cap", i);    // +20
            }
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];
            var hasBaseXP = !packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            packet.ReadBit("Unk Bit");
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var hasGroupRate = !packet.ReadBit();

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadByte("XP type");

            if (hasGroupRate)
                packet.ReadSingle("Group rate");

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 6);

            packet.ReadUInt32("Total XP");

            if (hasBaseXP)
                packet.ReadUInt32("Base XP");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
        }
    }
}
