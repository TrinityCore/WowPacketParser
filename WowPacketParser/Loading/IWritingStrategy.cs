using System;

namespace WowPacketParser.Loading
{
    public interface IWritingStrategy : IDisposable
    {
        void Write(object input);

        void Flush();
    }
}