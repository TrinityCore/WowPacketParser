using System.Runtime.InteropServices;

namespace WowPacketParser.Misc
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public unsafe struct ZStream
    {
        internal byte* NextIn;
        internal byte* NextOut;
        internal nint Msg;

        readonly nint InternalState;

        internal uint AvailIn;
        internal uint AvailOut;
    }
}
