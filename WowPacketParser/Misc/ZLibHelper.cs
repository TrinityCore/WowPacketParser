using System;
using System.Runtime.InteropServices;
using WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.Misc
{
    internal partial class ZLibHelper
    {
        private const string CompressionLib = "compression_native";

        [LibraryImport(CompressionLib, EntryPoint = "CompressionNative_InflateInit2_")]
        private static partial int InflateInit2(ref ZStream zStream, int windowBits);

        [LibraryImport(CompressionLib, EntryPoint = "CompressionNative_Inflate")]
        private static partial int Inflate(ref ZStream zStream, FlushCode flush);

        [LibraryImport(CompressionLib, EntryPoint = "CompressionNative_InflateEnd")]
        private static partial int InflateEnd(ref ZStream zStream);

        public static void ResetInflateStream(int connectionIndex)
        {
            ref var zStream = ref CollectionsMarshal.GetValueRefOrAddDefault(SessionHandler.ZStreams, connectionIndex, out bool exists);

            InflateInit2(ref zStream, 15);
        }

        public static unsafe bool TryInflate(int inflatedSize, int index, byte[] arr, ref byte[] newarr)
        {
            try
            {
                ref var zStream = ref CollectionsMarshal.GetValueRefOrAddDefault(SessionHandler.ZStreams, index, out bool exists);

                if (!exists)
                {
                    var initResult = InflateInit2(ref zStream, 15);

                    if (initResult != 0)
                        return false;
                }

                fixed (byte* compressedData = arr)
                fixed (byte* uncompressedData = newarr)
                {
                    zStream.AvailIn = (uint)arr.Length;
                    zStream.NextIn = compressedData;
                    zStream.AvailOut = (uint)inflatedSize;
                    zStream.NextOut = uncompressedData;

                    Inflate(ref zStream, FlushCode.SyncFlush);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static unsafe void Inflate(int inflatedSize, byte[] arr, byte[] newarr)
        {
            ZStream zStream = default;

            InflateInit2(ref zStream, 15);

            fixed (byte* compressedData = arr)
            fixed (byte* uncompressedData = newarr)
            {
                zStream.AvailIn = (uint)arr.Length;
                zStream.NextIn = compressedData;
                zStream.AvailOut = (uint)inflatedSize;
                zStream.NextOut = uncompressedData;

                Inflate(ref zStream, FlushCode.NoFlush);
                Inflate(ref zStream, FlushCode.Finish);
                InflateEnd(ref zStream);
            }
        }
    }
}
