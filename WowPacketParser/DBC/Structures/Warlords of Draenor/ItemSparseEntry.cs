using DBFilesClient.NET;

namespace WowPacketParser.DBC.Structures
{
    public sealed class ItemSparseEntry
    {
        public uint Id;

        [StoragePresence(StoragePresenceOption.Include, ArraySize = 69)]
        public int[] PlaceHolder1;

        public string Name;
        public string Name2;
        public string Name3;
        public string Name4;
        public string Description;

        [StoragePresence(StoragePresenceOption.Include, ArraySize = 27)]
        public int[] PlaceHolder2;
    }
}
