using System.Collections.Generic;
using WowPacketParser.Proto;
using WowPacketParser.Proto.Processing;

namespace WowPacketParser.Parsing.Proto;

public abstract class BaseProtoQueryBuilder : PacketProcessor<VoidType>, IProtoQueryBuilder
{
    public abstract bool IsEnabled();

    public string Process(IReadOnlyList<Packets> packetsList)
    {
        foreach (var sniffFile in packetsList)
        {
            // more complex logic is possible, i.e. clear state after each file
            foreach (var packet in sniffFile.Packets_)
            {
                Process(packet);
            }
        }

        return GenerateQuery();
    }

    protected abstract string GenerateQuery();
}