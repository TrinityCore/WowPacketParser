using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class BattlePetHandler
    {
        [Parser(Opcode.CMSG_BATTLE_PET_DELETE_PET)]
        public static void HandleBattlePetDeletePet(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 2, 4, 0, 7, 1);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 1, 5, 0, 7, 2, 3, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_MODIFY_NAME)]
        public static void HandleBattlePetModifyName(Packet packet)
        {
            var guid = new byte[8];
            guid[5] = packet.ReadBit(); // 93
            guid[7] = packet.ReadBit(); // 95
            guid[3] = packet.ReadBit(); // 91
            guid[0] = packet.ReadBit(); // 88
            guid[6] = packet.ReadBit(); // 94
            var len = packet.ReadBits("Len", 7);
            guid[2] = packet.ReadBit(); // 90
            guid[1] = packet.ReadBit(); // 89
            var hasDeclinedNames = packet.ReadBit("hasDeclNames"); // 421
            guid[4] = packet.ReadBit(); // 92

            var declinedNamesLen = new uint[5];

            if (hasDeclinedNames)
                for ( var i = 5; i > 0; i--)
                    declinedNamesLen[i-1] = packet.ReadBits(7);

            packet.ResetBitReader();

            packet.ParseBitStream(guid, 3, 0, 6, 1, 5, 2, 4, 7);
            packet.WriteGuid("Guid", guid);

            packet.ReadWoWString("Nick", len);

            if (hasDeclinedNames)
                for (var i = 5; i > 0; i--)
                    packet.ReadWoWString("declNames", declinedNamesLen[i - 1], i);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_NAME_QUERY)]
        public static void HandleBattlePetNameQuery(Packet packet)
        {
            var petName = new byte[8];
            var petGuid = new byte[8];

            petGuid[2] = packet.ReadBit(); // 26
            petName[6] = packet.ReadBit(); // 22
            petName[3] = packet.ReadBit(); // 19
            petGuid[3] = packet.ReadBit(); // 27
            petName[7] = packet.ReadBit(); // 23
            petGuid[4] = packet.ReadBit(); // 28
            petGuid[1] = packet.ReadBit(); // 25
            petGuid[0] = packet.ReadBit(); // 24
            petName[0] = packet.ReadBit(); // 16
            petGuid[7] = packet.ReadBit(); // 31
            petGuid[5] = packet.ReadBit(); // 29
            petGuid[6] = packet.ReadBit(); // 30
            petName[1] = packet.ReadBit(); // 17 
            petName[2] = packet.ReadBit(); // 18
            petName[5] = packet.ReadBit(); // 21
            petName[4] = packet.ReadBit(); // 20

            packet.ResetBitReader();

            packet.ReadXORByte(petGuid, 5);
            packet.ReadXORByte(petName, 1);
            packet.ReadXORByte(petGuid, 0);
            packet.ReadXORByte(petName, 4);
            packet.ReadXORByte(petGuid, 3);
            packet.ReadXORByte(petName, 3);
            packet.ReadXORByte(petGuid, 1);
            packet.ReadXORByte(petGuid, 6);
            packet.ReadXORByte(petName, 6);
            packet.ReadXORByte(petName, 0);
            packet.ReadXORByte(petName, 2);
            packet.ReadXORByte(petGuid, 7);
            packet.ReadXORByte(petGuid, 2);
            packet.ReadXORByte(petName, 7);
            packet.ReadXORByte(petGuid, 4);
            packet.ReadXORByte(petName, 5);

            packet.WriteGuid("petName", petName);
            packet.WriteGuid("petGuid", petGuid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_BATTLE_SLOT)]
        public static void HandleBattlePetSetBattleSlot(Packet packet)
        {
            packet.ReadByte("Slot");
            var guid = packet.StartBitStream(4, 6, 5, 7, 3, 1, 0, 2);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 1, 3, 5, 0, 7, 6, 4, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SET_FLAGS)]
        public static void HandleBattlePetSetFlags(Packet packet)
        {
            packet.ReadInt32("Flags");
            var guid = new byte[8];
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            packet.ReadBits("Mode", 2);
            guid[1] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 4, 0, 7, 3, 1, 6, 2, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_BATTLE_PET_SUMMON_COMPANION)]
        public static void HandleBattlePetSummonCompanion(Packet packet)
        {
            var guid = packet.StartBitStream(3, 2, 5, 0, 7, 1, 6, 4);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 6, 7, 3, 5, 0, 4, 1, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_DELETED)]
        public static void HandleBattlePetDeleted(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 7, 6, 1, 5, 2, 3);
            packet.ParseBitStream(guid, 6, 1, 7, 0, 4, 3, 5, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL)]
        public static void HandleBattlePetJournal(Packet packet)
        {
            var count16 = packet.ReadBits("count16", 19); // 16
            var guid = new byte[count16][];
            var unk148 = new bool[count16];
            var guid112 = new byte[count16][];
            var unk157 = new bool[count16];
            var unk40 = new bool[count16];
            var unk46 = new bool[count16];
            var len64 = new uint[count16];
            for (var i = 0; i < count16; i++)
            {
                guid[i] = new byte[8];
                unk46[i] = !packet.ReadBit("!unk46", i); // 46
                guid[i][3] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                len64[i] = packet.ReadBits("unk64", 7, i); // 64
                unk148[i] = packet.ReadBit("unk148", i); // 148
                if (unk148[i])
                    guid112[i] = packet.StartBitStream(4, 1, 6, 7, 0, 5, 2, 2);
                guid[i][0] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                packet.ReadBit("unk156", i); // 156
                guid[i][1] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                unk40[i] = !packet.ReadBit("!unk40", i); // 40
                guid[i][4] = packet.ReadBit();
                unk157[i] = !packet.ReadBit("!unk157", i); // 157
            }
            packet.ReadBit("unk50"); // 50
            var count32 = packet.ReadBits("count32", 25); // 32
            var guid36 = new byte[count32][];
            var unk44 = new bool[count32];
            var unk48 = new bool[count32];
            for (var i = 0; i < count32; i++)
            {
                packet.ReadBit("unk49", i); // 49
                unk44[i] = !packet.ReadBit("!unk44", i); // 44
                unk48[i] = !packet.ReadBit("!unk48", i); // 48
                packet.ReadBit("!unk144", i); // 144
                guid36[i] = packet.StartBitStream(0, 1, 7, 6, 4, 2, 5, 3);
            }
            for (var i = 0; i < count32; i++)
            {
                packet.ParseBitStream(guid36[i], 5, 1, 7, 2, 3, 0, 4, 6);
                packet.WriteGuid("Guid36", guid36[i], i);
                if (unk44[i])
                    packet.ReadInt32("unk44", i); //44
                if (unk48[i])
                    packet.ReadByte("unk48", i); // 48
            }
            for (var i = 0; i < count16; i++)
            {
                if (unk157[i])
                    packet.ReadByte("unk157", i); // 157
                packet.ReadInt32("unk48", i); // 48
                packet.ParseBitStream(guid[i], 7);
                packet.ReadInt16("unk42", i);
                if (unk148[i])
                {
                    packet.ParseBitStream(guid112[i], 1);
                    packet.ReadInt32("unk140", i); // 140
                    packet.ParseBitStream(guid112[i], 4, 3);
                    packet.ReadInt32("unk144", i);
                    packet.ParseBitStream(guid112[i], 0, 6, 7, 2, 5);
                    packet.WriteGuid("Guid112", guid112[i], i);
                }
                packet.ReadInt32("unk52", i); // 52
                if (unk40[i])
                    packet.ReadInt16("unk40", i);
                packet.ReadInt32("unk28", i);
                packet.ParseBitStream(guid[i], 2);
                if (unk46[i])
                    packet.ReadInt16("unk46", i);
                packet.ReadInt32("unk32", i); // 32
                packet.ReadInt32("unk36", i); // 36
                packet.ReadInt32("unk60", i); // 60
                packet.ReadWoWString("str", len64[i], i);
                packet.ParseBitStream(guid[i], 6, 5);
                packet.ReadInt32("unk56", i); // 56
                packet.ParseBitStream(guid[i], 4);
                packet.ReadInt16("unk44", i); // 44
                packet.ParseBitStream(guid[i], 0, 1, 3);
                packet.WriteGuid("Guid", guid[i], i);
            }
            packet.ReadInt16("unk24");
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_ACQUIRED)]
        public static void HandleBattlePetJournalLockAcquired(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLE_PET_JOURNAL_LOCK_DENIED)]
        public static void HandleBattlePetJournalLockDenied(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_BATTLE_PET_QUERY_NAME_RESPONSE)]
        public static void HandleBattlePetQueryNameResponse(Packet packet)
        {
            packet.ReadInt64("Petentry");
            packet.ReadTime("Time");
            packet.ReadInt32("SpeciesId");
            var declinedNamesLen = new uint[5];
            var hasDeclinedNames = packet.ReadBit("hasDeclNames");
            var len = 0u;
            if (hasDeclinedNames)
            {
                len = packet.ReadBits(8);
                for (var i = 5; i > 0; i--)
                    declinedNamesLen[i - 1] = packet.ReadBits(7);
                packet.ReadBit("unk");
            }

            if (hasDeclinedNames)
            {
                for (var i = 5; i > 0; i--)
                    packet.ReadWoWString("declNames", declinedNamesLen[i - 1], i);
                packet.ReadWoWString("Name", len);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PET_SLOT_UPDATE)]
        public static void HandleBattlePetSlotUpdate(Packet packet)
        {
            var count20 = packet.ReadBits("count20", 25);
            var unk16 = packet.ReadBit("unk16");
            var guid = new byte[count20][];
            var unk56 = new bool[count20];
            var unk36 = new bool[count20];
            for (var i = 0; i < count20; i++)
            {
                unk36[i] = !packet.ReadBit("!unk36", i); // 36
                unk56[i] = !packet.ReadBit("!unk56", i); // 56
                packet.ReadBit("unk37", i);
                packet.ReadBit("!unk40", i);
                guid[i] = packet.StartBitStream(4, 5, 2, 1, 0, 3, 7, 6);
            }
            packet.ReadBit("unk36");
            for (var i = 0; i < count20; i++)
            {
                packet.ParseBitStream(guid[i], 0, 3, 2, 1, 6, 4, 5, 7);
                packet.WriteGuid("Guid", guid[i], i);
                if (unk56[i])
                    packet.ReadInt32("unk56", i);
                if (unk36[i])
                    packet.ReadByte("unk36", i);
            }
        }

        [Parser(Opcode.SMSG_BATTLE_PET_UPDATES)]
        public static void HandleBattlePetUpdates(Packet packet)
        {
            var count20 = packet.ReadBits("count20", 19);
            var guid = new byte[count20][];
            var unk152 = new bool[count20];
            var guid112 = new byte[count20][];
            var len200 = new uint[count20];
            var unk64 = new bool[count20];
            var unk161 = new bool[count20];
            var unk76 = new bool[count20];
            for (var i = 0; i < count20; i++)
            {
                guid[i] = new byte[8];
                guid[i][4] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                unk161[i] = !packet.ReadBit("!unk161", i);
                unk64[i] = !packet.ReadBit("!unk64", i);
                guid[i][5] = packet.ReadBit();
                packet.ReadBit("unk160", i);
                guid[i][2] = packet.ReadBit();
                unk76[i] = !packet.ReadBit("!unk76", i);
                unk152[i] = packet.ReadBit("unk152", i);
                guid[i][6] = packet.ReadBit();
                if (unk152[i])
                    guid112[i] = packet.StartBitStream(5, 2, 4, 1, 6, 0, 7, 3);
                guid[i][3] = packet.ReadBit();
                len200[i] = packet.ReadBits("count200", 7, i);
                guid[i][0] = packet.ReadBit();
            }
            packet.ReadBit("unk16");
            for (var i = 0; i < count20; i++)
            {
                if (unk152[i])
                {
                    packet.ParseBitStream(guid112[i], 3);
                    packet.ReadInt32("unk520", i);
                    packet.ReadInt32("unk126*4", i);
                    packet.ParseBitStream(guid112[i], 0, 4, 2, 6, 1, 7, 5);
                    packet.WriteGuid("Guid112", guid112[i], i);
                }
                packet.ParseBitStream(guid[i], 1);
                packet.ReadWoWString("str", len200[i], i);
                packet.ReadInt32("unk156", i);
                packet.ReadInt32("unk56", i);
                packet.ParseBitStream(guid[i], 0);
                packet.ReadInt32("unk136", i);
                packet.ReadInt32("unk184", i);
                if (unk64[i])
                    packet.ReadInt16("unk64", i);
                packet.ParseBitStream(guid[i], 4);
                packet.ReadInt32("unk72", i);
                packet.ReadInt32("unk168", i);
                packet.ParseBitStream(guid[i], 6);
                if (unk161[i])
                    packet.ReadByte("unk161", i);
                packet.ParseBitStream(guid[i], 2, 3);
                packet.ReadInt16("unk72", i);
                packet.ParseBitStream(guid[i], 7);
                if (unk76[i])
                    packet.ReadInt16("unk76", i);
                packet.ParseBitStream(guid[i], 5);
                packet.ReadInt32("unk88", i);
                packet.ReadInt16("unk66", i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }
    }
}
