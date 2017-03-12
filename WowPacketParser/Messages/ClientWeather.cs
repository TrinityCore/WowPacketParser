using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientWeather
    {
        public bool Abrupt;
        public float Intensity;
        public uint WeatherID;
    }
}
