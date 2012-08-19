using WowPacketParser.Enums;

namespace WowPacketParser.Store.Objects
{
    public sealed class Player : WoWObject
    {
        public Race Race;

        public Class Class;

        public string Name;

        public bool FirstLogin;

        public int Level;

        // Used when inserting data from SMSG_CHAR_ENUM into the Objects container
        public static WoWObject UpdatePlayerInfo(Player oldPlayer, Player newPlayer)
        {
            oldPlayer.Race = newPlayer.Race;
            oldPlayer.Class = newPlayer.Class;
            oldPlayer.Name = newPlayer.Name;
            oldPlayer.FirstLogin = newPlayer.FirstLogin;
            oldPlayer.Level = newPlayer.Level;

            return oldPlayer;
        }
    }
}
