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

            packet.Translator.StartBitStream(guid, 4, 6, 7, 5, 2, 3, 0, 1);

            var count = packet.Translator.ReadBits("Count", 21);

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadByteE<PowerType>("Power type"); // Actually powertype for class
                packet.Translator.ReadInt32("Value");
            }

            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SETUP_CURRENCY)]
        public static void HandleInitCurrency(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 21);
            if (count == 0)
                return;

            var hasWeekCount = new bool[count];
            var hasWeekCap = new bool[count];
            var hasSeasonTotal = new bool[count];
            var flags = new uint[count];

            for (var i = 0; i < count; ++i)
            {
                hasWeekCount[i] = packet.Translator.ReadBit();     // +28
                flags[i] = packet.Translator.ReadBits(5);          // +32
                hasWeekCap[i] = packet.Translator.ReadBit();       // +20
                hasSeasonTotal[i] = packet.Translator.ReadBit();   // +12
            }

            for (var i = 0; i < count; ++i)
            {
                packet.AddValue("Flags", flags[i], i); // +32

                if (hasSeasonTotal[i])
                    packet.Translator.ReadUInt32("Season total earned", i);    // +12

                packet.Translator.ReadUInt32("Currency id", i);    // +5

                if (hasWeekCount[i])
                    packet.Translator.ReadUInt32("Weekly count", i);    // +28

                packet.Translator.ReadUInt32("Currency count", i);    // +4

                if (hasWeekCap[i])
                    packet.Translator.ReadUInt32("Weekly cap", i);    // +20
            }
        }

        [Parser(Opcode.SMSG_LOG_XP_GAIN)]
        public static void HandleLogXPGain(Packet packet)
        {
            var guid = new byte[8];
            var hasBaseXP = !packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk Bit");
            guid[0] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var hasGroupRate = !packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadByte("XP type");

            if (hasGroupRate)
                packet.Translator.ReadSingle("Group rate");

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.ReadUInt32("Total XP");

            if (hasBaseXP)
                packet.Translator.ReadUInt32("Base XP");

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 5);

            packet.Translator.WriteGuid("Guid", guid);
        }
    }
}
