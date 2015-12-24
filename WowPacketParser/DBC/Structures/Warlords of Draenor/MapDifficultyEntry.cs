using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    [DBFileName("MapDifficulty")]

    public sealed class MapDifficultyEntry
    {
        public string Message;
        public ushort MapID;
        public byte DifficultyID;
        public byte RaidDurationType;
        public byte MaxPlayers;
        public byte LockID;
        public byte ItemBonusTreeModID;
        public uint Context;
    }
}
