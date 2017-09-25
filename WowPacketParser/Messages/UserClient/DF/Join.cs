using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.DF
{
    public unsafe struct Join
    {
        public bool QueueAsGroup;
        public uint Roles;
        public byte PartyIndex;
        public string Comment;
        public List<uint> Slots;
        public fixed uint Needs[3];

        [Parser(Opcode.CMSG_DF_JOIN)]
        public static void HandleDFJoin(Packet packet)
        {
            packet.ReadBit("QueueAsGroup");
            var commentLength = packet.ReadBits("UnkBits8", 8);

            packet.ResetBitReader();

            packet.ReadByte("PartyIndex");
            packet.ReadInt32E<LfgRoleFlag>("Roles");
            var slotsCount = packet.ReadInt32();

            for (var i = 0; i < 3; ++i) // Needs
                packet.ReadUInt32("Need", i);

            packet.ReadWoWString("Comment", commentLength);

            for (var i = 0; i < slotsCount; ++i) // Slots
                packet.ReadUInt32("Slot", i);
        }
    }
}
