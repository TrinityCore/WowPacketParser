using System.Collections.Generic;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.DataStructures;
using System;

namespace PacketParser.Processing
{
    public class SessionStore : IPacketProcessor
    {
        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return null; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return null; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public Guid? LoginGuid = null;
        public int CurrentAreaId = -1;
        public uint CurrentMapId = 0;
        public int CurrentPhaseMask = 0;

        private Player LastPlayer = null;

        public bool Init(PacketFileProcessor p) { return true; }
        public void Finish() { }

        public Player LoggedInCharacter
        {
            get
            {
                if (LoginGuid != null)
                {
                    if (LastPlayer != null)
                    {
                        if (LastPlayer.GetGuid() == LoginGuid)
                            return LastPlayer;
                    }
                    return (Player)(PacketFileProcessor.Current.GetProcessor<ObjectStore>().GetObjectIfFound((Guid)LoginGuid));
                }
                return null;
            }
        }
        public Dictionary<uint, Guid> PetGuids = new Dictionary<uint,Guid>();
    }
}
