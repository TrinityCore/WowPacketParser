using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientFeatureSystemStatus
    {
        public bool VoiceEnabled;
        public bool BrowserEnabled;
        public bool BpayStoreAvailable;
        public bool RecruitAFriendSendingEnabled;
        public bool BpayStoreEnabled;
        public ClientSessionAlertConfig? SessionAlert; // Optional
        public uint ScrollOfResurrectionMaxRequestsPerDay;
        public bool ScrollOfResurrectionEnabled;
        public CliEuropaTicketConfig? EuropaTicketSystemStatus; // Optional
        public uint ScrollOfResurrectionRequestsRemaining;
        public uint CfgRealmID;
        public byte ComplaintStatus;
        public int CfgRealmRecID;
        public bool ItemRestorationButtonEnabled;
        public bool CharUndeleteEnabled;
        public bool BpayStoreDisabledByParentalControls;
    }
}
