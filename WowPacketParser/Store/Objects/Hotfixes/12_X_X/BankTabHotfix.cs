using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("bank_tab")]
    public sealed record BankTabHotfix1200: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Cost")]
        public ulong? Cost;

        [DBFieldName("BankType")]
        public byte? BankType;

        [DBFieldName("OrderIndex")]
        public sbyte? OrderIndex;

        [DBFieldName("PlayerConditionID")]
        public int? PlayerConditionID;

        [DBFieldName("PurchasePromptTitle")]
        public int? PurchasePromptTitle;

        [DBFieldName("PurchasePromptBody")]
        public int? PurchasePromptBody;

        [DBFieldName("PurchasePromptConfirmation")]
        public int? PurchasePromptConfirmation;

        [DBFieldName("TabCleanupConfirmation")]
        public int? TabCleanupConfirmation;

        [DBFieldName("TabNameEditBoxHeader")]
        public int? TabNameEditBoxHeader;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
