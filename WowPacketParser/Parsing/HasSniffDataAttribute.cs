using System;

namespace WowPacketParser.Parsing
{
    /// <summary>
    /// Defines that a packet handler uses Packet.AddSniffData method
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class HasSniffDataAttribute : Attribute
    {
    }
}
