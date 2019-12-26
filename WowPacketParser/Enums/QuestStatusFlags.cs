using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum QuestStatusFlags : int
    {
        QuestStatusUnk1     = 0x01, // repeatable related, not related to complete state
        KillCreditComplete  = 0x02, // related to complete state
        CollectableComplete = 0x04, // related to complete state
        QuestStatusUnk8     = 0x08, // related to complete state
        QuestStatusUnk16    = 0x10, // related to complete state
        NoRequestOnComplete = 0x20, // not related to complete state
        QuestStatusUnk64    = 0x40, // related to complete state
        QuestStatusUnk128   = 0x80, // related to complete state
        Complete            = KillCreditComplete | CollectableComplete | QuestStatusUnk8 | QuestStatusUnk16 | QuestStatusUnk64 | QuestStatusUnk128,
        Complete434         = KillCreditComplete | CollectableComplete | QuestStatusUnk8 | QuestStatusUnk16 | QuestStatusUnk64 //maybe still valid at WoD
    }
}
