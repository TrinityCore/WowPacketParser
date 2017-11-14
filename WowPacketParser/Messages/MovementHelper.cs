using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Player.Move
{
    public class MovementHelper
    {
        public static MovementInfo ReadMovementInfo(Packet packet, WowGuid guid, object index = null)
        {
            if (ClientVersion.Build == ClientVersionBuild.V4_2_0_14333)
                return ReadMovementInfo420(packet, index);

            return ReadMovementInfoGen(packet, guid, index);
        }

        private static MovementInfo ReadMovementInfoGen(Packet packet, WowGuid guid, object index)
        {
            var info = new MovementInfo
            {
                Flags = packet.ReadInt32E<MovementFlag>("Movement Flags", index)
            };

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_0_2_9056))
                info.FlagsExtra = packet.ReadInt16E<MovementFlagExtra>("Extra Movement Flags", index);
            else
                info.FlagsExtra = packet.ReadByteE<MovementFlagExtra>("Extra Movement Flags", index);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                if (packet.ReadGuid("Guid 2", index) != guid)
                    throw new InvalidDataException("Guids are not equal.");

            packet.ReadUInt32("Time", index);

            info.Position = packet.ReadVector3("Position", index);
            info.Orientation = packet.ReadSingle("Orientation", index);

            if (info.Flags.HasAnyFlag(MovementFlag.OnTransport))
            {
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V3_1_0_9767))
                    info.TransportGuid = packet.ReadPackedGuid("Transport GUID", index);
                else
                    info.TransportGuid = packet.ReadGuid("Transport GUID", index);

                info.TransportOffset = packet.ReadVector4("Transport Position", index);
                packet.ReadInt32("Transport Time", index);

                if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                    packet.ReadByte("Transport Seat", index);

                if (info.FlagsExtra.HasAnyFlag(MovementFlagExtra.InterpolateMove))
                    packet.ReadInt32("Transport Time", index);
            }

            if (info.Flags.HasAnyFlag(MovementFlag.Swimming | MovementFlag.Flying) ||
                info.FlagsExtra.HasAnyFlag(MovementFlagExtra.AlwaysAllowPitching))
                packet.ReadSingle("Swim Pitch", index);

            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
            {
                if (info.FlagsExtra.HasAnyFlag(MovementFlagExtra.InterpolateTurning))
                {
                    packet.ReadInt32("Fall Time", index);
                    packet.ReadSingle("Fall Velocity", index);

                    if (info.Flags.HasAnyFlag(MovementFlag.Falling))
                    {
                        packet.ReadSingle("Fall Sin Angle", index);
                        packet.ReadSingle("Fall Cos Angle", index);
                        packet.ReadSingle("Fall Speed", index);
                    }
                }
            }
            else
            {
                packet.ReadInt32("Fall Time", index);
                if (info.Flags.HasAnyFlag(MovementFlag.Falling))
                {
                    packet.ReadSingle("Fall Velocity", index);
                    packet.ReadSingle("Fall Sin Angle", index);
                    packet.ReadSingle("Fall Cos Angle", index);
                    packet.ReadSingle("Fall Speed", index);
                }
            }

            // HACK: "generic" movement flags are wrong for 4.2.2
            if (info.Flags.HasAnyFlag(MovementFlag.SplineElevation) && ClientVersion.Build != ClientVersionBuild.V4_2_2_14545)
                packet.ReadSingle("Spline Elevation", index);

            return info;
        }

        private static MovementInfo ReadMovementInfo420(Packet packet, object index)
        {
            var info = new MovementInfo
            {
                Flags = packet.ReadBitsE<MovementFlag>("Movement Flags", 30, index)
            };

            packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", 12, index);

            var onTransport = packet.ReadBit("OnTransport", index);
            var hasInterpolatedMovement = false;
            var time3 = false;
            if (onTransport)
            {
                hasInterpolatedMovement = packet.ReadBit("HasInterpolatedMovement", index);
                time3 = packet.ReadBit("Time3", index);
            }

            var swimming = packet.ReadBit("Swimming", index);
            var interPolatedTurning = packet.ReadBit("InterPolatedTurning", index);

            var jumping = false;
            if (interPolatedTurning)
                jumping = packet.ReadBit("Jumping", index);

            var splineElevation = packet.ReadBit("SplineElevation", index);

            info.HasSplineData = packet.ReadBit("HasSplineData", index);

            packet.ResetBitReader(); // reset bitreader

            packet.ReadGuid("GUID 2", index);

            packet.ReadUInt32("Time", index);

            info.Position = packet.ReadVector3("Position", index);
            info.Orientation = packet.ReadSingle("Orientation", index);

            if (onTransport)
            {
                info.TransportGuid = packet.ReadGuid("Transport GUID", index);
                info.TransportOffset = packet.ReadVector4("Transport Position", index);
                packet.ReadByte("Transport Seat", index);
                packet.ReadInt32("Transport Time", index);
                if (hasInterpolatedMovement)
                    packet.ReadInt32("Transport Time 2", index);
                if (time3)
                    packet.ReadInt32("Transport Time 3", index);
            }
            if (swimming)
                packet.ReadSingle("Swim Pitch", index);

            if (interPolatedTurning)
            {
                packet.ReadInt32("Time Fallen", index);
                packet.ReadSingle("Fall Start Velocity", index);
                if (jumping)
                {
                    packet.ReadSingle("Jump Sin", index);
                    packet.ReadSingle("Jump Cos", index);
                    packet.ReadSingle("Jump Velocity", index);

                }
            }
            if (splineElevation)
                packet.ReadSingle("Spline Elevation", index);

            return info;
        }

        public static void ReadMovementForce(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("ID", idx);
            packet.ReadVector3("Direction", idx);
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_1_2_19802)) // correct?
                packet.ReadVector3("TransportPosition", idx);
            packet.ReadInt32("TransportID", idx);
            packet.ReadSingle("Magnitude", idx);

            packet.ResetBitReader();

            packet.ReadBits("Type", 2, idx);
        }

        public static void ReadMovementAck(Packet packet)
        {
            ReadMovementStats(packet);
            packet.ReadInt32("AckIndex");
        }
        public static void ReadMovementStats(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("MoverGUID", idx);

            packet.ReadUInt32("MoveIndex", idx);
            packet.ReadVector4("Position", idx);

            packet.ReadSingle("Pitch", idx);
            packet.ReadSingle("StepUpStartElevation", idx);

            var int152 = packet.ReadInt32("RemoveForcesCount", idx);
            packet.ReadInt32("MoveTime", idx);

            for (var i = 0; i < int152; i++)
                packet.ReadPackedGuid128("RemoveForcesIDs", idx, i);

            packet.ResetBitReader();

            packet.ReadBitsE<MovementFlag>("Movement Flags", 30, idx);
            packet.ReadBitsE<MovementFlagExtra>("Extra Movement Flags", ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173) ? 16 : 15, idx);

            var hasTransport = packet.ReadBit("Has Transport Data", idx);
            var hasFall = packet.ReadBit("Has Fall Data", idx);
            packet.ReadBit("HasSpline", idx);
            packet.ReadBit("HeightChangeFailed", idx);
            packet.ReadBit("RemoteTimeValid", idx);

            if (hasTransport)
                ReadTransportData(packet, idx, "TransportData");

            if (hasFall)
                ReadFallData(packet, idx, "FallData");
        }



        public static void ReadTransportData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("TransportGuid", idx);
            packet.ReadVector4("TransportPosition", idx);
            packet.ReadByte("TransportSeat", idx);
            packet.ReadInt32("TransportMoveTime", idx);

            packet.ResetBitReader();

            var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", idx);
            var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", idx);

            if (hasPrevMoveTime)
                packet.ReadUInt32("PrevMoveTime", idx);

            if (hasVehicleRecID)
                packet.ReadUInt32("VehicleRecID", idx);
        }

        public static void ReadFallData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("FallTime", idx);
            packet.ReadSingle("JumpVelocity", idx);

            packet.ResetBitReader();

            var bit20 = packet.ReadBit("HasFallDirection", idx);
            if (bit20)
            {
                packet.ReadVector2("Direction", idx);
                packet.ReadSingle("HorizontalSpeed", idx);
            }
        }
    }
}
