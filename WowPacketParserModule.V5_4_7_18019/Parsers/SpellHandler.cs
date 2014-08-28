using System;
using System.Text;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;
using Guid = WowPacketParser.Misc.WowGuid;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.CMSG_CANCEL_AURA)]
        public static void HandleCancelAura(Packet packet)
        {
            packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID");

            packet.ReadBit("Unk");
            var guid = packet.StartBitStream(1, 5, 2, 0, 3, 4, 6, 7);
            packet.ParseBitStream(guid, 0, 1, 4, 5, 3, 6, 7, 2);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandleCancelCast(Packet packet)
        {
            var hasCounter = !packet.ReadBit("HasCounter");
            var hasSpellID = !packet.ReadBit("HasSpellID");

            if (hasCounter)
                packet.ReadByte("Counter");

            if (hasSpellID)
                packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID");
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                bool hasCastCount = !packet.ReadBit("hasCastCount");
                bool hasSrcLocation = packet.ReadBit("hasSrcLocation");
                bool hasTargetString = !packet.ReadBit("hasTargetString");
                bool hasTargetMask = !packet.ReadBit("hasTargetMask");
                bool hasSpellId = !packet.ReadBit("hasSpellId");
                bool hasCastFlags = !packet.ReadBit("hasCastFlags");
                bool hasDestLocation = packet.ReadBit("hasDestLocation");
                packet.ReadBit("unk");
                uint researchDataCount = packet.ReadBits("researchDataCount", 2);
                bool hasMovement = packet.ReadBit("hasMovement");
                packet.ReadBit("unk2");
                bool hasGlyphIndex = !packet.ReadBit("hasGlyphIndex");

                for (uint i = 0; i < researchDataCount; ++i)
                    packet.ReadBits(2);

                bool hasElevation = !packet.ReadBit("hasElevation");
                bool hasMissileSpeed = !packet.ReadBit("hasMissileSpeed");
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_NAME_QUERY_RESPONSE");
                /*Guid guid;
                    guid = packet.ReadPackedGuid("GUID");
                    var end = packet.ReadByte("Result");

                    if (end != 0)
                        return;

                var name = packet.ReadCString("Name");
                StoreGetters.AddName(guid, name);
                packet.ReadCString("Realm Name");

                TypeCode typeCode = ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767) ? TypeCode.Byte : TypeCode.Int32;
                packet.ReadEnum<Race>("Race", typeCode);
                packet.ReadEnum<Gender>("Gender", typeCode);
                packet.ReadEnum<Class>("Class", typeCode);

                if (!packet.ReadBoolean("Name Declined"))
                    return;

                for (var i = 0; i < 5; i++)
                    packet.ReadCString("Declined Name", i);

                var objectName = new ObjectName
                {
                    ObjectType = ObjectType.Player,
                    Name = name,
                };
                Storage.ObjectNames.Add((uint)guid.GetLow(), objectName, packet.TimeSpan);*/
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_UNLEARN_SKILL)]
        public static void HandleUnlearnSkill(Packet packet)
        {
            packet.ReadInt32("SkillID");
        }


        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = new byte[8];
            var casterGUID = new byte[8];
            bool notFlagCaster = false;
            guid[3] = packet.ReadBit();
            packet.ReadBit("unk");
            guid[4] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            var aurCount = packet.ReadBits("aurCount", 24);
            guid[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var notRemove = packet.ReadBit("notRemove");

            if (notRemove)
            {
                packet.ReadBit("hasDuration");
                packet.ReadBits("unk1", 22);
                packet.ReadBit("hasMaxDuration");
                notFlagCaster = packet.ReadBit("notFlagCaster");
                packet.ReadBits("effCount", 22);

                if (notFlagCaster)
                     casterGUID = packet.StartBitStream(1, 6, 0, 7, 5, 3, 2, 4);
            }

            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            if (notRemove)
            {
                if (notFlagCaster)
                {
                    packet.ParseBitStream(casterGUID, 2, 5, 6, 7, 0, 1, 4, 3);
                    packet.WriteGuid("Caster Guid", casterGUID);
                }

                packet.ReadUInt32("EffectMask");
            }
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            var guid = new byte[8];
            guid = packet.StartBitStream(4, 7, 1, 6, 5, 3, 0, 2);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_INITIAL_SPELLS)]
        public static void HandleInitialSpells547(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadBits("Unk Bits", 1);
                var count = packet.ReadBits("Spell Count", 22);
                packet.ResetBitReader();

                var spells = new List<uint>((int)count);
                for (var i = 0; i < count; i++)
                {
                    var spellId = packet.ReadEntry<UInt32>(StoreNameType.Spell, "Spell ID", i);
                    spells.Add((uint)spellId);
                }

                var startSpell = new StartSpell { Spells = spells };

                WoWObject character;
                if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
                {
                    var player = character as Player;
                    if (player != null && player.FirstLogin)
                        Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
                }
            } else
            {
                packet.WriteLine("              : CMSG_GUILD_DECLINE");
                packet.Opcode = (int)Opcode.CMSG_GUILD_DECLINE;

                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnedSpell(Packet packet)
        {
            packet.ReadBit("unk");
            var count = packet.ReadBits("Count", 22);

            for (var i = 0; i < count; ++i)
                packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID", i);
        }

        [Parser(Opcode.SMSG_PLAYERBOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            var guid = new byte[8];
            guid = packet.StartBitStream(3, 1, 0, 7, 6, 4, 2, 5);

            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);
            packet.ReadXORByte(guid, 7);
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadXORByte(guid, 1);
            packet.ReadByte("Cast count");
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        public static void HandleSpellFailure(Packet packet)
        {
            var guid = new byte[8];
            guid = packet.StartBitStream(1, 5, 3, 4, 2, 7, 0, 6);

            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadEnum<SpellCastFailureReason>("Reason", TypeCode.Byte);
            packet.ReadByte("Cast count");
            packet.ReadEntry<Int32>(StoreNameType.Spell, "Spell ID");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);

            packet.WriteGuid("Guid", guid);
        }
    }
}
