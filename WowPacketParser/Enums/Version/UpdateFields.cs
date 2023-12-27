using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Parsing.Parsers;

namespace WowPacketParser.Enums.Version
{
    public class UpdateFieldInfo
    {
        public int Value;
        public string Name;
        public int Size;
        public UpdateFieldType Format;
    }

    public static class UpdateFields
    {
        // TEMPORARY HACK
        // This is needed because currently opcode handlers are only registersd if version is matching
        // so we need to clear them and reinitialize default handlers
        // This will be obsolete when all version specific stuff is moved to their own modules
        public static void ResetUFDictionaries()
        {
            UpdateFieldDictionary.Clear();
            UpdateFieldNameDictionary.Clear();
            UpdateFieldsHandlers.Clear();
        }

        public static bool LoadUFDictionaries(Assembly asm, ClientVersionBuild build)
        {
            return LoadUFDictionariesInto(UpdateFieldDictionary, UpdateFieldNameDictionary, asm, build);
        }

        private static readonly Dictionary<Type, SortedList<int, UpdateFieldInfo>> UpdateFieldDictionary;
        private static readonly Dictionary<Type, Dictionary<string, int>> UpdateFieldNameDictionary;
        private static readonly Dictionary<ClientVersionBuild, UpdateFieldsHandlerBase> UpdateFieldsHandlers;

        static UpdateFields()
        {
            UpdateFieldDictionary = new Dictionary<Type, SortedList<int, UpdateFieldInfo>>();
            UpdateFieldNameDictionary = new Dictionary<Type, Dictionary<string, int>>();
            UpdateFieldsHandlers = new Dictionary<ClientVersionBuild, UpdateFieldsHandlerBase>();

            LoadUFDictionariesInto(UpdateFieldDictionary, UpdateFieldNameDictionary, Assembly.GetExecutingAssembly(), ClientVersionBuild.Zero);
        }

        private static bool LoadUFDictionariesInto(Dictionary<Type, SortedList<int, UpdateFieldInfo>> dicts,
            Dictionary<Type, Dictionary<string, int>> nameToValueDict,
            Assembly asm, ClientVersionBuild build)
        {
            Type[] enumTypes =
            {
                typeof(ObjectField), typeof(ItemField), typeof(ContainerField), typeof(AzeriteEmpoweredItemField), typeof(AzeriteItemField), typeof(UnitField),
                typeof(PlayerField), typeof(ActivePlayerField), typeof(GameObjectField), typeof(DynamicObjectField),
                typeof(CorpseField), typeof(AreaTriggerField), typeof(SceneObjectField), typeof(ConversationField),
                typeof(ObjectDynamicField), typeof(ItemDynamicField), typeof(ContainerDynamicField), typeof(AzeriteEmpoweredItemDynamicField), typeof(AzeriteItemDynamicField), typeof(UnitDynamicField),
                typeof(PlayerDynamicField), typeof(ActivePlayerDynamicField), typeof(GameObjectDynamicField), typeof(DynamicObjectDynamicField),
                typeof(CorpseDynamicField), typeof(AreaTriggerDynamicField), typeof(SceneObjectDynamicField), typeof(ConversationDynamicField)
            };

            bool loaded = false;
            foreach (Type enumType in enumTypes)
            {
                string vTypeString =
                    $"WowPacketParserModule.{GetUpdateFieldDictionaryBuildName(build)}.Enums.{enumType.Name}";
                Type vEnumType = asm.GetType(vTypeString);
                if (vEnumType == null)
                {
                    vTypeString =
                        $"WowPacketParser.Enums.Version.{GetUpdateFieldDictionaryBuildName(build)}.{enumType.Name}";
                    vEnumType = Assembly.GetExecutingAssembly().GetType(vTypeString);
                    if (vEnumType == null)
                        continue;   // versions prior to 4.3.0 do not have AreaTriggerField
                }

                Array vValues = Enum.GetValues(vEnumType);
                var vNames = Enum.GetNames(vEnumType);

                var result = new SortedList<int, UpdateFieldInfo>(vValues.Length);
                var namesResult = new Dictionary<string, int>(vNames.Length);

                for (int i = 0; i < vValues.Length; ++i)
                {
                    var format = enumType.GetMember(vNames[i])
                        .SelectMany(member => member.GetCustomAttributes(typeof(UpdateFieldAttribute), false))
                        .Where(attribute => ((UpdateFieldAttribute)attribute).Version <= ClientVersion.VersionDefiningBuild)
                        .OrderByDescending(attribute => ((UpdateFieldAttribute)attribute).Version)
                        .Select(attribute => ((UpdateFieldAttribute)attribute).UFAttribute)
                        .DefaultIfEmpty(UpdateFieldType.Default).First();

                    result.Add((int)vValues.GetValue(i), new UpdateFieldInfo() { Value = (int)vValues.GetValue(i), Name = vNames[i], Size = 0, Format = format });
                    namesResult.Add(vNames[i], (int)vValues.GetValue(i));
                }

                for (var i = 0; i < result.Count - 1; ++i)
                    result.Values[i].Size = result.Keys[i + 1] - result.Keys[i];

                dicts.Add(enumType, result);
                nameToValueDict.Add(enumType, namesResult);
                loaded = true;
            }

            return loaded;
        }

        public static void LoadUFHandlers(Assembly asm, ClientVersionBuild assemblyBuild)
        {
            var handlers = asm.GetTypes().Where(type => type.BaseType == typeof(UpdateFieldsHandlerBase));

            var namespaceRegex = new Regex($"WowPacketParserModule\\.{assemblyBuild}\\.UpdateFields\\.(.*)$");
            foreach (var handlerType in handlers)
            {
                var buildMatch = namespaceRegex.Match(handlerType.Namespace);
                if (buildMatch.Success)
                {
                    var group = buildMatch.Groups[1];
                    ClientVersionBuild handlerBuild;
                    if (Enum.TryParse(group.Value, out handlerBuild))
                        UpdateFieldsHandlers.Add(handlerBuild, (UpdateFieldsHandlerBase)Activator.CreateInstance(handlerType));
                }
            }
        }

        public static UpdateFieldsHandlerBase GetHandler()
        {
            ClientVersionBuild handlerBuild;
            UpdateFieldsHandlerBase handler;
            if (Enum.TryParse(GetUpdateFieldDictionaryBuildName(ClientVersion.Build), out handlerBuild))
                if (UpdateFieldsHandlers.TryGetValue(handlerBuild, out handler))
                    return handler;

            return null;
        }

        public static int GetUpdateField<T>(T field) where T: Enum
        {
            Dictionary<string, int> byNamesDict;
            if (UpdateFieldNameDictionary.TryGetValue(typeof(T), out byNamesDict))
            {
                int fieldValue;
                if (byNamesDict.TryGetValue(field.ToString(), out fieldValue))
                    return fieldValue;
            }

            return -1;
        }

        public static string GetUpdateFieldName<T>(int field) where T: Enum
        {
            SortedList<int, UpdateFieldInfo> infoDict;
            if (UpdateFieldDictionary.TryGetValue(typeof(T), out infoDict))
            {
                if (infoDict.Count != 0)
                {
                    var index = infoDict.BinarySearch(field);
                    if (index >= 0)
                        return infoDict.Values[index].Name;

                    index = ~index - 1;
                    var start = infoDict.Keys[index];
                    return infoDict.Values[index].Name + " + " + (field - start);
                }
            }

            return field.ToString(CultureInfo.InvariantCulture);
        }

        public static UpdateFieldInfo GetUpdateFieldInfo<T>(int field) where T: Enum
        {
            SortedList<int, UpdateFieldInfo> infoDict;
            if (UpdateFieldDictionary.TryGetValue(typeof(T), out infoDict))
            {
                if (infoDict.Count != 0)
                {
                    var index = infoDict.BinarySearch(field);
                    if (index >= 0)
                        return infoDict.Values[index];

                    return infoDict.Values[~index - 1];
                }
            }

            return null;
        }

        private static string GetUpdateFieldDictionaryBuildName(ClientVersionBuild build)
        {
            switch (build)
            {
                case ClientVersionBuild.V1_12_1_5875:
                case ClientVersionBuild.V2_0_1_6180:
                case ClientVersionBuild.V2_0_3_6299:
                case ClientVersionBuild.V2_0_6_6337:
                case ClientVersionBuild.V2_1_0_6692:
                case ClientVersionBuild.V2_1_1_6739:
                case ClientVersionBuild.V2_1_2_6803:
                case ClientVersionBuild.V2_1_3_6898:
                case ClientVersionBuild.V2_2_0_7272:
                case ClientVersionBuild.V2_2_2_7318:
                case ClientVersionBuild.V2_2_3_7359:
                case ClientVersionBuild.V2_3_0_7561:
                case ClientVersionBuild.V2_3_2_7741:
                case ClientVersionBuild.V2_3_3_7799:
                case ClientVersionBuild.V2_4_0_8089:
                case ClientVersionBuild.V2_4_1_8125:
                case ClientVersionBuild.V2_4_2_8209:
                case ClientVersionBuild.V2_4_3_8606:
                    return "V2_4_3_8606";
                case ClientVersionBuild.V3_0_2_9056:
                case ClientVersionBuild.V3_0_3_9183:
                case ClientVersionBuild.V3_0_8_9464:
                case ClientVersionBuild.V3_0_8a_9506:
                case ClientVersionBuild.V3_0_9_9551:
                    return "V3_0_9_9551";
                case ClientVersionBuild.V3_1_0_9767:
                case ClientVersionBuild.V3_1_1_9806:
                case ClientVersionBuild.V3_1_1a_9835:
                case ClientVersionBuild.V3_1_2_9901:
                case ClientVersionBuild.V3_1_3_9947:
                case ClientVersionBuild.V3_2_0_10192:
                case ClientVersionBuild.V3_2_0a_10314:
                case ClientVersionBuild.V3_2_2_10482:
                case ClientVersionBuild.V3_2_2a_10505:
                case ClientVersionBuild.V3_3_0_10958:
                case ClientVersionBuild.V3_3_0a_11159:
                {
                    return "V3_3_0_10958";
                }
                case ClientVersionBuild.V3_3_3_11685:
                case ClientVersionBuild.V3_3_3a_11723:
                case ClientVersionBuild.V3_3_5_12213:
                case ClientVersionBuild.V3_3_5a_12340:
                {
                    return "V3_3_5a_12340";
                }
                case ClientVersionBuild.V4_0_1_13164:
                case ClientVersionBuild.V4_0_1a_13205:
                case ClientVersionBuild.V4_0_3_13329:
                {
                    return "V4_0_3_13329";
                }
                case ClientVersionBuild.V4_0_6_13596:
                case ClientVersionBuild.V4_0_6a_13623:
                {
                    return "V4_0_6_13596";
                }
                case ClientVersionBuild.V4_1_0_13914:
                case ClientVersionBuild.V4_1_0a_14007:
                {
                    return "V4_1_0_13914";
                }
                case ClientVersionBuild.V4_2_0_14333:
                case ClientVersionBuild.V4_2_0a_14480:
                {
                    return "V4_2_0_14480";
                }
                case ClientVersionBuild.V4_2_2_14545:
                {
                    return "V4_2_2_14545";
                }
                case ClientVersionBuild.V4_3_0_15005:
                case ClientVersionBuild.V4_3_0a_15050:
                {
                    return "V4_3_0_15005";
                }
                case ClientVersionBuild.V4_3_2_15211:
                {
                    return "V4_3_2_15211";
                }
                case ClientVersionBuild.V4_3_3_15354:
                {
                    return "V4_3_3_15354";
                }
                case ClientVersionBuild.V4_3_4_15595:
                {
                    return "V4_3_4_15595";
                }
                case ClientVersionBuild.V5_0_4_16016:
                {
                    return "V5_0_4_16016";
                }
                case ClientVersionBuild.V5_0_5_16048:
                case ClientVersionBuild.V5_0_5a_16057:
                case ClientVersionBuild.V5_0_5b_16135:
                {
                    return "V5_0_5_16048";
                }
                case ClientVersionBuild.V5_1_0_16309:
                case ClientVersionBuild.V5_1_0a_16357:
                {
                    return "V5_1_0_16309";
                }
                case ClientVersionBuild.V5_2_0_16650:
                case ClientVersionBuild.V5_2_0_16669:
                case ClientVersionBuild.V5_2_0_16683:
                case ClientVersionBuild.V5_2_0_16685:
                case ClientVersionBuild.V5_2_0_16701:
                case ClientVersionBuild.V5_2_0_16709:
                case ClientVersionBuild.V5_2_0_16716:
                case ClientVersionBuild.V5_2_0_16733:
                case ClientVersionBuild.V5_2_0_16769:
                case ClientVersionBuild.V5_2_0_16826:
                {
                    return "V5_2_0_16650";
                }
                case ClientVersionBuild.V5_3_0_16981:
                case ClientVersionBuild.V5_3_0_16983:
                case ClientVersionBuild.V5_3_0_16992:
                case ClientVersionBuild.V5_3_0_17055:
                case ClientVersionBuild.V5_3_0_17116:
                case ClientVersionBuild.V5_3_0_17128:
                {
                    return "V5_3_0_16981";
                }
                case ClientVersionBuild.V5_4_0_17359:
                case ClientVersionBuild.V5_4_0_17371:
                case ClientVersionBuild.V5_4_0_17399:
                {
                    return "V5_4_0_17359";
                }
                case ClientVersionBuild.V5_4_1_17538:
                {
                    return "V5_4_1_17538";
                }
                case ClientVersionBuild.V5_4_2_17658:
                case ClientVersionBuild.V5_4_2_17688:
                {
                    return "V5_4_2_17688";
                }
                case ClientVersionBuild.V5_4_7_17898:
                case ClientVersionBuild.V5_4_7_17930:
                case ClientVersionBuild.V5_4_7_17956:
                case ClientVersionBuild.V5_4_7_18019:
                {
                    return "V5_4_7_17898";
                }
                case ClientVersionBuild.V5_4_8_18291:
                case ClientVersionBuild.V5_4_8_18414:
                {
                    return "V5_4_8_18291";
                }
                case ClientVersionBuild.V6_0_2_19033:
                case ClientVersionBuild.V6_0_2_19034:
                {
                    return "V6_0_2_19033";
                }
                case ClientVersionBuild.V6_0_3_19103:
                case ClientVersionBuild.V6_0_3_19116:
                case ClientVersionBuild.V6_0_3_19243:
                case ClientVersionBuild.V6_0_3_19342:
                {
                    return "V6_0_3_19103";
                }
                case ClientVersionBuild.V6_1_0_19678:
                case ClientVersionBuild.V6_1_0_19702:
                case ClientVersionBuild.V6_1_2_19802:
                case ClientVersionBuild.V6_1_2_19831:
                case ClientVersionBuild.V6_1_2_19865:
                {
                    return "V6_1_2_19802";
                }
                case ClientVersionBuild.V6_2_0_20173:
                case ClientVersionBuild.V6_2_0_20182:
                case ClientVersionBuild.V6_2_0_20201:
                case ClientVersionBuild.V6_2_0_20216:
                case ClientVersionBuild.V6_2_0_20253:
                case ClientVersionBuild.V6_2_0_20338:
                {
                    return "V6_2_0_20173";
                }
                case ClientVersionBuild.V6_2_2_20444:
                case ClientVersionBuild.V6_2_2a_20490:
                case ClientVersionBuild.V6_2_2a_20574:
                {
                    return "V6_2_2_20444";
                }
                case ClientVersionBuild.V6_2_3_20726:
                case ClientVersionBuild.V6_2_3_20779:
                case ClientVersionBuild.V6_2_3_20886:
                {
                    return "V6_2_3_20726";
                }
                case ClientVersionBuild.V6_2_4_21315:
                case ClientVersionBuild.V6_2_4_21336:
                case ClientVersionBuild.V6_2_4_21343:
                case ClientVersionBuild.V6_2_4_21345:
                case ClientVersionBuild.V6_2_4_21348:
                case ClientVersionBuild.V6_2_4_21355:
                case ClientVersionBuild.V6_2_4_21463:
                case ClientVersionBuild.V6_2_4_21676:
                case ClientVersionBuild.V6_2_4_21742:
                {
                    return "V6_2_4_21315";
                }
                case ClientVersionBuild.V7_0_3_22248:
                case ClientVersionBuild.V7_0_3_22267:
                case ClientVersionBuild.V7_0_3_22277:
                case ClientVersionBuild.V7_0_3_22280:
                case ClientVersionBuild.V7_0_3_22289:
                case ClientVersionBuild.V7_0_3_22293:
                case ClientVersionBuild.V7_0_3_22345:
                case ClientVersionBuild.V7_0_3_22396:
                case ClientVersionBuild.V7_0_3_22410:
                case ClientVersionBuild.V7_0_3_22423:
                case ClientVersionBuild.V7_0_3_22445:
                case ClientVersionBuild.V7_0_3_22498:
                case ClientVersionBuild.V7_0_3_22522:
                case ClientVersionBuild.V7_0_3_22566:
                case ClientVersionBuild.V7_0_3_22594:
                case ClientVersionBuild.V7_0_3_22624:
                case ClientVersionBuild.V7_0_3_22747:
                case ClientVersionBuild.V7_0_3_22810:
                {
                    return "V7_0_3_22248";
                }
                case ClientVersionBuild.V7_1_0_22900:
                case ClientVersionBuild.V7_1_0_22908:
                case ClientVersionBuild.V7_1_0_22950:
                case ClientVersionBuild.V7_1_0_22989:
                case ClientVersionBuild.V7_1_0_22995:
                case ClientVersionBuild.V7_1_0_22996:
                case ClientVersionBuild.V7_1_0_23171:
                case ClientVersionBuild.V7_1_0_23222:
                {
                    return "V7_1_0_22900";
                }
                case ClientVersionBuild.V7_1_5_23360:
                case ClientVersionBuild.V7_1_5_23420:
                {
                    return "V7_1_5_23360";
                }
                case ClientVersionBuild.V7_2_0_23706:
                case ClientVersionBuild.V7_2_0_23826:
                case ClientVersionBuild.V7_2_0_23835:
                case ClientVersionBuild.V7_2_0_23836:
                case ClientVersionBuild.V7_2_0_23846:
                case ClientVersionBuild.V7_2_0_23852:
                case ClientVersionBuild.V7_2_0_23857:
                case ClientVersionBuild.V7_2_0_23877:
                case ClientVersionBuild.V7_2_0_23911:
                case ClientVersionBuild.V7_2_0_23937:
                case ClientVersionBuild.V7_2_0_24015:
                {
                    return "V7_2_0_23826";
                }
                case ClientVersionBuild.V7_2_5_24330:
                case ClientVersionBuild.V7_2_5_24367:
                case ClientVersionBuild.V7_2_5_24414:
                case ClientVersionBuild.V7_2_5_24415:
                case ClientVersionBuild.V7_2_5_24430:
                case ClientVersionBuild.V7_2_5_24461:
                case ClientVersionBuild.V7_2_5_24742:
                {
                    return "V7_2_5_24330";
                }
                case ClientVersionBuild.V7_3_0_24920:
                case ClientVersionBuild.V7_3_0_24931:
                case ClientVersionBuild.V7_3_0_24956:
                case ClientVersionBuild.V7_3_0_24970:
                case ClientVersionBuild.V7_3_0_24974:
                case ClientVersionBuild.V7_3_0_25021:
                case ClientVersionBuild.V7_3_0_25195:
                {
                    return "V7_3_0_24920";
                }
                case ClientVersionBuild.V7_3_2_25383:
                case ClientVersionBuild.V7_3_2_25442:
                case ClientVersionBuild.V7_3_2_25455:
                case ClientVersionBuild.V7_3_2_25477:
                case ClientVersionBuild.V7_3_2_25480:
                case ClientVersionBuild.V7_3_2_25497:
                case ClientVersionBuild.V7_3_2_25549:
                {
                    return "V7_3_2_25383";
                }
                case ClientVersionBuild.V7_3_5_25848:
                case ClientVersionBuild.V7_3_5_25860:
                case ClientVersionBuild.V7_3_5_25864:
                case ClientVersionBuild.V7_3_5_25875:
                case ClientVersionBuild.V7_3_5_25881:
                case ClientVersionBuild.V7_3_5_25901:
                case ClientVersionBuild.V7_3_5_25928:
                case ClientVersionBuild.V7_3_5_25937:
                case ClientVersionBuild.V7_3_5_25944:
                case ClientVersionBuild.V7_3_5_25946:
                case ClientVersionBuild.V7_3_5_25950:
                case ClientVersionBuild.V7_3_5_25961:
                case ClientVersionBuild.V7_3_5_25996:
                case ClientVersionBuild.V7_3_5_26124:
                case ClientVersionBuild.V7_3_5_26365:
                case ClientVersionBuild.V7_3_5_26654:
                case ClientVersionBuild.V7_3_5_26755:
                case ClientVersionBuild.V7_3_5_26822:
                case ClientVersionBuild.V7_3_5_26899:
                case ClientVersionBuild.V7_3_5_26972:
                {
                    return "V7_3_5_25848";
                }
                case ClientVersionBuild.V8_0_1_27101:
                case ClientVersionBuild.V8_0_1_27144:
                case ClientVersionBuild.V8_0_1_27165:
                case ClientVersionBuild.V8_0_1_27178:
                case ClientVersionBuild.V8_0_1_27219:
                case ClientVersionBuild.V8_0_1_27291:
                case ClientVersionBuild.V8_0_1_27326:
                case ClientVersionBuild.V8_0_1_27355:
                case ClientVersionBuild.V8_0_1_27356:
                case ClientVersionBuild.V8_0_1_27366:
                case ClientVersionBuild.V8_0_1_27377:
                case ClientVersionBuild.V8_0_1_27404:
                case ClientVersionBuild.V8_0_1_27481:
                case ClientVersionBuild.V8_0_1_27547:
                case ClientVersionBuild.V8_0_1_27602:
                case ClientVersionBuild.V8_0_1_27791:
                case ClientVersionBuild.V8_0_1_27843:
                case ClientVersionBuild.V8_0_1_27980:
                case ClientVersionBuild.V8_0_1_28153:
                {
                    return "V8_0_1_27101";
                }
                case ClientVersionBuild.V8_1_0_28724:
                case ClientVersionBuild.V8_1_0_28768:
                case ClientVersionBuild.V8_1_0_28807:
                case ClientVersionBuild.V8_1_0_28822:
                case ClientVersionBuild.V8_1_0_28833:
                case ClientVersionBuild.V8_1_0_29088:
                case ClientVersionBuild.V8_1_0_29139:
                case ClientVersionBuild.V8_1_0_29235:
                case ClientVersionBuild.V8_1_0_29285:
                case ClientVersionBuild.V8_1_0_29297:
                case ClientVersionBuild.V8_1_0_29482:
                case ClientVersionBuild.V8_1_0_29600:
                case ClientVersionBuild.V8_1_0_29621:
                {
                    return "V8_1_0_28724";
                }
                case ClientVersionBuild.V8_1_5_29683:
                case ClientVersionBuild.V8_1_5_29701:
                case ClientVersionBuild.V8_1_5_29704:
                case ClientVersionBuild.V8_1_5_29705:
                case ClientVersionBuild.V8_1_5_29718:
                case ClientVersionBuild.V8_1_5_29732:
                case ClientVersionBuild.V8_1_5_29737:
                case ClientVersionBuild.V8_1_5_29814:
                case ClientVersionBuild.V8_1_5_29869:
                case ClientVersionBuild.V8_1_5_29896:
                case ClientVersionBuild.V8_1_5_29981:
                case ClientVersionBuild.V8_1_5_30477:
                case ClientVersionBuild.V8_1_5_30706:
                {
                    return "V8_1_5_29683";
                }
                case ClientVersionBuild.V8_2_0_30898:
                case ClientVersionBuild.V8_2_0_30918:
                case ClientVersionBuild.V8_2_0_30920:
                case ClientVersionBuild.V8_2_0_30948:
                case ClientVersionBuild.V8_2_0_30993:
                case ClientVersionBuild.V8_2_0_31229:
                case ClientVersionBuild.V8_2_0_31429:
                case ClientVersionBuild.V8_2_0_31478:
                {
                    return "V8_2_0_30898";
                }
                case ClientVersionBuild.V8_2_5_31921:
                case ClientVersionBuild.V8_2_5_31958:
                case ClientVersionBuild.V8_2_5_31960:
                case ClientVersionBuild.V8_2_5_31961:
                case ClientVersionBuild.V8_2_5_31984:
                case ClientVersionBuild.V8_2_5_32028:
                case ClientVersionBuild.V8_2_5_32144:
                case ClientVersionBuild.V8_2_5_32185:
                case ClientVersionBuild.V8_2_5_32265:
                case ClientVersionBuild.V8_2_5_32294:
                case ClientVersionBuild.V8_2_5_32305:
                case ClientVersionBuild.V8_2_5_32494:
                case ClientVersionBuild.V8_2_5_32580:
                case ClientVersionBuild.V8_2_5_32638:
                case ClientVersionBuild.V8_2_5_32722:
                case ClientVersionBuild.V8_2_5_32750:
                case ClientVersionBuild.V8_2_5_32978:
                {
                    return "V8_2_5_31921";
                }
                case ClientVersionBuild.V8_3_0_33062:
                case ClientVersionBuild.V8_3_0_33073:
                case ClientVersionBuild.V8_3_0_33084:
                case ClientVersionBuild.V8_3_0_33095:
                case ClientVersionBuild.V8_3_0_33115:
                case ClientVersionBuild.V8_3_0_33169:
                case ClientVersionBuild.V8_3_0_33237:
                case ClientVersionBuild.V8_3_0_33369:
                case ClientVersionBuild.V8_3_0_33528:
                case ClientVersionBuild.V8_3_0_33724:
                case ClientVersionBuild.V8_3_0_33775:
                case ClientVersionBuild.V8_3_0_33941:
                case ClientVersionBuild.V8_3_0_34220:
                case ClientVersionBuild.V8_3_0_34601:
                case ClientVersionBuild.V8_3_0_34769:
                case ClientVersionBuild.V8_3_0_34963:
                case ClientVersionBuild.V8_3_7_35249:
                case ClientVersionBuild.V8_3_7_35284:
                case ClientVersionBuild.V8_3_7_35435:
                case ClientVersionBuild.V8_3_7_35662:
                {
                    return "V8_3_0_33062";
                }
                case ClientVersionBuild.V9_0_1_36216:
                case ClientVersionBuild.V9_0_1_36228:
                case ClientVersionBuild.V9_0_1_36230:
                case ClientVersionBuild.V9_0_1_36247:
                case ClientVersionBuild.V9_0_1_36272:
                case ClientVersionBuild.V9_0_1_36322:
                case ClientVersionBuild.V9_0_1_36372:
                case ClientVersionBuild.V9_0_1_36492:
                case ClientVersionBuild.V9_0_1_36577:
                {
                    return "V9_0_1_36216";
                }
                case ClientVersionBuild.V9_0_2_36639:
                case ClientVersionBuild.V9_0_2_36665:
                case ClientVersionBuild.V9_0_2_36671:
                case ClientVersionBuild.V9_0_2_36710:
                case ClientVersionBuild.V9_0_2_36734:
                case ClientVersionBuild.V9_0_2_36751:
                case ClientVersionBuild.V9_0_2_36753:
                case ClientVersionBuild.V9_0_2_36839:
                case ClientVersionBuild.V9_0_2_36949:
                case ClientVersionBuild.V9_0_2_37106:
                case ClientVersionBuild.V9_0_2_37142:
                case ClientVersionBuild.V9_0_2_37176:
                case ClientVersionBuild.V9_0_2_37474:
                {
                    return "V9_0_2_36639";
                }
                case ClientVersionBuild.V9_0_5_37503:
                case ClientVersionBuild.V9_0_5_37862:
                case ClientVersionBuild.V9_0_5_37864:
                case ClientVersionBuild.V9_0_5_37893:
                case ClientVersionBuild.V9_0_5_37899:
                case ClientVersionBuild.V9_0_5_37988:
                case ClientVersionBuild.V9_0_5_38134:
                case ClientVersionBuild.V9_0_5_38556:
                {
                    return "V9_0_5_37862";
                }
                case ClientVersionBuild.V9_1_0_39185:
                case ClientVersionBuild.V9_1_0_39226:
                case ClientVersionBuild.V9_1_0_39229:
                case ClientVersionBuild.V9_1_0_39262:
                case ClientVersionBuild.V9_1_0_39282:
                case ClientVersionBuild.V9_1_0_39289:
                case ClientVersionBuild.V9_1_0_39291:
                case ClientVersionBuild.V9_1_0_39318:
                case ClientVersionBuild.V9_1_0_39335:
                case ClientVersionBuild.V9_1_0_39427:
                case ClientVersionBuild.V9_1_0_39497:
                case ClientVersionBuild.V9_1_0_39498:
                case ClientVersionBuild.V9_1_0_39584:
                case ClientVersionBuild.V9_1_0_39617:
                case ClientVersionBuild.V9_1_0_39653:
                case ClientVersionBuild.V9_1_0_39804:
                case ClientVersionBuild.V9_1_0_40000:
                case ClientVersionBuild.V9_1_0_40120:
                case ClientVersionBuild.V9_1_0_40443:
                case ClientVersionBuild.V9_1_0_40593:
                case ClientVersionBuild.V9_1_0_40725:
                {
                    return "V9_1_0_39185";
                }
                case ClientVersionBuild.V9_1_5_40772:
                case ClientVersionBuild.V9_1_5_40871:
                case ClientVersionBuild.V9_1_5_40906:
                case ClientVersionBuild.V9_1_5_40944:
                case ClientVersionBuild.V9_1_5_40966:
                case ClientVersionBuild.V9_1_5_41031:
                case ClientVersionBuild.V9_1_5_41079:
                case ClientVersionBuild.V9_1_5_41288:
                case ClientVersionBuild.V9_1_5_41323:
                case ClientVersionBuild.V9_1_5_41359:
                case ClientVersionBuild.V9_1_5_41488:
                case ClientVersionBuild.V9_1_5_41793:
                case ClientVersionBuild.V9_1_5_42010:
                {
                    return "V9_1_5_40772";
                }
                case ClientVersionBuild.V9_2_0_42423:
                case ClientVersionBuild.V9_2_0_42488:
                case ClientVersionBuild.V9_2_0_42521:
                case ClientVersionBuild.V9_2_0_42538:
                case ClientVersionBuild.V9_2_0_42560:
                case ClientVersionBuild.V9_2_0_42614:
                case ClientVersionBuild.V9_2_0_42698:
                case ClientVersionBuild.V9_2_0_42852:
                case ClientVersionBuild.V9_2_0_42937:
                case ClientVersionBuild.V9_2_0_42979:
                case ClientVersionBuild.V9_2_0_43114:
                case ClientVersionBuild.V9_2_0_43206:
                case ClientVersionBuild.V9_2_0_43340:
                case ClientVersionBuild.V9_2_0_43345:
                {
                    return "V9_2_0_42423";
                }
                case ClientVersionBuild.V9_2_5_43903:
                case ClientVersionBuild.V9_2_5_43971:
                case ClientVersionBuild.V9_2_5_44015:
                case ClientVersionBuild.V9_2_5_44061:
                case ClientVersionBuild.V9_2_5_44127:
                case ClientVersionBuild.V9_2_5_44232:
                case ClientVersionBuild.V9_2_5_44325:
                case ClientVersionBuild.V9_2_5_44730:
                case ClientVersionBuild.V9_2_5_44908:
                {
                    return "V9_2_5_43903";
                }
                case ClientVersionBuild.V9_2_7_45114:
                case ClientVersionBuild.V9_2_7_45161:
                case ClientVersionBuild.V9_2_7_45338:
                case ClientVersionBuild.V9_2_7_45745:
                {
                    return "V9_2_7_45114";
                }
                case ClientVersionBuild.V10_0_0_46181:
                case ClientVersionBuild.V10_0_0_46293:
                case ClientVersionBuild.V10_0_0_46313:
                case ClientVersionBuild.V10_0_0_46340:
                case ClientVersionBuild.V10_0_0_46366:
                case ClientVersionBuild.V10_0_0_46455:
                case ClientVersionBuild.V10_0_0_46547:
                case ClientVersionBuild.V10_0_0_46549:
                case ClientVersionBuild.V10_0_0_46597:
                {
                    return "V10_0_0_46181";
                }
                case ClientVersionBuild.V10_0_2_46479:
                case ClientVersionBuild.V10_0_2_46658:
                case ClientVersionBuild.V10_0_2_46619:
                case ClientVersionBuild.V10_0_2_46689:
                case ClientVersionBuild.V10_0_2_46702:
                case ClientVersionBuild.V10_0_2_46741:
                case ClientVersionBuild.V10_0_2_46781:
                case ClientVersionBuild.V10_0_2_46801:
                case ClientVersionBuild.V10_0_2_46879:
                case ClientVersionBuild.V10_0_2_46924:
                case ClientVersionBuild.V10_0_2_47067:
                case ClientVersionBuild.V10_0_2_47187:
                case ClientVersionBuild.V10_0_2_47213:
                case ClientVersionBuild.V10_0_2_47631:
                {
                    return "V10_0_2_46479";
                }
                case ClientVersionBuild.V10_0_5_47777:
                case ClientVersionBuild.V10_0_5_47799:
                case ClientVersionBuild.V10_0_5_47825:
                case ClientVersionBuild.V10_0_5_47849:
                case ClientVersionBuild.V10_0_5_47871:
                case ClientVersionBuild.V10_0_5_47884:
                case ClientVersionBuild.V10_0_5_47936:
                case ClientVersionBuild.V10_0_5_47967:
                case ClientVersionBuild.V10_0_5_48001:
                case ClientVersionBuild.V10_0_5_48069:
                case ClientVersionBuild.V10_0_5_48317:
                case ClientVersionBuild.V10_0_5_48397:
                case ClientVersionBuild.V10_0_5_48526:
                {
                    return "V10_0_5_47777";
                }
                case ClientVersionBuild.V10_0_7_48676:
                case ClientVersionBuild.V10_0_7_48749:
                case ClientVersionBuild.V10_0_7_48838:
                case ClientVersionBuild.V10_0_7_48865:
                case ClientVersionBuild.V10_0_7_48892:
                case ClientVersionBuild.V10_0_7_48966:
                case ClientVersionBuild.V10_0_7_48999:
                case ClientVersionBuild.V10_0_7_49267:
                case ClientVersionBuild.V10_0_7_49343:
                {
                    return "V10_0_7_48676";
                }
                case ClientVersionBuild.V10_1_0_49407:
                case ClientVersionBuild.V10_1_0_49426:
                case ClientVersionBuild.V10_1_0_49444:
                case ClientVersionBuild.V10_1_0_49474:
                case ClientVersionBuild.V10_1_0_49570:
                case ClientVersionBuild.V10_1_0_49679:
                case ClientVersionBuild.V10_1_0_49741:
                case ClientVersionBuild.V10_1_0_49801:
                case ClientVersionBuild.V10_1_0_49890:
                case ClientVersionBuild.V10_1_0_50000:
                {
                    return "V10_1_0_49318";
                }
                case ClientVersionBuild.V10_1_5_50232:
                case ClientVersionBuild.V10_1_5_50355:
                case ClientVersionBuild.V10_1_5_50379:
                case ClientVersionBuild.V10_1_5_50401:
                case ClientVersionBuild.V10_1_5_50438:
                case ClientVersionBuild.V10_1_5_50469:
                case ClientVersionBuild.V10_1_5_50504:
                case ClientVersionBuild.V10_1_5_50585:
                case ClientVersionBuild.V10_1_5_50622:
                case ClientVersionBuild.V10_1_5_50747:
                case ClientVersionBuild.V10_1_5_50791:
                case ClientVersionBuild.V10_1_5_51130:
                {
                    return "V10_1_5_50232";
                }
                case ClientVersionBuild.V10_1_7_51187:
                case ClientVersionBuild.V10_1_7_51237:
                case ClientVersionBuild.V10_1_7_51261:
                case ClientVersionBuild.V10_1_7_51313:
                case ClientVersionBuild.V10_1_7_51421:
                case ClientVersionBuild.V10_1_7_51485:
                case ClientVersionBuild.V10_1_7_51536:
                case ClientVersionBuild.V10_1_7_51754:
                case ClientVersionBuild.V10_1_7_51886:
                case ClientVersionBuild.V10_1_7_51972:
                {
                    return "V10_1_7_51187";
                }
                case ClientVersionBuild.V10_2_0_52038:
                case ClientVersionBuild.V10_2_0_52068:
                case ClientVersionBuild.V10_2_0_52095:
                case ClientVersionBuild.V10_2_0_52106:
                case ClientVersionBuild.V10_2_0_52129:
                case ClientVersionBuild.V10_2_0_52148:
                case ClientVersionBuild.V10_2_0_52188:
                case ClientVersionBuild.V10_2_0_52301:
                case ClientVersionBuild.V10_2_0_52393:
                case ClientVersionBuild.V10_2_0_52485:
                case ClientVersionBuild.V10_2_0_52545:
                case ClientVersionBuild.V10_2_0_52607:
                case ClientVersionBuild.V10_2_0_52649:
                {
                    return "V10_2_0_52038";
                }
                case ClientVersionBuild.V1_13_2_31446:
                case ClientVersionBuild.V1_13_2_31650:
                case ClientVersionBuild.V1_13_2_31687:
                case ClientVersionBuild.V1_13_2_31727:
                case ClientVersionBuild.V1_13_2_31830:
                case ClientVersionBuild.V1_13_2_31882:
                case ClientVersionBuild.V1_13_2_32089:
                case ClientVersionBuild.V1_13_2_32421:
                case ClientVersionBuild.V1_13_2_32600:
                {
                    return "V1_13_2_31446";
                }
                case ClientVersionBuild.V1_13_3_32790:
                case ClientVersionBuild.V1_13_3_32836:
                case ClientVersionBuild.V1_13_3_32887:
                case ClientVersionBuild.V1_13_3_33155:
                case ClientVersionBuild.V1_13_3_33302:
                case ClientVersionBuild.V1_13_3_33526:
                case ClientVersionBuild.V1_13_4_33598:
                case ClientVersionBuild.V1_13_4_33645:
                case ClientVersionBuild.V1_13_4_33728:
                case ClientVersionBuild.V1_13_4_33920:
                case ClientVersionBuild.v1_13_4_34219:
                case ClientVersionBuild.v1_13_4_34266:
                case ClientVersionBuild.v1_13_4_34600:
                case ClientVersionBuild.v1_13_4_34835:
                case ClientVersionBuild.v1_13_5_34713:
                case ClientVersionBuild.v1_13_5_34911:
                case ClientVersionBuild.v1_13_5_35000:
                case ClientVersionBuild.V1_13_5_35100:
                case ClientVersionBuild.V1_13_5_35186:
                case ClientVersionBuild.V1_13_5_35753:
                case ClientVersionBuild.V1_13_5_36035:
                case ClientVersionBuild.V1_13_5_36325:
                case ClientVersionBuild.v1_13_6_36231:
                case ClientVersionBuild.V1_13_6_36497:
                case ClientVersionBuild.V1_13_6_36324:
                case ClientVersionBuild.V1_13_6_36524:
                case ClientVersionBuild.V1_13_6_36611:
                case ClientVersionBuild.V1_13_6_36714:
                case ClientVersionBuild.V1_13_6_36935:
                case ClientVersionBuild.V1_13_6_37497:
                case ClientVersionBuild.V1_13_7_38363:
                case ClientVersionBuild.V1_13_7_38386:
                case ClientVersionBuild.V1_13_7_38475:
                case ClientVersionBuild.V1_13_7_38631:
                case ClientVersionBuild.V1_13_7_38704:
                case ClientVersionBuild.V1_13_7_39605:
                case ClientVersionBuild.V1_13_7_39692:
                {
                    return "V1_13_3_32790";
                }
                case ClientVersionBuild.V1_14_0_39802:
                case ClientVersionBuild.V1_14_0_39958:
                case ClientVersionBuild.V1_14_0_40140:
                case ClientVersionBuild.V1_14_0_40179:
                case ClientVersionBuild.V1_14_0_40237:
                case ClientVersionBuild.V1_14_0_40347:
                case ClientVersionBuild.V1_14_0_40441:
                case ClientVersionBuild.V1_14_0_40618:
                {
                    return "V1_14_0_40237";
                }
                case ClientVersionBuild.V1_14_1_40487:
                case ClientVersionBuild.V1_14_1_40594:
                case ClientVersionBuild.V1_14_1_40666:
                case ClientVersionBuild.V1_14_1_40688:
                case ClientVersionBuild.V1_14_1_40800:
                case ClientVersionBuild.V1_14_1_40818:
                case ClientVersionBuild.V1_14_1_40926:
                case ClientVersionBuild.V1_14_1_40962:
                case ClientVersionBuild.V1_14_1_41009:
                case ClientVersionBuild.V1_14_1_41030:
                case ClientVersionBuild.V1_14_1_41077:
                case ClientVersionBuild.V1_14_1_41137:
                case ClientVersionBuild.V1_14_1_41243:
                case ClientVersionBuild.V1_14_1_41511:
                case ClientVersionBuild.V1_14_1_41794:
                case ClientVersionBuild.V1_14_1_42032:
                case ClientVersionBuild.V1_14_2_41858:
                case ClientVersionBuild.V1_14_2_41959:
                case ClientVersionBuild.V1_14_2_42065:
                case ClientVersionBuild.V1_14_2_42082:
                case ClientVersionBuild.V1_14_2_42214:
                case ClientVersionBuild.V1_14_2_42597:
                {
                    return "V1_14_1_40688";
                }
                case ClientVersionBuild.V1_14_3_42770:
                case ClientVersionBuild.V1_14_3_42926:
                case ClientVersionBuild.V1_14_3_43037:
                case ClientVersionBuild.V1_14_3_43086:
                case ClientVersionBuild.V1_14_3_43154:
                case ClientVersionBuild.V1_14_3_43401:
                case ClientVersionBuild.V1_14_3_43639:
                case ClientVersionBuild.V1_14_3_44016:
                case ClientVersionBuild.V1_14_3_44170:
                case ClientVersionBuild.V1_14_3_44403:
                case ClientVersionBuild.V1_14_3_44834:
                case ClientVersionBuild.V1_14_3_45437:
                case ClientVersionBuild.V1_14_3_46575:
                case ClientVersionBuild.V1_14_3_48611:
                case ClientVersionBuild.V1_14_3_49229:
                case ClientVersionBuild.V1_14_3_49821:
                {
                    return "V1_14_3_42770";
                }
                case ClientVersionBuild.V2_5_1_38598:
                case ClientVersionBuild.V2_5_1_38644:
                case ClientVersionBuild.V2_5_1_38707:
                case ClientVersionBuild.V2_5_1_38741:
                case ClientVersionBuild.V2_5_1_38757:
                case ClientVersionBuild.V2_5_1_38835:
                case ClientVersionBuild.V2_5_1_38892:
                case ClientVersionBuild.V2_5_1_38921:
                case ClientVersionBuild.V2_5_1_38988:
                case ClientVersionBuild.V2_5_1_39170:
                case ClientVersionBuild.V2_5_1_39475:
                case ClientVersionBuild.V2_5_1_39603:
                case ClientVersionBuild.V2_5_1_39640:
                {
                    return "V2_5_1_38835";
                }
                case ClientVersionBuild.V2_5_2_39570:
                case ClientVersionBuild.V2_5_2_39618:
                case ClientVersionBuild.V2_5_2_39926:
                case ClientVersionBuild.V2_5_2_40011:
                case ClientVersionBuild.V2_5_2_40045:
                case ClientVersionBuild.V2_5_2_40203:
                case ClientVersionBuild.V2_5_2_40260:
                case ClientVersionBuild.V2_5_2_40422:
                case ClientVersionBuild.V2_5_2_40488:
                case ClientVersionBuild.V2_5_2_40617:
                case ClientVersionBuild.V2_5_2_40892:
                case ClientVersionBuild.V2_5_2_41446:
                case ClientVersionBuild.V2_5_2_41510:
                {
                    return "V2_5_2_39570";
                }
                case ClientVersionBuild.V2_5_3_41812:
                case ClientVersionBuild.V2_5_3_42083:
                case ClientVersionBuild.V2_5_3_42328:
                case ClientVersionBuild.V2_5_3_42598:
                {
                    return "V2_5_3_41812";
                }
                case ClientVersionBuild.V2_5_4_42695:
                case ClientVersionBuild.V2_5_4_42757:
                case ClientVersionBuild.V2_5_4_42800:
                case ClientVersionBuild.V2_5_4_42869:
                case ClientVersionBuild.V2_5_4_42870:
                case ClientVersionBuild.V2_5_4_42873:
                case ClientVersionBuild.V2_5_4_42917:
                case ClientVersionBuild.V2_5_4_42940:
                case ClientVersionBuild.V2_5_4_43400:
                case ClientVersionBuild.V2_5_4_43638:
                case ClientVersionBuild.V2_5_4_43861:
                case ClientVersionBuild.V2_5_4_44036:
                case ClientVersionBuild.V2_5_4_44171:
                case ClientVersionBuild.V2_5_4_44400:
                case ClientVersionBuild.V2_5_4_44833:
                {
                    return "V2_5_4_42800";
                }
                case ClientVersionBuild.V3_4_0_45166:
                case ClientVersionBuild.V3_4_0_44832:
                case ClientVersionBuild.V3_4_0_45189:
                case ClientVersionBuild.V3_4_0_45264:
                case ClientVersionBuild.V3_4_0_45327:
                case ClientVersionBuild.V3_4_0_45435:
                case ClientVersionBuild.V3_4_0_45506:
                case ClientVersionBuild.V3_4_0_45572:
                case ClientVersionBuild.V3_4_0_45613:
                case ClientVersionBuild.V3_4_0_45704:
                case ClientVersionBuild.V3_4_0_45772:
                case ClientVersionBuild.V3_4_0_45854:
                case ClientVersionBuild.V3_4_0_45942:
                case ClientVersionBuild.V3_4_0_46158:
                case ClientVersionBuild.V3_4_0_46182:
                case ClientVersionBuild.V3_4_0_46248:
                case ClientVersionBuild.V3_4_0_46368:
                case ClientVersionBuild.V3_4_0_46779:
                case ClientVersionBuild.V3_4_0_46902:
                case ClientVersionBuild.V3_4_0_47168:
                {
                    return "V3_4_0_45166";
                }
                case ClientVersionBuild.V3_4_1_47014:
                case ClientVersionBuild.V3_4_1_47612:
                case ClientVersionBuild.V3_4_1_47720:
                case ClientVersionBuild.V3_4_1_47800:
                case ClientVersionBuild.V3_4_1_47966:
                case ClientVersionBuild.V3_4_1_48019:
                case ClientVersionBuild.V3_4_1_48120:
                case ClientVersionBuild.V3_4_1_48340:
                case ClientVersionBuild.V3_4_1_48503:
                case ClientVersionBuild.V3_4_1_48632:
                case ClientVersionBuild.V3_4_1_49345:
                case ClientVersionBuild.V3_4_1_49822:
                case ClientVersionBuild.V3_4_1_49936:
                {
                    return "V3_4_1_47014";
                }
                case ClientVersionBuild.V3_4_2_50063:
                case ClientVersionBuild.V3_4_2_50129:
                case ClientVersionBuild.V3_4_2_50172:
                case ClientVersionBuild.V3_4_2_50250:
                case ClientVersionBuild.V3_4_2_50375:
                case ClientVersionBuild.V3_4_2_50664:
                {
                    return "V3_4_2_50129";
                }
                case ClientVersionBuild.V3_4_3_51505:
                case ClientVersionBuild.V3_4_3_51572:
                case ClientVersionBuild.V3_4_3_51666:
                case ClientVersionBuild.V3_4_3_51739:
                case ClientVersionBuild.V3_4_3_51831:
                case ClientVersionBuild.V3_4_3_51943:
                case ClientVersionBuild.V3_4_3_52237:
                {
                    return "V3_4_3_51666";
                }
                default:
                {
                    return "V3_3_5a_12340";
                }
            }
        }
    }
}
