// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathUtils.cs">
//   Copyright (c) 2013 Alexander Logger. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace WowPacketParser.Misc.BigMath
{
    /// <summary>
    ///     Math utils.
    /// </summary>
    public static class MathUtils
    {
        private const ulong Base = 0x100000000;

        /// <summary>
        ///     Bitwise shift array of <see cref="ulong" />.
        /// </summary>
        /// <param name="values">Bits to shift. Lower bits have lower index in array.</param>
        /// <param name="shift">Shift amount in bits. Negative for left shift, positive for right shift.</param>
        /// <returns>Shifted values.</returns>
        public static ulong[] Shift(ulong[] values, int shift)
        {
            if (shift == 0)
            {
                return values;
            }
            return shift < 0 ? ShiftLeft(values, -shift) : ShiftRight(values, shift);
        }


        /// <summary>
        ///     Bitwise right shift.
        /// </summary>
        /// <param name="values">Bits to shift. Lower bits have lower index in array.</param>
        /// <param name="shift">Shift amount in bits.</param>
        /// <returns>Shifted values.</returns>
        public static ulong[] ShiftRight(ulong[] values, int shift)
        {
            if (shift < 0)
            {
                return ShiftLeft(values, -shift);
            }

            const int valueLength = sizeof(ulong) * 8;
            int length = values.Length;

            shift = shift % (length * valueLength);

            int shiftOffset = shift / valueLength;
            int bshift = shift % valueLength;

            var shifted = new ulong[length];
            for (int i = 0; i < length; i++)
            {
                int ishift = i - shiftOffset;
                if (ishift < 0)
                {
                    continue;
                }
                shifted[ishift] |= values[i] >> bshift;
                if (bshift > 0 && i + 1 < length)
                {
                    shifted[ishift] |= values[i + 1] << valueLength - bshift;
                }
            }

            return shifted;
        }

        /// <summary>
        ///     Bitwise right shift.
        ///
        ///     Using an array of ulong's, but when called from Int128 and Int256, value is really a signed number, so we need to preserve the sign bits
        /// </summary>
        /// <param name="values">Bits to shift. Lower bits have lower index in array.</param>
        /// <param name="shift">Shift amount in bits.</param>
        /// <returns>Shifted values.</returns>
        public static ulong[] ShiftRightSigned(ulong[] values, int shift)
        {
            if (shift < 0)
            {
                return ShiftLeft(values, -shift);
            }

            const int valueLength = sizeof(ulong) * 8;
            int length = values.Length;

            shift = shift % (length * valueLength);     //This is the defined behavior of shift. Shifting by greater than the number of bits uses a mod

            //
            //  First, shift over by full ulongs. This could be optimized a bit for longer arrays (if shifting by multiple longs, we do more copies
            //  than needed), but for short arrays this is probably the best way to go
            //
            while (shift >= valueLength)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    values[i] = values[i + 1];
                }
                values[length - 1] = (ulong)((long)values[length - 1] >> (valueLength - 1));    //Preserve sign of upper long, will either be 0 or all f's
                shift -= valueLength;
            }

            //
            //  Now, we just have a sub-long shift left to do (shift will be < 64 at this point)
            //
            if (shift == 0)
                return (values);
            int bshift = valueLength - shift;

            //
            //  In right shifting, upper val is a special case because we need to preserve the sign bits, and because we don't need to or in
            //  any other values
            //
            var shifted = new ulong[length];
            shifted[length - 1] = (ulong)((long)values[length - 1] >> shift);    //Preserve sign of upper long
            for (int i = 0; i < length - 1; i++)
            {
                shifted[i] = values[i] >> shift;                   //Unsigned, so upper bits stay zero
                shifted[i] |= (values[i + 1] << bshift);
            }
            return shifted;
        }

        /// <summary>
        ///     Bitwise right shift.
        /// </summary>
        /// <param name="values">Bits to shift. Lower bits have lower index in array.</param>
        /// <param name="shift">Shift amount in bits.</param>
        /// <returns>Shifted values.</returns>
        public static ulong[] ShiftLeft(ulong[] values, int shift)
        {
            if (shift < 0)
            {
                return ShiftRight(values, -shift);
            }

            const int valueLength = sizeof(ulong) * 8;
            int length = values.Length;

            shift = shift % (length * valueLength);

            int shiftOffset = shift / valueLength;
            int bshift = shift % valueLength;

            var shifted = new ulong[length];
            for (int i = 0; i < length; i++)
            {
                int ishift = i + shiftOffset;
                if (ishift >= length)
                {
                    continue;
                }
                shifted[ishift] |= values[i] << bshift;
                if (bshift > 0 && i - 1 >= 0)
                {
                    shifted[ishift] |= values[i - 1] >> valueLength - bshift;
                }
            }

            return shifted;
        }

        public static Int128 GCD(this Int128 a, Int128 b)
        {
            while (true)
            {
                if (b == 0)
                {
                    return a;
                }
                Int128 a1 = a;
                a = b;
                b = a1 % b;
            }
        }

        private static int GetNormalizeShift(uint value)
        {
            int shift = 0;

            if ((value & 0xFFFF0000) == 0)
            {
                value <<= 16;
                shift += 16;
            }
            if ((value & 0xFF000000) == 0)
            {
                value <<= 8;
                shift += 8;
            }
            if ((value & 0xF0000000) == 0)
            {
                value <<= 4;
                shift += 4;
            }
            if ((value & 0xC0000000) == 0)
            {
                value <<= 2;
                shift += 2;
            }
            if ((value & 0x80000000) == 0)
            {
                shift += 1;
            }

            return shift;
        }

        private static void Normalize(uint[] u, int l, uint[] un, int shift)
        {
            uint carry = 0;
            int i;
            if (shift > 0)
            {
                int rshift = 32 - shift;
                for (i = 0; i < l; i++)
                {
                    uint ui = u[i];
                    un[i] = (ui << shift) | carry;
                    carry = ui >> rshift;
                }
            }
            else
            {
                for (i = 0; i < l; i++)
                {
                    un[i] = u[i];
                }
            }

            while (i < un.Length)
            {
                un[i++] = 0;
            }

            if (carry != 0)
            {
                un[l] = carry;
            }
        }

        private static void Unnormalize(uint[] un, out uint[] r, int shift)
        {
            int length = un.Length;
            r = new uint[length];

            if (shift > 0)
            {
                int lshift = 32 - shift;
                uint carry = 0;
                for (int i = length - 1; i >= 0; i--)
                {
                    uint uni = un[i];
                    r[i] = (uni >> shift) | carry;
                    carry = (uni << lshift);
                }
            }
            else
            {
                for (int i = 0; i < length; i++)
                {
                    r[i] = un[i];
                }
            }
        }

        private static int GetLength(uint[] uints)
        {
            int index = uints.Length - 1;
            while ((index >= 0) && (uints[index] == 0))
            {
                index--;
            }
            index = index < 0 ? 0 : index;
            return index + 1;
        }

        private static uint[] TrimZeros(uint[] uints)
        {
            var trimmed = new uint[GetLength(uints)];
            Buffer.BlockCopy(uints, 0, trimmed, 0, trimmed.Length * 4);
            return trimmed;
        }

        public static void DivModUnsigned(uint[] u, uint[] v, out uint[] q, out uint[] r)
        {
            int m = GetLength(u);
            int n = GetLength(v);

            if (n <= 1)
            {
                //  Divide by single digit
                //
                ulong rem = 0;
                uint v0 = v[0];
                q = new uint[m];
                r = new uint[1];

                for (int j = m - 1; j >= 0; j--)
                {
                    rem *= Base;
                    rem += u[j];

                    ulong div = rem / v0;
                    rem -= div * v0;
                    q[j] = (uint)div;
                }
                r[0] = (uint)rem;
            }
            else if (m >= n)
            {
                int shift = GetNormalizeShift(v[n - 1]);

                var un = new uint[m + 1];
                var vn = new uint[n];

                Normalize(u, m, un, shift);
                Normalize(v, n, vn, shift);

                q = new uint[m - n + 1];

                //  Main division loop
                //
                for (int j = m - n; j >= 0; j--)
                {
                    int i;

                    ulong rr = Base * un[j + n] + un[j + n - 1];
                    ulong qq = rr / vn[n - 1];
                    rr -= qq * vn[n - 1];

                    for (; ; )
                    {
                        // Estimate too big ?
                        //
                        if ((qq >= Base) || (qq * vn[n - 2] > (rr * Base + un[j + n - 2])))
                        {
                            qq--;
                            rr += vn[n - 1];
                            if (rr < Base)
                            {
                                continue;
                            }
                        }
                        break;
                    }


                    //  Multiply and subtract
                    //
                    long b = 0;
                    long t;
                    for (i = 0; i < n; i++)
                    {
                        ulong p = vn[i] * qq;
                        t = un[i + j] - (long)(uint)p - b;
                        un[i + j] = (uint)t;
                        p >>= 32;
                        t >>= 32;
                        b = (long)p - t;
                    }
                    t = un[j + n] - b;
                    un[j + n] = (uint)t;

                    //  Store the calculated value
                    //
                    q[j] = (uint)qq;

                    //  Add back vn[0..n] to un[j..j+n]
                    //
                    if (t < 0)
                    {
                        q[j]--;
                        ulong c = 0;
                        for (i = 0; i < n; i++)
                        {
                            c = (ulong)vn[i] + un[j + i] + c;
                            un[j + i] = (uint)c;
                            c >>= 32;
                        }
                        c += un[j + n];
                        un[j + n] = (uint)c;
                    }
                }

                Unnormalize(un, out r, shift);
            }
            else
            {
                q = new uint[] { 0 };
                r = u;
            }

            q = TrimZeros(q);
            r = TrimZeros(r);
        }
    }
}
