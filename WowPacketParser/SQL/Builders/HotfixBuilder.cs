using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Hotfix;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Store;

namespace WowPacketParser.SQL.Builders
{
    [BuilderClass]
    public static class HotfixBuilder
    {
        [BuilderMethod]
        public static string Hotfixes()
        {
            var stringBuilder = new StringBuilder();

            if (BinaryPacketReader.GetLocale() == LocaleConstant.enUS)
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

            var templatesDb = SQLDatabase.Get(Storage.HotfixDatas, Settings.HotfixesDatabase);

            return SQLUtil.Compare(Storage.HotfixDatas, templatesDb, StoreNameType.None);
        }
    }
}
