using System;

namespace WowPacketParserModule.BattleNet.V37165.Enums
{
    [Flags]
    public enum RealmInfoFlags : byte
    {
        VersionMismatch = 0x1,
        Offline = 0x2,
    }
}
