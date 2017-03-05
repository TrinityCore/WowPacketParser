using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.Parsing.Parsers
{
    public static class PetHandler
    {
        public static void ReadPetFlags(Packet packet)
        {
            var petModeFlag = packet.Translator.ReadUInt32();
            packet.AddValue("React state", (ReactState) ((petModeFlag >> 8) & 0xFF));
            packet.AddValue("Command state", (CommandState) ((petModeFlag >> 16) & 0xFF));
            packet.AddValue("Flag", (PetModeFlags) (petModeFlag & 0xFFFF0000));
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE)]
        public static void HandlePetSpells(Packet packet)
        {
            WowGuid guid = packet.Translator.ReadGuid("GUID");
            // Equal to "Clear spells" pre cataclysm
            if (guid.IsEmpty())
                return;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                packet.Translator.ReadUInt16E<CreatureFamily>("Pet Family"); // vehicles -> 0

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.Translator.ReadUInt16("Unk UInt16");

            packet.Translator.ReadUInt32("Expiration Time");

            ReadPetFlags(packet);

            bool isPet = guid.GetHighType() == HighGuidType.Pet;
            bool isVehicle = guid.GetHighType() == HighGuidType.Vehicle;
            bool isMinion = guid.GetHighType() == HighGuidType.Creature;
            const int maxCreatureSpells = 10;
            var spells = new List<uint?>(maxCreatureSpells);
            for (int i = 0; i < maxCreatureSpells; i++) // Read pet/vehicle spell ids
            {
                ushort spell16 = packet.Translator.ReadUInt16();
                byte spell8 = packet.Translator.ReadByte();
                int spellId = spell16 + (spell8 << 16);
                byte slot = packet.Translator.ReadByte("Slot", i);

                if (spellId <= 4)
                    packet.AddValue("Action", spellId, i);
                else
                    packet.AddValue("Spell", StoreGetters.GetName(StoreNameType.Spell, spellId), i);

                // Spells for pets are on DBCs; also no entry in guid
                // We don't need the actions sent for minions (slots lower than 8)
                if (!isPet && (isVehicle || (isMinion && slot >= 8)))
                    spells.Add((uint)spellId);
            }

            if (spells.Count != 0)
            {
                if (!Storage.SpellsX.ContainsKey(guid.GetEntry()))
                    Storage.SpellsX.Add(guid.GetEntry(), spells);
            }

            byte spellCount = packet.Translator.ReadByte("Spell Count"); // vehicles -> 0, pets -> != 0. Could this be auras?
            for (int i = 0; i < spellCount; i++)
            {
                packet.Translator.ReadUInt16<SpellId>("Spell", i);
                packet.Translator.ReadInt16("Active", i);
            }

            byte cdCount = packet.Translator.ReadByte("Cooldown count");
            for (int i = 0; i < cdCount; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.Translator.ReadUInt32<SpellId>("Spell", i);
                else
                    packet.Translator.ReadUInt16<SpellId>("Spell", i);

                packet.Translator.ReadUInt16("Category", i);
                packet.Translator.ReadUInt32("Cooldown", i);
                packet.Translator.ReadUInt32("Category Cooldown", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
            {
                byte unkLoopCounter = packet.Translator.ReadByte("Unk count");
                for (int i = 0; i < unkLoopCounter; i++)
                {
                    packet.Translator.ReadUInt32("Unk UInt32 1", i);
                    packet.Translator.ReadByte("Unk Byte", i);
                    packet.Translator.ReadUInt32("Unk UInt32 2", i);
                }
            }
        }

        [Parser(Opcode.SMSG_PET_TAME_FAILURE)]
        public static void HandlePetTameFailure(Packet packet)
        {
            packet.Translator.ReadByteE<PetTameFailureReason>("Reason");
        }

        [Parser(Opcode.CMSG_QUERY_PET_NAME)]
        public static void HandlePetNameQuery(Packet packet)
        {
            var number = packet.Translator.ReadInt32("Pet number").ToString(CultureInfo.InvariantCulture);
            var guid = packet.Translator.ReadGuid("Guid");

            // Store temporary name (will be replaced in SMSG_QUERY_PET_NAME_RESPONSE)
            StoreGetters.AddName(guid, number);
        }

        [Parser(Opcode.SMSG_QUERY_PET_NAME_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var number = packet.Translator.ReadInt32("Pet number").ToString(CultureInfo.InvariantCulture);

            var petName = packet.Translator.ReadCString("Pet name");
            if (petName.Length == 0)
            {
                packet.Translator.ReadBytes(7); // 0s
                return;
            }

            var guidArray = (from pair in StoreGetters.NameDict where Equals(pair.Value, number) select pair.Key).ToList();
            foreach (var guid in guidArray)
                StoreGetters.NameDict[guid] = petName;

            packet.Translator.ReadTime("Time");
            var declined = packet.Translator.ReadBool("Declined");

            const int maxDeclinedNameCases = 5;

            if (declined)
                for (var i = 0; i < maxDeclinedNameCases; i++)
                    packet.Translator.ReadCString("Declined name", i);
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.Translator.ReadGuid("Guid");
            ReadPetFlags(packet);
        }

        [Parser(Opcode.SMSG_PET_ACTION_SOUND)]
        public static void HandlePetSound(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32("Sound ID");
        }

        [Parser(Opcode.SMSG_PET_DISMISS_SOUND)]
        public static void HandlePetDismissSound(Packet packet)
        {
            packet.Translator.ReadInt32("Sound ID"); // CreatureSoundData.dbc - iRefID_soundPetDismissID
            packet.Translator.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_PET_SET_ACTION)]
        public static void HandlePetSetAction(Packet packet)
        {
            var i = 0;
            packet.Translator.ReadGuid("GUID");
            while (packet.CanRead())
            {
                packet.Translator.ReadUInt32("Position", i);
                var action = (uint)packet.Translator.ReadUInt16() + (packet.Translator.ReadByte() << 16);
                packet.AddValue("Action", action, i);
                packet.Translator.ReadByteE<ActionButtonType>("Type", i++);
            }
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            var action = (uint)packet.Translator.ReadUInt16() + (packet.Translator.ReadByte() << 16);
            packet.AddValue("Action", action);
            packet.Translator.ReadByteE<ActionButtonType>("Type");
            packet.Translator.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                packet.Translator.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_PET_CANCEL_AURA)]
        public static void HandlePetCancelAura(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELLS)]
        [Parser(Opcode.SMSG_PET_UNLEARNED_SPELLS)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            var state = packet.Translator.ReadByteE<PetFeedback>("Response");

            switch (state)
            {
                case PetFeedback.NothingToAttack:
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm) || packet.CanRead())
                        packet.Translator.ReadInt32<SpellId>("SpellID");
                    break;
                case PetFeedback.CantAttackTarget:
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                        packet.Translator.ReadInt32<SpellId>("SpellID");    // sub_8ADA60 2nd parameter is SpellID, check sub_8B22C0
                    break;
            }
        }

        [Parser(Opcode.CMSG_PET_STOP_ATTACK)]
        [Parser(Opcode.CMSG_DISMISS_CRITTER)]
        [Parser(Opcode.CMSG_PET_ABANDON)]
        public static void HandlePetMiscGuid(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_PET_UPDATE_COMBO_POINTS)]
        public static void HandlePetUpdateComboPoints(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Guid 1");
            packet.Translator.ReadPackedGuid("Guid 2");
            packet.Translator.ReadByte("Combo points");
        }

        [Parser(Opcode.SMSG_PET_GUIDS)]
        public static void HandlePetGuids(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < count; ++i)
                packet.Translator.ReadGuid("Guid", i);
        }

        [Parser(Opcode.MSG_LIST_STABLED_PETS)]
        public static void HandleListStabledPets(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");

            if (packet.Direction == Direction.ClientToServer)
                return;

            var count = packet.Translator.ReadByte("Count");
            packet.Translator.ReadByte("Stable Slots");

            for (var i = 0; i < count; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // not verified
                    packet.Translator.ReadInt32("Pet Slot", i);

                packet.Translator.ReadInt32("Pet Number", i);
                packet.Translator.ReadUInt32<UnitId>("Pet Entry", i);
                packet.Translator.ReadInt32("Pet Level", i);
                packet.Translator.ReadCString("Pet Name", i);
                packet.Translator.ReadByte("Stable Type", i); // 1 = current, 2/3 = in stable
            }
        }

        [Parser(Opcode.CMSG_SET_PET_SLOT)]
        public static void HandleSetPetSlot(Packet packet)
        {
            packet.Translator.ReadInt32("Unk 0");
            packet.Translator.ReadByte("Unk 1");
            var guid = packet.Translator.StartBitStream(3, 2, 0, 7, 5, 6, 1, 4);
            packet.Translator.ParseBitStream(guid, 5, 3, 1, 7, 4, 0, 6, 2);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Cast Count");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            var castFlags = packet.Translator.ReadByteE<CastFlag>("Cast Flags");
            SpellHandler.ReadSpellCastTargets(packet);
            if (castFlags.HasAnyFlag(CastFlag.HasTrajectory))
                SpellHandler.HandleSpellMissileAndMove(packet);
        }

        [Parser(Opcode.CMSG_REQUEST_PET_INFO)]
        public static void HandlePetNull(Packet packet)
        {
        }

        [Parser(Opcode.SMSG_PET_ADDED)] // 4.3.4
        public static void HandlePetAdded(Packet packet)
        {
            packet.Translator.ReadInt32("Pet Level");
            packet.Translator.ReadInt32("Pet Slot");
            packet.Translator.ReadByte("Stable Type");
            packet.Translator.ReadUInt32<UnitId>("Entry");
            packet.Translator.ReadInt32("Pet Number");

            var len = packet.Translator.ReadBits(8);
            packet.Translator.ReadWoWString("Pet Name", len);
        }

        [Parser(Opcode.CMSG_PET_RENAME)]
        public static void HandlePetRename(Packet packet)
        {
            packet.Translator.ReadGuid("Pet Guid");
            packet.Translator.ReadCString("Name");
            var declined = packet.Translator.ReadBool("Is Declined");
            if (declined)
                for (var i = 0; i < 5; ++i)
                    packet.Translator.ReadCString("Declined Name", i);
        }

        [Parser(Opcode.CMSG_PET_SPELL_AUTOCAST)]
        public static void HandlePetSpellAutocast(Packet packet)
        {
            packet.Translator.ReadGuid("Pet Guid");
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
            packet.Translator.ReadByte("State");
        }
    }
}
