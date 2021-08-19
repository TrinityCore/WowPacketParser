namespace WowPacketParser.Proto.Processing
{
    public interface IPacketProcessor<T>
    {
        T? Process(PacketHolder packet);
    }
}
