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
    class MovementHelper
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
    }
}
