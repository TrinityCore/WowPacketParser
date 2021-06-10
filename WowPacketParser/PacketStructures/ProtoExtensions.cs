using WowPacketParser.Misc;
using WoWPacketParser.Proto;

namespace WowPacketParser.PacketStructures
{
    public static class ProtoExtensions
    {
        public static UniversalGuid ToUniversal(this WowGuid128 guid)
        {
            return new UniversalGuid()
            {
                Entry = guid.GetEntry(),
                Type = (UniversalHighGuid)(int)guid.HighGuid.GetHighGuidType(),
                Guid128 = new UniversalGuid128()
                {
                    Low = guid.Low,
                    High = guid.High
                }
            };
        }
        
        public static UniversalGuid ToUniversal(this WowGuid64 guid)
        {
            return new UniversalGuid()
            {
                Entry = guid.GetEntry(),
                Type = (UniversalHighGuid)(int)guid.HighGuid.GetHighGuidType(),
                Guid64 = new UniversalGuid64()
                {
                    Low = guid.Low,
                    High = guid.High
                }
            };
        }
    }
}