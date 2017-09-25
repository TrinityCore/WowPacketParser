using WowPacketParser.Enums;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParser.Messages.UserClient.LFGList
{
    public unsafe struct Join
    {
        public LFGListJoinRequest Info;
        
        [Parser(Opcode.CMSG_LFG_LIST_JOIN)]
        public static void HandleLFGListJoin(Packet packet)
        {
            readLFGListJoinRequest(packet, "LFGListJoinRequest");
        }
        private static void readLFGListJoinRequest(Packet packet, params object[] idx)
        {
            packet.ReadInt32("ActivityID", idx);
            packet.ReadSingle("RequiredItemLevel", idx);

            packet.ResetBitReader();

            var lenName = packet.ReadBits(8);
            var lenComment = packet.ReadBits(11);
            var lenVoiceChat = packet.ReadBits(8);

            packet.ReadWoWString("Name", lenName, idx);
            packet.ReadWoWString("Comment", lenComment, idx);
            packet.ReadWoWString("VoiceChat", lenVoiceChat, idx);
        }

    }
}
