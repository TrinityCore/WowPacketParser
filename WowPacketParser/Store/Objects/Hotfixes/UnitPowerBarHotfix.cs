using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("unit_power_bar")]
    public sealed record UnitPowerBarHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Cost")]
        public string Cost;

        [DBFieldName("OutOfError")]
        public string OutOfError;

        [DBFieldName("ToolTip")]
        public string ToolTip;

        [DBFieldName("MinPower")]
        public uint? MinPower;

        [DBFieldName("MaxPower")]
        public uint? MaxPower;

        [DBFieldName("StartPower")]
        public uint? StartPower;

        [DBFieldName("CenterPower")]
        public byte? CenterPower;

        [DBFieldName("RegenerationPeace")]
        public float? RegenerationPeace;

        [DBFieldName("RegenerationCombat")]
        public float? RegenerationCombat;

        [DBFieldName("BarType")]
        public byte? BarType;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("StartInset")]
        public float? StartInset;

        [DBFieldName("EndInset")]
        public float? EndInset;

        [DBFieldName("FileDataID", 6)]
        public int?[] FileDataID;

        [DBFieldName("Color", 6)]
        public int?[] Color;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("unit_power_bar_locale")]
    public sealed record UnitPowerBarLocaleHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Cost_lang")]
        public string CostLang;

        [DBFieldName("OutOfError_lang")]
        public string OutOfErrorLang;

        [DBFieldName("ToolTip_lang")]
        public string ToolTipLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("unit_power_bar")]
    public sealed record UnitPowerBarHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("Name")]
        public string Name;

        [DBFieldName("Cost")]
        public string Cost;

        [DBFieldName("OutOfError")]
        public string OutOfError;

        [DBFieldName("ToolTip")]
        public string ToolTip;

        [DBFieldName("MinPower")]
        public uint? MinPower;

        [DBFieldName("MaxPower")]
        public uint? MaxPower;

        [DBFieldName("StartPower")]
        public ushort? StartPower;

        [DBFieldName("CenterPower")]
        public byte? CenterPower;

        [DBFieldName("RegenerationPeace")]
        public float? RegenerationPeace;

        [DBFieldName("RegenerationCombat")]
        public float? RegenerationCombat;

        [DBFieldName("BarType")]
        public byte? BarType;

        [DBFieldName("Flags")]
        public ushort? Flags;

        [DBFieldName("StartInset")]
        public float? StartInset;

        [DBFieldName("EndInset")]
        public float? EndInset;

        [DBFieldName("FileDataID", 6)]
        public int?[] FileDataID;

        [DBFieldName("Color", 6)]
        public int?[] Color;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("unit_power_bar_locale")]
    public sealed record UnitPowerBarLocaleHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("locale", true)]
        public string Locale = ClientLocale.PacketLocaleString;

        [DBFieldName("Name_lang")]
        public string NameLang;

        [DBFieldName("Cost_lang")]
        public string CostLang;

        [DBFieldName("OutOfError_lang")]
        public string OutOfErrorLang;

        [DBFieldName("ToolTip_lang")]
        public string ToolTipLang;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
