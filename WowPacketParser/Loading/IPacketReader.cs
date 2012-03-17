using System;
using WowPacketParser.Misc;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.Loading
{
    public interface IPacketReader : IDisposable
    {
        bool CanRead();
        Packet Read(int number, SniffFileInfo fileInfo);
        //void Close();
        //void Dispose();
    }
}
