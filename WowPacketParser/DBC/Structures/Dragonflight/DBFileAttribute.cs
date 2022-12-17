﻿using System;

namespace WowPacketParser.DBC.Structures.Dragonflight
{
    public sealed class DBFileAttribute : Attribute
    {
        public string FileName { get; }

        public DBFileAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}
