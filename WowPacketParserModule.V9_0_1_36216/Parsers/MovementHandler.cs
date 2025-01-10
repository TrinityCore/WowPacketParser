using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class MovementHandler
    {
        [HasSniffData]
        [Parser(Opcode.SMSG_NEW_WORLD)]
        public static void HandleNewWorld(Packet packet)
        {
            CoreParsers.MovementHandler.CurrentMapId = (uint)packet.ReadInt32<MapId>("Map");
            packet.ReadVector4("Position");
            packet.ReadInt32("Unused901_1");
            packet.ReadInt32("Unused901_2");
            packet.ReadUInt32("Reason");
            packet.ReadVector3("MovementOffset");
            if (ClientVersion.AddedInVersion(ClientType.TheWarWithin))
                packet.ReadInt32("Counter");

            packet.AddSniffData(StoreNameType.Map, (int)CoreParsers.MovementHandler.CurrentMapId, "NEW_WORLD");
        }

        public static void ReadVignetteData(Packet packet, params object[] idx)
        {
            packet.ReadVector3("Position", idx);
            packet.ReadPackedGuid128("ObjGUID", idx);
            packet.ReadInt32("VignetteID", idx);
            packet.ReadUInt32<AreaId>("ZoneID", idx);
            packet.ReadUInt32("WMOGroupID", idx);
            packet.ReadUInt32("WMODoodadPlacementID", idx);
        }

        public static void ReadVignetteDataSet(Packet packet, params object[] idx)
        {
            var idCount = packet.ReadUInt32();
            for (var i = 0u; i < idCount; ++i)
                packet.ReadPackedGuid128("IDs", idx, i);

            // Added VignetteClientData
            var dataCount = packet.ReadUInt32();
            for (var i = 0u; i < dataCount; ++i)
                ReadVignetteData(packet, idx, "Data", i);
        }

        [Parser(Opcode.SMSG_VIGNETTE_UPDATE)]
        public static void HandleVignetteUpdate(Packet packet)
        {
            packet.ReadBit("ForceUpdate");
            packet.ReadBit("InFogOfWar");

            var removedCount = packet.ReadUInt32();

            // Added
            ReadVignetteDataSet(packet, "Added");
            ReadVignetteDataSet(packet, "Updated");

            for (var i = 0; i < removedCount; ++i)
                packet.ReadPackedGuid128("IDs", i, "Removed");
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
            V6_0_2_19033.Parsers.MovementHandler.ReadMovementAck(packet, "MovementAck");
            packet.ReadSingle("Height");
            packet.ReadInt32("MountDisplayID");
            packet.ReadByte("Reason");
        }
    }
}
