using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WoWPacketParser.Proto;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V4_3_4_15595.Parsers
{
    public static class SpellHandler
    {
        [Parser(Opcode.SMSG_SPELL_START)]
        public static void HandleSpellStart(Packet packet)
        {
            PacketSpellStart packetSpellStart = packet.Holder.PacketSpellStart = new();
            packetSpellStart.Data = ReadSpellCastData(packet, "Cast");
        }
        
        [Parser(Opcode.SMSG_SPELL_GO)]
        public static void HandleSpellGo(Packet packet)
        {
            PacketSpellGo packetSpellGo = packet.Holder.PacketSpellGo = new();
            packetSpellGo.Data = ReadSpellCastData(packet, "Cast");
        }

        public static PacketSpellData ReadSpellCastData(Packet packet, params object[] idx)
        {
            var packetSpellData = new PacketSpellData();
            bool isSpellGo = packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_SPELL_GO, Direction.ServerToClient);
            WowGuid targetGUID = new WowGuid64();

            var casterGUID = packet.ReadPackedGuid("CasterGUID", idx);
            packetSpellData.Caster = packet.ReadPackedGuid("CasterUnit", idx);
            packet.ReadByte("CastID", idx);
            var spellId = packetSpellData.Spell = (uint)packet.ReadInt32<SpellId>("SpellID", idx);
            CastFlag flags = packet.ReadInt32E<CastFlag>("CastFlags", idx);
            packet.ReadUInt32("CastFlagsEx", idx);
            packet.ReadUInt32("CastTime", idx);

            if (isSpellGo)
            {
                var hitTargetsCount = packet.ReadByte("HitTargetsCount", idx);
                for (var i = 0; i < hitTargetsCount; ++i)
                    packetSpellData.HitTargets.Add(packet.ReadGuid("HitTarget", idx, i));

                var missCount = packet.ReadByte("MissStatusCount", idx);
                for (var i = 0; i < missCount; ++i)
                    ReadSpellMissStatus(packet, idx, "MissStatus", i);
            }

            TargetFlag targetFlags = TargetFlag.Self;
            ReadSpellTargetData(packet, ref targetFlags, targetGUID, idx, "Target");

            if (flags.HasAnyFlag(CastFlag.PredictedPower))
                packet.ReadUInt32("RemainingPower");

            if (flags.HasAnyFlag(CastFlag.RuneInfo))
                ReadRuneData(packet, idx, "RemainingRunes");

            if (isSpellGo)
                if (flags.HasAnyFlag(CastFlag.AdjustMissile))
                    ReadMissileTrajectoryResult(packet, idx, "MissileTrajectory");

            if (flags.HasAnyFlag(CastFlag.Projectile))
                ReadSpellAmmo(packet, idx, "Ammo");

            if (isSpellGo)
            {
                if (flags.HasAnyFlag(CastFlag.VisualChain))
                    ReadProjectileVisual(packet, idx, "ProjectileVisual");

                if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
                    packet.ReadByte("DestLocSpellCastIndex", idx);

                if (targetFlags.HasAnyFlag(TargetFlag.ExtraTargets))
                {
                    var targetPointsCount = packet.ReadInt32("TargetPointsCount", idx);
                    for (var i = 0; i < targetPointsCount; ++i)
                        ReadTargetLocation(packet, idx, "TargetPoints", i);
                }
            }
            else
            {
                if (flags.HasAnyFlag(CastFlag.Immunity))
                    ReadCreatureImmunities(packet, idx, "Immunities");

                if (flags.HasAnyFlag(CastFlag.HealPrediction))
                    ReadSpellHealPrediction(packet, idx, "Predict");
            }

            if (flags.HasAnyFlag(CastFlag.Unknown21) && !isSpellGo)
            {
                NpcSpellClick spellClick = new NpcSpellClick
                {
                    SpellID = spellId,
                    CasterGUID = casterGUID,
                    TargetGUID = targetGUID
                };

                Storage.SpellClicks.Add(spellClick, packet.TimeSpan);
            }

            if (isSpellGo)
                packet.AddSniffData(StoreNameType.Spell, (int)spellId, "SPELL_GO");

            return packetSpellData;
        }

        [Parser(Opcode.MSG_CHANNEL_START)]
        public static void HandleSpellChannelStart(Packet packet)
        {
            packet.ReadPackedGuid("CasterGUID");
            packet.ReadUInt32<SpellId>("SpellID");
            packet.ReadInt32("ChannelDuration");

            if (packet.ReadBool("HasInterruptImmunities"))
                ReadChannelStartInterruptImmunities(packet, "InterruptImmunities");

            if (packet.ReadBool("HasHealPrediction"))
            {
                packet.ReadPackedGuid("TargetGUID", "HealPrediction");
                ReadSpellHealPrediction(packet, "HealPrediction");
            }
        }

        [Parser(Opcode.SMSG_SPELL_INTERRUPT_LOG)]
        public static void HandleSpellInterruptLog(Packet packet)
        {
            var casterGuid = new byte[8];
            var victimGuid = new byte[8];

            victimGuid[4] = packet.ReadBit();
            casterGuid[5] = packet.ReadBit();
            casterGuid[6] = packet.ReadBit();
            casterGuid[1] = packet.ReadBit();
            casterGuid[3] = packet.ReadBit();
            casterGuid[0] = packet.ReadBit();
            victimGuid[3] = packet.ReadBit();
            victimGuid[5] = packet.ReadBit();
            victimGuid[1] = packet.ReadBit();
            casterGuid[4] = packet.ReadBit();
            casterGuid[7] = packet.ReadBit();
            victimGuid[7] = packet.ReadBit();
            victimGuid[6] = packet.ReadBit();
            casterGuid[2] = packet.ReadBit();
            victimGuid[2] = packet.ReadBit();
            victimGuid[0] = packet.ReadBit();

            packet.ReadXORByte(casterGuid, 7);
            packet.ReadXORByte(casterGuid, 6);
            packet.ReadXORByte(casterGuid, 3);
            packet.ReadXORByte(casterGuid, 2);
            packet.ReadXORByte(victimGuid, 3);
            packet.ReadXORByte(victimGuid, 6);
            packet.ReadXORByte(victimGuid, 2);
            packet.ReadXORByte(victimGuid, 4);
            packet.ReadXORByte(victimGuid, 7);
            packet.ReadXORByte(victimGuid, 0);
            packet.ReadXORByte(casterGuid, 4);
            packet.ReadInt32<SpellId>("SpellID");
            packet.ReadXORByte(victimGuid, 1);
            packet.ReadXORByte(casterGuid, 0);
            packet.ReadXORByte(casterGuid, 5);
            packet.ReadXORByte(casterGuid, 1);
            packet.ReadInt32<SpellId>("InterruptedSpellID");
            packet.ReadXORByte(victimGuid, 5);

            packet.WriteGuid("Victim", victimGuid);
            packet.WriteGuid("Caster", casterGuid);
        }

        public static void ReadSpellMissStatus(Packet packet, params object[] idx)
        {
            packet.ReadGuid("MissTarget", idx);

            var missType = packet.ReadByteE<SpellMissType>("Reason", idx);
            if (missType == SpellMissType.Reflect)
                packet.ReadByteE<SpellMissType>("ReflectStatus", idx);
        }

        public static void ReadSpellTargetData(Packet packet, ref TargetFlag targetFlags, WowGuid targetGUID, params object[] idx)
        {
            targetFlags = packet.ReadInt32E<TargetFlag>("Flags", idx);

            if (targetFlags.HasAnyFlag(TargetFlag.Unit | TargetFlag.CorpseEnemy | TargetFlag.GameObject | TargetFlag.CorpseAlly | TargetFlag.UnitMinipet))
                targetGUID = packet.ReadPackedGuid("Unit", idx);

            if (targetFlags.HasAnyFlag(TargetFlag.Item | TargetFlag.TradeItem))
                packet.ReadPackedGuid("Item", idx);

            if (targetFlags.HasAnyFlag(TargetFlag.SourceLocation))
                ReadLocation(packet, "SrcLocation");

            if (targetFlags.HasAnyFlag(TargetFlag.DestinationLocation))
                ReadLocation(packet, "DstLocation");

            if (targetFlags.HasAnyFlag(TargetFlag.NameString))
                packet.ReadCString("Name", idx);
        }

        public static Vector3 ReadLocation(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid("Transport", idx);
            return packet.ReadVector3("Location", idx);
        }

        public static void ReadTargetLocation(Packet packet, params object[] idx)
        {
            packet.ReadVector3("Location", idx);
            packet.ReadPackedGuid("Transport", idx);
        }

        public static void ReadRuneData(Packet packet, params object[] idx)
        {
            packet.ReadByte("Start", idx);
            packet.ReadByte("Count", idx);
            for (var i = 0; i < 6; ++i)
                packet.ReadByte("Cooldowns", idx, i);
        }

        public static void ReadMissileTrajectoryResult(Packet packet, params object[] idx)
        {
            packet.ReadSingle("Pitch", idx);
            packet.ReadUInt32("TravelTime", idx);
        }

        public static void ReadSpellAmmo(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("DisplayID", idx);
            packet.ReadByteE<InventoryType>("InventoryType", idx);
        }

        public static void ReadProjectileVisual(Packet packet, params object[] idx)
        {
            for (var i = 0; i < 2; ++i)
                packet.ReadInt32("Id", idx, i);
        }

        public static void ReadCreatureImmunities(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("School", idx);
            packet.ReadUInt32E<MechanicImmunityFlag>("Value", idx);
        }

        public static void ReadSpellHealPrediction(Packet packet, params object[] idx)
        {
            packet.ReadInt32("Points", idx);
            var type = packet.ReadByte("Type", idx);
            if (type == 2) // Beacon of Light target is an extra field used when type is 2
                packet.ReadPackedGuid("BeaconGUID", idx);
        }

        public static void ReadChannelStartInterruptImmunities(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("SchoolImmunities", idx);
            packet.ReadUInt32E<MechanicImmunityFlag>("Immunities", idx);
        }
    }
}
