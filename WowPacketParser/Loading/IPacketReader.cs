using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Loading
{
    public interface IPacketReader
    {
        bool CanRead();
        Packet Read(int number, SniffFileInfo fileInfo);
        void Close();
    }
}
