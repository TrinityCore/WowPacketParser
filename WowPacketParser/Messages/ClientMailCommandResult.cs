using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMailCommandResult
    {
        public int Command;
        public int MailID;
        public int QtyInInventory;
        public int BagResult;
        public int AttachID;
        public int ErrorCode;
    }
}
