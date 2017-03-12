using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGMCharacterRestoreResponse
    {
        public uint SrcAccount;
        public List<ClientGMCharacterRestoreResponseWarning> WarningStrings;
        public string ResultDescription;
        public uint DstAccount;
        public bool Success;
        public uint ResultCode;
        public uint Token;
        public ulong NewCharacterGUID;
        public ulong OldCharacterGUID;
    }
}
