using System.Collections.Generic;
using WowPacketParser.Messages.Player;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliMailListEntry
    {
        public int MailID;
        public byte SenderType;
        public ulong? SenderCharacter; // Optional
        public PlayerGuidLookupHint SenderHint;
        public int? AltSenderID; // Optional
        public ulong Cod;
        public int PackageID;
        public int StationeryID;
        public ulong SentMoney;
        public int Flags;
        public float DaysLeft;
        public int MailTemplateID;
        public string Subject;
        public string Body;
        public List<CliMailAttachedItem> Attachments;
    }
}
