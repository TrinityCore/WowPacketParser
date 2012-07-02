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
        string Header;
        public bool Init(SniffFile file)
        {
            FileName = file.FileName;
            LogPrefix = file.LogPrefix;
            Header = file.GetHeader();
            return Settings.SQLOutput != SQLOutputFlags.None;
        }
        public void ProcessPacket(Packet packet)
        {
        }
        public void ProcessedPacket(Packet packet)
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

            Builder.DumpSQL(string.Format("{0}: Dumping sql", LogPrefix), sqlFileName, Header);
        }
        public void ProcessData(string name, int? index, Object obj, Type t) { }
    }
}
