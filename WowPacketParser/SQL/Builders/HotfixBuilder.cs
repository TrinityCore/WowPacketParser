using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class HotfixBuilder
    {
        [BuilderMethod]
        public static string Hotfixes()
        {
            var stringBuilder = new StringBuilder();

            if (ClientLocale.PacketLocale == LocaleConstant.enUS)
            {
                // ReSharper disable once LoopCanBePartlyConvertedToQuery
                foreach (DB2Hash hashValue in Enum.GetValues(typeof (DB2Hash)))
                    if (HotfixSettings.Instance.ShouldLog(hashValue))
                        HotfixStoreMgr.GetStore(hashValue)?.Serialize(stringBuilder, null);
            }
            else
            {
                foreach (DB2Hash hashValue in Enum.GetValues(typeof (DB2Hash)))
                {
                    if (!HotfixSettings.Instance.ShouldLog(hashValue))
                        continue;

                    var localeBuilder = new StringBuilder();
                    HotfixStoreMgr.GetStore(hashValue)?.Serialize(stringBuilder, localeBuilder);
                    stringBuilder.Append(localeBuilder);
                }
            }

            return stringBuilder.ToString();
        }

        [BuilderMethod(true)]
        public static string HotfixData()
        {
            if (Storage.HotfixDatas.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfix_data))
                return string.Empty;

            var rows = new RowList<HotfixData>();

            foreach (var hotfix in Storage.HotfixDatas)
            {
                var row = new Row<HotfixData>
                {
                    Data = hotfix.Item1,
                    Comment = hotfix.Item1.TableHash.ToString()
                };

                rows.Add(row);
            }

            return "TRUNCATE `hotfix_data`;" + Environment.NewLine + new SQLInsert<HotfixData>(rows, false).Build();
        }

        [BuilderMethod(true)]
        public static string HotfixBlob()
        {
            if (Storage.HotfixBlobs.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfix_blob))
                return string.Empty;

            var rows = new RowList<HotfixBlob>();

            foreach (var hotfix in Storage.HotfixBlobs)
            {
                var row = new Row<HotfixBlob>
                {
                    Data = hotfix.Item1,
                    Comment = hotfix.Item1.TableHash.ToString()
                };

                rows.Add(row);
            }

            return "TRUNCATE `hotfix_blob`;" + Environment.NewLine + new SQLInsert<HotfixBlob>(rows, false).Build();
        }

        // Special Hotfix Builders
        [BuilderMethod(true)]
        public static string BroadcastText()
        {
            if (Storage.BroadcastTexts.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.broadcast_text))
                return string.Empty;

            foreach (var broadcastText in Storage.BroadcastTexts)
                broadcastText.Item1.ConvertToDBStruct();

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.BroadcastText>(), Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.BroadcastTexts, templatesDb, StoreNameType.None);
        }

        [BuilderMethod(true)]
        public static string BroadcastTextLocales()
        {
            if (Storage.BroadcastTextLocales.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.broadcast_text_locale))
                return string.Empty;

            // pass empty list, because we want to select the whole db table (faster than select only needed columns)
            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.BroadcastTextLocale>(), Settings.HotfixesDatabase);

            return "SET NAMES 'utf8';" + Environment.NewLine + SQLUtil.Compare(Storage.BroadcastTextLocales, templatesDb, StoreNameType.None) + Environment.NewLine + "SET NAMES 'latin1';";
        }
    }
}
