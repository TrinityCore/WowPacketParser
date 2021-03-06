using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WowPacketParser.Store.Objects.UpdateFields.LegacyImplementation;

namespace WowPacketParser.Misc
{
    public class DynamicUpdateField<T>
    {
        private readonly List<T> _values = new List<T>();
        public BitArray UpdateMask { get; private set; } = new BitArray(0);

        public delegate void ActionUpdater(int idx, ref T value);

        public void ReadAll(Action<T> action)
        {
            lock (_values)
            {
                for (int i = 0; i < _values.Count; ++i)
                {
                    action(_values[i]);
                }
            }
        }

        public void UpdateAllByUpdateMask(Func<int, T, T> updater)
        {
            lock (_values)
            {
                for (int i = 0; i < _values.Count; ++i)
                {
                    if (UpdateMask[i])
                    {
                        _values[i] = updater(i, _values[i]);
                    }
                }
            }
        }

        public void FillAllByUpdateMask(Func<int, T> updater)
        {
            lock (_values)
            {
                for (int i = 0; i < _values.Count; ++i)
                {
                    if (UpdateMask[i])
                    {
                        _values[i] = updater(i);
                    }
                }
            }
        }

        public void FillAll(Func<int, T> reader)
        {
            lock (_values)
            {
                for (int i = 0; i < _values.Count; ++i)
                {
                    _values[i] = reader(i);
                }
            }
        }

        public void Resize(uint newSize)
        {
            lock (_values)
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
        }

        public void ReadUpdateMask(Packet packet, int bitSizeCount = 32)
        {
            var newSize = packet.ReadBits(bitSizeCount);
            Resize(newSize);
            var rawMask = new int[(newSize + 31) / 32];
            if (newSize > 32)
            {
                Func<int> read;
                if (packet.HasUnreadBitsInBuffer())
                    read = () => (int)packet.ReadBits(32);
                else
                    read = () => (int)packet.ReadUInt32();

                for (var i = 0; i < newSize / 32; ++i)
                    rawMask[i] = read();
            }
            if ((newSize % 32) != 0)
                rawMask[newSize / 32] = (int)packet.ReadBits((int)newSize % 32);

            lock (_values)
            {
                UpdateMask = new BitArray(rawMask);
            }
        }

        public void SetUnsafe(int idx, T value)
        {
            _values[idx] = value;
        }
    }
}
