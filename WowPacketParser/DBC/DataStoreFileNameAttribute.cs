using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.DBC
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class DataStoreFileNameAttribute : Attribute
    {
        readonly string _fileName;

        public DataStoreFileNameAttribute(string filename)
        {
            this._fileName = filename;
        }

        public string FileName
        {
            get { return _fileName; }
        }
    }
}
