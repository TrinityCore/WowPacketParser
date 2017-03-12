using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientFeatureSystemStatusGlueScreen
    {
        public bool BpayStoreAvailable;
        public bool BpayStoreDisabledByParentalControls;
        public bool CharUndeleteEnabled;
        public bool BpayStoreEnabled;
    }
}
