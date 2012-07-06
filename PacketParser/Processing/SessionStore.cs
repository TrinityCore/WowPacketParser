using System.Collections.Generic;
using Guid = PacketParser.DataStructures.Guid;
using PacketParser.DataStructures;
using System;

namespace PacketParser.Processing
{
    public class SessionStore : IPacketProcessor
    {
        public Guid? LoginGuid = null;
        private Player LastPlayer = null;

        public bool Init(PacketFileProcessor p) { return true; }
        public void ProcessPacket(Packet packet) { }
        public void ProcessData(string name, int? index, Object obj, Type t) { }
        public void ProcessedPacket(Packet packet) { }
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
