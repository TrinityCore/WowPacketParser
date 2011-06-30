namespace WowPacketParser.Misc
{
    public sealed class UpdateField
    {
        public UpdateField(int val1, float val2)
        {
            Int32Value = val1;
            SingleValue = val2;
        }

        public readonly int Int32Value;

        public readonly float SingleValue;
    }
}
