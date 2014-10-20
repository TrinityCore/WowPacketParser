// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayUtils.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;

namespace WowPacketParser.Misc.BigMath
{
    /// <summary>
    ///     Utils for the <see cref="Array" /> class.
    /// </summary>
    public static class ArrayUtils
    {
        private static readonly byte[] CharToByteLookupTable =
        {
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e,
            0x0f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff,
            0xff, 0xff, 0xff, 0xff
        };

        private static readonly char[][] LookupTableUpper;
        private static readonly char[][] LookupTableLower;

        static ArrayUtils()
        {
            LookupTableLower = new char[256][];
            LookupTableUpper = new char[256][];
            for (int i = 0; i < 256; i++)
            {
                LookupTableLower[i] = i.ToString("x2").ToCharArray();
                LookupTableUpper[i] = i.ToString("X2").ToCharArray();
            }
        }

        /// <summary>
        ///     Converts an array of one type to an array of another type.
        /// </summary>
        /// <returns>
        ///     An array of the target type containing the converted elements from the source array.
        /// </returns>
        /// <param name="array">The one-dimensional, zero-based <see cref="T:System.Array" /> to convert to a target type.</param>
        /// <param name="convert">A <see cref="Func{TInput, TOutput}" /> that converts each element from one type to another type.</param>
        /// <typeparam name="TInput">The type of the elements of the source array.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the target array.</typeparam>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="array" /> is null.-or-<paramref name="convert" /> is
        ///     null.
        /// </exception>
        public static TOutput[] ConvertAll<TInput, TOutput>(this TInput[] array, Func<TInput, TOutput> convert)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }
            if (convert == null)
            {
                throw new ArgumentNullException("convert");
            }
            var outputArray = new TOutput[array.Length];
            for (int index = 0; index < array.Length; ++index)
            {
                outputArray[index] = convert(array[index]);
            }
            return outputArray;
        }

        /// <summary>
        ///     Get length of serial non zero items.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        /// <param name="asLittleEndian">True - skip all zero items from high. False - skip all zero items from low.</param>
        /// <returns>Length of serial non zero items.</returns>
        public static int GetNonZeroLength(this byte[] bytes, bool? asLittleEndian = null)
        {
            bool ale = GetIsLittleEndian(asLittleEndian);

            if (ale)
            {
                int index = bytes.Length - 1;
                while ((index >= 0) && (bytes[index] == 0))
                {
                    index--;
                }
                index = index < 0 ? 0 : index;
                return index + 1;
            }
            else
            {
                int index = 0;
                while ((index < bytes.Length) && (bytes[index] == 0))
                {
                    index++;
                }
                index = index >= bytes.Length ? bytes.Length - 1 : index;
                return bytes.Length - index;
            }
        }

        /// <summary>
        ///     Trim zero items.
        /// </summary>
        /// <param name="bytes">Array of bytes.</param>
        /// <param name="asLittleEndian">True - trim from high, False - trim from low.</param>
        /// <returns>Trimmed array of bytes.</returns>
        public static byte[] TrimZeros(this byte[] bytes, bool? asLittleEndian = null)
        {
            bool ale = GetIsLittleEndian(asLittleEndian);

            int length = GetNonZeroLength(bytes, ale);

            var trimmed = new byte[length];
            Buffer.BlockCopy(bytes, ale ? 0 : bytes.Length - length, trimmed, 0, length);
            return trimmed;
        }

        public static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static byte[] Combine(byte[] first, byte[] second, byte[] third)
        {
            var ret = new byte[first.Length + second.Length + third.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            Buffer.BlockCopy(third, 0, ret, first.Length + second.Length, third.Length);
            return ret;
        }

        public static byte[] Combine(params byte[][] arrays)
        {
            var ret = new byte[arrays.Sum(x => x.Length)];
            int offset = 0;
            foreach (byte[] data in arrays)
            {
                Buffer.BlockCopy(data, 0, ret, offset, data.Length);
                offset += data.Length;
            }
            return ret;
        }

        public static void Split<T>(this T[] array, int index, out T[] first, out T[] second)
        {
            first = array.Take(index).ToArray();
            second = array.Skip(index).ToArray();
        }

        public static void SplitMidPoint<T>(this T[] array, out T[] first, out T[] second)
        {
            Split(array, array.Length / 2, out first, out second);
        }

        public static byte[] RewriteWithValue(this byte[] bytes, byte value, int offset, int length)
        {
            if (offset + length > bytes.Length)
            {
                throw new InvalidOperationException("Offset + length must be less of equal of the bytes length.");
            }
            var tbytes = (byte[])bytes.Clone();
            for (int i = offset; i < offset + length; i++)
            {
                tbytes[i] = value;
            }
            return tbytes;
        }

        /// <summary>
        ///     Converts array of bytes to hexadecimal string.
        /// </summary>
        /// <param name="bytes">Bytes.</param>
        /// <param name="caps">Capitalize chars.</param>
        /// <param name="min">Minimum string length. 0 if there is no minimum length.</param>
        /// <param name="spaceEveryByte">Space every byte.</param>
        /// <param name="trimZeros">Trim zeros in the result string.</param>
        /// <returns>Hexadecimal string representation of the bytes array.</returns>
        public static unsafe string ToHexString(this byte[] bytes, bool caps = true, int min = 0, bool spaceEveryByte = false, bool trimZeros = false)
        {
            if (bytes.Length == 0)
            {
                return string.Empty;
            }

            int strLength = min;

            int bim = 0;
            if (trimZeros)
            {
                bim = bytes.Length - 1;
                for (int i = 0; i < bytes.Length; i++)
                {
                    if (bytes[i] > 0)
                    {
                        bim = i;
                        int l = (bytes.Length - i) * 2;
                        strLength = (l <= min) ? min : l;
                        break;
                    }
                }
            }
            else
            {
                strLength = bytes.Length * 2;
                strLength = strLength < min ? min : strLength;
            }

            if (strLength == 0)
            {
                return "0";
            }

            int step = 0;
            if (spaceEveryByte)
            {
                strLength += (strLength / 2 - 1);
                step = 1;
            }

            var chars = new char[strLength];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = '0';
            }

            if (spaceEveryByte)
            {
                for (int i = 2; i < chars.Length; i += 3)
                {
                    chars[i] = ' ';
                }
            }

            char[][] lookupTable = caps ? LookupTableUpper : LookupTableLower;
            int bi = bytes.Length - 1;
            int ci = strLength - 1;
            while (bi >= bim)
            {
                char[] chb = lookupTable[bytes[bi--]];
                chars[ci--] = chb[1];
                chars[ci--] = chb[0];
                ci -= step;
            }

            int offset = 0;
            if (trimZeros && strLength > min)
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (c != '0' && c != ' ')
                    {
                        offset = i;
                        break;
                    }
                }
            }

            string str;
            fixed (char* charPtr = chars)
            {
                str = new string(charPtr + offset);
            }
            return str;
        }

        /// <summary>
        ///     Converts string of hex numbers to array of bytes.
        /// </summary>
        /// <param name="hexString">String value.</param>
        /// <returns>Array of bytes.</returns>
        public static byte[] HexToBytes(this string hexString)
        {
            byte[] bytes;
            if (String.IsNullOrWhiteSpace(hexString))
            {
                bytes = new byte[0];
            }
            else
            {
                int stringLength = hexString.Length;
                int characterIndex = (hexString.StartsWith("0x", StringComparison.Ordinal)) ? 2 : 0;
                // Does the string define leading HEX indicator '0x'. Adjust starting index accordingly.
                int numberOfCharacters = stringLength - characterIndex;

                bool addLeadingZero = false;
                if (0 != (numberOfCharacters % 2))
                {
                    addLeadingZero = true;

                    numberOfCharacters += 1; // Leading '0' has been striped from the string presentation.
                }

                bytes = new byte[numberOfCharacters / 2]; // Initialize our byte array to hold the converted string.

                int writeIndex = 0;
                if (addLeadingZero)
                {
                    bytes[writeIndex++] = CharToByteLookupTable[hexString[characterIndex]];
                    characterIndex += 1;
                }

                while (characterIndex < hexString.Length)
                {
                    int hi = CharToByteLookupTable[hexString[characterIndex++]];
                    int lo = CharToByteLookupTable[hexString[characterIndex++]];

                    bytes[writeIndex++] = (byte)(hi << 4 | lo);
                }
            }

            return bytes;
        }

        private static bool GetIsLittleEndian(bool? asLittleEndian)
        {
            return asLittleEndian.HasValue ? asLittleEndian.Value : BitConverter.IsLittleEndian;
        }
    }
}
