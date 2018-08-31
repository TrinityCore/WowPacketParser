using System;

namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    public sealed class DBFileAttribute : Attribute
    {
        public string FileName { get;}

        public DBFileAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}
