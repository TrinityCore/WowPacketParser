using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class PetHandler
    {
        public static void ReadPetFlags(Packet packet)
        {
            var petModeFlag = packet.ReadUInt32();
            packet.AddValue("React state", (ReactState) ((petModeFlag >> 8) & 0xFF));
            packet.AddValue("Command state", (CommandState) ((petModeFlag >> 16) & 0xFF));
            packet.AddValue("Flag", (PetModeFlags) (petModeFlag & 0xFFFF0000));
        }

        [Parser(Opcode.SMSG_PET_SPELLS_MESSAGE)]
        public static void HandlePetSpells(Packet packet)
        {
            var guid = packet.ReadGuid("GUID");
            // Equal to "Clear spells" pre cataclysm
            if (guid.IsEmpty())
                return;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                packet.ReadUInt16E<CreatureFamily>("Pet Family"); // vehicles -> 0

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                packet.ReadUInt16("Unk UInt16");

            packet.ReadUInt32("Expiration Time");

            ReadPetFlags(packet);

            var isPet = guid.GetHighType() == HighGuidType.Pet;
            var isVehicle = guid.GetHighType() == HighGuidType.Vehicle;
            var isMinion = guid.GetHighType() == HighGuidType.Creature;
            const int maxCreatureSpells = 10;
            var spells = new List<uint>(maxCreatureSpells);
            for (var i = 0; i < maxCreatureSpells; i++) // Read pet/vehicle spell ids
            {
                var spell16 = packet.ReadUInt16();
                var spell8 = packet.ReadByte();
                var spellId = spell16 + (spell8 << 16);
                var slot = packet.ReadByte("Slot", i);

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
                SpellsX spellsCr;
                spellsCr.Spells = spells.ToArray();
                Storage.SpellsX.Add(guid.GetEntry(), spellsCr, packet.TimeSpan);
            }

            var spellCount = packet.ReadByte("Spell Count"); // vehicles -> 0, pets -> != 0. Could this be auras?
            for (var i = 0; i < spellCount; i++)
            {
                packet.ReadUInt16<SpellId>("Spell", i);
                packet.ReadInt16("Active", i);
            }

            var cdCount = packet.ReadByte("Cooldown count");
            for (var i = 0; i < cdCount; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadUInt32<SpellId>("Spell", i);
                else
                    packet.ReadUInt16<SpellId>("Spell", i);

                packet.ReadUInt16("Category", i);
                packet.ReadUInt32("Cooldown", i);
                packet.ReadUInt32("Category Cooldown", i);
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
            {
                var unkLoopCounter = packet.ReadByte("Unk count");
                for (var i = 0; i < unkLoopCounter; i++)
                {
                    packet.ReadUInt32("Unk UInt32 1", i);
                    packet.ReadByte("Unk Byte", i);
                    packet.ReadUInt32("Unk UInt32 2", i);
                }
            }
        }

        [Parser(Opcode.SMSG_PET_TAME_FAILURE)]
        public static void HandlePetTameFailure(Packet packet)
        {
            packet.ReadByteE<PetTameFailureReason>("Reason");
        }

        [Parser(Opcode.CMSG_QUERY_PET_NAME)]
        public static void HandlePetNameQuery(Packet packet)
        {
            var number = packet.ReadInt32("Pet number").ToString(CultureInfo.InvariantCulture);
            var guid = packet.ReadGuid("Guid");

            // Store temporary name (will be replaced in SMSG_QUERY_PET_NAME_RESPONSE)
            StoreGetters.AddName(guid, number);
        }

        [Parser(Opcode.SMSG_QUERY_PET_NAME_RESPONSE)]
        public static void HandlePetNameQueryResponse(Packet packet)
        {
            var number = packet.ReadInt32("Pet number").ToString(CultureInfo.InvariantCulture);

            var petName = packet.ReadCString("Pet name");
            if (petName.Length == 0)
            {
                packet.ReadBytes(7); // 0s
                return;
            }

            var guidArray = (from pair in StoreGetters.NameDict where Equals(pair.Value, number) select pair.Key).ToList();
            foreach (var guid in guidArray)
                StoreGetters.NameDict[guid] = petName;

            packet.ReadTime("Time");
            var declined = packet.ReadBool("Declined");

            const int maxDeclinedNameCases = 5;

            if (declined)
                for (var i = 0; i < maxDeclinedNameCases; i++)
                    packet.ReadCString("Declined name", i);
        }

        [Parser(Opcode.SMSG_PET_MODE)]
        public static void HandlePetMode(Packet packet)
        {
            packet.ReadGuid("Guid");
            ReadPetFlags(packet);
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
                packet.AddValue("Action", action, i);
                packet.ReadByteE<ActionButtonType>("Type", i++);
            }
        }

        [Parser(Opcode.CMSG_PET_ACTION)]
        public static void HandlePetAction(Packet packet)
        {
            packet.ReadGuid("GUID");
            var action = (uint)packet.ReadUInt16() + (packet.ReadByte() << 16);
            packet.AddValue("Action", action);
            packet.ReadByteE<ActionButtonType>("Type");
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6_13596))
                packet.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_PET_CANCEL_AURA)]
        public static void HandlePetCancelAura(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PET_LEARNED_SPELLS)]
        [Parser(Opcode.SMSG_PET_UNLEARNED_SPELLS)]
        public static void HandlePetSpellsLearnedRemoved(Packet packet)
        {
            packet.ReadInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PET_ACTION_FEEDBACK)]
        public static void HandlePetActionFeedback(Packet packet)
        {
            var state = packet.ReadByteE<PetFeedback>("Response");

            switch (state)
            {
                case PetFeedback.NothingToAttack:
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm) || packet.CanRead())
                        packet.ReadInt32<SpellId>("SpellID");
                    break;
                case PetFeedback.CantAttackTarget:
                    if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                        packet.ReadInt32<SpellId>("SpellID");    // sub_8ADA60 2nd parameter is SpellID, check sub_8B22C0
                    break;
            }
        }

        [Parser(Opcode.CMSG_PET_STOP_ATTACK)]
        [Parser(Opcode.CMSG_DISMISS_CRITTER)]
        [Parser(Opcode.CMSG_PET_ABANDON)]
        public static void HandlePetMiscGuid(Packet packet)
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
                    packet.ReadInt32("Pet Slot", i);

                packet.ReadInt32("Pet Number", i);
                packet.ReadUInt32<UnitId>("Pet Entry", i);
                packet.ReadInt32("Pet Level", i);
                packet.ReadCString("Pet Name", i);
                packet.ReadByte("Stable Type", i); // 1 = current, 2/3 = in stable
            }
        }

        [Parser(Opcode.CMSG_SET_PET_SLOT)]
        public static void HandleSetPetSlot(Packet packet)
        {
            packet.ReadInt32("Unk 0");
            packet.ReadByte("Unk 1");
            var guid = packet.StartBitStream(3, 2, 0, 7, 5, 6, 1, 4);
            packet.ParseBitStream(guid, 5, 3, 1, 7, 4, 0, 6, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_PET_CAST_SPELL)]
        public static void HandlePetCastSpell(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Cast Count");
            packet.ReadInt32<SpellId>("Spell ID");
            var castFlags = packet.ReadByteE<CastFlag>("Cast Flags");
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
            packet.ReadInt32("Pet Level");
            packet.ReadInt32("Pet Slot");
            packet.ReadByte("Stable Type");
            packet.ReadUInt32<UnitId>("Entry");
            packet.ReadInt32("Pet Number");

            var len = packet.ReadBits(8);
            packet.ReadWoWString("Pet Name", len);
        }

        [Parser(Opcode.CMSG_PET_RENAME)]
        public static void HandlePetRename(Packet packet)
        {
            packet.ReadGuid("Pet Guid");
            packet.ReadCString("Name");
            var declined = packet.ReadBool("Is Declined");
            if (declined)
                for (var i = 0; i < 5; ++i)
                    packet.ReadCString("Declined Name", i);
        }

        [Parser(Opcode.CMSG_PET_SPELL_AUTOCAST)]
        public static void HandlePetSpellAutocast(Packet packet)
        {
            packet.ReadGuid("Pet Guid");
            packet.ReadUInt32<SpellId>("Spell Id");
            packet.ReadByte("State");
        }
    }
}
