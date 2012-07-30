using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using PacketParser.Enums;
using PacketDumper.Misc;
using PacketParser.DataStructures;
using PacketParser.Processing;

namespace PacketDumper.Processing
{
    public class EnumErrorOutput : IPacketProcessor
    {
        private readonly Dictionary<string, List<long>> EnumLogs =
            new Dictionary<string, List<long>>();

        public bool LoadOnDepend { get { return false; } }
        public Type[] DependsOn { get { return null; } }

        public ProcessPacketEventHandler ProcessAnyPacketHandler { get { return null; } }
        public ProcessedPacketEventHandler ProcessedAnyPacketHandler { get { return null; } }
        public ProcessDataEventHandler ProcessAnyDataHandler { get { return ProcessData; } }
        public ProcessedDataNodeEventHandler ProcessedAnyDataNodeHandler { get { return null; } }

        public bool Init(PacketFileProcessor proc)
        {
            return Settings.LogEnumErrors;
        }

        public void Finish()
        {
            WriteErrors();
        }

        public void ProcessData(string name, int? index, Object obj, Type t)
        {
            if (obj is StoreEnum)
            {
                var e = (StoreEnum)obj;

                if (e.rawVal > 0)
                    CheckForMissingValues(e.rawVal, e.GetEnumType());
            }
        }
        public void CheckForMissingValues(long rawValue, Type t)
        {
            if (!t.IsEnum || !Attribute.IsDefined(t, typeof(FlagsAttribute)))
                return;

            var key = t.ToString().Replace("PacketParser.Enums.", "");

            // Remove all know values
            foreach (IConvertible value in Enum.GetValues(t))
                rawValue = rawValue & ~Convert.ToInt64(value, CultureInfo.InvariantCulture);

            if (rawValue == 0)
                return;

            long temp = 1;
            while (temp < rawValue)
            {
                if ((rawValue & temp) == temp)
                    AddEnumErrorLog(key, temp);
                temp <<= 2;
            }
        }

        private void AddEnumErrorLog(string key, long rawValue)
        {
            var exists = EnumLogs.ContainsKey(key);
            var list = exists ? EnumLogs[key] : new List<long>();

            if (list.Contains(rawValue))
                return;

            list.Add(rawValue);
            if (!exists)
                EnumLogs.Add(key, list);
        }

        public void WriteErrors()
        {
            Trace.WriteLine(Environment.NewLine);
            foreach (var pair in EnumLogs)
            {
                pair.Value.Sort();

                var errors = "";
                foreach (var error in pair.Value)
                {
                    if (errors.Length > 0)
                        errors += ", ";

                    var str = "";
                    UnknownFlags enumFlag;
                    if (Enum.TryParse(error.ToString(CultureInfo.InvariantCulture), out enumFlag))
                        str = enumFlag.ToString();

                    errors += str;
                }

                Trace.WriteLine(string.Format("{0} has undefined flags: {1}", pair.Key, errors));
            }
        }
    }
}
