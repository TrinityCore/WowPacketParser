using System;
using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Loading
{
    public abstract class Loader
    {
        public readonly string FileToParse;

        protected Loader(string file)
        {
            FileToParse = file;
        }

        public abstract IEnumerable<Packet> ParseFile();
    }
}
