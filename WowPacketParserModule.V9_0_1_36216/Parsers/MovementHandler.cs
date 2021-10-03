using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class MovementHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadInt32("Unused901_1");
            packet.ReadInt32("Unused901_2");
            packet.ReadUInt32("Reason");
            packet.ReadVector3("MovementOffset");

            packet.AddSniffData(StoreNameType.Map, (int)WowPacketParser.Parsing.Parsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleVignetteUpdate(Packet packet)
        {
            packet.ReadBit("ForceUpdate");
            packet.ReadBit("Unk_Bit_901");

            // VignetteInstanceIDList
            var int1 = packet.ReadUInt32("RemovedCount");
            for (var i = 0; i < int1; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Added
            var int2 = packet.ReadUInt32("AddedCount");
            for (var i = 0; i < int2; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Added VignetteClientData
            var int3 = packet.ReadUInt32("VignetteClientDataCount");
            for (var i = 0; i < int3; ++i)
            {
                packet.ReadVector3("Position", i);
                packet.ReadPackedGuid128("ObjGUID", i);
                packet.ReadInt32("VignetteID", i);
                packet.ReadUInt32<AreaId>("AreaID", i);
                packet.ReadUInt32("Unk901_1", i);
                packet.ReadUInt32("Unk901_2", i);
            }

            // Updated
            var int4 = packet.ReadUInt32("UpdatedCount");
            for (var i = 0; i < int4; ++i)
                packet.ReadPackedGuid128("IDs", i);

            // Updated VignetteClientData
            var int5 = packet.ReadUInt32("VignetteClientDataCount");
            for (var i = 0; i < int5; ++i)
            {
                packet.ReadVector3("Position", i);
                packet.ReadPackedGuid128("ObjGUID", i);
                packet.ReadInt32("VignetteID", i);
                packet.ReadUInt32<AreaId>("AreaID", i);
                packet.ReadUInt32("Unk901_1", i);
                packet.ReadUInt32("Unk901_2", i);
            }
        }

        [Parser(Opcode.SMSG_MOVE_SET_COLLISION_HEIGHT)]
        public static void HandleSetCollisionHeight(Packet packet)
        {
            packet.ReadPackedGuid128("MoverGUID");
            packet.ReadInt32("SequenceIndex");
            packet.ReadSingle("Height");
            packet.ReadSingle("Scale");
            packet.ReadByte("Reason");
            packet.ReadUInt32("MountDisplayID");
            packet.ReadInt32("ScaleDuration");
        }

        [Parser(Opcode.CMSG_MOVE_SET_COLLISION_HEIGHT_ACK)]
        public static void HandleMoveSetCollisionHeightAck(Packet packet)
        {
            V7_0_3_22248.Parsers.MovementHandler.ReadMovementAck(packet, "MovementAck");
            packet.ReadSingle("Height");
            packet.ReadInt32("MountDisplayID");
            packet.ReadByte("Reason");
        }
    }
}
