// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExtendedBitConverter.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace WowPacketParser.Misc.BigMath
{
    /// <summary>
    ///     Bit converter methods which support explicit endian.
    /// </summary>
    public static class ExtendedBitConverter
    {
        /// <summary>
        ///     Indicates the byte order ("endianness") in which data is stored in this computer architecture.
        /// </summary>
        public static readonly bool IsLittleEndian = BitConverter.IsLittleEndian;

        #region Int16
        /// <summary>
        ///     Converts <see cref="short" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this short value, bool? asLittleEndian = null)
        {
            var buffer = new byte[2];
            value.ToBytes(buffer, 0, asLittleEndian);
            return buffer;
        }

        /// <summary>
        ///     Converts <see cref="short" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">Buffer at least 2 bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this short value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian)
            {
                buffer[offset] = (byte)value;
                buffer[offset + 1] = (byte)(value >> 8);
            }
            else
            {
                buffer[offset] = (byte)(value >> 8);
                buffer[offset + 1] = (byte)(value);
            }
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="short" />.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="short" /> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ToInt16(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 2, offset, ale);

            return (short)(ale ? bytes[offset] | bytes[offset + 1] << 8 : bytes[offset] << 8 | bytes[offset + 1]);
        }
        #endregion

        #region UInt16
        /// <summary>
        ///     Converts <see cref="ushort" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this ushort value, bool? asLittleEndian = null)
        {
            return unchecked((short)value).ToBytes(asLittleEndian);
        }

        /// <summary>
        ///     Converts <see cref="ushort" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">Buffer at least 2 bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this ushort value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            unchecked((short)value).ToBytes(buffer, offset, asLittleEndian);
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="ushort" />.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="ushort" /> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ToUInt16(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            return (ushort)bytes.ToInt16(offset, asLittleEndian);
        }
        #endregion

        #region Int32
        /// <summary>
        ///     Converts <see cref="int" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this int value, bool? asLittleEndian = null)
        {
            var buffer = new byte[4];
            value.ToBytes(buffer, 0, asLittleEndian);
            return buffer;
        }

        /// <summary>
        ///     Converts <see cref="int" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">Buffer at least 4 bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this int value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            if (asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian)
            {
                buffer[offset] = (byte)value;
                buffer[offset + 1] = (byte)(value >> 8);
                buffer[offset + 2] = (byte)(value >> 16);
                buffer[offset + 3] = (byte)(value >> 24);
            }
            else
            {
                buffer[offset] = (byte)(value >> 24);
                buffer[offset + 1] = (byte)(value >> 16);
                buffer[offset + 2] = (byte)(value >> 8);
                buffer[offset + 3] = (byte)value;
            }
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="int" />.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="int" /> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt32(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 4, offset, ale);

            return (ale)
                ? bytes[offset] | bytes[offset + 1] << 8 | bytes[offset + 2] << 16 | bytes[offset + 3] << 24
                : bytes[offset] << 24 | bytes[offset + 1] << 16 | bytes[offset + 2] << 8 | bytes[offset + 3];
        }
        #endregion

        private static void EnsureLength(ref byte[] bytes, int minLength, int offset, bool ale)
        {
            int bytesLength = bytes.Length - offset;
            if (bytesLength < minLength)
            {
                var b = new byte[minLength];
                Buffer.BlockCopy(bytes, offset, b, ale ? 0 : minLength - bytesLength, bytesLength);
                bytes = b;
            }
        }

        private static bool GetIsLittleEndian(bool? asLittleEndian)
        {
            return asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian;
        }

        #region UInt32
        /// <summary>
        ///     Converts <see cref="uint" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this uint value, bool? asLittleEndian = null)
        {
            return unchecked((int)value).ToBytes(asLittleEndian);
        }

        /// <summary>
        ///     Converts <see cref="uint" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">Buffer at least 4 bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this uint value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            unchecked((int)value).ToBytes(buffer, offset, asLittleEndian);
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="uint" />.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="uint" /> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToUInt32(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            return (uint)bytes.ToInt32(offset, asLittleEndian);
        }
        #endregion

        #region Int64
        /// <summary>
        ///     Converts <see cref="long" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this long value, bool? asLittleEndian = null)
        {
            var buffer = new byte[8];
            value.ToBytes(buffer, 0, asLittleEndian);
            return buffer;
        }

        /// <summary>
        ///     Converts <see cref="long" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">Buffer at least 8 bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this long value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            if (asLittleEndian.HasValue ? asLittleEndian.Value : IsLittleEndian)
            {
                buffer[offset] = (byte)value;
                buffer[offset + 1] = (byte)(value >> 8);
                buffer[offset + 2] = (byte)(value >> 16);
                buffer[offset + 3] = (byte)(value >> 24);
                buffer[offset + 4] = (byte)(value >> 32);
                buffer[offset + 5] = (byte)(value >> 40);
                buffer[offset + 6] = (byte)(value >> 48);
                buffer[offset + 7] = (byte)(value >> 56);
            }
            else
            {
                buffer[offset] = (byte)(value >> 56);
                buffer[offset + 1] = (byte)(value >> 48);
                buffer[offset + 2] = (byte)(value >> 40);
                buffer[offset + 3] = (byte)(value >> 32);
                buffer[offset + 4] = (byte)(value >> 24);
                buffer[offset + 5] = (byte)(value >> 16);
                buffer[offset + 6] = (byte)(value >> 8);
                buffer[offset + 7] = (byte)value;
            }
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="long" />.
        /// </summary>
        /// <param name="bytes">An array of bytes. </param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="long" /> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToInt64(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 8, offset, ale);

            return ale
                ? bytes[offset] | (long)bytes[offset + 1] << 8 | (long)bytes[offset + 2] << 16 | (long)bytes[offset + 3] << 24 | (long)bytes[offset + 4] << 32 |
                    (long)bytes[offset + 5] << 40 | (long)bytes[offset + 6] << 48 | (long)bytes[offset + 7] << 56
                : (long)bytes[offset] << 56 | (long)bytes[offset + 1] << 48 | (long)bytes[offset + 2] << 40 | (long)bytes[offset + 3] << 32 |
                    (long)bytes[offset + 4] << 24 | (long)bytes[offset + 5] << 16 | (long)bytes[offset + 6] << 8 | bytes[offset + 7];
        }
        #endregion

        #region UInt64
        /// <summary>
        ///     Converts <see cref="ulong" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        /// <returns>Array of bytes.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] ToBytes(this ulong value, bool? asLittleEndian = null)
        {
            return unchecked((long)value).ToBytes(asLittleEndian);
        }

        /// <summary>
        ///     Converts <see cref="ulong" /> to array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">Buffer at least 8 bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert to little endian.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ToBytes(this ulong value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            unchecked((long)value).ToBytes(buffer, offset, asLittleEndian);
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="ulong" />.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="ulong" /> value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToUInt64(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            return (ulong)bytes.ToInt64(offset, asLittleEndian);
        }
        #endregion

        #region Int128
        /// <summary>
        ///     Converts an <see cref="Int128" /> value to an array of bytes.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="buffer">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="buffer" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        public static void ToBytes(this Int128 value, byte[] buffer, int offset = 0, bool? asLittleEndian = null)
        {
            bool ale = GetIsLittleEndian(asLittleEndian);
            value.Low.ToBytes(buffer, ale ? offset : offset + 8, ale);
            value.High.ToBytes(buffer, ale ? offset + 8 : offset, ale);
        }

        /// <summary>
        ///     Converts an <see cref="Int128" /> value to a byte array.
        /// </summary>
        /// <param name="value">Value.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <param name="trimZeros">Trim zero bytes from left or right, depending on endian.</param>
        /// <returns>Array of bytes.</returns>
        public static byte[] ToBytes(this Int128 value, bool? asLittleEndian = null, bool trimZeros = false)
        {
            var buffer = new byte[16];
            value.ToBytes(buffer, 0, asLittleEndian);

            if (trimZeros)
            {
                buffer = buffer.TrimZeros(asLittleEndian);
            }

            return buffer;
        }

        /// <summary>
        ///     Converts array of bytes to <see cref="Int128" />.
        /// </summary>
        /// <param name="bytes">An array of bytes.</param>
        /// <param name="offset">The starting position within <paramref name="bytes" />.</param>
        /// <param name="asLittleEndian">Convert from little endian.</param>
        /// <returns><see cref="Int128" /> value.</returns>
        public static Int128 ToInt128(this byte[] bytes, int offset = 0, bool? asLittleEndian = null)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (bytes.Length == 0)
            {
                return 0;
            }
            if (bytes.Length <= offset)
            {
                throw new InvalidOperationException("Array length must be greater than offset.");
            }
            bool ale = GetIsLittleEndian(asLittleEndian);
            EnsureLength(ref bytes, 16, offset, ale);

            return new Int128(bytes.ToUInt64(ale ? offset + 8 : offset, ale), bytes.ToUInt64(ale ? offset : offset + 8, ale));
        }
        #endregion
    }
}
