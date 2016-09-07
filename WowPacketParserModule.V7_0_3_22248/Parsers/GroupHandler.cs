using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V6_0_2_19033.Parsers
{
    public static class GroupHandler
    {
        [Parser(Opcode.SMSG_PARTY_MEMBER_STATE)]
        public static void HandlePartyMemberState(Packet packet)
        {
            packet.ReadBit("ForEnemy");

            for (var i = 0; i < 2; i++)
                packet.ReadByte("PartyType", i);

            packet.ReadInt16E<GroupMemberStatusFlag>("Flags");

            packet.ReadByte("PowerType");
            packet.ReadInt16("OverrideDisplayPower");
            packet.ReadInt32("CurrentHealth");
            packet.ReadInt32("MaxHealth");
            packet.ReadInt16("MaxPower");
            packet.ReadInt16("MaxPower");
            packet.ReadInt16("Level");
            packet.ReadInt16("Spec");
            packet.ReadInt16("AreaID");

            packet.ReadInt16("WmoGroupID");
            packet.ReadInt32("WmoDoodadPlacementID");

            packet.ReadInt16("PositionX");
            packet.ReadInt16("PositionY");
            packet.ReadInt16("PositionZ");

            packet.ReadInt32("VehicleSeatRecID");
            var auraCount = packet.ReadInt32("AuraCount");

            packet.ReadInt32("PhaseShiftFlags");
            var int4 = packet.ReadInt32("PhaseCount");
            packet.ReadPackedGuid128("PersonalGUID");
            for (int i = 0; i < int4; i++)
            {
                packet.ReadInt16("PhaseFlags", i);
                packet.ReadInt16("Id", i);
            }

            for (int i = 0; i < auraCount; i++)
            {
                packet.ReadInt32<SpellId>("Aura", i);
                packet.ReadByte("Flags", i);
                packet.ReadInt32("ActiveFlags", i);
                var byte3 = packet.ReadInt32("PointsCount", i);

                for (int j = 0; j < byte3; j++)
                    packet.ReadSingle("Points", i, j);
            }

            packet.ResetBitReader();

            var hasPet = packet.ReadBit("HasPet");
            if (hasPet) // Pet
            {
                packet.ReadPackedGuid128("PetGuid");
                packet.ReadInt16("PetDisplayID");
                packet.ReadInt32("PetMaxHealth");
                packet.ReadInt32("PetHealth");

                var petAuraCount = packet.ReadInt32("PetAuraCount");
                for (int i = 0; i < petAuraCount; i++)
                {
                    packet.ReadInt32<SpellId>("PetAura", i);
                    packet.ReadByte("PetFlags", i);
                    packet.ReadInt32("PetActiveFlags", i);
                    var byte3 = packet.ReadInt32("PetPointsCount", i);

                    for (int j = 0; j < byte3; j++)
                        packet.ReadSingle("PetPoints", i, j);
                }

                packet.ResetBitReader();

                var len = packet.ReadBits(8);
                packet.ReadWoWString("PetName", len);
            }

            packet.ReadPackedGuid128("MemberGuid");
        }
    }
}
