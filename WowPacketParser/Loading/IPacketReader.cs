using System;
using WowPacketParser.Misc;
using WowPacketParser.Enums;
namespace WowPacketParser.Loading
{
    public interface IPacketReader : IDisposable
    {
        bool CanRead();
        Packet Read(int number, string fileName);
        ClientVersionBuild GetBuild();
    }
}
