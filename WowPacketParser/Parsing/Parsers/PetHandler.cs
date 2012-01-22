using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using Guid = WowPacketParser.Misc.Guid;

namespace WowPacketParser.Parsing.Parsers
{
    public static class PetHandler
    {
        [Parser(Opcode.SMSG_PET_SPELLS)]
        public static void HandlePetSpells(Packet packet)
        {
            var guid64 = packet.ReadUInt64();
            // Equal to "Clear spells" pre cataclysm
            if (guid64 == 0)
                return;

            var guid = new Guid(guid64);
            packet.Writer.WriteLine("GUID: " + guid);
            var isPet = guid.GetHighType() == HighGuidType.Pet;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                packet.ReadEnum<CreatureFamily>("Pet Family", TypeCode.UInt16); // vehicles -> 0

            packet.ReadUInt32("Unknown 1");

            // Following int8,int8,int16 is sent like int32
            /*var reactState = */ packet.ReadByte("React state"); // 1
            /*var commandState = */ packet.ReadByte("Command state"); // 1
            packet.ReadUInt16("Unknown 2"); // pets -> 0, vehicles -> 0x800 (2048)

            for (var i = 1; i <= (int)MiscConstants.CreatureMaxSpells + 2; i++) // Read pet/vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadByte();
                var slotid = packet.ReadByte();
                var spellId = spell16 + (spell8 << 16);
                if (!isPet) // cleanup vehicle spells (start at 1 instead 8,
                {           // and do not print spells with id 0)
                    slotid -= (int)MiscConstants.PetSpellsOffset - 1;
                    if (spellId == 0)
                        continue;
                }

                packet.Writer.WriteLine("Spell " + slotid + ": " + StoreGetters.GetName(StoreNameType.Spell, spellId));
            }

            var spellCount = packet.ReadByte(); // vehicles -> 0, pets -> != 0. Could this be auras?
            packet.Writer.WriteLine("Spell count: " + spellCount);

            for (var i = 0; i < spellCount; i++)
            {
                // Sent as int32
                var spellId = packet.ReadUInt16();
                var active = packet.ReadInt16();
                packet.Writer.WriteLine("Spell " + i + ": " + StoreGetters.GetName(StoreNameType.Spell, spellId) + ", active: " + active);
            }

            var cdCount = packet.ReadByte();
            packet.Writer.WriteLine("Cooldown count: " + cdCount);

            for (var i = 0; i < cdCount; i++)
            {
                int spellId;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    spellId = packet.ReadInt32();
                else
                    spellId = packet.ReadUInt16();

                var category = packet.ReadUInt16();
                var cooldown = packet.ReadUInt32();
                var categoryCooldown = packet.ReadUInt32();

                packet.Writer.WriteLine("Cooldown: Spell: " + StoreGetters.GetName(StoreNameType.Spell, spellId) + " category: " + category +
                    " cooldown: " + cooldown + " category cooldown: " + categoryCooldown);
            }
        }

        [Parser(Opcode.SMSG_PET_TAME_FAILURE)]
        public static void HandlePetTameFailure(Packet packet)
        {
            packet.ReadEnum<PetTameFailureReason>("Reason", TypeCode.Byte);
        }

        [Parser(Opcode.CMSG_PET_NAME_QUERY)]
        public static void HandlePetNameQuery(Packet packet)
        {
            packet.ReadInt32("Pet number");
            packet.ReadGuid("Guid");
        }

        [Parser(Opcode.SMSG_PET_NAME_QUERY_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            packet.ReadInt32("Pet number");

            var petName = packet.ReadCString("Pet name");
            if (petName.Length == 0)
            {
                packet.ReadBytes(7); // 0s
                return;
            }

            packet.ReadTime("Time");
            var declined = packet.ReadBoolean("Declined");

            if (declined)
                for (var i = 0; i < (int)MiscConstants.MaxDeclinedNameCases; i++)
                    packet.ReadCString("Declined name", i);
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadGuid("Guid");
            packet.ReadEnum<PetModeFlags>("Pet Mode Flags", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_PET_ACTION_SOUND)]
        public static void HandlePetSound(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32("Sound ID");
        }

        [Parser(Opcode.SMSG_PET_DISMISS_SOUND)]
        public static void HandlePetDismissSound(Packet packet)
        {
            packet.ReadInt32("Sound ID"); // CreatureSoundData.dbc - iRefID_soundPetDismissID
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_PET_SET_ACTION)]
        public static void HandlePetSetAction(Packet packet)
        {
            var i = 0;
            packet.ReadGuid("GUID");
            while (packet.CanRead())
            {
                packet.ReadUInt32("Position", i);
                var action = (uint)packet.ReadUInt16() + (packet.ReadByte() << 16);
                packet.Writer.WriteLine("[{0}] Action: {1}", i, action);
                packet.ReadEnum<ActionButtonType>("Type", TypeCode.Byte, i++);
            }
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            packet.ReadGuid("GUID");
            var action = (uint)packet.ReadUInt16() + (packet.ReadByte() << 16);
            packet.Writer.WriteLine("Action: {0}", action);
            packet.ReadEnum<ActionButtonType>("Type", TypeCode.Byte);
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_PET_CANCEL_AURA)]
        public static void HandlePetCancelAura(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELL)]
        [Parser(Opcode.SMSG_PET_REMOVED_SPELL)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            var state = packet.ReadEnum<PetFeedback>("Pet state", TypeCode.Byte);

            switch (state)
            {
                case PetFeedback.NothingToAttack:
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm) || packet.CanRead())
                        packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");
                    break;
                case PetFeedback.CantAttackTarget:
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                        packet.ReadInt32("Unk int32");
                    break;
            }
        }

        [Parser(Opcode.CMSG_PET_STOP_ATTACK)]
        [Parser(Opcode.CMSG_DISMISS_CRITTER)]
        public static void HandleDismissCritter(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_PET_UPDATE_COMBO_POINTS)]
        public static void HandlePetUpdateComboPoints(Packet packet)
        {
            packet.ReadPackedGuid("Guid 1");
            packet.ReadPackedGuid("Guid 2");
            packet.ReadByte("Combo points");
        }

        [Parser(Opcode.SMSG_PET_GUIDS)]
        public static void HandlePetGuids(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.ReadGuid("Guid", i);
        }

        [Parser(Opcode.MSG_LIST_STABLED_PETS)]
        public static void HandleListStabledPets(Packet packet)
        {
            packet.ReadGuid("GUID");

            if (packet.Direction == Direction.ClientToServer)
                return;

            var count = packet.ReadByte("Count");
            packet.ReadByte("Stable Slots");

            for (var i = 0; i < count; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // not verified
                    packet.ReadInt32("Unk", i);

                packet.ReadInt32("Pet Number", i);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Unit, "Pet Entry", i);
                packet.ReadInt32("Pet Level", i);
                packet.ReadCString("Pet Name", i);
                packet.ReadByte("Stable Type", i); // 1 = current, 2/3 = in stable
            }

        }
    }
}
