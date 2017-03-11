namespace WowPacketParser.Misc
{
    public interface IOutputBuilder
    {
        void Append(string value);

        void Clear();
    }
}