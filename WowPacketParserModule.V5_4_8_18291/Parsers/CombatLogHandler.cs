using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CombatLogHandler
    {
        [Parser(Opcode.SMSG_SPELLNONMELEEDAMAGELOG)]
        public static void HandleSpellNonMeleeDmgLog(Packet packet)
        {
            var guidA = new byte[8];
            var guid12 = new byte[8];

            var bit1C = false;
            var bit20 = false;
            var bit24 = false;
            var bit28 = false;
            var bit2C = false;
            var bit30 = false;
            var bit34 = false;
            var bit38 = false;
            var bit3C = false;
            var bit40 = false;

            guid12[2] = packet.ReadBit();
            guidA[7] = packet.ReadBit();
            guidA[6] = packet.ReadBit();
            guidA[1] = packet.ReadBit();
            guidA[5] = packet.ReadBit();
            var bit5C = packet.ReadBit();
            guidA[0] = packet.ReadBit();
            guid12[0] = packet.ReadBit();
            guid12[7] = packet.ReadBit();
            guidA[3] = packet.ReadBit();
            guid12[6] = packet.ReadBit();
            var bit14 = packet.ReadBit();
            var hasPowerData = packet.ReadBit();
            guid12[1] = packet.ReadBit();
            var bit44 = packet.ReadBit();
            if (bit44)
            {
                bit3C = !packet.ReadBit();
                bit28 = !packet.ReadBit();
                bit24 = !packet.ReadBit();
                bit30 = !packet.ReadBit();
                bit2C = !packet.ReadBit();
                bit20 = !packet.ReadBit();
                bit1C = !packet.ReadBit();
                bit34 = !packet.ReadBit();
                bit38 = !packet.ReadBit();
                bit40 = !packet.ReadBit();
            }

            guid12[5] = packet.ReadBit();
            guidA[2] = packet.ReadBit();
            guidA[4] = packet.ReadBit();
            guid12[3] = packet.ReadBit();

            var bits70 = 0u;
            if (hasPowerData)
                bits70 = packet.ReadBits(21);

            guid12[4] = packet.ReadBit();
            packet.ReadInt32("Int8C");
            if (bit44)
            {
                if (bit30)
                    packet.ReadSingle("Float30");
                if (bit20)
                    packet.ReadSingle("Float20");
                if (bit3C)
                    packet.ReadSingle("Float3C");
                if (bit2C)
                    packet.ReadSingle("Float2C");
                if (bit34)
                    packet.ReadSingle("Float34");
                if (bit24)
                    packet.ReadSingle("Float24");
                if (bit1C)
                    packet.ReadSingle("Float1C");
                if (bit40)
                    packet.ReadSingle("Float40");
                if (bit28)
                    packet.ReadSingle("Float28");
                if (bit38)
                    packet.ReadSingle("Float38");
            }

            packet.ReadXORByte(guidA, 1);
            packet.ReadInt32("Overkill");
            packet.ReadXORByte(guid12, 3);
            packet.ReadXORByte(guidA, 0);
            packet.ReadXORByte(guid12, 6);
            packet.ReadXORByte(guid12, 4);
            packet.ReadXORByte(guidA, 7);
            packet.ReadInt32("Resist");
            packet.ReadInt32("Absorb");
            if (hasPowerData)
            {
                packet.ReadInt32("Int64");
                for (var i = 0; i < bits70; ++i)
                {
                    packet.ReadInt32("IntED", i);
                    packet.ReadInt32("IntED", i);
                }

                packet.ReadInt32("Int6C");
                packet.ReadInt32("Int68");
            }

            packet.ReadXORByte(guidA, 5);
            packet.ReadXORByte(guid12, 5);
            packet.ReadXORByte(guidA, 3);
            packet.ReadXORByte(guidA, 2);
            packet.ReadXORByte(guid12, 2);
            packet.ReadXORByte(guidA, 6);
            packet.ReadXORByte(guid12, 0);
            packet.ReadXORByte(guidA, 4);
            packet.ReadInt32("Damage");
            packet.ReadByte("SchoolMask");
            packet.ReadXORByte(guid12, 7);
            packet.ReadEnum<AttackerStateFlags>("Attacker State Flags", TypeCode.Int32);
            packet.ReadXORByte(guid12, 1);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Spell, "Spell ID");

            packet.WriteGuid("GuidA", guidA);
            packet.WriteGuid("Guid12", guid12);
        }        
    }
}
