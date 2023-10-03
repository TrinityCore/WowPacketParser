namespace WowPacketParser.Proto.Processing
{
    public interface IPacketProcessor<T>
    {
        void Initialize(ulong gameBuild) { }
        T? Process(PacketHolder packet);
    }
}
