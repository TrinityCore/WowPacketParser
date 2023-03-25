using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V9_0_1_36216.Parsers
{
    public static class MailHandler
    {
        [Parser(Opcode.CMSG_MAIL_MARK_AS_READ, ClientVersionBuild.V9_1_0_39185)]
        public static void HandleMailMarkAsRead(Packet packet)
        {
            packet.ReadPackedGuid128("Mailbox");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V10_0_5_47777))
                packet.ReadUInt64("MailID");
            else
                packet.ReadInt32("MailID");
        }
    }
}
