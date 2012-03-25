namespace WowPacketParser.Misc
{
    public struct UpdateField
    {
        public UpdateField(uint val1, float val2)
        {
            UInt32Value = val1;
            SingleValue = val2;
        }

        public readonly uint UInt32Value;

        public readonly float SingleValue;
    }
}
