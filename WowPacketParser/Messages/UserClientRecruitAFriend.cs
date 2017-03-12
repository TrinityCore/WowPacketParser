using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientRecruitAFriend
    {
        public string Email;
        public string Text;
        public string Name;
    }
}
