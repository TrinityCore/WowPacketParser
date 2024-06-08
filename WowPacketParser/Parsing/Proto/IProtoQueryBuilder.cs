using System.Collections.Generic;
using WowPacketParser.Proto;

namespace WowPacketParser.Parsing.Proto;

public interface IProtoQueryBuilder
{
    bool IsEnabled();
    string Process(IReadOnlyList<Packets> packets);
}