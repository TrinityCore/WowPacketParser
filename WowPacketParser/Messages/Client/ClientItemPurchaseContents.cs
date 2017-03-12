namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientItemPurchaseContents
    {
        public uint Money;
        public ClientItemPurchaseRefundItem[/*5*/] Items;
        public ClientItemPurchaseRefundCurrency[/*5*/] Currencies;
    }
}
