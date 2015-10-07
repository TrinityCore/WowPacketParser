using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowPacketParserModule.BattleNet.V37165.Enums
{
    [Flags]
    public enum RealmInfoFlags : byte
    {
        VersionMismatch = 0x1,
        Offline = 0x2,
    }
}
