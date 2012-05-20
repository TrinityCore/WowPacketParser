using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WowPacketParser.Misc;
using WowPacketParser.Loading;
using WowPacketParser.Enums;
using WowPacketParser.SQL;

namespace WowPacketParser.Processing
{
    public class SQLFileOutput : IPacketProcessor
    {
        string FileName;
        string LogPrefix;
        SniffFile File;
        public bool Init(SniffFile file)
        {
            FileName = file.FileName;
            LogPrefix = file.LogPrefix;
            File = file;
            return Settings.SQLOutput != SQLOutputFlags.None;
        }
        public void ProcessPacket(Packet packet)
        {
        }
        public void Finish() 
        {
            string sqlFileName;
            if (String.IsNullOrWhiteSpace(Settings.SQLFileName))
                sqlFileName = string.Format("{0}_{1}.sql",
                    Utilities.FormattedDateTimeForFiles(), Path.GetFileName(FileName));
            else
                sqlFileName = Settings.SQLFileName;

            Builder.DumpSQL(string.Format("{0}: Dumping sql", LogPrefix), sqlFileName, File.GetHeader());
        }
        public void ProcessData(string name, Object obj, Type t) { }
    }
}
