using System;
using System.Collections.Generic;
using System.Globalization;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Parsing.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)] // 4.3.4
        public static void HandleSpellInterruptLog(Packet packet)
        {
            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[4] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[6] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid1, 4);
            packet.ReadInt32<SpellId>("Interrupt Spell ID");
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid1, 1);
            packet.ReadInt32<SpellId>("Interrupted Spell ID");
            packet.ReadXORByte(guid2, 5);

            packet.WriteGuid("GUID 1", guid1);
            packet.WriteGuid("GUID 2", guid1);
        }

        [Parser(Opcode.SMSG_PLAYER_BOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32<AreaId>("Area ID");
        }

        [Parser(Opcode.CMSG_CANCEL_TEMP_ENCHANTMENT)]
        public static void HandleCancelTempEnchantment(Packet packet)
        {
            packet.ReadUInt32("Slot");
        }

        [Parser(Opcode.SMSG_SUPERCEDED_SPELLS)]
        public static void HandleSupercededSpell(Packet packet)
        {
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadInt32<SpellId>("Next Spell ID");
        }

        [Parser(Opcode.SMSG_RESYNC_RUNES)]
        public static void HandleResyncRunes(Packet packet)
        {
            var count = packet.ReadUInt32("Count");
            for (var i = 0; i < count; ++i)
            {
                packet.ReadByte("Rune Type");
                packet.ReadByte("Cooldown Time");
            }
        }

        [Parser(Opcode.SMSG_CONVERT_RUNE)]
        public static void HandleConvertRune(Packet packet)
        {
            packet.ReadByte("Index");
            packet.ReadByte("New Rune Type");
        }

        [Parser(Opcode.SMSG_ADD_RUNE_POWER)]
        public static void HandleAddRunePower(Packet packet)
        {
            packet.ReadUInt32("Mask?"); // TC: 1 << index
        }

        [Parser(Opcode.SMSG_COOLDOWN_CHEAT)]
        public static void HandleCooldownCheat(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_COOLDOWN_EVENT)]
        public static void HandleCooldownEvent(Packet packet)
        {
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_NOTIFY_DEST_LOC_SPELL_CAST)]
        public static void HandleNotifyDestLocSpellCast(Packet packet)
        {
            // TODO: Verify and/or finish this
            // Everything is guessed
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadVector3("Position");
            packet.ReadVector3("Target Position");
            packet.ReadSingle("Elevation");
            packet.ReadSingle("Speed");
            packet.ReadUInt32("Duration");
            packet.ReadInt32("Unk");

            if (packet.Length == 64) // packet always has length 64 length except for some rare exceptions with length 60 (hardcoded in the client)
                packet.ReadSingle("Unk");
        }

        [Parser(Opcode.SMSG_WEEKLY_SPELL_USAGE)]
        public static void HandleWeeklySpellUsage(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Unk Int32");
                packet.ReadByte("Unk Int8");
            }
        }

        [Parser(Opcode.SMSG_SEND_UNLEARN_SPELLS)]
        public static void HandleSendUnlearnSpells(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.ReadInt32<SpellId>("Spell ID", i);
        }

        [Parser(Opcode.SMSG_RESUME_CAST_BAR)]
        public static void HandleResumeCastBar(Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");

            packet.ReadPackedGuid("Target GUID");

            packet.ReadInt32<SpellId>("Spell ID");

            packet.ReadInt32("Cast Time");

            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS, ClientVersionBuild.Zero, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleInitialSpells(Packet packet)
        {
            packet.ReadByte("Talent Spec");

            var count = packet.ReadUInt16("Spell Count");
            var spells = new List<uint>(count);
            for (var i = 0; i < count; i++)
            {
                uint spellId;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    spellId = (uint) packet.ReadInt32<SpellId>("Spell ID", i);
                else
                    spellId = packet.ReadUInt16<SpellId>("Spell ID", i);

                packet.ReadInt16("Unk Int16", i);

                spells.Add(spellId);
            }

            var startSpell = new StartSpell {Spells = spells};

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }

            var cooldownCount = packet.ReadUInt16("Cooldown Count");
            for (var i = 0; i < cooldownCount; i++)
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    packet.ReadInt32<SpellId>("Cooldown Spell ID", i);
                else
                    packet.ReadUInt16<SpellId>("Cooldown Spell ID", i);

                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                    packet.ReadInt32("Cooldown Cast Item ID", i);
                else
                    packet.ReadUInt16("Cooldown Cast Item ID", i);

                packet.ReadUInt16("Cooldown Spell Category", i);
                packet.ReadInt32("Cooldown Time", i);
                var catCd = packet.ReadUInt32();
                packet.AddValue("Cooldown Category Time", ((catCd >> 31) != 0 ? "Infinite" : (catCd & 0x7FFFFFFF).ToString(CultureInfo.InvariantCulture)), i);
            }
        }

        [Parser(Opcode.SMSG_SEND_KNOWN_SPELLS, ClientVersionBuild.V5_1_0_16309)]
        public static void HandleInitialSpells510(Packet packet)
        {
            var count = packet.ReadBits("Spell Count", 24);
            packet.ReadBit("InitialLogin");
            packet.ResetBitReader();

            var spells = new List<uint>((int)count);
            for (var i = 0; i < count; i++)
            {
                var spellId = packet.ReadUInt32<SpellId>("Spell ID", i);
                spells.Add(spellId);
            }

            var startSpell = new StartSpell { Spells = spells };

            WoWObject character;
            if (Storage.Objects.TryGetValue(SessionHandler.LoginGuid, out character))
            {
                var player = character as Player;
                if (player != null && player.FirstLogin)
                    Storage.StartSpells.Add(new Tuple<Race, Class>(player.Race, player.Class), startSpell, packet.TimeSpan);
            }
        }

        private static Aura ReadAuraUpdateBlock(Packet packet, int i)
        {
            var aura = new Aura
            {
                Slot = packet.ReadByte("Slot", i)
            };

            var id = packet.ReadInt32<SpellId>("Spell ID", i);
            if (id <= 0)
                return null;
            aura.SpellId = (uint)id;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
                aura.AuraFlags = packet.ReadInt16E<AuraFlag>("Flags", i);
            else
                aura.AuraFlags = packet.ReadByteE<AuraFlag>("Flags", i);

            aura.Level = packet.ReadByte("Level", i);

            aura.Charges = packet.ReadByte("Charges", i);

            aura.CasterGuid = !aura.AuraFlags.HasAnyFlag(AuraFlag.NotCaster) ? packet.ReadPackedGuid("Caster GUID", i) : new WowGuid64();

            if (aura.AuraFlags.HasAnyFlag(AuraFlag.Duration))
            {
                aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                aura.Duration = packet.ReadInt32("Duration", i);
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
                    packet.ReadInt32("Effect 0 Value", i);
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex1))
                    packet.ReadInt32("Effect 1 Value", i);
                if (aura.AuraFlags.HasAnyFlag(AuraFlag.EffectIndex2))
                    packet.ReadInt32("Effect 2 Value", i);
            }

            packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");

            return aura;
        }

        private static Aura ReadAuraUpdateBlock505(Packet packet, int i)
        {
            var aura = new Aura
            {
                Slot = packet.ReadByte("Slot", i)
            };

            var id = packet.ReadInt32<SpellId>("Spell ID", i);
            if (id <= 0)
                return null;

            aura.SpellId = (uint)id;

            aura.AuraFlags = packet.ReadByteE<AuraFlagMoP>("Flags", i);

            var mask = packet.ReadUInt32("Effect Mask", i);

            aura.Level = (uint)packet.ReadInt16("Level", i);

            aura.Charges = packet.ReadByte("Charges", i);

            aura.CasterGuid = !aura.AuraFlags.HasAnyFlag(AuraFlagMoP.NoCaster) ? packet.ReadPackedGuid("Caster GUID", i) : new WowGuid64();

            if (aura.AuraFlags.HasAnyFlag(AuraFlagMoP.Duration))
            {
                aura.MaxDuration = packet.ReadInt32("Max Duration", i);
                aura.Duration = packet.ReadInt32("Duration", i);
            }
            else
            {
                aura.MaxDuration = 0;
                aura.Duration = 0;
            }

            if (aura.AuraFlags.HasAnyFlag(AuraFlagMoP.Scalable))
            {
                var b1 = packet.ReadByte("Effect Count", i);
                for (var j = 0; j < b1; ++j)
                    if (((1 << j) & mask) != 0)
                        packet.ReadSingle("Effect Value", i, j);
            }

            packet.AddSniffData(StoreNameType.Spell, (int)aura.SpellId, "AURA_UPDATE");

            return aura;
        }

        [HasSniffData] // in ReadAuraUpdateBlock and ReadAuraUpdateBlock505
        [Parser(Opcode.SMSG_AURA_UPDATE_ALL)]
        [Parser(Opcode.SMSG_AURA_UPDATE)]
        public static void HandleAuraUpdate(Packet packet)
        {
            var guid = packet.ReadPackedGuid("GUID");
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
            packet.ReadByte("Cast Count");
            packet.ReadInt32<SpellId>("Spell ID");

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                packet.ReadInt32("Glyph Index");

            var castFlags = packet.ReadByteE<CastFlag>("Cast Flags");
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
            var targetFlags = packet.ReadInt32E<TargetFlag>("Target Flags");

            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.CorpseEnemy | TargetFlag.GameObject |
                TargetFlag.CorpseAlly | TargetFlag.UnitMinipet))
                packet.ReadPackedGuid("Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.ReadPackedGuid("Item Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    packet.ReadPackedGuid("Source Transport GUID");

                packet.ReadVector3("Source Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
                    packet.ReadPackedGuid("Destination Transport GUID");

                packet.ReadVector3("Destination Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.ReadCString("Target String");

            return targetFlags;
        }

        public static void HandleSpellMissileAndMove(Packet packet) // 4.3.4
        {
            packet.ReadSingle("Elevation");
            packet.ReadSingle("Missile speed");
            if (ClientVersion.RemovedInVersion(ClientType.Cataclysm))
            {
                var opcode = packet.ReadInt32();
                // None length is recieved, so we have to calculate the remaining bytes.
                var remainingLength = packet.Length - packet.Position;
                var bytes = packet.ReadBytes((int)remainingLength);

                using (var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName))
                    Handler.Parse(newpacket, true);
            }
            else
                HandleSpellMove(packet);
        }

        public static void HandleSpellMove510(Packet packet)
        {
            var hasMovement = packet.ReadBool("Has Movement Data");
            if (hasMovement)
            {
                var guid = new byte[8];
                var transportGuid = new byte[8];
                var hasTransTime2 = false;
                var hasTransTime3 = false;
                var hasFallDirection = false;
                var pos = new Vector4();

                pos.Z = packet.ReadSingle();
                pos.X = packet.ReadSingle();
                pos.Y = packet.ReadSingle();

                guid[7] = packet.ReadBit();
                var hasTrans = packet.ReadBit("Has Transport");
                var hasFallData = packet.ReadBit("Has Fall Data");
                var hasField152 = !packet.ReadBit("Lacks field152");
                var hasMovementFlags = !packet.ReadBit();
                packet.ReadBit();
                guid[0] = packet.ReadBit();
                var hasMovementFlags2 = !packet.ReadBit();
                var hasO = !packet.ReadBit("Lacks Orientation");
                guid[2] = packet.ReadBit();
                var hasTime = !packet.ReadBit("Lacks Timestamp");
                guid[1] = packet.ReadBit();
                packet.ReadBit("Has Spline");
                guid[3] = packet.ReadBit();
                var unkLoopCounter = packet.ReadBits(24);
                guid[5] = packet.ReadBit();
                guid[6] = packet.ReadBit();
                var hasPitch = !packet.ReadBit("Lacks Pitch");
                guid[4] = packet.ReadBit();
                var hasSplineElev = !packet.ReadBit("Lacks Spline Elevation");
                packet.ReadBit();
                if (hasTrans)
                {
                    hasTransTime2 = packet.ReadBit();
                    transportGuid[2] = packet.ReadBit();
                    transportGuid[3] = packet.ReadBit();
                    transportGuid[4] = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();
                    transportGuid[5] = packet.ReadBit();
                    hasTransTime3 = packet.ReadBit();
                    transportGuid[6] = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();
                    transportGuid[1] = packet.ReadBit();
                }

                if (hasMovementFlags2)
                    packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 13);

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("Has Fall Direction");

                if (hasMovementFlags)
                    packet.ReadBitsE<MovementFlag>("Movement Flags", 30);

                packet.ReadXORByte(guid, 1);
                packet.ReadXORByte(guid, 2);
                packet.ReadXORByte(guid, 5);
                for (var i = 0; i < unkLoopCounter; i++)
                    packet.ReadUInt32("Unk UInt32", i);

                packet.ReadXORByte(guid, 6);
                packet.ReadXORByte(guid, 7);
                packet.ReadXORByte(guid, 4);
                packet.ReadXORByte(guid, 0);
                packet.ReadXORByte(guid, 3);

                if (hasTrans)
                {
                    var tpos = new Vector4();
                    packet.ReadUInt32("Transport Time");
                    packet.ReadXORByte(transportGuid, 5);
                    packet.ReadSByte("Transport Seat");
                    tpos.O = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 7);
                    packet.ReadXORByte(transportGuid, 1);
                    tpos.Z = packet.ReadSingle();
                    tpos.X = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 6);
                    tpos.Y = packet.ReadSingle();
                    packet.ReadXORByte(transportGuid, 4);
                    packet.ReadXORByte(transportGuid, 2);

                    if (hasTransTime3)
                        packet.ReadUInt32("Transport Time 3");

                    if (hasTransTime2)
                        packet.ReadUInt32("Transport Time 2");

                    packet.ReadXORByte(transportGuid, 0);
                    packet.ReadXORByte(transportGuid, 3);

                    packet.WriteGuid("Transport Guid", transportGuid);
                    packet.AddValue("Transport Position", tpos);
                }

                if (hasTime)
                    packet.ReadUInt32("Timestamp");

                if (hasO)
                    pos.O = packet.ReadSingle();

                if (hasFallData)
                {
                    packet.ReadUInt32("Fall Time");
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Fall Sin");
                        packet.ReadSingle("Fall Cos");
                        packet.ReadSingle("Horizontal Speed");
                    }
                    packet.ReadSingle("Vertical Speed");
                }

                if (hasField152)
                    packet.ReadUInt32("Unk field152");

                if (hasPitch)
                    packet.ReadSingle("Pitch");

                if (hasSplineElev)
                    packet.ReadSingle("Spline Elevation");

                packet.WriteGuid("Guid", guid);
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

            var hasMovement = packet.ReadBool("Has Movement Data");
            if (hasMovement)
            {
                var guid = new byte[8];
                var transportGuid = new byte[8];
                var hasTransTime2 = false;
                var hasTransTime3 = false;
                var hasFallDirection = false;
                var pos = new Vector4();

                pos.Z = packet.ReadSingle();
                pos.Y = packet.ReadSingle();
                pos.X = packet.ReadSingle();

                var bytes = new byte[8];
                var hasFallData = packet.ReadBit("Has fall data");
                var hasTime = !packet.ReadBit("Has timestamp");
                var hasO = !packet.ReadBit("Has O");
                packet.ReadBit("Has Spline");
                packet.ReadBit();
                guid[6] = bytes[4] = packet.ReadBit(); //6
                guid[4] = bytes[5] = packet.ReadBit(); //4
                var hasMovementFlags2 = !packet.ReadBit();
                var bytes2 = new byte[8];
                guid[3] = bytes2[0] = packet.ReadBit(); //3
                guid[5] = bytes2[1] = packet.ReadBit(); //5
                var hasSplineElev = !packet.ReadBit("Has Spline Elevation");
                var hasPitch = !packet.ReadBit("Has Pitch");
                guid[7] = bytes2[6] = packet.ReadBit(); //7
                var hasTrans = packet.ReadBit("Has transport");
                guid[2] = bytes2[5] = packet.ReadBit(); //2
                var hasMovementFlags = !packet.ReadBit();
                guid[1] = packet.ReadBit();
                guid[0] = packet.ReadBit();
                if (hasTrans)
                {
                    transportGuid[6] = packet.ReadBit();//54
                    transportGuid[2] = packet.ReadBit();//50
                    transportGuid[5] = packet.ReadBit();//53
                    hasTransTime2 = packet.ReadBit();
                    transportGuid[7] = packet.ReadBit();//55
                    transportGuid[4] = packet.ReadBit();//52
                    hasTransTime3 = packet.ReadBit();
                    transportGuid[0] = packet.ReadBit();//48
                    transportGuid[1] = packet.ReadBit();//49
                    transportGuid[3] = packet.ReadBit();//51
                }

                if (hasMovementFlags2)
                    packet.ReadBitsE<MovementFlagExtra>("Movement flags extra", 12);

                if (hasMovementFlags)
                    packet.ReadBitsE<MovementFlag>("Movement flags", 30);

                if (hasFallData)
                    hasFallDirection = packet.ReadBit("hasFallDirection");

                packet.ParseBitStream(guid, 1, 4, 7, 3, 0, 2, 5, 6);

                if (hasTrans)
                {
                    var tpos = new Vector4();
                    packet.ReadSByte("Transport Seat");
                    tpos.O = packet.ReadSingle();
                    packet.ReadUInt32("Transport Time");

                    packet.ReadXORByte(transportGuid, 6);
                    packet.ReadXORByte(transportGuid, 5);

                    if (hasTransTime2)
                        packet.ReadUInt32("Transport Time 2");

                    tpos.X = packet.ReadSingle();

                    packet.ReadXORByte(transportGuid, 4);

                    tpos.Z = packet.ReadSingle();

                    packet.ReadXORByte(transportGuid, 2);
                    packet.ReadXORByte(transportGuid, 0);

                    if (hasTransTime3)
                        packet.ReadUInt32("Transport Time 3");

                    packet.ReadXORByte(transportGuid, 1);
                    packet.ReadXORByte(transportGuid, 3);

                    tpos.Y = packet.ReadSingle();

                    packet.ReadXORByte(transportGuid, 7);

                    packet.WriteGuid("Transport Guid", transportGuid);
                    packet.AddValue("Transport Position", pos);
                }

                if (hasO)
                    pos.O = packet.ReadSingle();

                if (hasSplineElev)
                    packet.ReadSingle("Spline elevation");

                if (hasFallData)
                {
                    packet.ReadUInt32("Fall time");
                    if (hasFallDirection)
                    {
                        packet.ReadSingle("Fall Cos");
                        packet.ReadSingle("Fall Sin");
                        packet.ReadSingle("Horizontal Speed");
                    }
                    packet.ReadSingle("Vertical Speed");
                }

                if (hasTime)
                    packet.ReadUInt32("Timestamp");

                if (hasPitch)
                    packet.ReadSingle("Pitch");

                packet.WriteGuid("Guid", guid);
                packet.AddValue("Position", pos);
            }
        }

        [HasSniffData]
        [Parser(Opcode.SMSG_SPELL_START)]
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellStart(Packet packet)
        {
            bool isSpellGo = packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO, Direction.ServerToClient);

            var casterGUID = packet.ReadPackedGuid("Caster GUID");
            packet.ReadPackedGuid("Caster Unit GUID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                packet.ReadByte("Cast Count");

            var spellId = packet.ReadInt32<SpellId>("Spell ID");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V3_0_2_9056) && !isSpellGo)
                packet.ReadByte("Cast Count");

            CastFlag flags;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                flags = packet.ReadInt32E<CastFlag>("Cast Flags");
            else
                flags = packet.ReadUInt16E<CastFlag>("Cast Flags");

            packet.ReadUInt32("Time");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.ReadUInt32("Time2");

            if (isSpellGo)
            {
                var hitCount = packet.ReadByte("Hit Count");
                for (var i = 0; i < hitCount; i++)
                    packet.ReadGuid("Hit GUID", i);

                var missCount = packet.ReadByte("Miss Count");
                for (var i = 0; i < missCount; i++)
                {
                    packet.ReadGuid("Miss GUID", i);

                    var missType = packet.ReadByteE<SpellMissType>("Miss Type", i);
                    if (missType == SpellMissType.Reflect)
                        packet.ReadByteE<SpellMissType>("Miss Reflect", i);
                }
            }

            var targetFlags = packet.ReadInt32E<TargetFlag>("Target Flags");

            WowGuid targetGUID = new WowGuid64();
            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.CorpseEnemy | TargetFlag.GameObject |
                TargetFlag.CorpseAlly | TargetFlag.UnitMinipet))
                targetGUID = packet.ReadPackedGuid("Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.ReadPackedGuid("Item Target GUID");

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_2_0_10192))
                    packet.ReadPackedGuid("Source Transport GUID");

                packet.ReadVector3("Source Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_8_9464))
                    packet.ReadPackedGuid("Destination Transport GUID");

                packet.ReadVector3("Destination Position");
            }

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.ReadCString("Target String");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (flags.HasAnyFlag(CastFlag.PredictedPower))
                {
                    if (ClientVersion.AddedInVersion(ClientVersionBuild.V5_1_0_16309))
                    {
                        var count = packet.ReadUInt32("Modified Power Count");
                        for (var i = 0; i < count; i++)
                        {
                            packet.ReadUInt32E<PowerType>("Power Type", i);
                            packet.ReadInt32("Power Value", i);
                        }
                    }
                    else
                        packet.ReadInt32("Rune Cooldown");
                }

                if (flags.HasAnyFlag(CastFlag.RuneInfo))
                {
                    var spellRuneState = packet.ReadByte("Spell Rune State");
                    var playerRuneState = packet.ReadByte("Player Rune State");

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

                        packet.ReadByte("Rune Cooldown Passed", i);
                    }
                }

                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.AdjustMissile))
                    {
                        packet.ReadSingle("Elevation");
                        packet.ReadInt32("Delay time");
                    }
                }
            }

            if (flags.HasAnyFlag(CastFlag.Projectile))
            {
                packet.ReadInt32("Ammo Display ID");
                packet.ReadInt32E<InventoryType>("Ammo Inventory Type");
            }

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
            {
                if (isSpellGo)
                {
                    if (flags.HasAnyFlag(CastFlag.VisualChain))
                    {
                        packet.ReadInt32("Unk Int32 2");
                        packet.ReadInt32("Unk Int32 3");
                    }

                    if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
                        packet.ReadSByte("Unk Byte 2"); // Some count

                    if (targetFlags.HasAnyFlag(TargetFlag.ExtraTargets))
                    {
                        var targetCount = packet.ReadInt32("Extra Targets Count");
                        for (var i = 0; i < targetCount; i++)
                        {
                            packet.ReadVector3("Extra Target Position", i);
                            packet.ReadGuid("Extra Target GUID", i);
                        }
                    }
                }
                else
                {
                    if (flags.HasAnyFlag(CastFlag.Immunity))
                    {
                        packet.ReadInt32("CastSchoolImmunities");
                        packet.ReadInt32("CastImmunities");
                    }

                    if (flags.HasAnyFlag(CastFlag.HealPrediction))
                    {
                        packet.ReadInt32<SpellId>("Predicted Spell ID");

                        if (packet.ReadByte("Unk Byte") == 2)
                            packet.ReadPackedGuid("Unk Guid");
                    }
                }
            }

            if (flags.HasAnyFlag(CastFlag.Unknown21) && !isSpellGo)
            {
                var spellClick = new NpcSpellClick
                {
                    SpellId = (uint) spellId,
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
            packet.ReadInt32<SpellId>("Spell ID");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_3_0_10958))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                    packet.ReadInt32("Unk Int32");
                else
                    packet.ReadInt16("Unk Int16");
            }
        }

        [Parser(Opcode.CMSG_UPDATE_PROJECTILE_POSITION)]
        public static void HandleUpdateProjectilePosition(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32<SpellId>("Spell ID");
            packet.ReadByte("Cast ID");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_SET_PROJECTILE_POSITION)]
        public static void HandleSetProjectilePosition(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Cast ID");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.SMSG_UNLEARNED_SPELLS, ClientVersionBuild.Zero, ClientVersionBuild.V3_1_0_9767)]
        public static void HandleRemovedSpell(Packet packet)
        {
            packet.ReadUInt16<SpellId>("Spell ID");
        }

        [Parser(Opcode.CMSG_CANCEL_AURA)]
        [Parser(Opcode.SMSG_UNLEARNED_SPELLS, ClientVersionBuild.V3_1_0_9767)]
        [Parser(Opcode.CMSG_CANCEL_CHANNELLING)]
        public static void HandleRemovedSpell2(Packet packet)
        {
            packet.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL, ClientVersionBuild.Zero, ClientVersionBuild.V4_3_4_15595)]
        [Parser(Opcode.SMSG_PLAY_SPELL_IMPACT)]
        public static void HandleCastVisual(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadUInt32("SpellVisualKit ID");
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCastVisual434(Packet packet)
        {
            packet.ReadSingle("Z");
            packet.ReadInt32("SpellVisualKit ID"); // not confirmed
            packet.ReadInt16("Unk Int16 1"); // usually 0
            packet.ReadSingle("O");
            packet.ReadSingle("X");
            packet.ReadInt16("Unk Int16 2"); // usually 0
            packet.ReadSingle("Y");

            var guid1 = new byte[8];
            var guid2 = new byte[8];

            guid2[1] = packet.ReadBit();
            guid1[3] = packet.ReadBit();
            guid1[0] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid1[2] = packet.ReadBit();
            guid1[4] = packet.ReadBit();
            guid2[6] = packet.ReadBit();

            guid2[0] = packet.ReadBit();
            packet.ReadBit("Unk Bit"); // usually 1
            guid1[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid1[5] = packet.ReadBit();
            guid1[1] = packet.ReadBit();
            guid1[7] = packet.ReadBit();
            guid2[3] = packet.ReadBit();

            guid2[4] = packet.ReadBit();

            packet.ReadXORByte(guid1, 7);
            packet.ReadXORByte(guid1, 4);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid1, 1);
            packet.ReadXORByte(guid1, 3);
            packet.ReadXORByte(guid1, 0);
            packet.ReadXORByte(guid1, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid1, 5);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid1, 2);
            packet.ReadXORByte(guid2, 3);

            packet.WriteGuid("Caster Guid", guid1);
            packet.WriteGuid("Unk Guid", guid2);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT, ClientVersionBuild.V4_3_0_15005, ClientVersionBuild.V4_3_4_15595)]
        public static void HandleCastVisualKit430(Packet packet)
        {
            packet.ReadUInt32("SpellVisualKit ID");
            packet.ReadUInt32("Unk");
            packet.ReadUInt32("Unk");

            var guid = packet.StartBitStream(0, 4, 3, 6, 5, 7, 2, 1);
            packet.ParseBitStream(guid, 5, 7, 6, 1, 4, 3, 2, 0);
            packet.WriteGuid("Caster Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAY_SPELL_VISUAL_KIT, ClientVersionBuild.V4_3_4_15595)] // 4.3.4
        public static void HandleCastVisualKit434(Packet packet)
        {
            packet.ReadUInt32("Unk");
            packet.ReadUInt32("SpellVisualKit ID");
            packet.ReadUInt32("Unk");

            var guid = packet.StartBitStream(4, 7, 5, 3, 1, 2, 0, 6);
            packet.ParseBitStream(guid, 0, 4, 1, 6, 7, 2, 3, 5);
            packet.WriteGuid("Caster Guid", guid);
        }

        [Parser(Opcode.SMSG_PET_CAST_FAILED)]
        [Parser(Opcode.SMSG_CAST_FAILED)]
        public static void HandleCastFailed(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                packet.ReadByte("Cast count");

            packet.ReadUInt32<SpellId>("Spell ID");

            var result = packet.ReadByteE<SpellCastFailureReason>("Reason");

            if (ClientVersion.RemovedInVersion(ClientType.WrathOfTheLichKing))
                packet.ReadByte("Cast count");

            switch (result)
            {
                case SpellCastFailureReason.NeedExoticAmmo:
                    if (packet.CanRead())
                        packet.ReadUInt32("Item SubclassMask");
                    break;
                case SpellCastFailureReason.TooManyOfItem:
                    if (packet.CanRead())
                        packet.ReadUInt32("Limit");
                    break;
                case SpellCastFailureReason.Totems:
                case SpellCastFailureReason.TotemCategory:
                    if (packet.CanRead())
                        packet.ReadUInt32("Totem 1");
                    if (packet.CanRead())
                        packet.ReadUInt32("Totem 2");
                    break;
                case SpellCastFailureReason.Reagents:
                    if (packet.CanRead())
                        packet.ReadUInt32("Reagent ID");
                    break;
                case SpellCastFailureReason.RequiresSpellFocus:
                    if (packet.CanRead())
                        packet.ReadUInt32("Spell Focus");
                    break;
                case SpellCastFailureReason.RequiresArea:
                    if (packet.CanRead())
                        packet.ReadUInt32<AreaId>("Area ID");
                    break;
                case SpellCastFailureReason.CustomError:
                    if (packet.CanRead())
                        packet.ReadUInt32("Error ID");
                    break;
                case SpellCastFailureReason.PreventedByMechanic:
                    if (packet.CanRead())
                        packet.ReadUInt32E<SpellMechanic>("Mechanic");
                    break;
                case SpellCastFailureReason.EquippedItemClass:
                case SpellCastFailureReason.EquippedItemClassMainhand:
                case SpellCastFailureReason.EquippedItemClassOffhand:
                    if (packet.CanRead())
                        packet.ReadUInt32E<ItemClass>("Class");
                    if (packet.CanRead())
                        packet.ReadUInt32("SubclassMask");
                    break;
                case SpellCastFailureReason.MinSkill:
                    if (packet.CanRead())
                        packet.ReadUInt32("Skill Type"); // SkillLine.dbc
                    if (packet.CanRead())
                        packet.ReadUInt32("Required Amount");
                    break;
                case SpellCastFailureReason.FishingTooLow:
                    if (packet.CanRead())
                        packet.ReadUInt32("Required fishing skill");
                    break;
                // Following is post 3.3.5a
                case SpellCastFailureReason.NotReady:
                    if (packet.CanRead())
                        packet.ReadInt32("Extra Cast Number");
                    break;
                case SpellCastFailureReason.Silenced:
                case SpellCastFailureReason.NotStanding:
                    if (packet.CanRead())
                        packet.ReadInt32("Unk");
                    break;
                default:
                    if (packet.CanRead())
                        packet.ReadUInt32("Unknown1");
                    if (packet.CanRead())
                        packet.ReadUInt32("Unknown2");
                    break;
            }
        }

        [Parser(Opcode.SMSG_SPELL_FAILURE)]
        [Parser(Opcode.SMSG_SPELL_FAILED_OTHER)]
        public static void HandleSpellFailedOther(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
            packet.ReadByte("Cast count");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadByteE<SpellCastFailureReason>("Reason");
        }

        [Parser(Opcode.SMSG_SPELL_INSTAKILL_LOG)]
        public static void HandleSpellInstakillLog(Packet packet)
        {
            packet.ReadGuid("Target GUID");
            packet.ReadGuid("Caster GUID");
            packet.ReadUInt32<SpellId>("Spell ID");
        }

        [Parser(Opcode.SMSG_SPELL_OR_DAMAGE_IMMUNE)]
        [Parser(Opcode.SMSG_PROC_RESIST)]
        public static void HandleSpellProcResist(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadBool("Debug Log");

            // not found in 3.3.5a sniff nor 3.3.3a
            //if (ClientVersion.Build == ClientVersionBuild.V3_3_5a_12340) // blizzard retardness, client doesn't even read this
            //{
            //    packet.ReadSingle("Unk");
            //    packet.ReadSingle("Unk");
            //}
        }

        [Parser(Opcode.MSG_CHANNEL_UPDATE)]
        public static void HandleSpellChannelUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadUInt32("Timestamp");
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadInt32("Duration");

            if (ClientVersion.RemovedInVersion(ClientVersionBuild.V4_0_6a_13623))
                return;

            if (packet.ReadBool("Has castflag Immunity"))
            {
                packet.ReadUInt32("CastSchoolImmunities");
                packet.ReadUInt32("CastImmunities");
            }

            if (packet.ReadBool("Has castflag HealPrediction"))
            {
                packet.ReadPackedGuid("Target GUID");
                packet.ReadInt32("Heal Amount");

                var type = packet.ReadByte("Type");
                if (type == 2)
                    packet.ReadPackedGuid("Unk GUID");
            }
        }

        [Parser(Opcode.SMSG_BREAK_TARGET)]
        [Parser(Opcode.SMSG_DISMOUNT)]
        public static void HandleDismount(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_MOUNT_RESULT)]
        public static void HandleMountResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623) && ClientVersion.RemovedInVersion(ClientVersionBuild.V4_3_4_15595))
                packet.ReadPackedGuid("GUID");
            packet.ReadInt32E<MountResult>("Result");
        }

        [Parser(Opcode.SMSG_DISMOUNT_RESULT)]
        public static void HandleDismountResult(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_4_15595)) // Don't know for previous.
                packet.ReadInt32E<DismountResult>("Result");
        }

        [Parser(Opcode.CMSG_GET_MIRROR_IMAGE_DATA)]
        public static void HandleGetMirrorImageData(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.ReadUInt32("Display Id");
        }

        [Parser(Opcode.SMSG_MIRROR_IMAGE_COMPONENTED_DATA)]
        public static void HandleMirrorImageData(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Display ID");
            var race = packet.ReadByteE<Race>("Race");

            if (race == Race.None)
                return;

            packet.ReadByteE<Gender>("Gender");
            packet.ReadByteE<Class>("Class");

            if (!packet.CanRead())
                return;

            packet.ReadByte("Skin");
            packet.ReadByte("Face");
            packet.ReadByte("Hair Style");
            packet.ReadByte("Hair Color");
            packet.ReadByte("Facial Hair");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadGuid("Guild Guid");
            else
                packet.ReadUInt32("Guild Id");

            EquipmentSlotType[] slots = {
                EquipmentSlotType.Head, EquipmentSlotType.Shoulders, EquipmentSlotType.Shirt,
                EquipmentSlotType.Chest, EquipmentSlotType.Waist, EquipmentSlotType.Legs,
                EquipmentSlotType.Feet, EquipmentSlotType.Wrists, EquipmentSlotType.Hands,
                EquipmentSlotType.Back, EquipmentSlotType.Tabard };

            for (var i = 0; i < 11; ++i)
                packet.ReadUInt32<ItemId>("ItemEntry", slots[i]);
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWN)]
        public static void HandleClearCooldown(Packet packet)
        {
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_CLEAR_COOLDOWNS)] // 4.3.4
        public static void HandleClearCooldowns(Packet packet)
        {
            var guid = new byte[8];
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var count = packet.ReadBits("Spell Count", 24);
            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 3);
            for (var i = 0; i < count; i++)
                packet.ReadUInt32<SpellId>("Spell ID", i);

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 6);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPELL_COOLDOWN)]
        public static void HandleSpellCooldown(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("Unk mask");
            while (packet.CanRead())
            {
                packet.ReadUInt32<SpellId>("Spell ID");
                packet.ReadInt32("Time");
            }
        }

        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.Zero, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetSpellModifier(Packet packet)
        {
            packet.ReadByte("Spell Mask bitpos");
            packet.ReadByteE<SpellModOp>("Spell Mod");
            packet.ReadInt32("Amount");
        }

        [Parser(Opcode.SMSG_SET_PCT_SPELL_MODIFIER, ClientVersionBuild.V4_0_6_13596)]
        [Parser(Opcode.SMSG_SET_FLAT_SPELL_MODIFIER, ClientVersionBuild.V4_0_6_13596)]
        public static void HandleSetSpellModifierFlat406(Packet packet)
        {
            var modCount = packet.ReadUInt32("Modifier type count");
            for (var j = 0; j < modCount; ++j)
            {
                var modTypeCount = packet.ReadUInt32("Count", j);
                packet.ReadByteE<SpellModOp>("Spell Mod", j);
                for (var i = 0; i < modTypeCount; ++i)
                {
                    packet.ReadByte("Spell Mask bitpos", j, i);
                    packet.ReadSingle("Amount", j, i);
                }
            }
        }

        [Parser(Opcode.SMSG_DISPEL_FAILED)]
        public static void HandleDispelFailed(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadUInt32<SpellId>("Dispelling Spell ID");

            for (var i = 0; packet.CanRead(); i++)
                packet.ReadUInt32<SpellId>("Dispelled Spell ID", i);
        }

        [Parser(Opcode.CMSG_TOTEM_DESTROYED)]
        public static void HandleTotemDestroyed(Packet packet)
        {
            packet.ReadByte("Slot");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005)) // guessing, present on 4.3.4
                packet.ReadGuid("Unk");
        }

        [Parser(Opcode.SMSG_TOTEM_CREATED)]
        public static void HandleTotemCreated(Packet packet)
        {
            packet.ReadByte("Slot");
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Duration");
            packet.ReadUInt32<SpellId>("Spell Id");
        }

        [Parser(Opcode.CMSG_CANCEL_CAST)]
        public static void HandlePlayerCancelCast(Packet packet)
        {
            packet.ReadByte("Count");
            packet.ReadUInt32<SpellId>("Spell Id");
        }

        [Parser(Opcode.SMSG_UNIT_SPELLCAST_START)]
        public static void HandleUnitSpellcastStart(Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadPackedGuid("Target GUID");
            packet.ReadUInt32<SpellId>("Spell Id");
            packet.ReadInt32("Time Casted");
            packet.ReadInt32("Cast time");

            if (packet.ReadBool("Unknown bool"))
            {
                packet.ReadUInt32("Unk");
                packet.ReadUInt32("Unk");
            }
        }

        [Parser(Opcode.SMSG_SPELL_DELAYED)]
        public static void HandleSpellDelayed(Packet packet)
        {
            packet.ReadPackedGuid("Caster GUID");
            packet.ReadInt32("Delay Time");
        }

        [Parser(Opcode.SMSG_SPELL_UPDATE_CHAIN_TARGETS)]
        public static void HandleUpdateChainTargets(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadUInt32<SpellId>("Spell ID");
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
                packet.ReadGuid("Chain target");
        }

        [Parser(Opcode.SMSG_AURACASTLOG)]
        public static void HandleAuraCastLog(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadSingle("Unk 1");
            packet.ReadSingle("Unk 2");
        }


        [Parser(Opcode.SMSG_SPELL_CHANCE_PROC_LOG)]
        public static void HandleChanceProcLog(Packet packet)
        {
            packet.ReadGuid("Caster GUID");
            packet.ReadGuid("Target GUID");
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadSingle("unk1");
            packet.ReadSingle("unk2");
            packet.ReadUInt32E<UnknownFlags>("ProcFlags");
            packet.ReadUInt32E<UnknownFlags>("Flags2");
        }

        [Parser(Opcode.SMSG_CATEGORY_COOLDOWN)]
        public static void HandleSpellCategoryCooldown(Packet packet)
        {
            var count = packet.ReadBits("Count", 23);

            for (int i = 0; i < count; ++i)
            {
                packet.ReadInt32("Category Cooldown");
                packet.ReadInt32("Cooldown");
            }
        }

        [Parser(Opcode.SMSG_AURA_POINTS_DEPLETED)]
        public static void HandleAuraPointsDepleted(Packet packet)
        {
            var guid = packet.StartBitStream(2, 4, 1, 7, 5, 0, 3, 6);

            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 0);
            packet.ReadByte("Points?");
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadByte("Slot ID?");
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_DISENCHANT_CREDIT)] // 4.3.4
        public static void HandleDisenchantCredit(Packet packet)
        {
            var guid = packet.StartBitStream(0, 6, 3, 1, 7, 5, 2, 4);

            packet.ReadUInt32<ItemId>("Item Entry");

            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 1);

            packet.ReadInt32("Unk Int32");
            packet.ReadInt32("Unk Int32");

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_MISSILE_CANCEL)] // 4.3.4
        public static void HandleMissileCancel(Packet packet)
        {
            var guid = new byte[8];
            guid[7] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            packet.ReadBit("Cancel");
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_SPIRIT_HEALER_CONFIRM)]
        [Parser(Opcode.SMSG_CLEAR_TARGET)]
        public static void HandleMiscGuid(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_MODIFY_COOLDOWN)]
        public static void HandleModifyCooldown(Packet packet)
        {
            packet.ReadUInt32<SpellId>("Spell ID");
            packet.ReadGuid("GUID");
            packet.ReadInt32("Cooldown Mod");
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
