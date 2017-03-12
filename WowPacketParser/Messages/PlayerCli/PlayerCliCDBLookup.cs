namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliCDBLookup
    {
        public string SearchString;
        public bool ReturnLocalizedStrings;
        public int Locale;
        public bool OnlySearchLocalizedFields;
    }
}
