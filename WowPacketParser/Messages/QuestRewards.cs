using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct QuestRewards
    {
        public int ChoiceItemCount;
        public int ItemCount;
        public int Money;
        public int Xp;
        public int Title;
        public int Talents;
        public int FactionFlags;
        public int SpellCompletionDisplayID;
        public int SpellCompletionID;
        public int SkillLineID;
        public int NumSkillUps;
        public QuestChoiceItem[/*6*/] ChoiceItems;
        public fixed int ItemID[4];
        public fixed int ItemQty[4];
        public fixed int FactionID[5];
        public fixed int FactionValue[5];
        public fixed int FactionOverride[5];
        public fixed int CurrencyID[4];
        public fixed int CurrencyQty[4];
    }
}
