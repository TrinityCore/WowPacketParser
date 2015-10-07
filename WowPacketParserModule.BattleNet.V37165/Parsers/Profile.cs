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
            packet.Read<byte>(5);
            packet.Stream.AddValue("Path", Utilities.ByteArrayToHexString(packet.ReadBytes(packet.Read<int>(6))));
            packet.Read<uint>(21);
            packet.Read<ulong>("Id", 64, "Address");
            packet.Read<uint>("Label", 32, "Address");
            packet.Read<SettingsType>("Type", 2);
        }
    }
}
