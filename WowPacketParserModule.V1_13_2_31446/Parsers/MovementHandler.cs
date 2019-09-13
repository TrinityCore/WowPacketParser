using System.Collections.Concurrent;
using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V1_13_2_31446.Parsers
{
    public static class MovementHandler
    {
        public static readonly IDictionary<ushort, bool> ActivePhases = new ConcurrentDictionary<ushort, bool>();

        [Parser(Opcode.SMSG_LOGIN_SET_TIME_SPEED)]
        public static void HandleLoginSetTimeSpeed(Packet packet)
        {
            packet.ReadPackedTime("ServerTime");
            packet.ReadPackedTime("GameTime");
            packet.ReadSingle("NewSpeed");
        }
    }
}
