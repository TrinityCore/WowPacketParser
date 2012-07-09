using System;
using PacketParser.Enums;
using PacketParser.DataStructures;

namespace PacketParser.Loading
{
    public interface IPacketReader : IDisposable
    {
        bool CanRead();
        Packet Read(int number, string fileName);
        ClientVersionBuild GetBuild();
        uint GetProgress();
    }
}
