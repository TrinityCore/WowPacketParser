using System;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public interface IPacketReader : IDisposable
    {
        bool CanRead();
        Packet Read(int number, string fileName);
        long GetTotalSize();
        long GetCurrentSize();
    }
}
