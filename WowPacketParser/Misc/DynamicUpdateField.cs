using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace WowPacketParser.Misc
{
    public class DynamicUpdateField<T>
    {
        private readonly List<T> _values = new List<T>();
        public BitArray UpdateMask { get; private set; } = new BitArray(0);

        public int Count => _values.Count;

        public T this[int index]
        {
            get { return _values[index]; }
            set { _values[index] = value; }
        }

        public void Resize(uint newSize)
        {
            int newCount = (int)newSize; // this is stupid but packets have unsigned values in them and this method is intended to be used in packet readers
            int current = _values.Count;
            if (newCount < current)
                _values.RemoveRange(newCount, current - newCount);
            else if (newCount > current)
            {
                if (newCount > _values.Capacity)
                    _values.Capacity = newCount;
                _values.AddRange(Enumerable.Repeat(default(T), newCount - current));
            }
        }

        public void ReadUpdateMask(Packet packet, int bitSizeCount = 32)
        {
            var newSize = packet.ReadBits(bitSizeCount);
            Resize(newSize);
            var rawMask = new int[(newSize + 31) / 32];
            if (newSize > 32)
            {
                if (packet.HasUnreadBitsInBuffer())
                {
                    for (var i = 0; i < newSize / 32; ++i)
                        rawMask[i] = (int)packet.ReadBits(32);
                }
                else
                {
                    for (var i = 0; i < newSize / 32; ++i)
                        rawMask[i] = packet.ReadInt32();
                }
            }
            else if (newSize == 32)
                rawMask[0] = (int)packet.ReadBits(32);

            if ((newSize % 32) != 0)
                rawMask[newSize / 32] = (int)packet.ReadBits((int)newSize % 32);

            UpdateMask = new BitArray(rawMask);
        }
    }
}
