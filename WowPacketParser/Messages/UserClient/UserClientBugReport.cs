using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientBugReport
    {
        public ReportType Type;
        public string Text;
        public string DiagInfo;
    }
}
