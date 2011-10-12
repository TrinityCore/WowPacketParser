using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public interface IPacketReader
    {
        bool CanRead();
        Packet Read(int number);
        void Close();
    }
}
