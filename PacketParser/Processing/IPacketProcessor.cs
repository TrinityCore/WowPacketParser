using System;
using PacketParser.DataStructures;

namespace PacketParser.Processing
{
    public interface IPacketProcessor
    {
        // if true won't be loaded when there's no processor depending on it
        bool LoadOnDepend {get;}
        // array of packet processors which this processor uses
        // processors listed here will be called before this processor when hooks are executed
        Type[] DependsOn{get;}

        // called at initialization of processor, return false to not load the processor
        // this function should register used packet processing functions in the processor
        bool Init(PacketFileProcessor processor);
        // called when processing of a file is finished
        void Finish();

        ProcessPacketEventHandler ProcessAnyPacketHandler {get;}
        ProcessedPacketEventHandler ProcessedAnyPacketHandler{get;}
        ProcessDataEventHandler ProcessAnyDataHandler{get;}
        ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get; }
    }

    public delegate void ProcessPacketEventHandler(Packet packet);

    public delegate void ProcessedPacketEventHandler(Packet packet);

    public delegate void ProcessDataEventHandler(string name, int? index, Object obj, Type t);

    public delegate void ProcessedDataNodeEventHandler(string name, Object obj, Type t);

}
