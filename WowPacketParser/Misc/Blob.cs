namespace WowPacketParser.Misc
{
    public sealed class Blob
    {
        public byte[] Data { get; }

        public Blob(byte[] data)
        {
            this.Data = data;
        }
    }
}
