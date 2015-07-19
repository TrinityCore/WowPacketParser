using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class VendorItem
    {
        public uint ItemId;

        public uint Slot;

        public int MaxCount;

        public uint ExtendedCostId;

        public uint Type;

        public int PlayerConditionID;

        public bool IgnoreFiltering;

        public int VerifiedBuild = ClientVersion.BuildInt;

    }
}
