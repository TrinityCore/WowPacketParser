using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.Substructures
{
    public static class MovementHandler
    {
        public static MovementInfo.TransportInfo ReadTransportData(Packet packet, params object[] idx)
        {
            var transportInfo = new MovementInfo.TransportInfo();

            transportInfo.Guid = packet.ReadPackedGuid128("Guid", idx);
            transportInfo.Offset = packet.ReadVector4("Position", idx);
            packet.ReadByte("Seat", idx);
            packet.ReadInt32("MoveTime", idx);

            packet.ResetBitReader();
            var hasPrevMoveTime = packet.ReadBit("HasPrevMoveTime", idx);
            var hasVehicleRecID = packet.ReadBit("HasVehicleRecID", idx);

            if (hasPrevMoveTime)
                packet.ReadUInt32("PrevMoveTime", idx);

            if (hasVehicleRecID)
                packet.ReadUInt32("VehicleRecID", idx);

            return transportInfo;
        }

        public static void ReadFallData(Packet packet, params object[] idx)
        {
            packet.ReadUInt32("FallTime", idx);
            packet.ReadSingle("ZSpeed", idx);

            packet.ResetBitReader();
            var hasFallDirection = packet.ReadBit("HasFallDirection", idx);
            if (hasFallDirection)
            {
                packet.ReadSingle("SinAngle", idx);
                packet.ReadSingle("CosAngle", idx);
                packet.ReadSingle("XYSpeed", idx);
            }
        }

        public static void ReadInertiaData(Packet packet, params object[] idx)
        {
            packet.ReadPackedGuid128("GUID", idx);
            packet.ReadVector4("Force", idx);
            packet.ReadUInt32("Lifetime", idx);
        }

        public static void ReadAdvFlyingData(Packet packet, params object[] idx)
        {
            packet.ReadSingle("ForwardVelocity", idx);
            packet.ReadSingle("UpVelocity", idx);
        }

        public static void ReadDriveStatusData(Packet packet, params object[] idx)
        {
            packet.ResetBitReader();
            packet.ReadBit("Accelerating", idx);
            packet.ReadBit("Drifting", idx);
            packet.ReadSingle("Speed", idx);
            packet.ReadSingle("MovementAngle", idx);
        }

        public static MovementInfo ReadMovementStats602(Packet packet, params object[] idx)
        {
            MovementInfo info = new();
            info.MoverGuid = packet.ReadPackedGuid128("MoverGUID", idx);

            packet.ReadUInt32("MoveTime", idx);
            var position = packet.ReadVector4("Position", idx);
            info.Position = new Vector3 { X = position.X, Y = position.Y, Z = position.Z };
            info.Orientation = position.O;

            packet.ReadSingle("Pitch", idx);
            packet.ReadSingle("StepUpStartElevation", idx);

            var int152 = packet.ReadInt32("RemoveForcesCount", idx);
            packet.ReadInt32("MoveIndex", idx);

            for (var i = 0; i < int152; i++)
                packet.ReadPackedGuid128("RemoveForcesIDs", idx, i);

            packet.ResetBitReader();

            info.Flags = (uint)packet.ReadBitsE<MovementFlag>("MovementFlags", 30, idx);
            var movementFlag2Size = 0;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V7_0_3_22248))
                movementFlag2Size = 18;
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                movementFlag2Size = 16;
            else // < 6.2.0
                movementFlag2Size = 15;

            info.Flags2 = (uint)packet.ReadBitsE<MovementFlag2>("MovementFlag2", movementFlag2Size, idx);

            var hasTransport = packet.ReadBit("HasTransportData", idx);
            var hasFall = packet.ReadBit("HasFallData", idx);
            packet.ReadBit("HasSpline", idx);

            packet.ReadBit("HeightChangeFailed", idx);
            packet.ReadBit("RemoteTimeValid", idx);

            if (hasTransport)
                info.Transport = ReadTransportData(packet, idx, "TransportData");

            if (hasFall)
                ReadFallData(packet, idx, "FallData");
            return info;
        }

        public static MovementInfo ReadMovementStats920(Packet packet, params object[] idx)
        {
            MovementInfo info = new();
            info.MoverGuid = packet.ReadPackedGuid128("MoverGUID", idx);
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V9_2_0_42423) ||
                ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_1_40666) ||
                ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166))
            {
                info.Flags = (uint)packet.ReadUInt32E<MovementFlag>("MovementFlags", idx);
                info.Flags2 = (uint)packet.ReadUInt32E<MovementFlag2>("MovementFlags2", idx);
                info.Flags3 = (uint)packet.ReadUInt32E<MovementFlag3>("MovementFlags3", idx);
            }
            packet.ReadUInt32("MoveTime", idx);
            var position = packet.ReadVector4("Position", idx);
            info.Position = new Vector3 { X = position.X, Y = position.Y, Z = position.Z };
            info.Orientation = position.O;

            packet.ReadSingle("Pitch", idx);
            packet.ReadSingle("StepUpStartElevation", idx);

            var int152 = packet.ReadInt32("RemoveForcesCount", idx);
            packet.ReadInt32("MoveIndex", idx);

            for (var i = 0; i < int152; i++)
                packet.ReadPackedGuid128("RemoveForcesIDs", idx, i);

            packet.ResetBitReader();

            var hasStandingOnGameObjectGUID = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_1_0_49407))
                hasStandingOnGameObjectGUID = packet.ReadBit("HasStandingOnGameObjectGUID", idx);

            var hasTransport = packet.ReadBit("HasTransportData", idx);
            var hasFall = packet.ReadBit("HasFallData", idx);
            packet.ReadBit("HasSpline", idx);

            packet.ReadBit("HeightChangeFailed", idx);
            packet.ReadBit("RemoteTimeValid", idx);
            var hasInertia = (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V9_2_0_42423) ||
                              ClientVersion.AddedInVersion(ClientBranch.Classic, ClientVersionBuild.V1_14_1_40666) ||
                              ClientVersion.AddedInVersion(ClientBranch.TBC, ClientVersionBuild.V2_5_3_41812) ||
                              ClientVersion.AddedInVersion(ClientBranch.WotLK, ClientVersionBuild.V3_4_0_45166)) &&
                              packet.ReadBit("HasInertia", idx);
            bool hasAdvFlying = false;
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_0_46181))
                hasAdvFlying = packet.ReadBit("HasAdvFlying", idx);

            var hasDriveStatus = false;
            if (ClientVersion.AddedInVersion(ClientBranch.Retail, ClientVersionBuild.V11_1_0_59347))
                hasDriveStatus = packet.ReadBit("HasDriveStatus", idx);

            if (hasTransport)
                info.Transport = ReadTransportData(packet, idx, "TransportData");

            if (hasStandingOnGameObjectGUID)
                packet.ReadPackedGuid128("StandingOnGameObjectGUID", idx);

            if (hasInertia)
                ReadInertiaData(packet, idx, "Inertia");

            if (hasAdvFlying)
                ReadAdvFlyingData(packet, idx, "AdvFlying");

            if (hasFall)
                ReadFallData(packet, idx, "FallData");

            if (hasDriveStatus)
                ReadDriveStatusData(packet, idx, "DriveStatus");

            return info;
        }

        public static MovementInfo ReadMovementStats(Packet packet, params object[] idx)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V9_2_0_42423))
                return ReadMovementStats920(packet, idx);

            return ReadMovementStats602(packet, idx);
        }
    }
}
