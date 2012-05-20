using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Processing
{
    public class FileLock<T>
    {
        private const int Timeout = 3000;
        private static readonly Dictionary<T, References> Locks = new Dictionary<T, References>();

        public IDisposable Lock(T fileName)
        {
            Monitor.Enter(Locks);
            References obj;
            if (Locks.TryGetValue(fileName, out obj))
            {
                obj.Addquire();
                Monitor.Exit(Locks);
                if (!Monitor.TryEnter(obj, Timeout))
                    throw new TimeoutException(String.Format("{0}", fileName));
            }
            else
            {
                obj = new References();
                Monitor.Enter(obj);
                Locks.Add(fileName, obj);
                Monitor.Exit(Locks);
            }

            return new Locker<T>(fileName);
        }

        private static void Unlock(T fileName)
        {
            lock (Locks)
            {
                References obj;
                if (Locks.TryGetValue(fileName, out obj))
                {
                    Monitor.Exit(obj);
                    if (0 == obj.Release())
                        Locks.Remove(fileName);
                }
            }
        }

        private class References
        {
            private int _count = 1;
            public void Addquire()
            {
                Interlocked.Increment(ref _count);
            }

            public int Release()
            {
                return Interlocked.Decrement(ref _count);
            }
        }

        class Locker<T2> : IDisposable
        {
            private readonly T2 FileName;

            public Locker(T2 fileName)
            {
                FileName = fileName;
            }

            public void Dispose()
            {
                FileLock<T2>.Unlock(FileName);
            }
        }
    }

    public static class SplitRawOutput
    {
        public static readonly FileLock<string> Locks = new FileLock<string>();
        public const string Folder = "split"; // might want to move to config later
    }
}
