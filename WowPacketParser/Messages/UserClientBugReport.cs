using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBugReport
    {
        public ReportType Type;
        public string Text;
        public string DiagInfo;
    }
}
