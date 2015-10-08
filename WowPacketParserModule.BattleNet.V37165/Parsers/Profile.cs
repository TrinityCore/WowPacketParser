using WowPacketParser.Enums.Battlenet;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.BattleNet.V37165.Enums;

namespace WowPacketParserModule.BattleNet.V37165.Parsers
{
    public static class Profile
    {
        [BattlenetParser(ProfileServerCommand.SettingsAvailable)]
        public static void HandleSettingsAvailable(BattlenetPacket packet)
        {
            packet.ReadSkip(5);
            packet.ReadByteArray("Path", 0, 6);
            packet.ReadSkip(21);
            packet.Read<ulong>("Id", 0, 64, "Address");
            packet.Read<uint>("Label", 0, 32, "Address");
            packet.Read<SettingsType>("Type", 1, 2);
        }
    }
}
