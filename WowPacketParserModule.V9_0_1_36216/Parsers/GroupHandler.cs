using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class GroupHandler
    {
        public static void ReadAuraInfos(Packet packet, params object[] index)
        {
            packet.ReadUInt32<SpellId>("Aura", index);
            if (ClientVersion.AddedInVersion(ClientType.Shadowlands))
                packet.ReadUInt16("Flags", index);
            else
                packet.ReadByte("Flags", index);
            packet.ReadInt32("ActiveFlags", index);
            var byte3 = packet.ReadInt32("PointsCount", index);

            for (int j = 0; j < byte3; j++)
                packet.ReadSingle("Points", index, j);
        }

        [Parser(Opcode.SMSG_PARTY_MEMBER_PARTIAL_STATE)]
        public static void HandlePartyMemberPartialState(Packet packet)
        {
            packet.ReadBit("ForEnemyChanged");
            packet.ReadBit("SetPvPInactive"); // adds GroupMemberStatusFlag 0x0020 if true, removes 0x0020 if false
            packet.ReadBit("Unk901_1");

            var partyTypeChanged = packet.ReadBit("PartyTypeChanged");
            var flagsChanged = packet.ReadBit("FlagsChanged");
            var powerTypeChanged = packet.ReadBit("PowerTypeChanged");
            var overrideDisplayPowerChanged = packet.ReadBit("OverrideDisplayPowerChanged");
            var currentHealthChanged = packet.ReadBit("CurrentHealthChanged");
            var maxHealthChanged = packet.ReadBit("MaxHealthChanged");
            var powerChanged = packet.ReadBit("PowerChanged");
            var maxPowerChanged = packet.ReadBit("MaxPowerChanged");
            var levelChanged = packet.ReadBit("LevelChanged");
            var specChanged = packet.ReadBit("SpecChanged");
            var areaIdChanged = packet.ReadBit("AreaIdChanged");
            var wmoGroupIdChanged = packet.ReadBit("WmoGroupIdChanged");
            var wmoDoodadPlacementIdChanged = packet.ReadBit("WmoDoodadPlacementIdChanged");
            var positionChanged = packet.ReadBit("PositionChanged");
            var vehicleSeatRecIdChanged = packet.ReadBit("VehicleSeatRecIdChanged");
            var aurasChanged = packet.ReadBit("AurasChanged");
            var petChanged = packet.ReadBit("PetChanged");
            var phaseChanged = packet.ReadBit("PhaseChanged");
            var unk901_2 = packet.ReadBit("Unk901_2");

            if (petChanged)
            {
                packet.ResetBitReader();
                var petGuidChanged = packet.ReadBit("GuidChanged", "Pet");
                var petNameChanged = packet.ReadBit("NameChanged", "Pet");
                var petDisplayIdChanged = packet.ReadBit("DisplayIdChanged", "Pet");
                var petMaxHealthChanged = packet.ReadBit("MaxHealthChanged", "Pet");
                var petHealthChanged = packet.ReadBit("HealthChanged", "Pet");
                var petAurasChanged = packet.ReadBit("AurasChanged", "Pet");
                if (petNameChanged)
                {
                    packet.ResetBitReader();
                    var len = packet.ReadBits(8);
                    packet.ReadWoWString("NewPetName", len, "Pet");
                }
                if (petGuidChanged)
                    packet.ReadPackedGuid128("NewPetGuid", "Pet");
                if (petDisplayIdChanged)
                    packet.ReadUInt32("PetDisplayID", "Pet");
                if (petMaxHealthChanged)
                    packet.ReadUInt32("PetMaxHealth", "Pet");
                if (petHealthChanged)
                    packet.ReadUInt32("PetHealth", "Pet");
                if (petAurasChanged)
                {
                    var cnt = packet.ReadInt32("AuraCount", "Pet", "Aura");
                    for (int i = 0; i < cnt; i++)
                        ReadAuraInfos(packet, "Pet", "Aura", i);
                }
            }

            packet.ReadPackedGuid128("AffectedGUID");
            if (partyTypeChanged)
            {
                for (int i = 0; i < 2; i++)
                    packet.ReadByte("PartyType", i);
            }

            if (flagsChanged)
                packet.ReadUInt16E<GroupMemberStatusFlag>("Flags");
            if (powerTypeChanged)
                packet.ReadByte("PowerType");
            if (overrideDisplayPowerChanged)
                packet.ReadUInt16("OverrideDisplayPower");
            if (currentHealthChanged)
                packet.ReadUInt32("CurrentHealth");
            if (maxHealthChanged)
                packet.ReadUInt32("MaxHealth");
            if (powerChanged)
                packet.ReadUInt16("Power");
            if (maxPowerChanged)
                packet.ReadUInt16("MaxPower");
            if (levelChanged)
                packet.ReadUInt16("Level");
            if (specChanged)
                packet.ReadUInt16("Spec");
            if (areaIdChanged)
                packet.ReadUInt16("AreaID");
            if (wmoGroupIdChanged)
                packet.ReadUInt16("WmoGroupID");
            if (wmoDoodadPlacementIdChanged)
                packet.ReadUInt32("WmoDoodadPlacementID");
            if (positionChanged)
            {
                packet.ReadUInt16("PositionX");
                packet.ReadUInt16("PositionY");
                packet.ReadUInt16("PositionZ");
            }
            if (vehicleSeatRecIdChanged)
                packet.ReadUInt32("VehicleSeatRecID");
            if (aurasChanged)
            {
                var cnt = packet.ReadInt32("AuraCount", "Aura");
                for (int i = 0; i < cnt; i++)
                    ReadAuraInfos(packet, "Aura", i);
            }
            if (phaseChanged)
                V6_0_2_19033.Parsers.GroupHandler.ReadPhaseInfos(packet, "Phase");

            if (unk901_2)
            {
                packet.ReadUInt32("Unk902_3");
                packet.ReadUInt32("Unk902_4");
                packet.ReadUInt32("Unk902_5");
            }
        }

        [Parser(Opcode.SMSG_GROUP_NEW_LEADER)]
        public static void HandleGroupNewLeader(Packet packet)
        {
            packet.ReadByte("PartyIndex");
            var len = packet.ReadBits(9);
            packet.ReadWoWString("Name", len);
        }

        [Parser(Opcode.CMSG_DO_COUNTDOWN)]
        public static void HandleDoCountdown(Packet packet)
        {
            packet.ReadInt32("TotalTime");
            packet.ReadByte("PartyIndex");
        }
    }
}
