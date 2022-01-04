using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Misc;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class HotfixBuilder
    {
        [BuilderMethod(TargetSQLDatabase.Hotfixes)]
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
                var emptyStringBuilder = new StringBuilder();

                foreach (DB2Hash hashValue in Enum.GetValues(typeof (DB2Hash)))
                {
                    if (!HotfixSettings.Instance.ShouldLog(hashValue))
                        continue;

                    var localeBuilder = new StringBuilder();
                    HotfixStoreMgr.GetStore(hashValue)?.Serialize(stringBuilder, localeBuilder);
                    emptyStringBuilder.Append(localeBuilder);
                }
                return emptyStringBuilder.ToString();
            }

            return stringBuilder.ToString();
        }

        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
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

            return "DELETE FROM `hotfix_data` WHERE `VerifiedBuild`>0;" + Environment.NewLine + new SQLInsert<HotfixData>(rows, false).Build();
        }

        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
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

            return $"DELETE FROM `hotfix_blob` WHERE `locale` = '{ClientLocale.PacketLocale}' AND `VerifiedBuild`>0;" + Environment.NewLine + new SQLInsert<HotfixBlob>(rows, false).Build();
        }

        // Special Hotfix Builders
        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
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

        [BuilderMethod(true, TargetSQLDatabase.Hotfixes)]
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

        [BuilderMethod(TargetSQLDatabase.Hotfixes)]
        public static string HotfixOptionalData()
        {
            if (Storage.HotfixOptionalDatas.IsEmpty())
                return string.Empty;

            if (!Settings.SQLOutputFlag.HasAnyFlagBit(SQLOutput.hotfix_optional_data))
                return string.Empty;

            var templatesDb = SQLDatabase.Get(new RowList<Store.Objects.HotfixOptionalData>(), Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.HotfixOptionalDatas, templatesDb, StoreNameType.None);
        }
    }
}
