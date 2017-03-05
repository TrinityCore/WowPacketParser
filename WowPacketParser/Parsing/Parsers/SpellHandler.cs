using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    [SuppressMessage("ReSharper", "UseObjectOrCollectionInitializer")]
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)] // 4.3.4
        public static void HandleSpellInterruptLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[4] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[6] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid2[1] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 3);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadInt32<SpellId>("Interrupt Spell ID");
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadInt32<SpellId>("Interrupted Spell ID");
            packet.Translator.ReadXORByte(guid2, 5);

            packet.Translator.WriteGuid("GUID 1", guid1);
            packet.Translator.WriteGuid("GUID 2", guid1);
        }

        [Parser(Opcode.SMSG_PLAYER_BOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32<AreaId>("Area ID");
        }

        [Parser(Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT)]
        public static void HandleCancelTempEnchantment(Packet packet)
        {
            packet.Translator.ReadUInt32("Slot");
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELLS)]
        public static void HandleSupercededSpell(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32<SpellId>("Next Spell ID");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            var count = packet.Translator.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.Translator.ReadByte("Rune Type");
                packet.Translator.ReadByte("Cooldown Time");
            }
        }

        [Parser(Opcode.SMSG_CONVERT_RUNE)]
        public static void HandleConvertRune(Packet packet)
        {
            packet.Translator.ReadByte("Index");
            packet.Translator.ReadByte("New Rune Type");
        }

        [Parser(Opcode.SMSG_ADD_RUNE_POWER)]
        public static void HandleAddRunePower(Packet packet)
        {
            packet.Translator.ReadUInt32("Mask?"); // TC: 1 << index
        }

        [Parser(Opcode.SMSG_COOLDOWN_CHEAT)]
        public static void HandleCooldownCheat(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST)]
        public static void HandleNotifyDestLocSpellCast(Packet packet)
        {
            // TODO: Verify and/or finish this
            // Everything is guessed
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadVector3("Position");
            packet.Translator.ReadVector3("Target Position");
            packet.Translator.ReadSingle("Elevation");
            packet.Translator.ReadSingle("Speed");
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadInt32("Unk");

            if (packet.Length == 64) // packet always has length 64 length except for some rare exceptions with length 60 (hardcoded in the client)
                packet.Translator.ReadSingle("Unk");
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 23);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Unk Int32");
                packet.Translator.ReadByte("Unk Int8");
            }
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Caster GUID");

            packet.Translator.ReadPackedGuid("Target GUID");

            packet.Translator.ReadInt32<SpellId>("Spell ID");

            packet.Translator.ReadInt32("Cast Time");

            packet.Translator.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.Translator.ReadByte("Talent Spec");

            var count = packet.Translator.ReadUInt16("Spell Count");
            for (var i = 0; i < count; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                     packet.Translator.ReadInt32<SpellId>("Spell ID", i);
                else
                    packet.Translator.ReadUInt16<SpellId>("Spell ID", i);

                packet.Translator.ReadInt16("Unk Int16", i);
            }

            var cooldownCount = packet.Translator.ReadUInt16("Cooldown Count");
            for (var i = 0; i < cooldownCount; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.Translator.ReadInt32<SpellId>("Cooldown Spell ID", i);
                else
                    packet.Translator.ReadUInt16<SpellId>("Cooldown Spell ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.Translator.ReadInt32("Cooldown Cast Item ID", i);
                else
                    packet.Translator.ReadUInt16("Cooldown Cast Item ID", i);

                packet.Translator.ReadUInt16("Cooldown Spell Category", i);
                packet.Translator.ReadInt32("Cooldown Time", i);
                var catCd = packet.Translator.ReadUInt32();
                packet.AddValue("Cooldown Category Time", ((catCd >> 31) != 0 ? "Infinite" : (catCd & 0x7FFFFFFF).ToString(CultureInfo.InvariantCulture)), i);
            }
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleInitialSpells510(Packet packet)
        {
            var count = packet.Translator.ReadBits("Spell Count", 24);
            packet.Translator.ReadBit("InitialLogin");
            packet.Translator.ResetBitReader();

            for (var i = 0; i < count; i++)
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);
            }
        }

        private static Aura ReadAuraUpdateBlock(Packet packet, int i)
        {
            var aura = new Aura
            {
                Slot = packet.Translator.ReadByte("Slot", i)
            };

            var id = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
            if (id <= 0)
                return null;
            aura.SpellId = (uint)id;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                aura.AuraFlags = packet.Translator.ReadInt16E<AuraFlag>("Flags", i);
            else
                aura.AuraFlags = packet.Translator.ReadByteE<AuraFlag>("Flags", i);

            aura.Level = packet.Translator.ReadByte("Level", i);

            aura.Charges = packet.Translator.ReadByte("Charges", i);

            aura.CasterGuid = !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster) ? packet.Translator.ReadPackedGuid("Caster GUID", i) : new WowGuid64();

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Duration))
            {
                aura.MaxDuration = packet.Translator.ReadInt32("Max Duration", i);
                aura.Duration = packet.Translator.ReadInt32("Duration", i);
            }
            else
            {
                aura.MaxDuration = 0;
                aura.Duration = 0;
            }

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Scalable))
            {
                // This aura is scalable with level/talents
                // Here we show each effect value after scaling
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex0))
                    packet.Translator.ReadInt32("Effect 0 Value", i);
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex1))
                    packet.Translator.ReadInt32("Effect 1 Value", i);
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex2))
                    packet.Translator.ReadInt32("Effect 2 Value", i);
            }

            packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");

            return aura;
        }

        private static Aura ReadAuraUpdateBlock505(Packet packet, int i)
        {
            var aura = new Aura
            {
                Slot = packet.Translator.ReadByte("Slot", i)
            };

            var id = packet.Translator.ReadInt32<SpellId>("Spell ID", i);
            if (id <= 0)
                return null;

            aura.SpellId = (uint)id;

            aura.AuraFlags = packet.Translator.ReadByteE<AuraFlagMoP>("Flags", i);

            var mask = packet.Translator.ReadUInt32("Effect Mask", i);

            aura.Level = (uint)packet.Translator.ReadInt16("Level", i);

            aura.Charges = packet.Translator.ReadByte("Charges", i);

            aura.CasterGuid = !aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) ? packet.Translator.ReadPackedGuid("Caster GUID", i) : new WowGuid64();

            if (aura.AuraFlags.HasAnyFlag(AuraFlagMoP.Duration))
            {
                aura.MaxDuration = packet.Translator.ReadInt32("Max Duration", i);
                aura.Duration = packet.Translator.ReadInt32("Duration", i);
            }
            else
            {
                aura.MaxDuration = 0;
                aura.Duration = 0;
            }

            if (aura.AuraFlags.HasAnyFlag(AuraFlagMoP.Scalable))
            {
                var b1 = packet.Translator.ReadByte("Effect Count", i);
                for (var j = 0; j < b1; ++j)
                    if (((1 << j) & mask) != 0)
                        packet.Translator.ReadSingle("Effect Value", i, j);
            }

            packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");

            return aura;
        }

        [HasSniffData] // in ReadAuraUpdateBlock and ReadAuraUpdateBlock505
        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = packet.Translator.ReadPackedGuid("GUID");
            var i = 0;
            var auras = new List<Aura>();
            while (packet.CanRead())
            {
                Aura aura;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_0_5_16048))
                    aura = ReadAuraUpdateBlock505(packet, i++);
                else
                    aura = ReadAuraUpdateBlock(packet, i++);

                if (aura != null)
                    auras.Add(aura);
            }

            // This only works if the parser saw UPDATE_OBJECT before this packet
            if (Storage.Objects.ContainsKey(guid))
            {
                var unit = Storage.Objects[guid].Item1 as Unit;
                if (unit != null)
                {
                    // If this is the first packet that sends auras
                    // (hopefully at spawn time) add it to the "Auras" field,
                    // if not create another row of auras in AddedAuras
                    // (similar to ChangedUpdateFields)

                    if (unit.Auras == null)
                        unit.Auras = auras;
                    else
                        unit.AddedAuras.Add(auras);
                }
            }
        }

        [Parser(Opcode.CMSG_CAST_SPELL)]
        public static void HandleCastSpell(Packet packet)
        {
            packet.Translator.ReadByte("Cast Count");
            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.Translator.ReadInt32("Glyph Index");

            var castFlags = packet.Translator.ReadByteE<CastFlag>("Cast Flags");
            if (castFlags.HasAnyFlag(CastFlag.HasTrajectory))
            {
                ReadSpellCastTargets(packet);
                HandleSpellMissileAndMove(packet);
            }
            else
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                    if (castFlags.HasAnyFlag(CastFlag.Unknown4))
                        HandleSpellMove(packet);

                ReadSpellCastTargets(packet);
            }
        }

        public static TargetFlag ReadSpellCastTargets(Packet packet)
        {
            var targetFlags = packet.Translator.ReadInt32E<TargetFlag>("Target Flags");

            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.CorpseEnemy | TargetFlag.GameObject |
                TargetFlag.CorpseAlly | TargetFlag.UnitMinipet))
                packet.Translator.ReadPackedGuid("Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.Translator.ReadPackedGuid("Item Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    packet.Translator.ReadPackedGuid("Source Transport GUID");

                packet.Translator.ReadVector3("Source Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
                    packet.Translator.ReadPackedGuid("Destination Transport GUID");

                packet.Translator.ReadVector3("Destination Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.Translator.ReadCString("Target String");

            return targetFlags;
        }

        public static void HandleSpellMissileAndMove(Packet packet) // 4.3.4
        {
            packet.Translator.ReadSingle("Elevation");
            packet.Translator.ReadSingle("Missile speed");
            if (ClientVersion.RemovedInVersion(ClientType.Cataclysm))
            {
                var opcode = packet.Translator.ReadInt32();
                // None length is recieved, so we have to calculate the remaining bytes.
                var remainingLength = packet.Length - packet.Position;
                var bytes = packet.Translator.ReadBytes((int)remainingLength);

                using (var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Formatter, packet.FileName))
                    Handler.Parse(newpacket, true);
            }
            else
                HandleSpellMove(packet);
        }

        public static void HandleSpellMove510(Packet packet)
        {
            var hasMovement = packet.Translator.ReadBool("Has Movement Data");
            if (hasMovement)
            {
                var guid = new byte[8];
                var transportGuid = new byte[8];
                var hasTransTime2 = false;
                var hasTransTime3 = false;
                var hasFallDirection = false;
                var pos = new Vector4();

                pos.Z = packet.Translator.ReadSingle();
                pos.X = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();

                guid[7] = packet.Translator.ReadBit();
                var hasTrans = packet.Translator.ReadBit("Has Transport");
                var hasFallData = packet.Translator.ReadBit("Has Fall Data");
                var hasField152 = !packet.Translator.ReadBit("Lacks field152");
                var hasMovementFlags = !packet.Translator.ReadBit();
                packet.Translator.ReadBit();
                guid[0] = packet.Translator.ReadBit();
                var hasMovementFlags2 = !packet.Translator.ReadBit();
                var hasO = !packet.Translator.ReadBit("Lacks Orientation");
                guid[2] = packet.Translator.ReadBit();
                var hasTime = !packet.Translator.ReadBit("Lacks Timestamp");
                guid[1] = packet.Translator.ReadBit();
                packet.Translator.ReadBit("Has Spline");
                guid[3] = packet.Translator.ReadBit();
                var unkLoopCounter = packet.Translator.ReadBits(24);
                guid[5] = packet.Translator.ReadBit();
                guid[6] = packet.Translator.ReadBit();
                var hasPitch = !packet.Translator.ReadBit("Lacks Pitch");
                guid[4] = packet.Translator.ReadBit();
                var hasSplineElev = !packet.Translator.ReadBit("Lacks Spline Elevation");
                packet.Translator.ReadBit();
                if (hasTrans)
                {
                    hasTransTime2 = packet.Translator.ReadBit();
                    transportGuid[2] = packet.Translator.ReadBit();
                    transportGuid[3] = packet.Translator.ReadBit();
                    transportGuid[4] = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();
                    transportGuid[5] = packet.Translator.ReadBit();
                    hasTransTime3 = packet.Translator.ReadBit();
                    transportGuid[6] = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();
                    transportGuid[1] = packet.Translator.ReadBit();
                }

                if (hasMovementFlags2)
                    packet.Translator.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("Has Fall Direction");

                if (hasMovementFlags)
                    packet.Translator.ReadBitsE<MovementFlag>("Movement Flags", 30);

                packet.Translator.ReadXORByte(guid, 1);
                packet.Translator.ReadXORByte(guid, 2);
                packet.Translator.ReadXORByte(guid, 5);
                for (var i = 0; i < unkLoopCounter; i++)
                    packet.Translator.ReadUInt32("Unk UInt32", i);

                packet.Translator.ReadXORByte(guid, 6);
                packet.Translator.ReadXORByte(guid, 7);
                packet.Translator.ReadXORByte(guid, 4);
                packet.Translator.ReadXORByte(guid, 0);
                packet.Translator.ReadXORByte(guid, 3);

                if (hasTrans)
                {
                    var tpos = new Vector4();
                    packet.Translator.ReadUInt32("Transport Time");
                    packet.Translator.ReadXORByte(transportGuid, 5);
                    packet.Translator.ReadSByte("Transport Seat");
                    tpos.O = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 7);
                    packet.Translator.ReadXORByte(transportGuid, 1);
                    tpos.Z = packet.Translator.ReadSingle();
                    tpos.X = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 6);
                    tpos.Y = packet.Translator.ReadSingle();
                    packet.Translator.ReadXORByte(transportGuid, 4);
                    packet.Translator.ReadXORByte(transportGuid, 2);

                    if (hasTransTime3)
                        packet.Translator.ReadUInt32("Transport Time 3");

                    if (hasTransTime2)
                        packet.Translator.ReadUInt32("Transport Time 2");

                    packet.Translator.ReadXORByte(transportGuid, 0);
                    packet.Translator.ReadXORByte(transportGuid, 3);

                    packet.Translator.WriteGuid("Transport Guid", transportGuid);
                    packet.AddValue("Transport Position", tpos);
                }

                if (hasTime)
                    packet.Translator.ReadUInt32("Timestamp");

                if (hasO)
                    pos.O = packet.Translator.ReadSingle();

                if (hasFallData)
                {
                    packet.Translator.ReadUInt32("Fall Time");
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Fall Sin");
                        packet.Translator.ReadSingle("Fall Cos");
                        packet.Translator.ReadSingle("Horizontal Speed");
                    }
                    packet.Translator.ReadSingle("Vertical Speed");
                }

                if (hasField152)
                    packet.Translator.ReadUInt32("Unk field152");

                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch");

                if (hasSplineElev)
                    packet.Translator.ReadSingle("Spline Elevation");

                packet.Translator.WriteGuid("Guid", guid);
                packet.AddValue("Position", pos);
            }
        }

        public static void HandleSpellMove(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
            {
                HandleSpellMove510(packet);
                return;
            }

            var hasMovement = packet.Translator.ReadBool("Has Movement Data");
            if (hasMovement)
            {
                var guid = new byte[8];
                var transportGuid = new byte[8];
                var hasTransTime2 = false;
                var hasTransTime3 = false;
                var hasFallDirection = false;
                var pos = new Vector4();

                pos.Z = packet.Translator.ReadSingle();
                pos.Y = packet.Translator.ReadSingle();
                pos.X = packet.Translator.ReadSingle();

                var bytes = new byte[8];
                var hasFallData = packet.Translator.ReadBit("Has fall data");
                var hasTime = !packet.Translator.ReadBit("Has timestamp");
                var hasO = !packet.Translator.ReadBit("Has O");
                packet.Translator.ReadBit("Has Spline");
                packet.Translator.ReadBit();
                guid[6] = bytes[4] = packet.Translator.ReadBit(); //6
                guid[4] = bytes[5] = packet.Translator.ReadBit(); //4
                var hasMovementFlags2 = !packet.Translator.ReadBit();
                var bytes2 = new byte[8];
                guid[3] = bytes2[0] = packet.Translator.ReadBit(); //3
                guid[5] = bytes2[1] = packet.Translator.ReadBit(); //5
                var hasSplineElev = !packet.Translator.ReadBit("Has Spline Elevation");
                var hasPitch = !packet.Translator.ReadBit("Has Pitch");
                guid[7] = bytes2[6] = packet.Translator.ReadBit(); //7
                var hasTrans = packet.Translator.ReadBit("Has transport");
                guid[2] = bytes2[5] = packet.Translator.ReadBit(); //2
                var hasMovementFlags = !packet.Translator.ReadBit();
                guid[1] = packet.Translator.ReadBit();
                guid[0] = packet.Translator.ReadBit();
                if (hasTrans)
                {
                    transportGuid[6] = packet.Translator.ReadBit();//54
                    transportGuid[2] = packet.Translator.ReadBit();//50
                    transportGuid[5] = packet.Translator.ReadBit();//53
                    hasTransTime2 = packet.Translator.ReadBit();
                    transportGuid[7] = packet.Translator.ReadBit();//55
                    transportGuid[4] = packet.Translator.ReadBit();//52
                    hasTransTime3 = packet.Translator.ReadBit();
                    transportGuid[0] = packet.Translator.ReadBit();//48
                    transportGuid[1] = packet.Translator.ReadBit();//49
                    transportGuid[3] = packet.Translator.ReadBit();//51
                }

                if (hasMovementFlags2)
                    packet.Translator.ReadBitsE<MovementFlagExtra>("Movement flags extra", 12);

                if (hasMovementFlags)
                    packet.Translator.ReadBitsE<MovementFlag>("Movement flags", 30);

                if (hasFallData)
                    hasFallDirection = packet.Translator.ReadBit("hasFallDirection");

                packet.Translator.ParseBitStream(guid, 1, 4, 7, 3, 0, 2, 5, 6);

                if (hasTrans)
                {
                    var tpos = new Vector4();
                    packet.Translator.ReadSByte("Transport Seat");
                    tpos.O = packet.Translator.ReadSingle();
                    packet.Translator.ReadUInt32("Transport Time");

                    packet.Translator.ReadXORByte(transportGuid, 6);
                    packet.Translator.ReadXORByte(transportGuid, 5);

                    if (hasTransTime2)
                        packet.Translator.ReadUInt32("Transport Time 2");

                    tpos.X = packet.Translator.ReadSingle();

                    packet.Translator.ReadXORByte(transportGuid, 4);

                    tpos.Z = packet.Translator.ReadSingle();

                    packet.Translator.ReadXORByte(transportGuid, 2);
                    packet.Translator.ReadXORByte(transportGuid, 0);

                    if (hasTransTime3)
                        packet.Translator.ReadUInt32("Transport Time 3");

                    packet.Translator.ReadXORByte(transportGuid, 1);
                    packet.Translator.ReadXORByte(transportGuid, 3);

                    tpos.Y = packet.Translator.ReadSingle();

                    packet.Translator.ReadXORByte(transportGuid, 7);

                    packet.Translator.WriteGuid("Transport Guid", transportGuid);
                    packet.AddValue("Transport Position", tpos);
                }

                if (hasO)
                    pos.O = packet.Translator.ReadSingle();

                if (hasSplineElev)
                    packet.Translator.ReadSingle("Spline elevation");

                if (hasFallData)
                {
                    packet.Translator.ReadUInt32("Fall time");
                    if (hasFallDirection)
                    {
                        packet.Translator.ReadSingle("Fall Cos");
                        packet.Translator.ReadSingle("Fall Sin");
                        packet.Translator.ReadSingle("Horizontal Speed");
                    }
                    packet.Translator.ReadSingle("Vertical Speed");
                }

                if (hasTime)
                    packet.Translator.ReadUInt32("Timestamp");

                if (hasPitch)
                    packet.Translator.ReadSingle("Pitch");

                packet.Translator.WriteGuid("Guid", guid);
                packet.AddValue("Position", pos);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellStart(Packet packet)
        {
            bool isSpellGo = packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO, Direction.ServerToClient);

            var casterGUID = packet.Translator.ReadPackedGuid("Caster GUID");
            packet.Translator.ReadPackedGuid("Caster Unit GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.Translator.ReadByte("Cast Count");

            var spellId = packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056) && !isSpellGo)
                packet.Translator.ReadByte("Cast Count");

            CastFlag flags;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                flags = packet.Translator.ReadInt32E<CastFlag>("Cast Flags");
            else
                flags = packet.Translator.ReadUInt16E<CastFlag>("Cast Flags");

            packet.Translator.ReadUInt32("Time");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.Translator.ReadUInt32("Time2");

            if (isSpellGo)
            {
                var hitCount = packet.Translator.ReadByte("Hit Count");
                for (var i = 0; i < hitCount; i++)
                    packet.Translator.ReadGuid("Hit GUID", i);

                var missCount = packet.Translator.ReadByte("Miss Count");
                for (var i = 0; i < missCount; i++)
                {
                    packet.Translator.ReadGuid("Miss GUID", i);

                    var missType = packet.Translator.ReadByteE<SpellMissType>("Miss Type", i);
                    if (missType == SpellMissType.Reflect)
                        packet.Translator.ReadByteE<SpellMissType>("Miss Reflect", i);
                }
            }

            var targetFlags = packet.Translator.ReadInt32E<TargetFlag>("Target Flags");

            WowGuid targetGUID = new WowGuid64();
            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.CorpseEnemy | TargetFlag.GameObject |
                TargetFlag.CorpseAlly | TargetFlag.UnitMinipet))
                targetGUID = packet.Translator.ReadPackedGuid("Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.Translator.ReadPackedGuid("Item Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    packet.Translator.ReadPackedGuid("Source Transport GUID");

                packet.Translator.ReadVector3("Source Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
                    packet.Translator.ReadPackedGuid("Destination Transport GUID");

                packet.Translator.ReadVector3("Destination Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.Translator.ReadCString("Target String");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (flags.HasAnyFlag(CastFlag.PredictedPower))
                {
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                    {
                        var count = packet.Translator.ReadUInt32("Modified Power Count");
                        for (var i = 0; i < count; i++)
                        {
                            packet.Translator.ReadUInt32E<PowerType>("Power Type", i);
                            packet.Translator.ReadInt32("Power Value", i);
                        }
                    }
                    else
                        packet.Translator.ReadInt32("Rune Cooldown");
                }

                if (flags.HasAnyFlag(CastFlag.RuneInfo))
                {
                    var spellRuneState = packet.Translator.ReadByte("Spell Rune State");
                    var playerRuneState = packet.Translator.ReadByte("Player Rune State");

                    for (var i = 0; i < 6; i++)
                    {
                        if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_2_2_14545))
                        {
                            var mask = 1 << i;
                            if ((mask & spellRuneState) == 0)
                                continue;

                            if ((mask & playerRuneState) != 0)
                                continue;
                        }

                        packet.Translator.ReadByte("Rune Cooldown Passed", i);
                    }
                }

                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.AdjustMissile))
                    {
                        packet.Translator.ReadSingle("Elevation");
                        packet.Translator.ReadInt32("Delay time");
                    }
                }
            }

            if (flags.HasAnyFlag(CastFlag.Projectile))
            {
                packet.Translator.ReadInt32("Ammo Display ID");
                packet.Translator.ReadInt32E<InventoryType>("Ammo Inventory Type");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.VisualChain))
                    {
                        packet.Translator.ReadInt32("Unk Int32 2");
                        packet.Translator.ReadInt32("Unk Int32 3");
                    }

                    if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
                        packet.Translator.ReadSByte("Unk Byte 2"); // Some count

                    if (targetFlags.HasAnyFlag(TargetFlag.ExtraTargets))
                    {
                        var targetCount = packet.Translator.ReadInt32("Extra Targets Count");
                        for (var i = 0; i < targetCount; i++)
                        {
                            packet.Translator.ReadVector3("Extra Target Position", i);
                            packet.Translator.ReadGuid("Extra Target GUID", i);
                        }
                    }
                }
                else
                {
                    if (flags.HasAnyFlag(CastFlag.Immunity))
                    {
                        packet.Translator.ReadInt32("CastSchoolImmunities");
                        packet.Translator.ReadInt32("CastImmunities");
                    }

                    if (flags.HasAnyFlag(CastFlag.HealPrediction))
                    {
                        packet.Translator.ReadInt32<SpellId>("Predicted Spell ID");

                        if (packet.Translator.ReadByte("Unk Byte") == 2)
                            packet.Translator.ReadPackedGuid("Unk Guid");
                    }
                }
            }

            if (flags.HasAnyFlag(CastFlag.Unknown21) && !isSpellGo)
            {
                NpcSpellClick spellClick = new NpcSpellClick
                {
                    SpellID = (uint) spellId,
                    CasterGUID = casterGUID,
                    TargetGUID = targetGUID
                };

                Storage.SpellClicks.Add(spellClick, packet.TimeSpan);
            }

            if (isSpellGo)
                packet.AddSniffData(StoreNameType.Spell, spellId, "SPELL_GO");
        }

        [Parser(Opcode.SMSG_LEARNED_SPELL)]
        public static void HandleLearnedSpell(Packet packet)
        {
            packet.Translator.ReadInt32<SpellId>("Spell ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    packet.Translator.ReadInt32("Unk Int32");
                else
                    packet.Translator.ReadInt16("Unk Int16");
            }
        }

        [Parser(Opcode.CMSG_UPDATE_PROJECTILE_POSITION)]
        public static void HandleUpdateProjectilePosition(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32<SpellId>("Spell ID");
            packet.Translator.ReadByte("Cast ID");
            packet.Translator.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_SET_PROJECTILE_POSITION)]
        public static void HandleSetProjectilePosition(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Cast ID");
            packet.Translator.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS, ClientVersionBuild.Zero, ClientVersionBuild.V3_1_0_9767)]
        public static void HandleRemovedSpell(Packet packet)
        {
            packet.Translator.ReadUInt16<SpellId>("Spell ID");
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        [Parser(Opcode.SMSG_UNLEARNED_SPELLS, ClientVersionBuild.V3_1_0_9767)]
        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_PLAY_SPELL_IMPACT)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadUInt32("SpellVisualKit ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCastVisual434(Packet packet)
        {
            packet.Translator.ReadSingle("Z");
            packet.Translator.ReadInt32("SpellVisualKit ID"); // not confirmed
            packet.Translator.ReadInt16("Unk Int16 1"); // usually 0
            packet.Translator.ReadSingle("O");
            packet.Translator.ReadSingle("X");
            packet.Translator.ReadInt16("Unk Int16 2"); // usually 0
            packet.Translator.ReadSingle("Y");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = packet.Translator.ReadBit();
            guid1[3] = packet.Translator.ReadBit();
            guid1[0] = packet.Translator.ReadBit();
            guid2[2] = packet.Translator.ReadBit();
            guid2[5] = packet.Translator.ReadBit();
            guid1[2] = packet.Translator.ReadBit();
            guid1[4] = packet.Translator.ReadBit();
            guid2[6] = packet.Translator.ReadBit();

            guid2[0] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Unk Bit"); // usually 1
            guid1[6] = packet.Translator.ReadBit();
            guid2[7] = packet.Translator.ReadBit();
            guid1[5] = packet.Translator.ReadBit();
            guid1[1] = packet.Translator.ReadBit();
            guid1[7] = packet.Translator.ReadBit();
            guid2[3] = packet.Translator.ReadBit();

            guid2[4] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid1, 7);
            packet.Translator.ReadXORByte(guid1, 4);
            packet.Translator.ReadXORByte(guid2, 7);
            packet.Translator.ReadXORByte(guid1, 1);
            packet.Translator.ReadXORByte(guid1, 3);
            packet.Translator.ReadXORByte(guid1, 0);
            packet.Translator.ReadXORByte(guid1, 6);
            packet.Translator.ReadXORByte(guid2, 0);
            packet.Translator.ReadXORByte(guid2, 4);
            packet.Translator.ReadXORByte(guid1, 5);
            packet.Translator.ReadXORByte(guid2, 1);
            packet.Translator.ReadXORByte(guid2, 5);
            packet.Translator.ReadXORByte(guid2, 6);
            packet.Translator.ReadXORByte(guid2, 2);
            packet.Translator.ReadXORByte(guid1, 2);
            packet.Translator.ReadXORByte(guid2, 3);

            packet.Translator.WriteGuid("Caster Guid", guid1);
            packet.Translator.WriteGuid("Unk Guid", guid2);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCastVisualKit430(Packet packet)
        {
            packet.Translator.ReadUInt32("SpellVisualKit ID");
            packet.Translator.ReadUInt32("Unk");
            packet.Translator.ReadUInt32("Unk");

            var guid = packet.Translator.StartBitStream(0, 4, 3, 6, 5, 7, 2, 1);
            packet.Translator.ParseBitStream(guid, 5, 7, 6, 1, 4, 3, 2, 0);
            packet.Translator.WriteGuid("Caster Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT, ClientVersionBuild.V4_3_4_15595)] // 4.3.4
        public static void HandleCastVisualKit434(Packet packet)
        {
            packet.Translator.ReadUInt32("Unk");
            packet.Translator.ReadUInt32("SpellVisualKit ID");
            packet.Translator.ReadUInt32("Unk");

            var guid = packet.Translator.StartBitStream(4, 7, 5, 3, 1, 2, 0, 6);
            packet.Translator.ParseBitStream(guid, 0, 4, 1, 6, 7, 2, 3, 5);
            packet.Translator.WriteGuid("Caster Guid", guid);
        }

        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                packet.Translator.ReadByte("Cast count");

            packet.Translator.ReadUInt32<SpellId>("Spell ID");

            var result = packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");

            if (ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing))
                packet.Translator.ReadByte("Cast count");

            switch (result)
            {
                case SpellCastFailureReason.NeedExoticAmmo:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Item SubclassMask");
                    break;
                case SpellCastFailureReason.TooManyOfItem:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Limit");
                    break;
                case SpellCastFailureReason.Totems:
                case SpellCastFailureReason.TotemCategory:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Totem 1");
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Totem 2");
                    break;
                case SpellCastFailureReason.Reagents:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Reagent ID");
                    break;
                case SpellCastFailureReason.RequiresSpellFocus:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Spell Focus");
                    break;
                case SpellCastFailureReason.RequiresArea:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32<AreaId>("Area ID");
                    break;
                case SpellCastFailureReason.CustomError:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Error ID");
                    break;
                case SpellCastFailureReason.PreventedByMechanic:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32E<SpellMechanic>("Mechanic");
                    break;
                case SpellCastFailureReason.EquippedItemClass:
                case SpellCastFailureReason.EquippedItemClassMainhand:
                case SpellCastFailureReason.EquippedItemClassOffhand:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32E<ItemClass>("Class");
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("SubclassMask");
                    break;
                case SpellCastFailureReason.MinSkill:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Skill Type"); // SkillLine.dbc
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Required Amount");
                    break;
                case SpellCastFailureReason.FishingTooLow:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Required fishing skill");
                    break;
                // Following is post 3.3.5a
                case SpellCastFailureReason.NotReady:
                    if (packet.CanRead())
                        packet.Translator.ReadInt32("Extra Cast Number");
                    break;
                case SpellCastFailureReason.Silenced:
                case SpellCastFailureReason.NotStanding:
                    if (packet.CanRead())
                        packet.Translator.ReadInt32("Unk");
                    break;
                default:
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Unknown1");
                    if (packet.CanRead())
                        packet.Translator.ReadUInt32("Unknown2");
                    break;
            }
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Guid");
            packet.Translator.ReadByte("Cast count");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadByteE<SpellCastFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE)]
        [Parser(Opcode.SMSG_PROC_RESIST)]
        public static void HandleSpellProcResist(Packet packet)
        {
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadBool("Debug Log");

            // not found in 3.3.5a sniff nor 3.3.3a
            //if (ClientVersion.Build == ClientVersionBuild.V3_3_5a_12340) // blizzard retardness, client doesn't even read this
            //{
            //    packet.Translator.ReadSingle("Unk");
            //    packet.Translator.ReadSingle("Unk");
            //}
        }

        [Parser(Opcode.MSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadUInt32("Timestamp");
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadInt32("Duration");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                return;

            if (packet.Translator.ReadBool("Has castflag Immunity"))
            {
                packet.Translator.ReadUInt32("CastSchoolImmunities");
                packet.Translator.ReadUInt32("CastImmunities");
            }

            if (packet.Translator.ReadBool("Has castflag HealPrediction"))
            {
                packet.Translator.ReadPackedGuid("Target GUID");
                packet.Translator.ReadInt32("Heal Amount");

                var type = packet.Translator.ReadByte("Type");
                if (type == 2)
                    packet.Translator.ReadPackedGuid("Unk GUID");
            }
        }

        [Parser(Opcode.SMSG_BREAK_TARGET)]
        [Parser(Opcode.SMSG_DISMOUNT)]
        public static void HandleDismount(Packet packet)
        {
            packet.Translator.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_MOUNT_RESULT)]
        public static void HandleMountResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.Translator.ReadPackedGuid("GUID");
            packet.Translator.ReadInt32E<MountResult>("Result");
        }

        [Parser(Opcode.SMSG_DISMOUNT_RESULT)]
        public static void HandleDismountResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)) // Don't know for previous.
                packet.Translator.ReadInt32E<DismountResult>("Result");
        }

        [Parser(Opcode.CMSG_GET_MIRROR_IMAGE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.Translator.ReadUInt32("Display Id");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Display ID");
            var race = packet.Translator.ReadByteE<Race>("Race");

            if (race == Race.None)
                return;

            packet.Translator.ReadByteE<Gender>("Gender");
            packet.Translator.ReadByteE<Class>("Class");

            if (!packet.CanRead())
                return;

            packet.Translator.ReadByte("Skin");
            packet.Translator.ReadByte("Face");
            packet.Translator.ReadByte("Hair Style");
            packet.Translator.ReadByte("Hair Color");
            packet.Translator.ReadByte("Facial Hair");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.Translator.ReadGuid("Guild Guid");
            else
                packet.Translator.ReadUInt32("Guild Id");

            EquipmentSlotType[] slots = {
                EquipmentSlotType.Head, EquipmentSlotType.Shoulders, EquipmentSlotType.Shirt,
                EquipmentSlotType.Chest, EquipmentSlotType.Waist, EquipmentSlotType.Legs,
                EquipmentSlotType.Feet, EquipmentSlotType.Wrists, EquipmentSlotType.Hands,
                EquipmentSlotType.Back, EquipmentSlotType.Tabard };

            for (var i = 0; i < 11; ++i)
                packet.Translator.ReadUInt32<ItemId>("ItemEntry", slots[i]);
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN)]
        public static void HandleClearCooldown(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWNS)] // 4.3.4
        public static void HandleClearCooldowns(Packet packet)
        {
            var guid = new byte[8];
            guid[1] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            var count = packet.Translator.ReadBits("Spell Count", 24);
            guid[7] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 3);
            for (var i = 0; i < count; i++)
                packet.Translator.ReadUInt32<SpellId>("Spell ID", i);

            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 6);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Unk mask");
            while (packet.CanRead())
            {
                packet.Translator.ReadUInt32<SpellId>("Spell ID");
                packet.Translator.ReadInt32("Time");
            }
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetSpellModifier(Packet packet)
        {
            packet.Translator.ReadByte("Spell Mask bitpos");
            packet.Translator.ReadByteE<SpellModOp>("Spell Mod");
            packet.Translator.ReadInt32("Amount");
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetSpellModifierFlat406(Packet packet)
        {
            var modCount = packet.Translator.ReadUInt32("Modifier type count");
            for (var j = 0; j < modCount; ++j)
            {
                var modTypeCount = packet.Translator.ReadUInt32("Count", j);
                packet.Translator.ReadByteE<SpellModOp>("Spell Mod", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    packet.Translator.ReadByte("Spell Mask bitpos", j, i);
                    packet.Translator.ReadSingle("Amount", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadUInt32<SpellId>("Dispelling Spell ID");

            for (var i = 0; packet.CanRead(); i++)
                packet.Translator.ReadUInt32<SpellId>("Dispelled Spell ID", i);
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.Translator.ReadByte("Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005)) // guessing, present on 4.3.4
                packet.Translator.ReadGuid("Unk");
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.Translator.ReadByte("Slot");
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadUInt32("Duration");
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            packet.Translator.ReadByte("Count");
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
        }

        [Parser(Opcode.SMSG_UNIT_SPELLCAST_START)]
        public static void HandleUnitSpellcastStart(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Caster GUID");
            packet.Translator.ReadPackedGuid("Target GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell Id");
            packet.Translator.ReadInt32("Time Casted");
            packet.Translator.ReadInt32("Cast time");

            if (packet.Translator.ReadBool("Unknown bool"))
            {
                packet.Translator.ReadUInt32("Unk");
                packet.Translator.ReadUInt32("Unk");
            }
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.Translator.ReadPackedGuid("Caster GUID");
            packet.Translator.ReadInt32("Delay Time");
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            var count = packet.Translator.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.Translator.ReadGuid("Chain target");
        }

        [Parser(Opcode.SMSG_AURACASTLOG)]
        public static void HandleAuraCastLog(Packet packet)
        {
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadSingle("Unk 1");
            packet.Translator.ReadSingle("Unk 2");
        }


        [Parser(Opcode.SMSG_SPELL_CHANCE_PROC_LOG)]
        public static void HandleChanceProcLog(Packet packet)
        {
            packet.Translator.ReadGuid("Caster GUID");
            packet.Translator.ReadGuid("Target GUID");
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadSingle("unk1");
            packet.Translator.ReadSingle("unk2");
            packet.Translator.ReadUInt32E<UnknownFlags>("ProcFlags");
            packet.Translator.ReadUInt32E<UnknownFlags>("Flags2");
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.Translator.ReadBits("Count", 23);

            for (int i = 0; i < count; ++i)
            {
                packet.Translator.ReadInt32("Category Cooldown");
                packet.Translator.ReadInt32("Cooldown");
            }
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(2, 4, 1, 7, 5, 0, 3, 6);

            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadByte("Points?");
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadByte("Slot ID?");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_DISENCHANT_CREDIT)] // 4.3.4
        public static void HandleDisenchantCredit(Packet packet)
        {
            var guid = packet.Translator.StartBitStream(0, 6, 3, 1, 7, 5, 2, 4);

            packet.Translator.ReadUInt32<ItemId>("Item Entry");

            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 1);

            packet.Translator.ReadInt32("Unk Int32");
            packet.Translator.ReadInt32("Unk Int32");

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 0);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MISSILE_CANCEL)] // 4.3.4
        public static void HandleMissileCancel(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Cancel");
            guid[1] = packet.Translator.ReadBit();
            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[5] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadXORByte(guid, 4);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 5);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadXORByte(guid, 3);

            packet.Translator.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        public static void HandleMiscGuid(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN)]
        public static void HandleModifyCooldown(Packet packet)
        {
            packet.Translator.ReadUInt32<SpellId>("Spell ID");
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadInt32("Cooldown Mod");
        }

        [Parser(Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA)]
        [Parser(Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL)]
        [Parser(Opcode.CMSG_CANCEL_GROWTH_AURA)]
        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        [Parser(Opcode.CMSG_REQUEST_CATEGORY_COOLDOWNS)]
        public static void HandleSpellNull(Packet packet)
        {
        }
    }
}
