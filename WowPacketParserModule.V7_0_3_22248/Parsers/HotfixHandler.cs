using System;
using System.Diagnostics.CodeAnalysis;
using WowPacketParser.Enums;
using WowPacketParser.Loading;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParser.Store;
using WowPacketParser.Store.Objects;

namespace WowPacketParserModule.V7_0_3_22248.Parsers
{
    public static class HotfixHandler
    {

        [HasSniffData]
        [Parser(Opcode.SMSG_DB_REPLY)]
        public static void HandleDBReply(Packet packet)
        {
            DB2Hash type = packet.ReadUInt32E<DB2Hash>("TableHash");
            uint entry = (uint)packet.ReadInt32("RecordID");
            bool allow = (int)entry >= 0;
            packet.ReadTime("Timestamp");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V6_2_0_20173))
                allow = packet.ReadBit("Allow");

            int size = packet.ReadInt32("Size");
            var data = packet.ReadBytes(size);
            Packet db2File = new Packet(data, packet.Opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.FileName);

            HotfixData hotfixData = new HotfixData
            {
                TableHash = type,
            };

            if (allow)
            {
                if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, (int)entry)))
                {
                    hotfixData.Deleted = false;
                    hotfixData.RecordID = (int)entry;
                    hotfixData.Timestamp =
                        Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, (int)entry)].Item1.Timestamp;
                    Storage.HotfixDatas.Add(hotfixData);
                }
            }
            else
            {
                if (Storage.HotfixDataStore.ContainsKey(Tuple.Create(type, -(int)entry)))
                {
                    hotfixData.Deleted = true;
                    hotfixData.RecordID = -(int)entry;
                    hotfixData.Timestamp =
                        Storage.HotfixDataStore[new Tuple<DB2Hash, int>(type, -(int)entry)].Item1.Timestamp;
                    Storage.HotfixDatas.Add(hotfixData);
                }
                packet.WriteLine("Row {0} has been removed.", -(int) entry);
                return;
            }

            switch (type)
            {
                case DB2Hash.Achievement:
                {
                    db2File.ReadCString("Title");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt32("Flags");
                    db2File.ReadCString("Reward");
                    db2File.ReadInt16("MapID");
                    db2File.ReadUInt16("Supercedes");
                    db2File.ReadUInt16("Category");
                    db2File.ReadUInt16("UIOrder");
                    db2File.ReadUInt16("IconID");
                    db2File.ReadUInt16("SharesCriteria");
                    db2File.ReadUInt16("CriteriaTree");
                    db2File.ReadSByte("Faction");
                    db2File.ReadByte("Points");
                    db2File.ReadByte("MinimumCriteria");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.Achievement_Category:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.AdventureJournal:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadCString("5");
                    db2File.ReadCString("6");
                    db2File.ReadInt16("7");
                    db2File.ReadInt16("8");
                    db2File.ReadInt16("9");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt16("10", i);
                    db2File.ReadInt16("11");
                    db2File.ReadInt16("12");
                    db2File.ReadByte("13");
                    db2File.ReadByte("14");
                    db2File.ReadByte("15");
                    db2File.ReadByte("16");
                    db2File.ReadByte("17");
                    db2File.ReadByte("18");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadByte("19", i);
                    db2File.ReadByte("20");
                    db2File.ReadByte("21");
                    break;
                }
                case DB2Hash.AdventureMapPOI:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadInt32("1");
                    db2File.ReadCString("2");
                    db2File.ReadCString("3");
                    db2File.ReadByte("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    break;
                }
                case DB2Hash.AnimKit:
                {
                    db2File.ReadUInt32("OneShotDuration");
                    db2File.ReadUInt16("OneShotStopAnimKitID");
                    db2File.ReadUInt16("LowDefAnimKitID");
                    break;
                }
                case DB2Hash.AnimKitBoneSet:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.AnimKitBoneSetAlias:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.AnimKitConfig:
                {
                    db2File.ReadInt32("0");
                    break;
                }
                case DB2Hash.AnimKitConfigBoneSet:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.AnimKitPriority:
                {
                    db2File.ReadByte("0");
                    break;
                }
                case DB2Hash.AnimKitSegment:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    db2File.ReadByte("14");
                    db2File.ReadInt32("15");
                    break;
                }
                case DB2Hash.AnimReplacement:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    break;
                }
                case DB2Hash.AnimReplacementSet:
                {
                    db2File.ReadByte("0");
                    break;
                }
                case DB2Hash.AnimationData:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.AreaGroupMember:
                {
                    db2File.ReadUInt16("AreaGroupID");
                    db2File.ReadUInt16("AreaID");
                    break;
                }
                case DB2Hash.AreaPOI:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadInt16("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadInt32("12");
                    break;
                }
                case DB2Hash.AreaPOIState:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.AreaTable:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Flags", i);
                    db2File.ReadCString("ZoneName");
                    db2File.ReadSingle("AmbientMultiplier");
                    db2File.ReadCString("AreaName");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("ParentAreaID");
                    db2File.ReadInt16("AreaBit");
                    db2File.ReadUInt16("AmbienceID");
                    db2File.ReadUInt16("ZoneMusic");
                    db2File.ReadUInt16("IntroSound");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("LiquidTypeID", i);
                    db2File.ReadUInt16("UWZoneMusic");
                    db2File.ReadUInt16("UWAmbience");
                    db2File.ReadUInt16("PvPCombatWorldStateID");
                    db2File.ReadByte("SoundProviderPref");
                    db2File.ReadByte("SoundProviderPrefUnderwater");
                    db2File.ReadByte("ExplorationLevel");
                    db2File.ReadByte("FactionGroupMask");
                    db2File.ReadByte("MountFlags");
                    db2File.ReadByte("WildBattlePetLevelMin");
                    db2File.ReadByte("WildBattlePetLevelMax");
                    db2File.ReadByte("WindSettingsID");
                    db2File.ReadUInt32("UWIntroSound");
                    break;
                }
                case DB2Hash.AreaTrigger:
                {
                    db2File.ReadVector3("Pos");
                    db2File.ReadSingle("Radius");
                    db2File.ReadSingle("BoxLength");
                    db2File.ReadSingle("BoxWidth");
                    db2File.ReadSingle("BoxHeight");
                    db2File.ReadSingle("BoxYaw");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("PhaseID");
                    db2File.ReadUInt16("PhaseGroupID");
                    db2File.ReadUInt16("ShapeID");
                    db2File.ReadUInt16("AreaTriggerActionSetID");
                    db2File.ReadByte("PhaseUseFlags");
                    db2File.ReadByte("ShapeType");
                    db2File.ReadByte("Flag");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.AreaTriggerActionSet:
                {
                    db2File.ReadInt16("0");
                    break;
                }
                case DB2Hash.AreaTriggerBox:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    break;
                }
                case DB2Hash.AreaTriggerCylinder:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    break;
                }
                case DB2Hash.AreaTriggerSphere:
                {
                    db2File.ReadSingle("0");
                    break;
                }
                case DB2Hash.ArmorLocation:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadSingle("Modifier", i);
                    break;
                }
                case DB2Hash.Artifact:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    break;
                }
                case DB2Hash.ArtifactAppearance:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    break;
                }
                case DB2Hash.ArtifactAppearanceSet:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ArtifactCategory:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ArtifactPower:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.ArtifactPowerLink:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ArtifactPowerRank:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.ArtifactQuestXP:
                {
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadInt32("0", i);
                    break;
                }
                case DB2Hash.ArtifactUnlock:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.AuctionHouse:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt16("FactionID");
                    db2File.ReadByte("DepositRate");
                    db2File.ReadByte("ConsignmentRate");
                    break;
                }
                case DB2Hash.BankBagSlotPrices:
                {
                    db2File.ReadUInt32("Cost");
                    break;
                }
                case DB2Hash.BannedAddOns:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Version");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.BarberShopStyle:
                {
                    db2File.ReadCString("DisplayName");
                    db2File.ReadCString("Description");
                    db2File.ReadSingle("CostModifier");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Race");
                    db2File.ReadByte("Sex");
                    db2File.ReadByte("Data");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.BattlePetAbility:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadInt32("6");
                    break;
                }
                case DB2Hash.BattlePetAbilityEffect:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadInt16("4", i);
                    db2File.ReadByte("5");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.BattlePetAbilityState:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.BattlePetAbilityTurn:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.BattlePetBreedQuality:
                {
                    db2File.ReadSingle("Modifier");
                    db2File.ReadByte("Quality");
                    break;
                }
                case DB2Hash.BattlePetBreedState:
                {
                    db2File.ReadInt16("Value");
                    db2File.ReadByte("BreedID");
                    db2File.ReadByte("State");
                    break;
                }
                case DB2Hash.BattlePetEffectProperties:
                {
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadCString("0", i);
                    db2File.ReadInt16("1");
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadByte("2", i);
                    break;
                }
                case DB2Hash.BattlePetNPCTeamMember:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.BattlePetSpecies:
                {
                    db2File.ReadUInt32("CreatureID");
                    db2File.ReadUInt32("IconFileID");
                    db2File.ReadUInt32("SummonSpellID");
                    db2File.ReadCString("SourceText");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadByte("PetType");
                    db2File.ReadSByte("Source");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.BattlePetSpeciesState:
                {
                    db2File.ReadUInt32("Value");
                    db2File.ReadUInt16("SpeciesID");
                    db2File.ReadByte("State");
                    break;
                }
                case DB2Hash.BattlePetSpeciesXAbility:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.BattlePetState:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.BattlePetVisual:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.BattlemasterList:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt32("IconFileDataID");
                    db2File.ReadCString("GameType");
                    for (int i = 0; i < 16; ++i)
                        db2File.ReadInt16("MapID", i);
                    db2File.ReadUInt16("HolidayWorldState");
                    db2File.ReadUInt16("PlayerConditionID");
                    db2File.ReadByte("InstanceType");
                    db2File.ReadByte("GroupsAllowed");
                    db2File.ReadByte("MaxGroupSize");
                    db2File.ReadByte("MinLevel");
                    db2File.ReadByte("MaxLevel");
                    db2File.ReadByte("RatedPlayers");
                    db2File.ReadByte("MinPlayers");
                    db2File.ReadByte("MaxPlayers");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.BoneWindModifierModel:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.BoneWindModifiers:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadSingle("1");
                    break;
                }
                case DB2Hash.Bounty:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.BountySet:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.BroadcastText:
                {
                    db2File.ReadCString("MaleText");
                    db2File.ReadCString("FemaleText");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt16("EmoteID", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt16("EmoteDelay", i);
                    db2File.ReadUInt16("UnkEmoteID");
                    db2File.ReadByte("Language");
                    db2File.ReadByte("Type");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("SoundID", i);
                    db2File.ReadUInt32("PlayerConditionID");
                    break;
                }
                case DB2Hash.CameraEffect:
                {
                    db2File.ReadByte("0");
                    break;
                }
                case DB2Hash.CameraEffectEntry:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadInt16("8");
                    db2File.ReadInt16("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    db2File.ReadByte("14");
                    db2File.ReadByte("15");
                    break;
                }
                case DB2Hash.CameraMode:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    break;
                }
                case DB2Hash.CameraShakes:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadInt32("8");
                    break;
                }
                case DB2Hash.CastableRaidBuffs:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.Cfg_Categories:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.Cfg_Configs:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.Cfg_Regions:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.CharBaseInfo:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.CharBaseSection:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.CharComponentTextureLayouts:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.CharComponentTextureSections:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.CharHairGeosets:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadInt32("9");
                    break;
                }
                case DB2Hash.CharSections:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("TextureFileDataID", i);
                    db2File.ReadUInt16("Flags");
                    db2File.ReadByte("Race");
                    db2File.ReadByte("Gender");
                    db2File.ReadByte("GenType");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Color");
                    break;
                }
                case DB2Hash.CharShipment:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    break;
                }
                case DB2Hash.CharShipmentContainer:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    db2File.ReadByte("14");
                    db2File.ReadInt32("15");
                    break;
                }
                case DB2Hash.CharStartOutfit:
                {
                    for (int i = 0; i < 24; ++i)
                        db2File.ReadInt32("ItemID", i);
                    db2File.ReadUInt32("PetDisplayID");
                    db2File.ReadByte("RaceID");
                    db2File.ReadByte("ClassID");
                    db2File.ReadByte("GenderID");
                    db2File.ReadByte("OutfitID");
                    db2File.ReadByte("PetFamilyID");
                    break;
                }
                case DB2Hash.CharTitles:
                {
                    db2File.ReadCString("NameMale");
                    db2File.ReadCString("NameFemale");
                    db2File.ReadUInt16("MaskID");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.CharacterFaceBoneSet:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.CharacterFacialHairStyles:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadInt32("0", i);
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.CharacterLoadout:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.CharacterLoadoutItem:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ChatChannels:
                {
                    db2File.ReadUInt32("Flags");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Shortcut");
                    db2File.ReadByte("FactionGroup");
                    break;
                }
                case DB2Hash.ChatProfanity:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.ChrClassRaceSex:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.ChrClassTitle:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ChrClassUIDisplay:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ChrClassVillain:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ChrClasses:
                {
                    db2File.ReadCString("PetNameToken");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("NameFemale");
                    db2File.ReadCString("NameMale");
                    db2File.ReadCString("Filename");
                    db2File.ReadUInt32("CreateScreenFileDataID");
                    db2File.ReadUInt32("SelectScreenFileDataID");
                    db2File.ReadUInt32("LowResScreenFileDataID");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("CinematicSequenceID");
                    db2File.ReadUInt16("DefaultSpec");
                    db2File.ReadByte("PowerType");
                    db2File.ReadByte("SpellClassSet");
                    db2File.ReadByte("AttackPowerPerStrength");
                    db2File.ReadByte("AttackPowerPerAgility");
                    db2File.ReadByte("RangedAttackPowerPerAgility");
                    db2File.ReadByte("IconFileDataID");
                    db2File.ReadByte("Unk1");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ChrClassesXPowerTypes:
                {
                    db2File.ReadByte("ClassID");
                    db2File.ReadByte("PowerType");
                    break;
                }
                case DB2Hash.ChrRaces:
                {
                    db2File.ReadUInt32("Flags");
                    db2File.ReadCString("ClientPrefix");
                    db2File.ReadCString("ClientFileString");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("NameFemale");
                    db2File.ReadCString("NameMale");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadCString("FacialHairCustomization", i);
                    db2File.ReadCString("HairCustomization");
                    db2File.ReadUInt32("CreateScreenFileDataID");
                    db2File.ReadUInt32("SelectScreenFileDataID");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("MaleCustomizeOffset", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("FemaleCustomizeOffset", i);
                    db2File.ReadUInt32("LowResScreenFileDataID");
                    db2File.ReadUInt16("FactionID");
                    db2File.ReadUInt16("ExplorationSoundID");
                    db2File.ReadUInt16("MaleDisplayID");
                    db2File.ReadUInt16("FemaleDisplayID");
                    db2File.ReadUInt16("ResSicknessSpellID");
                    db2File.ReadUInt16("SplashSoundID");
                    db2File.ReadUInt16("CinematicSequenceID");
                    db2File.ReadUInt16("UAMaleCreatureSoundDataID");
                    db2File.ReadUInt16("UAFemaleCreatureSoundDataID");
                    db2File.ReadByte("BaseLanguage");
                    db2File.ReadByte("CreatureType");
                    db2File.ReadByte("TeamID");
                    db2File.ReadByte("RaceRelated");
                    db2File.ReadByte("UnalteredVisualRaceID");
                    db2File.ReadByte("CharComponentTextureLayoutID");
                    db2File.ReadByte("DefaultClassID");
                    db2File.ReadByte("NeutralRaceID");
                    db2File.ReadByte("ItemAppearanceFrameRaceID");
                    db2File.ReadByte("CharComponentTexLayoutHiResID");
                    db2File.ReadUInt32("HighResMaleDisplayID");
                    db2File.ReadUInt32("HighResFemaleDisplayID");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("Unk", i);
                    break;
                }
                case DB2Hash.ChrSpecialization:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("MasterySpellID", i);
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Name2");
                    db2File.ReadCString("Description");
                    db2File.ReadCString("BackgroundFile");
                    db2File.ReadUInt16("SpellIconID");
                    db2File.ReadByte("ClassID");
                    db2File.ReadByte("OrderIndex");
                    db2File.ReadByte("PetTalentType");
                    db2File.ReadByte("Role");
                    db2File.ReadByte("PrimaryStatOrder");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("Flags");
                    db2File.ReadUInt32("AnimReplacementSetID");
                    break;
                }
                case DB2Hash.ChrUpgradeBucket:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ChrUpgradeBucketSpell:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ChrUpgradeTier:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.CinematicCamera:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadSingle("2");
                    db2File.ReadInt16("3");
                    break;
                }
                case DB2Hash.CinematicSequences:
                {
                    db2File.ReadUInt16("SoundID");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("Camera", i);
                    break;
                }
                case DB2Hash.CloakDampening:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadSingle("0", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadSingle("1", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("2", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("3", i);
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    break;
                }
                case DB2Hash.CombatCondition:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt16("3", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt16("4", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadByte("5", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadByte("6", i);
                    db2File.ReadByte("7");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadByte("8", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadByte("9", i);
                    db2File.ReadByte("10");
                    break;
                }
                case DB2Hash.ComponentModelFileData:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.ComponentTextureFileData:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ConversationLine:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    break;
                }
                case DB2Hash.Creature:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("ItemID", i);
                    db2File.ReadUInt32("Mount");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("DisplayID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadSingle("DisplayIDProbability", i);
                    db2File.ReadCString("Name");
                    db2File.ReadCString("FemaleName");
                    db2File.ReadCString("SubName");
                    db2File.ReadCString("FemaleSubName");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Family");
                    db2File.ReadByte("Classification");
                    db2File.ReadByte("InhabitType");
                    break;
                }
                case DB2Hash.CreatureDifficulty:
                {
                    db2File.ReadInt32("0");
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadInt32("1", i);
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    break;
                }
                case DB2Hash.CreatureDispXUiCamera:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.CreatureDisplayInfo:
                {
                    db2File.ReadUInt32("ExtendedDisplayInfoID");
                    db2File.ReadSingle("CreatureModelScale");
                    db2File.ReadSingle("PlayerModelScale");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("TextureVariation", i);
                    db2File.ReadCString("PortraitTextureName");
                    db2File.ReadUInt32("PortraitCreatureDisplayInfoID");
                    db2File.ReadUInt32("CreatureGeosetData");
                    db2File.ReadUInt32("StateSpellVisualKitID");
                    db2File.ReadSingle("InstanceOtherPlayerPetScale");
                    db2File.ReadUInt16("ModelID");
                    db2File.ReadUInt16("SoundID");
                    db2File.ReadUInt16("NPCSoundID");
                    db2File.ReadUInt16("ParticleColorID");
                    db2File.ReadUInt16("ObjectEffectPackageID");
                    db2File.ReadUInt16("AnimReplacementSetID");
                    db2File.ReadByte("CreatureModelAlpha");
                    db2File.ReadByte("SizeClass");
                    db2File.ReadByte("BloodID");
                    db2File.ReadByte("Flags");
                    db2File.ReadSByte("Gender");
                    db2File.ReadSByte("Unk700");
                    break;
                }
                case DB2Hash.CreatureDisplayInfoCond:
                {
                    db2File.ReadInt32("0");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("1", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("2", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("3", i);
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("14", i);
                    break;
                }
                case DB2Hash.CreatureDisplayInfoExtra:
                {
                    db2File.ReadUInt32("FileDataID");
                    db2File.ReadUInt32("HDFileDataID");
                    db2File.ReadByte("DisplayRaceID");
                    db2File.ReadByte("DisplaySexID");
                    db2File.ReadByte("DisplayClassID");
                    db2File.ReadByte("SkinID");
                    db2File.ReadByte("FaceID");
                    db2File.ReadByte("HairStyleID");
                    db2File.ReadByte("HairColorID");
                    db2File.ReadByte("FacialHairID");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadByte("CustomDisplayOption", i);
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.CreatureDisplayInfoTrn:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadSingle("2");
                    db2File.ReadInt16("3");
                    break;
                }
                case DB2Hash.CreatureFamily:
                {
                    db2File.ReadSingle("MinScale");
                    db2File.ReadSingle("MaxScale");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("IconFile");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt16("SkillLine", i);
                    db2File.ReadUInt16("PetFoodMask");
                    db2File.ReadByte("MinScaleLevel");
                    db2File.ReadByte("MaxScaleLevel");
                    db2File.ReadByte("PetTalentType");
                    break;
                }
                case DB2Hash.CreatureImmunities:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("0", i);
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadInt32("6");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadInt32("7", i);
                    for (int i = 0; i < 16; ++i)
                        db2File.ReadInt32("8", i);
                    break;
                }
                case DB2Hash.CreatureModelData:
                {
                    db2File.ReadSingle("ModelScale");
                    db2File.ReadSingle("FootprintTextureLength");
                    db2File.ReadSingle("FootprintTextureWidth");
                    db2File.ReadSingle("FootprintParticleScale");
                    db2File.ReadSingle("CollisionWidth");
                    db2File.ReadSingle("CollisionHeight");
                    db2File.ReadSingle("MountHeight");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("GeoBoxMin", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("GeoBoxMax", i);
                    db2File.ReadSingle("WorldEffectScale");
                    db2File.ReadSingle("AttachedEffectScale");
                    db2File.ReadSingle("MissileCollisionRadius");
                    db2File.ReadSingle("MissileCollisionPush");
                    db2File.ReadSingle("MissileCollisionRaise");
                    db2File.ReadSingle("OverrideLootEffectScale");
                    db2File.ReadSingle("OverrideNameScale");
                    db2File.ReadSingle("OverrideSelectionRadius");
                    db2File.ReadSingle("TamedPetBaseScale");
                    db2File.ReadSingle("HoverHeight");
                    db2File.ReadUInt32("Flags");
                    db2File.ReadUInt32("FileDataID");
                    db2File.ReadUInt32("SizeClass");
                    db2File.ReadUInt32("BloodID");
                    db2File.ReadUInt32("FootprintTextureID");
                    db2File.ReadUInt32("FoleyMaterialID");
                    db2File.ReadUInt32("FootstepEffectID");
                    db2File.ReadUInt32("DeathThudEffectID");
                    db2File.ReadUInt32("FootstepShakeSize");
                    db2File.ReadUInt32("DeathThudShakeSize");
                    db2File.ReadUInt32("SoundID");
                    db2File.ReadUInt32("CreatureGeosetDataID");
                    break;
                }
                case DB2Hash.CreatureMovementInfo:
                {
                    db2File.ReadSingle("0");
                    break;
                }
                case DB2Hash.CreatureSoundData:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    db2File.ReadInt32("15");
                    db2File.ReadInt32("16");
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadInt32("17", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("18", i);
                    db2File.ReadInt32("19");
                    db2File.ReadInt32("20");
                    db2File.ReadInt32("21");
                    db2File.ReadInt32("22");
                    db2File.ReadInt32("23");
                    db2File.ReadInt32("24");
                    db2File.ReadInt32("25");
                    db2File.ReadInt32("26");
                    db2File.ReadInt32("27");
                    db2File.ReadInt32("28");
                    db2File.ReadInt32("29");
                    db2File.ReadInt32("30");
                    db2File.ReadInt32("31");
                    db2File.ReadInt32("32");
                    db2File.ReadInt32("33");
                    db2File.ReadInt32("34");
                    db2File.ReadInt32("35");
                    db2File.ReadInt32("36");
                    break;
                }
                case DB2Hash.CreatureType:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.Criteria:
                {
                    db2File.ReadUInt32("Asset");
                    db2File.ReadUInt32("StartAsset");
                    db2File.ReadUInt32("FailAsset");
                    db2File.ReadUInt16("StartTimer");
                    db2File.ReadUInt16("ModifierTreeId");
                    db2File.ReadUInt16("EligibilityWorldStateID");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("StartEvent");
                    db2File.ReadByte("FailEvent");
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("EligibilityWorldStateValue");
                    break;
                }
                case DB2Hash.CriteriaTree:
                {
                    db2File.ReadUInt32("CriteriaID");
                    db2File.ReadUInt32("Amount");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt16("Parent");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadByte("Operator");
                    db2File.ReadUInt32("OrderIndex");
                    break;
                }
                case DB2Hash.CriteriaTreeXEffect:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.CurrencyCategory:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.CurrencyTypes:
                {
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadCString("InventoryIcon", i);
                    db2File.ReadUInt32("MaxQty");
                    db2File.ReadUInt32("MaxEarnablePerWeek");
                    db2File.ReadUInt32("Flags");
                    db2File.ReadCString("Description");
                    db2File.ReadByte("CategoryID");
                    db2File.ReadByte("SpellCategory");
                    db2File.ReadByte("Quality");
                    db2File.ReadUInt32("SpellWeight");
                    break;
                }
                case DB2Hash.Curve:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.CurvePoint:
                {
                    db2File.ReadVector2("Point");
                    db2File.ReadUInt16("CurveID");
                    db2File.ReadByte("Index");
                    break;
                }
                case DB2Hash.DeathThudLookups:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.DecalProperties:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    break;
                }
                case DB2Hash.DeclinedWord:
                {
                    db2File.ReadCString("0");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.DeclinedWordCases:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.DestructibleModelData:
                {
                    db2File.ReadUInt16("StateDamagedDisplayID");
                    db2File.ReadUInt16("StateDestroyedDisplayID");
                    db2File.ReadUInt16("StateRebuildingDisplayID");
                    db2File.ReadUInt16("StateSmokeDisplayID");
                    db2File.ReadUInt16("HealEffectSpeed");
                    db2File.ReadByte("StateDamagedImpactEffectDoodadSet");
                    db2File.ReadByte("StateDamagedAmbientDoodadSet");
                    db2File.ReadByte("StateDamagedNameSet");
                    db2File.ReadByte("StateDestroyedDestructionDoodadSet");
                    db2File.ReadByte("StateDestroyedImpactEffectDoodadSet");
                    db2File.ReadByte("StateDestroyedAmbientDoodadSet");
                    db2File.ReadByte("StateDestroyedNameSet");
                    db2File.ReadByte("StateRebuildingDestructionDoodadSet");
                    db2File.ReadByte("StateRebuildingImpactEffectDoodadSet");
                    db2File.ReadByte("StateRebuildingAmbientDoodadSet");
                    db2File.ReadByte("StateRebuildingNameSet");
                    db2File.ReadByte("StateSmokeInitDoodadSet");
                    db2File.ReadByte("StateSmokeAmbientDoodadSet");
                    db2File.ReadByte("StateSmokeNameSet");
                    db2File.ReadByte("EjectDirection");
                    db2File.ReadByte("DoNotHighlight");
                    db2File.ReadByte("HealEffect");
                    break;
                }
                case DB2Hash.DeviceBlacklist:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.DeviceDefaultSettings:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.Difficulty:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadByte("FallbackDifficultyID");
                    db2File.ReadByte("InstanceType");
                    db2File.ReadByte("MinPlayers");
                    db2File.ReadByte("MaxPlayers");
                    db2File.ReadSByte("OldEnumValue");
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("ToggleDifficultyID");
                    db2File.ReadByte("GroupSizeHealthCurveID");
                    db2File.ReadByte("GroupSizeDmgCurveID");
                    db2File.ReadByte("GroupSizeSpellPointsCurveID");
                    db2File.ReadByte("ItemBonusTreeModID");
                    db2File.ReadByte("OrderIndex");
                    break;
                }
                case DB2Hash.DissolveEffect:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    break;
                }
                case DB2Hash.DriverBlacklist:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.DungeonEncounter:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt32("CreatureDisplayID");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("SpellIconID");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("Bit");
                    db2File.ReadByte("Flags");
                    db2File.ReadUInt32("OrderIndex");
                    break;
                }
                case DB2Hash.DungeonMap:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.DungeonMapChunk:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    break;
                }
                case DB2Hash.DurabilityCosts:
                {
                    for (int i = 0; i < 21; ++i)
                        db2File.ReadUInt16("WeaponSubClassCost", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("ArmorSubClassCost", i);
                    break;
                }
                case DB2Hash.DurabilityQuality:
                {
                    db2File.ReadSingle("QualityMod");
                    break;
                }
                case DB2Hash.EdgeGlowEffect:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadByte("9");
                    break;
                }
                case DB2Hash.Emotes:
                {
                    db2File.ReadCString("EmoteSlashCommand");
                    db2File.ReadUInt32("SpellVisualKitID");
                    db2File.ReadUInt32("EmoteFlags");
                    db2File.ReadUInt16("AnimID");
                    db2File.ReadByte("EmoteSpecProc");
                    db2File.ReadUInt32("EmoteSpecProcParam");
                    db2File.ReadUInt32("EmoteSoundID");
                    db2File.ReadUInt32("ClassMask");
                    db2File.ReadUInt32("RaceMask");
                    break;
                }
                case DB2Hash.EmotesText:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt16("EmoteID");
                    break;
                }
                case DB2Hash.EmotesTextData:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.EmotesTextSound:
                {
                    db2File.ReadUInt16("EmotesTextId");
                    db2File.ReadByte("RaceId");
                    db2File.ReadByte("SexId");
                    db2File.ReadByte("ClassId");
                    db2File.ReadUInt32("SoundId");
                    break;
                }
                case DB2Hash.EnvironmentalDamage:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.Exhaustion:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadCString("4");
                    db2File.ReadSingle("5");
                    db2File.ReadCString("6");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.Faction:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("ReputationRaceMask", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("ReputationBase", i);
                    db2File.ReadSingle("ParentFactionModIn");
                    db2File.ReadSingle("ParentFactionModOut");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Description");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("ReputationMax", i);
                    db2File.ReadInt16("ReputationIndex");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("ReputationClassMask", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("ReputationFlags", i);
                    db2File.ReadUInt16("ParentFactionID");
                    db2File.ReadByte("ParentFactionCapIn");
                    db2File.ReadByte("ParentFactionCapOut");
                    db2File.ReadByte("Expansion");
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("FriendshipRepID");
                    break;
                }
                case DB2Hash.FactionGroup:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.FactionTemplate:
                {
                    db2File.ReadUInt16("Faction");
                    db2File.ReadUInt16("Flags");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("Enemies", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("Friends", i);
                    db2File.ReadByte("Mask");
                    db2File.ReadByte("FriendMask");
                    db2File.ReadByte("EnemyMask");
                    break;
                }
                case DB2Hash.FootprintTextures:
                {
                    db2File.ReadCString("0");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.FootstepTerrainLookup:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.FriendshipRepReaction:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.FriendshipReputation:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.FullScreenEffect:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadSingle("11");
                    db2File.ReadSingle("12");
                    db2File.ReadSingle("13");
                    db2File.ReadSingle("14");
                    db2File.ReadSingle("15");
                    db2File.ReadSingle("16");
                    db2File.ReadSingle("17");
                    db2File.ReadSingle("18");
                    db2File.ReadInt32("19");
                    db2File.ReadInt32("20");
                    db2File.ReadSingle("21");
                    db2File.ReadSingle("22");
                    db2File.ReadSingle("23");
                    db2File.ReadSingle("24");
                    db2File.ReadSingle("25");
                    db2File.ReadSingle("26");
                    db2File.ReadSingle("27");
                    db2File.ReadSingle("28");
                    db2File.ReadSingle("29");
                    db2File.ReadSingle("30");
                    db2File.ReadSingle("31");
                    db2File.ReadSingle("32");
                    db2File.ReadSingle("33");
                    db2File.ReadSingle("34");
                    db2File.ReadByte("35");
                    db2File.ReadInt32("36");
                    db2File.ReadInt32("37");
                    db2File.ReadInt32("38");
                    break;
                }
                case DB2Hash.GMSurveyAnswers:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.GMSurveyCurrentSurvey:
                {
                    db2File.ReadByte("0");
                    break;
                }
                case DB2Hash.GMSurveyQuestions:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.GMSurveySurveys:
                {
                    for (int i = 0; i < 15; ++i)
                        db2File.ReadByte("0", i);
                    break;
                }
                case DB2Hash.GameObjectArtKit:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadCString("0", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadCString("1", i);
                    break;
                }
                case DB2Hash.GameObjectDiffAnimMap:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.GameObjectDisplayInfo:
                {
                    db2File.ReadInt32("FileDataID");
                    db2File.ReadVector3("GeoBoxMin");
                    db2File.ReadVector3("GeoBoxMax");
                    db2File.ReadSingle("OverrideLootEffectScale");
                    db2File.ReadSingle("OverrideNameScale");
                    db2File.ReadInt16("ObjectEffectPackageID");
                    break;
                }
                case DB2Hash.GameObjectDisplayInfoXSoundKit:
                {
                    db2File.ReadByte("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.GameObjects:
                {
                    db2File.ReadVector3("Position");
                    db2File.ReadSingle("RotationX");
                    db2File.ReadSingle("RotationY");
                    db2File.ReadSingle("RotationZ");
                    db2File.ReadSingle("RotationW");
                    db2File.ReadSingle("Size");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadInt32("Data", i);
                    db2File.ReadCString("Name");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("DisplayID");
                    db2File.ReadUInt16("PhaseID");
                    db2File.ReadUInt16("PhaseGroupID");
                    db2File.ReadByte("PhaseUseFlags");
                    db2File.ReadByte("Type");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GameTips:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.GarrAbility:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt32("IconFileDataID");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("OtherFactionGarrAbilityID");
                    db2File.ReadByte("GarrAbilityCategoryID");
                    db2File.ReadByte("FollowerTypeID");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrAbilityCategory:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.GarrAbilityEffect:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrBuilding:
                {
                    db2File.ReadUInt32("HordeGameObjectID");
                    db2File.ReadUInt32("AllianceGameObjectID");
                    db2File.ReadCString("NameAlliance");
                    db2File.ReadCString("NameHorde");
                    db2File.ReadCString("Description");
                    db2File.ReadCString("Tooltip");
                    db2File.ReadUInt32("IconFileDataID");
                    db2File.ReadUInt16("CostCurrencyID");
                    db2File.ReadUInt16("HordeTexPrefixKitID");
                    db2File.ReadUInt16("AllianceTexPrefixKitID");
                    db2File.ReadUInt16("AllianceActivationScenePackageID");
                    db2File.ReadUInt16("HordeActivationScenePackageID");
                    db2File.ReadUInt16("FollowerRequiredGarrAbilityID");
                    db2File.ReadUInt16("FollowerGarrAbilityEffectID");
                    db2File.ReadUInt16("CostMoney");
                    db2File.ReadByte("Unknown");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Level");
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("MaxShipments");
                    db2File.ReadByte("GarrTypeID");
                    db2File.ReadUInt32("BuildDuration");
                    db2File.ReadInt32("CostCurrencyAmount");
                    db2File.ReadUInt32("BonusAmount");
                    break;
                }
                case DB2Hash.GarrBuildingDoodadSet:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.GarrBuildingPlotInst:
                {
                    db2File.ReadVector2("LandmarkOffset");
                    db2File.ReadUInt16("UiTextureAtlasMemberID");
                    db2File.ReadUInt16("GarrSiteLevelPlotInstID");
                    db2File.ReadByte("GarrBuildingID");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrClassSpec:
                {
                    db2File.ReadCString("NameMale");
                    db2File.ReadCString("NameFemale");
                    db2File.ReadCString("NameGenderless");
                    db2File.ReadUInt16("ClassAtlasID");
                    db2File.ReadByte("GarrFollItemSetID");
                    db2File.ReadByte("Limit");
                    db2File.ReadByte("Flags");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrClassSpecPlayerCond:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.GarrEncounter:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt16("5");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrEncounterSetXEncounter:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.GarrEncounterXMechanic:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.GarrFollItemSetMember:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.GarrFollSupportSpell:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadByte("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.GarrFollower:
                {
                    db2File.ReadUInt32("HordeCreatureID");
                    db2File.ReadUInt32("AllianceCreatureID");
                    db2File.ReadCString("HordeSourceText");
                    db2File.ReadCString("AllianceSourceText");
                    db2File.ReadUInt32("HordePortraitIconID");
                    db2File.ReadUInt32("AlliancePortraitIconID");
                    db2File.ReadUInt32("HordeAddedBroadcastTextID");
                    db2File.ReadUInt32("AllianceAddedBroadcastTextID");
                    db2File.ReadUInt16("HordeGarrFollItemSetID");
                    db2File.ReadUInt16("AllianceGarrFollItemSetID");
                    db2File.ReadUInt16("ItemLevelWeapon");
                    db2File.ReadUInt16("ItemLevelArmor");
                    db2File.ReadUInt16("HordeListPortraitTextureKitID");
                    db2File.ReadUInt16("AllianceListPortraitTextureKitID");
                    db2File.ReadByte("FollowerTypeID");
                    db2File.ReadByte("HordeUiAnimRaceInfoID");
                    db2File.ReadByte("AllianceUiAnimRaceInfoID");
                    db2File.ReadByte("Quality");
                    db2File.ReadByte("HordeGarrClassSpecID");
                    db2File.ReadByte("AllianceGarrClassSpecID");
                    db2File.ReadByte("Level");
                    db2File.ReadByte("Unknown1");
                    db2File.ReadByte("Flags");
                    db2File.ReadSByte("Unknown2");
                    db2File.ReadSByte("Unknown3");
                    db2File.ReadByte("GarrTypeID");
                    db2File.ReadByte("MaxDurability");
                    db2File.ReadByte("Class");
                    db2File.ReadByte("HordeFlavorTextGarrStringID");
                    db2File.ReadByte("AllianceFlavorTextGarrStringID");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrFollowerLevelXP:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.GarrFollowerQuality:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadInt32("6");
                    break;
                }
                case DB2Hash.GarrFollowerSetXFollower:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.GarrFollowerType:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.GarrFollowerUICreature:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    break;
                }
                case DB2Hash.GarrFollowerXAbility:
                {
                    db2File.ReadUInt16("GarrFollowerID");
                    db2File.ReadUInt16("GarrAbilityID");
                    db2File.ReadByte("FactionIndex");
                    break;
                }
                case DB2Hash.GarrMechanic:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.GarrMechanicSetXMechanic:
                {
                    db2File.ReadByte("0");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.GarrMechanicType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt32("2");
                    db2File.ReadByte("3");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.GarrMission:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadCString("2");
                    db2File.ReadCString("3");
                    db2File.ReadCString("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("7", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("8", i);
                    db2File.ReadInt16("9");
                    db2File.ReadInt16("10");
                    db2File.ReadInt16("11");
                    db2File.ReadInt16("12");
                    db2File.ReadInt16("13");
                    db2File.ReadInt16("14");
                    db2File.ReadByte("15");
                    db2File.ReadByte("16");
                    db2File.ReadByte("17");
                    db2File.ReadByte("18");
                    db2File.ReadByte("19");
                    db2File.ReadByte("20");
                    db2File.ReadByte("21");
                    db2File.ReadByte("22");
                    db2File.ReadByte("23");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("25");
                    db2File.ReadInt32("26");
                    db2File.ReadInt32("27");
                    db2File.ReadInt32("28");
                    break;
                }
                case DB2Hash.GarrMissionTexture:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.GarrMissionType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.GarrMissionXEncounter:
                {
                    db2File.ReadByte("0");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.GarrMissionXFollower:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.GarrMssnBonusAbility:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.GarrPlot:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt32("AllianceConstructionGameObjectID");
                    db2File.ReadUInt32("HordeConstructionGameObjectID");
                    db2File.ReadByte("GarrPlotUICategoryID");
                    db2File.ReadByte("PlotType");
                    db2File.ReadByte("Flags");
                    db2File.ReadUInt32("MinCount");
                    db2File.ReadUInt32("MaxCount");
                    break;
                }
                case DB2Hash.GarrPlotBuilding:
                {
                    db2File.ReadByte("GarrPlotID");
                    db2File.ReadByte("GarrBuildingID");
                    break;
                }
                case DB2Hash.GarrPlotInstance:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadByte("GarrPlotID");
                    break;
                }
                case DB2Hash.GarrPlotUICategory:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.GarrSiteLevel:
                {
                    db2File.ReadVector2("TownHall");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("SiteID");
                    db2File.ReadUInt16("UpgradeResourceCost");
                    db2File.ReadUInt16("UpgradeMoneyCost");
                    db2File.ReadByte("Level");
                    db2File.ReadByte("UITextureKitID");
                    db2File.ReadByte("MovieID");
                    db2File.ReadByte("Level2");
                    break;
                }
                case DB2Hash.GarrSiteLevelPlotInst:
                {
                    db2File.ReadVector2("Landmark");
                    db2File.ReadUInt16("GarrSiteLevelID");
                    db2File.ReadByte("GarrPlotInstanceID");
                    db2File.ReadByte("Unknown");
                    break;
                }
                case DB2Hash.GarrSpecialization:
                {
                    db2File.ReadInt32("0");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadCString("2");
                    db2File.ReadCString("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.GarrString:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.GarrTalent:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt32("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    db2File.ReadInt32("15");
                    db2File.ReadInt32("16");
                    db2File.ReadInt32("17");
                    db2File.ReadInt32("18");
                    db2File.ReadInt32("19");
                    break;
                }
                case DB2Hash.GarrTalentTree:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.GarrType:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.GarrUiAnimClassInfo:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.GarrUiAnimRaceInfo:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadSingle("11");
                    db2File.ReadByte("12");
                    break;
                }
                case DB2Hash.GemProperties:
                {
                    db2File.ReadUInt32("Type");
                    db2File.ReadUInt16("EnchantID");
                    db2File.ReadUInt16("MinItemLevel");
                    break;
                }
                case DB2Hash.GlobalStrings:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.GlyphBindableSpell:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.GlyphExclusiveCategory:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.GlyphProperties:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt16("SpellIconID");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("GlyphExclusiveCategoryID");
                    break;
                }
                case DB2Hash.GlyphRequiredSpec:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.GroundEffectDoodad:
                {
                    db2File.ReadCString("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.GroundEffectTexture:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt16("0", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadByte("1", i);
                    db2File.ReadByte("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.GroupFinderActivity:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    break;
                }
                case DB2Hash.GroupFinderActivityGrp:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.GroupFinderCategory:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.GuildColorBackground:
                {
                    db2File.ReadByte("Red");
                    db2File.ReadByte("Green");
                    db2File.ReadByte("Blue");
                    break;
                }
                case DB2Hash.GuildColorBorder:
                {
                    db2File.ReadByte("Red");
                    db2File.ReadByte("Green");
                    db2File.ReadByte("Blue");
                    break;
                }
                case DB2Hash.GuildColorEmblem:
                {
                    db2File.ReadByte("Red");
                    db2File.ReadByte("Green");
                    db2File.ReadByte("Blue");
                    break;
                }
                case DB2Hash.GuildPerkSpells:
                {
                    db2File.ReadUInt32("SpellID");
                    break;
                }
                case DB2Hash.Heirloom:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadCString("SourceText");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("OldItem", i);
                    db2File.ReadUInt32("NextDifficultyItemID");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("UpgradeItemID", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt16("ItemBonusListID", i);
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("Source");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.HelmetAnimScaling:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.HelmetGeosetVisData:
                {
                    for (int i = 0; i < 9; ++i)
                        db2File.ReadInt32("0", i);
                    break;
                }
                case DB2Hash.HighlightColor:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.HolidayDescriptions:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.HolidayNames:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.Holidays:
                {
                    for (int i = 0; i < 16; ++i)
                        db2File.ReadUInt32("Date", i);
                    db2File.ReadCString("TextureFilename");
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadUInt16("Duration", i);
                    db2File.ReadUInt16("Region");
                    db2File.ReadByte("Looping");
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadByte("CalendarFlags", i);
                    db2File.ReadByte("HolidayNameID");
                    db2File.ReadByte("HolidayDescriptionID");
                    db2File.ReadByte("Priority");
                    db2File.ReadSByte("CalendarFilterType");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.ImportPriceArmor:
                {
                    db2File.ReadSingle("ClothFactor");
                    db2File.ReadSingle("LeatherFactor");
                    db2File.ReadSingle("MailFactor");
                    db2File.ReadSingle("PlateFactor");
                    break;
                }
                case DB2Hash.ImportPriceQuality:
                {
                    db2File.ReadSingle("Factor");
                    break;
                }
                case DB2Hash.ImportPriceShield:
                {
                    db2File.ReadSingle("Factor");
                    break;
                }
                case DB2Hash.ImportPriceWeapon:
                {
                    db2File.ReadSingle("Factor");
                    break;
                }
                case DB2Hash.InvasionClientData:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    break;
                }
                case DB2Hash.Item:
                {
                    db2File.ReadUInt32("FileDataID");
                    db2File.ReadByte("Class");
                    db2File.ReadByte("SubClass");
                    db2File.ReadSByte("SoundOverrideSubclass");
                    db2File.ReadSByte("Material");
                    db2File.ReadByte("InventoryType");
                    db2File.ReadByte("Sheath");
                    db2File.ReadByte("GroupSoundsID");
                    break;
                }
                case DB2Hash.ItemAppearance:
                {
                    db2File.ReadUInt32("DisplayID");
                    db2File.ReadUInt32("IconFileDataID");
                    db2File.ReadUInt32("UIOrder");
                    db2File.ReadByte("ObjectComponentSlot");
                    break;
                }
                case DB2Hash.ItemAppearanceXUiCamera:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ItemArmorQuality:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("QualityMod", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemArmorShield:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("Quality", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemArmorTotal:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadSingle("Value", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemBagFamily:
                {
                    db2File.ReadCString("Name");
                    break;
                }
                case DB2Hash.ItemBonus:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("Value", i);
                    db2File.ReadUInt16("BonusListID");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Index");
                    break;
                }
                case DB2Hash.ItemBonusListLevelDelta:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ItemBonusTreeNode:
                {
                    db2File.ReadUInt16("BonusTreeID");
                    db2File.ReadUInt16("SubTreeID");
                    db2File.ReadUInt16("BonusListID");
                    db2File.ReadByte("BonusTreeModID");
                    break;
                }
                case DB2Hash.ItemChildEquipment:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt32("AltItemID");
                    db2File.ReadByte("AltEquipmentSlot");
                    break;
                }
                case DB2Hash.ItemClass:
                {
                    db2File.ReadSingle("PriceMod");
                    db2File.ReadCString("Name");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.ItemContextPickerEntry:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.ItemCurrencyCost:
                {
                    db2File.ReadUInt32("ItemId");
                    break;
                }
                case DB2Hash.ItemDamageAmmo:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ItemDamageOneHand:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("DPS", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemDamageOneHandCaster:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("DPS", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemDamageTwoHand:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("DPS", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemDamageTwoHandCaster:
                {
                    for (int i = 0; i < 7; ++i)
                        db2File.ReadSingle("DPS", i);
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemDisenchantLoot:
                {
                    db2File.ReadUInt16("MinItemLevel");
                    db2File.ReadUInt16("MaxItemLevel");
                    db2File.ReadUInt16("RequiredDisenchantSkill");
                    db2File.ReadByte("ItemClass");
                    db2File.ReadSByte("ItemSubClass");
                    db2File.ReadByte("ItemQuality");
                    break;
                }
                case DB2Hash.ItemDisplayInfo:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("0", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("1", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("2", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("3", i);
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("6", i);
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    break;
                }
                case DB2Hash.ItemDisplayInfoMaterialRes:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ItemDisplayXUiCamera:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.ItemEffect:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadInt32("Cooldown");
                    db2File.ReadInt32("CategoryCooldown");
                    db2File.ReadInt16("Charges");
                    db2File.ReadUInt16("Category");
                    db2File.ReadUInt16("ChrSpecializationID");
                    db2File.ReadByte("OrderIndex");
                    db2File.ReadByte("Trigger");
                    break;
                }
                case DB2Hash.ItemExtendedCost:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt32("RequiredItem", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt32("RequiredCurrencyCount", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt16("RequiredItemCount", i);
                    db2File.ReadUInt16("RequiredPersonalArenaRating");
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt16("RequiredCurrency", i);
                    db2File.ReadByte("RequiredArenaSlot");
                    db2File.ReadByte("RequiredFactionId");
                    db2File.ReadByte("RequiredFactionStanding");
                    db2File.ReadByte("RequirementFlags");
                    db2File.ReadByte("RequiredAchievement");
                    break;
                }
                case DB2Hash.ItemGroupSounds:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("0", i);
                    break;
                }
                case DB2Hash.ItemLimitCategory:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadByte("Quantity");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.ItemLimitCategoryCondition:
                {
                    db2File.ReadByte("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.ItemModifiedAppearance:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt16("AppearanceID");
                    db2File.ReadByte("AppearanceModID");
                    db2File.ReadByte("Index");
                    db2File.ReadByte("SourceType");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ItemModifiedAppearanceExtra:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.ItemNameDescription:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.ItemPetFood:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.ItemPriceBase:
                {
                    db2File.ReadSingle("ArmorFactor");
                    db2File.ReadSingle("WeaponFactor");
                    db2File.ReadUInt16("ItemLevel");
                    break;
                }
                case DB2Hash.ItemRandomProperties:
                {
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt16("Enchantment", i);
                    break;
                }
                case DB2Hash.ItemRandomSuffix:
                {
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt16("Enchantment", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt16("AllocationPct", i);
                    break;
                }
                case DB2Hash.ItemRangedDisplayInfo:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.ItemSearchName:
                {
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("Flags", i);
                    db2File.ReadUInt32("AllowableRace");
                    db2File.ReadUInt32("RequiredSpell");
                    db2File.ReadUInt16("RequiredReputationFaction");
                    db2File.ReadUInt16("RequiredSkill");
                    db2File.ReadUInt16("RequiredSkillRank");
                    db2File.ReadUInt16("ItemLevel");
                    db2File.ReadByte("Quality");
                    db2File.ReadByte("RequiredExpansion");
                    db2File.ReadByte("RequiredReputationRank");
                    db2File.ReadByte("RequiredLevel");
                    db2File.ReadUInt32("AllowableClass");
                    break;
                }
                case DB2Hash.ItemSet:
                {
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 17; ++i)
                        db2File.ReadUInt32("ItemID", i);
                    db2File.ReadUInt16("RequiredSkillRank");
                    db2File.ReadUInt32("RequiredSkill");
                    db2File.ReadUInt32("Flags");
                    break;
                }
                case DB2Hash.ItemSetSpell:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt16("ItemSetID");
                    db2File.ReadUInt16("ChrSpecID");
                    db2File.ReadByte("Threshold");
                    break;
                }
                case DB2Hash.Item_sparse:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("Flags", i);
                    db2File.ReadSingle("Unk1");
                    db2File.ReadSingle("Unk2");
                    db2File.ReadUInt32("BuyPrice");
                    db2File.ReadUInt32("SellPrice");
                    db2File.ReadUInt32("AllowableClass");
                    db2File.ReadUInt32("AllowableRace");
                    db2File.ReadUInt32("RequiredSpell");
                    db2File.ReadInt32("MaxCount");
                    db2File.ReadInt32("Stackable");
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadInt32("ItemStatAllocation", i);
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadSingle("ItemStatSocketCostMultiplier", i);
                    db2File.ReadSingle("RangedModRange");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Name2");
                    db2File.ReadCString("Name3");
                    db2File.ReadCString("Name4");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt32("BagFamily");
                    db2File.ReadSingle("ArmorDamageModifier");
                    db2File.ReadUInt32("Duration");
                    db2File.ReadSingle("StatScalingFactor");
                    db2File.ReadUInt16("ItemLevel");
                    db2File.ReadUInt16("RequiredSkill");
                    db2File.ReadUInt16("RequiredSkillRank");
                    db2File.ReadUInt16("RequiredReputationFaction");
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadInt16("ItemStatValue", i);
                    db2File.ReadUInt16("ScalingStatDistribution");
                    db2File.ReadUInt16("Delay");
                    db2File.ReadUInt16("PageText");
                    db2File.ReadUInt16("StartQuest");
                    db2File.ReadUInt16("LockID");
                    db2File.ReadUInt16("RandomProperty");
                    db2File.ReadUInt16("RandomSuffix");
                    db2File.ReadUInt16("ItemSet");
                    db2File.ReadUInt16("Area");
                    db2File.ReadUInt16("Map");
                    db2File.ReadUInt16("SocketBonus");
                    db2File.ReadUInt16("GemProperties");
                    db2File.ReadUInt16("ItemLimitCategory");
                    db2File.ReadUInt16("HolidayID");
                    db2File.ReadUInt16("ItemNameDescriptionID");
                    db2File.ReadByte("Quality");
                    db2File.ReadByte("BuyCount");
                    db2File.ReadByte("InventoryType");
                    db2File.ReadSByte("RequiredLevel");
                    db2File.ReadByte("RequiredHonorRank");
                    db2File.ReadByte("RequiredCityRank");
                    db2File.ReadByte("RequiredReputationRank");
                    db2File.ReadByte("ContainerSlots");
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadSByte("ItemStatType", i);
                    db2File.ReadByte("DamageType");
                    db2File.ReadByte("Bonding");
                    db2File.ReadByte("LanguageID");
                    db2File.ReadByte("PageMaterial");
                    db2File.ReadSByte("Material");
                    db2File.ReadByte("Sheath");
                    db2File.ReadByte("TotemCategory");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadByte("SocketColor", i);
                    db2File.ReadByte("CurrencySubstitutionID");
                    db2File.ReadByte("CurrencySubstitutionCount");
                    db2File.ReadByte("ArtifactID");
                    db2File.ReadByte("RequiredExpansion");
                    break;
                }
                case DB2Hash.ItemSpec:
                {
                    db2File.ReadUInt16("SpecID");
                    db2File.ReadByte("MinLevel");
                    db2File.ReadByte("MaxLevel");
                    db2File.ReadByte("ItemType");
                    db2File.ReadByte("PrimaryStat");
                    db2File.ReadByte("SecondaryStat");
                    break;
                }
                case DB2Hash.ItemSpecOverride:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt16("SpecID");
                    break;
                }
                case DB2Hash.ItemSubClass:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    break;
                }
                case DB2Hash.ItemSubClassMask:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ItemUpgrade:
                {
                    db2File.ReadUInt32("CurrencyCost");
                    db2File.ReadUInt16("PrevItemUpgradeID");
                    db2File.ReadUInt16("CurrencyID");
                    db2File.ReadByte("ItemUpgradePathID");
                    db2File.ReadByte("ItemLevelBonus");
                    break;
                }
                case DB2Hash.ItemVisualEffects:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.ItemVisuals:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadInt16("0", i);
                    break;
                }
                case DB2Hash.ItemXBonusTree:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt16("BonusTreeID");
                    break;
                }
                case DB2Hash.JournalEncounter:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadInt32("9");
                    break;
                }
                case DB2Hash.JournalEncounterCreature:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadCString("2");
                    db2File.ReadCString("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.JournalEncounterItem:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.JournalEncounterSection:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadInt16("8");
                    db2File.ReadInt16("9");
                    db2File.ReadInt16("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    break;
                }
                case DB2Hash.JournalEncounterXDifficulty:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.JournalInstance:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadCString("4");
                    db2File.ReadCString("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.JournalItemXDifficulty:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.JournalSectionXDifficulty:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.JournalTier:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.JournalTierXInstance:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.KeyChain:
                {
                    for (int i = 0; i < 32; ++i)
                        db2File.ReadByte("Key", i);
                    break;
                }
                case DB2Hash.KeystoneAffix:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.LanguageWords:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.Languages:
                {
                    db2File.ReadCString("0");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.LfgDungeonExpansion:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    break;
                }
                case DB2Hash.LfgDungeonGroup:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.LfgDungeons:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt32("Flags");
                    db2File.ReadCString("TextureFilename");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt32("PlayerConditionID");
                    db2File.ReadUInt16("MaxLevel");
                    db2File.ReadUInt16("TargetLevelMax");
                    db2File.ReadInt16("MapID");
                    db2File.ReadUInt16("RandomID");
                    db2File.ReadUInt16("ScenarioID");
                    db2File.ReadUInt16("LastBossJournalEncounterID");
                    db2File.ReadUInt16("BonusReputationAmount");
                    db2File.ReadUInt16("MentorItemLevel");
                    db2File.ReadByte("MinLevel");
                    db2File.ReadByte("TargetLevel");
                    db2File.ReadByte("TargetLevelMin");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Faction");
                    db2File.ReadByte("Expansion");
                    db2File.ReadByte("OrderIndex");
                    db2File.ReadByte("GroupID");
                    db2File.ReadByte("CountTank");
                    db2File.ReadByte("CountHealer");
                    db2File.ReadByte("CountDamage");
                    db2File.ReadByte("MinCountTank");
                    db2File.ReadByte("MinCountHealer");
                    db2File.ReadByte("MinCountDamage");
                    db2File.ReadByte("SubType");
                    db2File.ReadByte("MentorCharLevel");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.LfgDungeonsGroupingMap:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.LfgRoleRequirement:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.Light:
                {
                    db2File.ReadVector3("Pos");
                    db2File.ReadSingle("FalloffStart");
                    db2File.ReadSingle("FalloffEnd");
                    db2File.ReadUInt16("MapID");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("LightParamsID", i);
                    break;
                }
                case DB2Hash.LightData:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    db2File.ReadInt32("15");
                    db2File.ReadInt32("16");
                    db2File.ReadInt32("17");
                    db2File.ReadSingle("18");
                    db2File.ReadSingle("19");
                    db2File.ReadSingle("20");
                    db2File.ReadSingle("21");
                    db2File.ReadSingle("22");
                    db2File.ReadSingle("23");
                    db2File.ReadSingle("24");
                    db2File.ReadSingle("25");
                    db2File.ReadSingle("26");
                    db2File.ReadInt32("27");
                    db2File.ReadInt32("28");
                    db2File.ReadInt32("29");
                    db2File.ReadInt32("30");
                    db2File.ReadInt32("31");
                    db2File.ReadInt32("32");
                    db2File.ReadInt16("33");
                    db2File.ReadInt16("34");
                    break;
                }
                case DB2Hash.LightParams:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("5", i);
                    db2File.ReadInt16("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.LightSkybox:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.LiquidMaterial:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.LiquidObject:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.LiquidType:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadSingle("MaxDarkenDepth");
                    db2File.ReadSingle("FogDarkenIntensity");
                    db2File.ReadSingle("AmbDarkenIntensity");
                    db2File.ReadSingle("DirDarkenIntensity");
                    db2File.ReadSingle("ParticleScale");
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadCString("Texture", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Color", i);
                    for (int i = 0; i < 18; ++i)
                        db2File.ReadSingle("Float", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("Int", i);
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("LightID");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("ParticleMovement");
                    db2File.ReadByte("ParticleTexSlots");
                    db2File.ReadByte("MaterialID");
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadByte("DepthTexCount", i);
                    db2File.ReadUInt32("SoundID");
                    break;
                }
                case DB2Hash.LoadingScreenTaxiSplines:
                {
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadSingle("0", i);
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.LoadingScreens:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.Locale:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.Location:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    break;
                }
                case DB2Hash.Lock:
                {
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt32("Index", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("Skill", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadByte("Type", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadByte("Action", i);
                    break;
                }
                case DB2Hash.LockType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadCString("3");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.LookAtController:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    db2File.ReadInt32("15");
                    db2File.ReadInt32("16");
                    db2File.ReadInt32("17");
                    break;
                }
                case DB2Hash.MailTemplate:
                {
                    db2File.ReadCString("Body");
                    break;
                }
                case DB2Hash.ManifestInterfaceActionIcon:
                {
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ManifestInterfaceData:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    break;
                }
                case DB2Hash.ManifestInterfaceItemIcon:
                {
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ManifestInterfaceTOCData:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.ManifestMP3:
                {
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.Map:
                {
                    db2File.ReadCString("Directory");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Flags", i);
                    db2File.ReadSingle("MinimapIconScale");
                    db2File.ReadVector2("CorpsePos");
                    db2File.ReadCString("MapName");
                    db2File.ReadCString("MapDescription0");
                    db2File.ReadCString("MapDescription1");
                    db2File.ReadUInt16("AreaTableID");
                    db2File.ReadUInt16("LoadingScreenID");
                    db2File.ReadInt16("CorpseMapID");
                    db2File.ReadUInt16("TimeOfDayOverride");
                    db2File.ReadInt16("ParentMapID");
                    db2File.ReadInt16("CosmeticParentMapID");
                    db2File.ReadUInt16("WindSettingsID");
                    db2File.ReadByte("InstanceType");
                    db2File.ReadByte("unk5");
                    db2File.ReadByte("ExpansionID");
                    db2File.ReadByte("MaxPlayers");
                    db2File.ReadByte("TimeOffset");
                    break;
                }
                case DB2Hash.MapChallengeMode:
                {
                    db2File.ReadInt16("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt16("1", i);
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.MapDifficulty:
                {
                    db2File.ReadCString("Message_lang");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("RaidDurationType");
                    db2File.ReadByte("MaxPlayers");
                    db2File.ReadByte("LockID");
                    db2File.ReadByte("ItemBonusTreeModID");
                    db2File.ReadUInt32("Context");
                    break;
                }
                case DB2Hash.MapDifficultyXCondition:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.MarketingPromotionsXLocale:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadCString("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.Material:
                {
                    db2File.ReadByte("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.MinorTalent:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.ModelAnimCloakDampening:
                {
                    db2File.ReadByte("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.ModelFileData:
                {
                    db2File.ReadByte("0");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.ModelRibbonQuality:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.ModifierTree:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Asset", i);
                    db2File.ReadUInt32("Parent");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("Unk700");
                    db2File.ReadByte("Operator");
                    db2File.ReadByte("Amount");
                    break;
                }
                case DB2Hash.Mount:
                {
                    db2File.ReadUInt32("SpellId");
                    db2File.ReadUInt32("DisplayId");
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Description");
                    db2File.ReadCString("SourceDescription");
                    db2File.ReadSingle("CameraPivotMultiplier");
                    db2File.ReadUInt16("MountTypeId");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("PlayerConditionId");
                    db2File.ReadByte("Source");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.MountCapability:
                {
                    db2File.ReadUInt32("RequiredSpell");
                    db2File.ReadUInt32("SpeedModSpell");
                    db2File.ReadUInt16("RequiredRidingSkill");
                    db2File.ReadUInt16("RequiredArea");
                    db2File.ReadInt16("RequiredMap");
                    db2File.ReadByte("Flags");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("RequiredAura");
                    break;
                }
                case DB2Hash.MountTypeXCapability:
                {
                    db2File.ReadUInt16("MountTypeID");
                    db2File.ReadUInt16("MountCapabilityID");
                    db2File.ReadByte("OrderIndex");
                    break;
                }
                case DB2Hash.Movie:
                {
                    db2File.ReadUInt32("AudioFileDataID");
                    db2File.ReadUInt32("SubtitleFileDataID");
                    db2File.ReadByte("Volume");
                    db2File.ReadByte("KeyID");
                    break;
                }
                case DB2Hash.MovieFileData:
                {
                    db2File.ReadInt16("0");
                    break;
                }
                case DB2Hash.MovieVariation:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.NPCSounds:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("0", i);
                    break;
                }
                case DB2Hash.NameGen:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadByte("Race");
                    db2File.ReadByte("Sex");
                    break;
                }
                case DB2Hash.NamesProfanity:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadSByte("Language");
                    break;
                }
                case DB2Hash.NamesReserved:
                {
                    db2File.ReadCString("Name");
                    break;
                }
                case DB2Hash.NamesReservedLocale:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadByte("LocaleMask");
                    break;
                }
                case DB2Hash.NpcModelItemSlotDisplayInfo:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ObjectEffect:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadInt32("8");
                    break;
                }
                case DB2Hash.ObjectEffectGroup:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.ObjectEffectModifier:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.ObjectEffectPackage:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.ObjectEffectPackageElem:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.OutlineEffect:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.OverrideSpellData:
                {
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadUInt32("SpellID", i);
                    db2File.ReadUInt32("PlayerActionbarFileDataID");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.PageTextMaterial:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.PaperDollItemFrame:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ParticleColor:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("0", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("1", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("2", i);
                    break;
                }
                case DB2Hash.Path:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.PathNode:
                {
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    break;
                }
                case DB2Hash.PathNodeProperty:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.PathProperty:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.Phase:
                {
                    db2File.ReadUInt16("Flags");
                    break;
                }
                case DB2Hash.PhaseShiftZoneSounds:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    break;
                }
                case DB2Hash.PhaseXPhaseGroup:
                {
                    db2File.ReadUInt16("PhaseID");
                    db2File.ReadUInt16("PhaseGroupID");
                    break;
                }
                case DB2Hash.PlayerCondition:
                {
                    db2File.ReadUInt32("RaceMask");
                    db2File.ReadUInt32("SkillLogic");
                    db2File.ReadUInt32("ReputationLogic");
                    db2File.ReadUInt32("PrevQuestLogic");
                    db2File.ReadUInt32("CurrQuestLogic");
                    db2File.ReadUInt32("CurrentCompletedQuestLogic");
                    db2File.ReadUInt32("SpellLogic");
                    db2File.ReadUInt32("ItemLogic");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Time", i);
                    db2File.ReadUInt32("AuraSpellLogic");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("AuraSpellID", i);
                    db2File.ReadUInt32("AchievementLogic");
                    db2File.ReadUInt32("AreaLogic");
                    db2File.ReadUInt32("QuestKillLogic");
                    db2File.ReadCString("FailureDescription");
                    db2File.ReadUInt16("MinLevel");
                    db2File.ReadUInt16("MaxLevel");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("SkillID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("MinSkill", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("MaxSkill", i);
                    db2File.ReadUInt16("MaxFactionID");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("PrevQuestID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("CurrQuestID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("CurrentCompletedQuestID", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt16("Explored", i);
                    db2File.ReadUInt16("WorldStateExpressionID");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("Achievement", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("AreaID", i);
                    db2File.ReadUInt16("QuestKillID");
                    db2File.ReadUInt16("PhaseID");
                    db2File.ReadUInt16("MinAvgEquippedItemLevel");
                    db2File.ReadUInt16("MaxAvgEquippedItemLevel");
                    db2File.ReadUInt16("ModifierTreeID");
                    db2File.ReadByte("Flags");
                    db2File.ReadSByte("Gender");
                    db2File.ReadSByte("NativeGender");
                    db2File.ReadByte("MinLanguage");
                    db2File.ReadByte("MaxLanguage");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadByte("MinReputation", i);
                    db2File.ReadByte("MaxReputation");
                    db2File.ReadByte("Unknown1");
                    db2File.ReadByte("MinPVPRank");
                    db2File.ReadByte("MaxPVPRank");
                    db2File.ReadByte("PvpMedal");
                    db2File.ReadByte("ItemFlags");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadByte("AuraCount", i);
                    db2File.ReadByte("WeatherID");
                    db2File.ReadByte("PartyStatus");
                    db2File.ReadByte("LifetimeMaxPVPRank");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadByte("LfgStatus", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadByte("LfgCompare", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadByte("CurrencyCount", i);
                    db2File.ReadSByte("MinExpansionLevel");
                    db2File.ReadSByte("MaxExpansionLevel");
                    db2File.ReadSByte("MinExpansionTier");
                    db2File.ReadSByte("MaxExpansionTier");
                    db2File.ReadByte("MinGuildLevel");
                    db2File.ReadByte("MaxGuildLevel");
                    db2File.ReadByte("PhaseUseFlags");
                    db2File.ReadSByte("ChrSpecializationIndex");
                    db2File.ReadSByte("ChrSpecializationRole");
                    db2File.ReadSByte("PowerType");
                    db2File.ReadSByte("PowerTypeComp");
                    db2File.ReadSByte("PowerTypeValue");
                    db2File.ReadUInt32("ClassMask");
                    db2File.ReadUInt32("LanguageID");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("MinFactionID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("SpellID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("ItemID", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("ItemCount", i);
                    db2File.ReadUInt32("LfgLogic");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("LfgValue", i);
                    db2File.ReadUInt32("CurrencyLogic");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("CurrencyID", i);
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadUInt32("QuestKillMonster", i);
                    db2File.ReadUInt32("PhaseGroupID");
                    db2File.ReadUInt32("MinAvgItemLevel");
                    db2File.ReadUInt32("MaxAvgItemLevel");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Unknown700", i);
                    break;
                }
                case DB2Hash.Positioner:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.PositionerState:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    break;
                }
                case DB2Hash.PositionerStateEntry:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadInt32("10");
                    break;
                }
                case DB2Hash.PowerDisplay:
                {
                    db2File.ReadCString("GlobalStringBaseTag");
                    db2File.ReadByte("PowerType");
                    db2File.ReadByte("Red");
                    db2File.ReadByte("Green");
                    db2File.ReadByte("Blue");
                    break;
                }
                case DB2Hash.PowerType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    break;
                }
                case DB2Hash.PrestigeLevelInfo:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.PvpBracketTypes:
                {
                    db2File.ReadByte("0");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("1", i);
                    break;
                }
                case DB2Hash.PvpDifficulty:
                {
                    db2File.ReadUInt16("MapID");
                    db2File.ReadByte("BracketID");
                    db2File.ReadByte("MinLevel");
                    db2File.ReadByte("MaxLevel");
                    break;
                }
                case DB2Hash.PvpItem:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.PvpReward:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.PvpTalent:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    break;
                }
                case DB2Hash.PvpTalentUnlock:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.QuestFactionReward:
                {
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadInt16("QuestRewFactionValue", i);
                    break;
                }
                case DB2Hash.QuestFeedbackEffect:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    break;
                }
                case DB2Hash.QuestInfo:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.QuestLine:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.QuestLineXQuest:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.QuestMoneyReward:
                {
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadUInt32("Money", i);
                    break;
                }
                case DB2Hash.QuestObjective:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    break;
                }
                case DB2Hash.QuestPOIBlob:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.QuestPOIPoint:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.QuestPOIPointCliTask:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadInt32("6");
                    break;
                }
                case DB2Hash.QuestPackageItem:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt16("QuestPackageID");
                    db2File.ReadByte("ItemCount");
                    db2File.ReadByte("FilterType");
                    break;
                }
                case DB2Hash.QuestSort:
                {
                    db2File.ReadCString("SortName");
                    db2File.ReadByte("SortOrder");
                    break;
                }
                case DB2Hash.QuestV2:
                {
                    db2File.ReadUInt16("UniqueBitFlag");
                    break;
                }
                case DB2Hash.QuestV2CliTask:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt16("7", i);
                    db2File.ReadInt16("8");
                    db2File.ReadInt16("9");
                    db2File.ReadInt16("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    db2File.ReadByte("14");
                    db2File.ReadByte("15");
                    db2File.ReadByte("16");
                    db2File.ReadByte("17");
                    db2File.ReadByte("18");
                    db2File.ReadByte("19");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("21");
                    db2File.ReadInt32("22");
                    break;
                }
                case DB2Hash.QuestXP:
                {
                    for (int i = 0; i < 10; ++i)
                        db2File.ReadUInt16("Exp", i);
                    break;
                }
                case DB2Hash.RacialMounts:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.RandPropPoints:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt32("EpicPropertiesPoints", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt32("RarePropertiesPoints", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt32("UncommonPropertiesPoints", i);
                    break;
                }
                case DB2Hash.ResearchBranch:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.ResearchField:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.ResearchProject:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt32("2");
                    db2File.ReadCString("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("8");
                    break;
                }
                case DB2Hash.ResearchSite:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.Resistances:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.RewardPack:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.RewardPackXCurrencyType:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.RewardPackXItem:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.RibbonQuality:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.RulesetItemUpgrade:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadUInt16("ItemUpgradeID");
                    break;
                }
                case DB2Hash.ScalingStatDistribution:
                {
                    db2File.ReadUInt16("ItemLevelCurveID");
                    db2File.ReadUInt32("MinLevel");
                    db2File.ReadUInt32("MaxLevel");
                    break;
                }
                case DB2Hash.Scenario:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.ScenarioEventEntry:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.ScenarioStep:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadInt32("8");
                    break;
                }
                case DB2Hash.SceneScript:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    break;
                }
                case DB2Hash.SceneScriptPackage:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SceneScriptPackageMember:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.ScheduledInterval:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.ScheduledWorldState:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    break;
                }
                case DB2Hash.ScheduledWorldStateGroup:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    break;
                }
                case DB2Hash.ScheduledWorldStateXUniqCat:
                {
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.ScreenEffect:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("1", i);
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    break;
                }
                case DB2Hash.ScreenLocation:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SeamlessSite:
                {
                    db2File.ReadInt32("0");
                    break;
                }
                case DB2Hash.ServerMessages:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.ShadowyEffect:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    break;
                }
                case DB2Hash.SkillLine:
                {
                    db2File.ReadCString("DisplayName");
                    db2File.ReadCString("Description");
                    db2File.ReadCString("AlternateVerb");
                    db2File.ReadUInt16("SpellIconID");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadByte("CategoryID");
                    db2File.ReadByte("CanLink");
                    db2File.ReadUInt32("ParentSkillLineID");
                    break;
                }
                case DB2Hash.SkillLineAbility:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("RaceMask");
                    db2File.ReadUInt32("SupercedesSpell");
                    db2File.ReadUInt32("Unknown703");
                    db2File.ReadUInt16("SkillLine");
                    db2File.ReadUInt16("MinSkillLineRank");
                    db2File.ReadUInt16("TrivialSkillLineRankHigh");
                    db2File.ReadUInt16("TrivialSkillLineRankLow");
                    db2File.ReadUInt16("UniqueBit");
                    db2File.ReadUInt16("TradeSkillCategoryID");
                    db2File.ReadByte("AcquireMethod");
                    db2File.ReadByte("NumSkillUps");
                    db2File.ReadUInt32("ClassMask");
                    break;
                }
                case DB2Hash.SkillRaceClassInfo:
                {
                    db2File.ReadInt32("RaceMask");
                    db2File.ReadUInt16("SkillID");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("SkillTierID");
                    db2File.ReadByte("Availability");
                    db2File.ReadByte("MinLevel");
                    db2File.ReadInt32("ClassMask");
                    break;
                }
                case DB2Hash.SoundAmbience:
                {
                    db2File.ReadByte("0");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("1", i);
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.SoundAmbienceFlavor:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.SoundBus:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SoundEmitterPillPoints:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.SoundEmitters:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadCString("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("11");
                    break;
                }
                case DB2Hash.SoundFilter:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SoundFilterElem:
                {
                    for (int i = 0; i < 9; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.SoundKit:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadSingle("VolumeFloat");
                    db2File.ReadSingle("MinDistance");
                    db2File.ReadSingle("DistanceCutoff");
                    db2File.ReadSingle("VolumeVariationPlus");
                    db2File.ReadSingle("VolumeVariationMinus");
                    db2File.ReadSingle("PitchVariationPlus");
                    db2File.ReadSingle("PitchVariationMinus");
                    db2File.ReadSingle("PitchAdjust");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("SoundEntriesAdvancedID");
                    db2File.ReadUInt16("BusOverwriteID");
                    db2File.ReadByte("SoundType");
                    db2File.ReadByte("EAXDef");
                    db2File.ReadByte("DialogType");
                    db2File.ReadByte("Unk700");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SoundKitAdvanced:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadSingle("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    db2File.ReadSingle("14");
                    db2File.ReadSingle("15");
                    db2File.ReadSingle("16");
                    db2File.ReadSingle("17");
                    db2File.ReadSingle("18");
                    db2File.ReadSingle("19");
                    db2File.ReadInt32("20");
                    db2File.ReadInt16("21");
                    db2File.ReadByte("22");
                    db2File.ReadByte("23");
                    db2File.ReadByte("24");
                    db2File.ReadByte("25");
                    db2File.ReadInt32("26");
                    db2File.ReadInt32("27");
                    db2File.ReadInt32("28");
                    db2File.ReadInt32("29");
                    db2File.ReadInt32("30");
                    db2File.ReadInt32("31");
                    break;
                }
                case DB2Hash.SoundKitChild:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.SoundKitEntry:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadByte("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.SoundKitFallback:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.SoundOverride:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.SoundProviderPreferences:
                {
                    db2File.ReadCString("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadSingle("11");
                    db2File.ReadSingle("12");
                    db2File.ReadSingle("13");
                    db2File.ReadSingle("14");
                    db2File.ReadSingle("15");
                    db2File.ReadInt16("16");
                    db2File.ReadInt16("17");
                    db2File.ReadInt16("18");
                    db2File.ReadInt16("19");
                    db2File.ReadInt16("20");
                    db2File.ReadByte("21");
                    db2File.ReadByte("22");
                    break;
                }
                case DB2Hash.SourceInfo:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.SpamMessages:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SpecializationSpells:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("OverridesSpellID");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt16("SpecID");
                    db2File.ReadByte("OrderIndex");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.Spell:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadCString("NameSubtext");
                    db2File.ReadCString("Description");
                    db2File.ReadCString("AuraDescription");
                    db2File.ReadUInt32("MiscID");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("DescriptionVariablesID");
                    break;
                }
                case DB2Hash.SpellActionBarPref:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.SpellActivationOverlay:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadSingle("3");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadInt32("4", i);
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadInt32("7");
                    break;
                }
                case DB2Hash.SpellAuraOptions:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("ProcCharges");
                    db2File.ReadUInt32("ProcTypeMask");
                    db2File.ReadUInt32("ProcCategoryRecovery");
                    db2File.ReadUInt16("CumulativeAura");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("ProcChance");
                    db2File.ReadByte("SpellProcsPerMinuteID");
                    break;
                }
                case DB2Hash.SpellAuraRestrictions:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("CasterAuraSpell");
                    db2File.ReadUInt32("TargetAuraSpell");
                    db2File.ReadUInt32("ExcludeCasterAuraSpell");
                    db2File.ReadUInt32("ExcludeTargetAuraSpell");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("CasterAuraState");
                    db2File.ReadByte("TargetAuraState");
                    db2File.ReadByte("ExcludeCasterAuraState");
                    db2File.ReadByte("ExcludeTargetAuraState");
                    break;
                }
                case DB2Hash.SpellAuraVisXChrSpec:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.SpellAuraVisibility:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SpellCastTimes:
                {
                    db2File.ReadInt32("CastTime");
                    db2File.ReadInt32("MinCastTime");
                    db2File.ReadInt16("CastTimePerLevel");
                    break;
                }
                case DB2Hash.SpellCastingRequirements:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt16("MinFactionID");
                    db2File.ReadUInt16("RequiredAreasID");
                    db2File.ReadUInt16("RequiresSpellFocus");
                    db2File.ReadByte("FacingCasterFlags");
                    db2File.ReadByte("MinReputation");
                    db2File.ReadByte("RequiredAuraVision");
                    break;
                }
                case DB2Hash.SpellCategories:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt16("Category");
                    db2File.ReadUInt16("StartRecoveryCategory");
                    db2File.ReadUInt16("ChargeCategory");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("DefenseType");
                    db2File.ReadByte("DispelType");
                    db2File.ReadByte("Mechanic");
                    db2File.ReadByte("PreventionType");
                    break;
                }
                case DB2Hash.SpellCategory:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadInt32("ChargeRecoveryTime");
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("UsesPerWeek");
                    db2File.ReadByte("MaxCharges");
                    db2File.ReadUInt32("Unk703");
                    break;
                }
                case DB2Hash.SpellChainEffects:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadSingle("11");
                    db2File.ReadSingle("12");
                    db2File.ReadSingle("13");
                    db2File.ReadSingle("14");
                    db2File.ReadSingle("15");
                    db2File.ReadSingle("16");
                    db2File.ReadSingle("17");
                    db2File.ReadSingle("18");
                    db2File.ReadSingle("19");
                    db2File.ReadSingle("20");
                    db2File.ReadSingle("21");
                    db2File.ReadSingle("22");
                    db2File.ReadSingle("23");
                    db2File.ReadSingle("24");
                    db2File.ReadSingle("25");
                    db2File.ReadSingle("26");
                    db2File.ReadSingle("27");
                    db2File.ReadSingle("28");
                    db2File.ReadSingle("29");
                    db2File.ReadSingle("30");
                    db2File.ReadSingle("31");
                    db2File.ReadSingle("32");
                    db2File.ReadSingle("33");
                    db2File.ReadSingle("34");
                    db2File.ReadSingle("35");
                    db2File.ReadSingle("36");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("37", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("38", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("39", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("40", i);
                    db2File.ReadInt32("41");
                    db2File.ReadSingle("42");
                    db2File.ReadSingle("43");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadCString("44", i);
                    db2File.ReadCString("45");
                    db2File.ReadInt16("46");
                    db2File.ReadInt16("47");
                    for (int i = 0; i < 11; ++i)
                        db2File.ReadInt16("48", i);
                    db2File.ReadInt16("49");
                    db2File.ReadByte("50");
                    db2File.ReadByte("51");
                    db2File.ReadByte("52");
                    db2File.ReadByte("53");
                    db2File.ReadByte("54");
                    db2File.ReadByte("55");
                    db2File.ReadByte("56");
                    db2File.ReadByte("57");
                    db2File.ReadByte("58");
                    db2File.ReadByte("59");
                    db2File.ReadByte("60");
                    db2File.ReadInt32("61");
                    break;
                }
                case DB2Hash.SpellClassOptions:
                {
                    db2File.ReadUInt32("SpellID");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("SpellClassMask", i);
                    db2File.ReadByte("SpellClassSet");
                    db2File.ReadUInt32("ModalNextSpell");
                    break;
                }
                case DB2Hash.SpellCooldowns:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("CategoryRecoveryTime");
                    db2File.ReadUInt32("RecoveryTime");
                    db2File.ReadUInt32("StartRecoveryTime");
                    db2File.ReadByte("DifficultyID");
                    break;
                }
                case DB2Hash.SpellDescriptionVariables:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SpellDispelType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.SpellDuration:
                {
                    db2File.ReadInt32("Duration");
                    db2File.ReadInt32("MaxDuration");
                    db2File.ReadInt16("DurationPerLevel");
                    break;
                }
                case DB2Hash.SpellEffect:
                {
                    db2File.ReadSingle("EffectAmplitude");
                    db2File.ReadSingle("EffectBonusCoefficient");
                    db2File.ReadSingle("EffectChainAmplitude");
                    db2File.ReadSingle("EffectPointsPerResource");
                    db2File.ReadSingle("EffectRealPointsPerLevel");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("EffectSpellClassMask", i);
                    db2File.ReadSingle("EffectPosFacing");
                    db2File.ReadSingle("BonusCoefficientFromAP");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("DifficultyID");
                    db2File.ReadUInt32("Effect");
                    db2File.ReadUInt32("EffectAura");
                    db2File.ReadUInt32("EffectAuraPeriod");
                    db2File.ReadUInt32("EffectBasePoints");
                    db2File.ReadUInt32("EffectChainTargets");
                    db2File.ReadUInt32("EffectDieSides");
                    db2File.ReadUInt32("EffectItemType");
                    db2File.ReadUInt32("EffectMechanic");
                    db2File.ReadInt32("EffectMiscValueA");
                    db2File.ReadInt32("EffectMiscValueB");
                    db2File.ReadUInt32("EffectRadiusIndex");
                    db2File.ReadUInt32("EffectRadiusMaxIndex");
                        db2File.ReadUInt32("EffectTriggerSpell");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("ImplicitTarget", i);
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("EffectIndex");
                    db2File.ReadUInt32("EffectAttributes");
                    break;
                }
                case DB2Hash.SpellEffectCameraShakes:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt16("0", i);
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.SpellEffectEmission:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.SpellEffectGroupSize:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    break;
                }
                case DB2Hash.SpellEffectScaling:
                {
                    db2File.ReadSingle("Coefficient");
                    db2File.ReadSingle("Variance");
                    db2File.ReadSingle("ResourceCoefficient");
                    db2File.ReadUInt32("SpellEffectID");
                    break;
                }
                case DB2Hash.SpellEquippedItems:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadInt32("EquippedItemInventoryTypeMask");
                    db2File.ReadInt32("EquippedItemSubClassMask");
                    db2File.ReadSByte("EquippedItemClass");
                    break;
                }
                case DB2Hash.SpellFlyout:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.SpellFlyoutItem:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.SpellFocusObject:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SpellIcon:
                {
                    db2File.ReadCString("Name");
                    break;
                }
                case DB2Hash.SpellInterrupts:
                {
                    db2File.ReadUInt32("SpellID");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("AuraInterruptFlags", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("ChannelInterruptFlags", i);
                    db2File.ReadUInt16("InterruptFlags");
                    db2File.ReadByte("DifficultyID");
                    break;
                }
                case DB2Hash.SpellItemEnchantment:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("EffectSpellID", i);
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("EffectScalingPoints", i);
                    db2File.ReadUInt32("TransmogCost");
                    db2File.ReadUInt32("TextureFileDataID");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt16("EffectPointsMin", i);
                    db2File.ReadUInt16("ItemVisual");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadUInt16("RequiredSkillID");
                    db2File.ReadUInt16("RequiredSkillRank");
                    db2File.ReadUInt16("ItemLevel");
                    db2File.ReadByte("Charges");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadByte("Effect", i);
                    db2File.ReadByte("ConditionID");
                    db2File.ReadByte("MinLevel");
                    db2File.ReadByte("MaxLevel");
                    db2File.ReadSByte("ScalingClass");
                    db2File.ReadSByte("ScalingClassRestricted");
                    db2File.ReadUInt32("PlayerConditionID");
                    break;
                }
                case DB2Hash.SpellItemEnchantmentCondition:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadByte("LTOperandType", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadByte("Operator", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadByte("RTOperandType", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadByte("RTOperand", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadByte("Logic", i);
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadUInt32("LTOperand", i);
                    break;
                }
                case DB2Hash.SpellKeyboundOverride:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.SpellLabel:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.SpellLearnSpell:
                {
                    db2File.ReadUInt32("LearnSpellID");
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("OverridesSpellID");
                    break;
                }
                case DB2Hash.SpellLevels:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt16("BaseLevel");
                    db2File.ReadUInt16("MaxLevel");
                    db2File.ReadUInt16("SpellLevel");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("MaxUsableLevel");
                    break;
                }
                case DB2Hash.SpellMechanic:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SpellMisc:
                {
                    db2File.ReadUInt32("Attributes");
                    db2File.ReadUInt32("AttributesEx");
                    db2File.ReadUInt32("AttributesExB");
                    db2File.ReadUInt32("AttributesExC");
                    db2File.ReadUInt32("AttributesExD");
                    db2File.ReadUInt32("AttributesExE");
                    db2File.ReadUInt32("AttributesExF");
                    db2File.ReadUInt32("AttributesExG");
                    db2File.ReadUInt32("AttributesExH");
                    db2File.ReadUInt32("AttributesExI");
                    db2File.ReadUInt32("AttributesExJ");
                    db2File.ReadUInt32("AttributesExK");
                    db2File.ReadUInt32("AttributesExL");
                    db2File.ReadUInt32("AttributesExM");
                    db2File.ReadSingle("Speed");
                    db2File.ReadSingle("MultistrikeSpeedMod");
                    db2File.ReadUInt16("CastingTimeIndex");
                    db2File.ReadUInt16("DurationIndex");
                    db2File.ReadUInt16("RangeIndex");
                    db2File.ReadUInt16("SpellIconID");
                    db2File.ReadUInt16("ActiveIconID7");
                    db2File.ReadByte("SchoolMask");
                    break;
                }
                case DB2Hash.SpellMiscDifficulty:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SpellMissile:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadSingle("11");
                    db2File.ReadSingle("12");
                    db2File.ReadSingle("13");
                    db2File.ReadByte("14");
                    break;
                }
                case DB2Hash.SpellMissileMotion:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.SpellPower:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("ManaCost");
                    db2File.ReadSingle("ManaCostPercentage");
                    db2File.ReadSingle("ManaCostPercentagePerSecond");
                    db2File.ReadUInt32("RequiredAura");
                    db2File.ReadSingle("HealthCostPercentage");
                    db2File.ReadByte("PowerIndex");
                    db2File.ReadByte("PowerType");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("ManaCostPerLevel");
                    db2File.ReadUInt32("ManaCostPerSecond");
                    db2File.ReadUInt32("ManaCostAdditional");
                    db2File.ReadUInt32("PowerDisplayID");
                    db2File.ReadUInt32("UnitPowerBarID");
                    break;
                }
                case DB2Hash.SpellPowerDifficulty:
                {
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("PowerIndex");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SpellProceduralEffect:
                {
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadByte("1");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SpellProcsPerMinute:
                {
                    db2File.ReadSingle("BaseProcRate");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.SpellProcsPerMinuteMod:
                {
                    db2File.ReadSingle("Coeff");
                    db2File.ReadUInt16("Param");
                    db2File.ReadByte("Type");
                    db2File.ReadByte("SpellProcsPerMinuteID");
                    break;
                }
                case DB2Hash.SpellRadius:
                {
                    db2File.ReadSingle("Radius");
                    db2File.ReadSingle("RadiusPerLevel");
                    db2File.ReadSingle("RadiusMin");
                    db2File.ReadSingle("RadiusMax");
                    break;
                }
                case DB2Hash.SpellRange:
                {
                    db2File.ReadSingle("MinRangeHostile");
                    db2File.ReadSingle("MinRangeFriend");
                    db2File.ReadSingle("MaxRangeHostile");
                    db2File.ReadSingle("MaxRangeFriend");
                    db2File.ReadCString("DisplayName");
                    db2File.ReadCString("DisplayNameShort");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.SpellReagents:
                {
                    db2File.ReadUInt32("SpellID");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadInt32("Reagent", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("ReagentCount", i);
                    break;
                }
                case DB2Hash.SpellReagentsCurrency:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.SpellScaling:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt16("ScalesFromItemLevel");
                    db2File.ReadInt32("ScalingClass");
                    db2File.ReadUInt32("MinScalingLevel");
                    db2File.ReadUInt32("MaxScalingLevel");
                    break;
                }
                case DB2Hash.SpellShapeshift:
                {
                    db2File.ReadUInt32("SpellID");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("ShapeshiftExclude", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("ShapeshiftMask", i);
                    db2File.ReadByte("StanceBarOrder");
                    break;
                }
                case DB2Hash.SpellShapeshiftForm:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadSingle("WeaponDamageVariance");
                    db2File.ReadUInt32("Flags");
                    db2File.ReadUInt16("AttackIconID");
                    db2File.ReadUInt16("CombatRoundTime");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt16("CreatureDisplayID", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("PresetSpellID", i);
                    db2File.ReadUInt16("MountTypeID");
                    db2File.ReadSByte("CreatureType");
                    db2File.ReadByte("BonusActionBar");
                    break;
                }
                case DB2Hash.SpellSpecialUnitEffect:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt32("1");
                    break;
                }
                case DB2Hash.SpellTargetRestrictions:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadSingle("ConeAngle");
                    db2File.ReadSingle("Width");
                    db2File.ReadUInt32("Targets");
                    db2File.ReadUInt16("TargetCreatureType");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("MaxAffectedTargets");
                    db2File.ReadUInt32("MaxTargetLevel");
                    break;
                }
                case DB2Hash.SpellTotems:
                {
                    db2File.ReadUInt32("SpellID");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("Totem", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt16("RequiredTotemCategoryID", i);
                    break;
                }
                case DB2Hash.SpellVisual:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadInt32("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    db2File.ReadInt32("13");
                    db2File.ReadInt32("14");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("15", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("16", i);
                    db2File.ReadInt32("17");
                    db2File.ReadInt32("18");
                    db2File.ReadInt16("19");
                    db2File.ReadInt16("20");
                    db2File.ReadInt16("21");
                    db2File.ReadByte("22");
                    db2File.ReadByte("23");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("25");
                    db2File.ReadInt32("26");
                    db2File.ReadInt32("27");
                    break;
                }
                case DB2Hash.SpellVisualAnim:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.SpellVisualColorEffect:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadInt32("1");
                    db2File.ReadSingle("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadInt32("10");
                    break;
                }
                case DB2Hash.SpellVisualEffectName:
                {
                    db2File.ReadCString("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadInt32("7");
                    db2File.ReadInt32("8");
                    db2File.ReadInt32("9");
                    db2File.ReadByte("10");
                    db2File.ReadInt32("11");
                    db2File.ReadInt32("12");
                    break;
                }
                case DB2Hash.SpellVisualKit:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("5");
                    break;
                }
                case DB2Hash.SpellVisualKitAreaModel:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    break;
                }
                case DB2Hash.SpellVisualKitEffect:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.SpellVisualKitModelAttach:
                {
                    db2File.ReadInt32("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("2", i);
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    db2File.ReadSingle("10");
                    db2File.ReadInt32("11");
                    db2File.ReadSingle("12");
                    db2File.ReadInt16("13");
                    db2File.ReadInt16("14");
                    db2File.ReadInt16("15");
                    db2File.ReadInt16("16");
                    db2File.ReadInt16("17");
                    db2File.ReadInt16("18");
                    db2File.ReadByte("19");
                    db2File.ReadByte("20");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.SpellVisualMissile:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("3", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("4", i);
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadInt16("8");
                    db2File.ReadInt16("9");
                    db2File.ReadInt16("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("14");
                    db2File.ReadInt32("15");
                    break;
                }
                case DB2Hash.SpellXSpellVisual:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadSingle("Unk620");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt16("SpellVisualID", i);
                    db2File.ReadUInt16("PlayerConditionID");
                    db2File.ReadByte("DifficultyID");
                    db2File.ReadByte("Flags");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.Startup_Strings:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    break;
                }
                case DB2Hash.Stationery:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.StringLookups:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.SummonProperties:
                {
                    db2File.ReadUInt32("Category");
                    db2File.ReadUInt32("Faction");
                    db2File.ReadUInt32("Type");
                    db2File.ReadInt32("Slot");
                    db2File.ReadUInt32("Flags");
                    break;
                }
                case DB2Hash.TactKey:
                {
                    for (int i = 0; i < 16; ++i)
                        db2File.ReadByte("0", i);
                    break;
                }
                case DB2Hash.TactKeyLookup:
                {
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadByte("0", i);
                    break;
                }
                case DB2Hash.Talent:
                {
                    db2File.ReadUInt32("SpellID");
                    db2File.ReadUInt32("OverridesSpellID");
                    db2File.ReadCString("Description");
                    db2File.ReadUInt16("SpecID");
                    db2File.ReadByte("TierID");
                    db2File.ReadByte("ColumnIndex");
                    db2File.ReadByte("Flags");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadByte("CategoryMask", i);
                    db2File.ReadByte("ClassID");
                    break;
                }
                case DB2Hash.TaxiNodes:
                {
                    db2File.ReadVector3("Pos");
                    db2File.ReadCString("Name");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadUInt32("MountCreatureID", i);
                    db2File.ReadVector2("MapOffset");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("ConditionID");
                    db2File.ReadUInt16("LearnableIndex");
                    db2File.ReadByte("Flags");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.TaxiPath:
                {
                    db2File.ReadUInt16("From");
                    db2File.ReadUInt16("To");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("Cost");
                    break;
                }
                case DB2Hash.TaxiPathNode:
                {
                    db2File.ReadVector3("Loc");
                    db2File.ReadUInt32("Delay");
                    db2File.ReadUInt16("PathID");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("ArrivalEventID");
                    db2File.ReadUInt16("DepartureEventID");
                    db2File.ReadByte("NodeIndex");
                    db2File.ReadByte("Flags");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.TerrainMaterial:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.TerrainType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.TerrainTypeSounds:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.TextureBlendSet:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("0", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("2", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("3", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("4", i);
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadSingle("5", i);
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    break;
                }
                case DB2Hash.TextureFileData:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadByte("1");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.TotemCategory:
                {
                    db2File.ReadCString("Name");
                    db2File.ReadUInt32("CategoryMask");
                    db2File.ReadByte("CategoryType");
                    break;
                }
                case DB2Hash.Toy:
                {
                    db2File.ReadUInt32("ItemID");
                    db2File.ReadCString("Description");
                    db2File.ReadByte("Flags");
                    db2File.ReadByte("CategoryFilter");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.TradeSkillCategory:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.TradeSkillItem:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.TransformMatrix:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    break;
                }
                case DB2Hash.TransmogSet:
                {
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.TransmogSetItem:
                {
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.TransportAnimation:
                {
                    db2File.ReadUInt32("TransportID");
                    db2File.ReadUInt32("TimeIndex");
                    db2File.ReadVector3("Pos");
                    db2File.ReadByte("SequenceID");
                    break;
                }
                case DB2Hash.TransportPhysics:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadSingle("9");
                    break;
                }
                case DB2Hash.TransportRotation:
                {
                    db2File.ReadUInt32("TransportID");
                    db2File.ReadUInt32("TimeIndex");
                    db2File.ReadSingle("X");
                    db2File.ReadSingle("Y");
                    db2File.ReadSingle("Z");
                    db2File.ReadSingle("W");
                    break;
                }
                case DB2Hash.Trophy:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.UiCamFbackTransmogChrRace:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.UiCamFbackTransmogWeapon:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.UiCamera:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("2", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("3", i);
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadInt32("8");
                    break;
                }
                case DB2Hash.UiCameraType:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt32("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.UiMapPOI:
                {
                    db2File.ReadInt32("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadInt32("2");
                    db2File.ReadInt32("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.UiTextureAtlas:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.UiTextureAtlasMember:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    break;
                }
                case DB2Hash.UiTextureKit:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.UnitBlood:
                {
                    for (int i = 0; i < 5; ++i)
                        db2File.ReadCString("0", i);
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt32("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    break;
                }
                case DB2Hash.UnitBloodLevels:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadByte("0", i);
                    break;
                }
                case DB2Hash.UnitCondition:
                {
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadInt32("0", i);
                    db2File.ReadByte("1");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadByte("2", i);
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadByte("3", i);
                    break;
                }
                case DB2Hash.UnitPowerBar:
                {
                    db2File.ReadSingle("RegenerationPeace");
                    db2File.ReadSingle("RegenerationCombat");
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadUInt32("FileDataID", i);
                    for (int i = 0; i < 6; ++i)
                        db2File.ReadUInt32("Color", i);
                    db2File.ReadCString("Name");
                    db2File.ReadCString("Cost");
                    db2File.ReadCString("OutOfError");
                    db2File.ReadCString("ToolTip");
                    db2File.ReadSingle("StartInset");
                    db2File.ReadSingle("EndInset");
                    db2File.ReadUInt16("StartPower");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadByte("CenterPower");
                    db2File.ReadByte("BarType");
                    db2File.ReadUInt32("MinPower");
                    db2File.ReadUInt32("MaxPower");
                    break;
                }
                case DB2Hash.Vehicle:
                {
                    db2File.ReadUInt32("Flags");
                    db2File.ReadSingle("TurnSpeed");
                    db2File.ReadSingle("PitchSpeed");
                    db2File.ReadSingle("PitchMin");
                    db2File.ReadSingle("PitchMax");
                    db2File.ReadSingle("MouseLookOffsetPitch");
                    db2File.ReadSingle("CameraFadeDistScalarMin");
                    db2File.ReadSingle("CameraFadeDistScalarMax");
                    db2File.ReadSingle("CameraPitchOffset");
                    db2File.ReadSingle("FacingLimitRight");
                    db2File.ReadSingle("FacingLimitLeft");
                    db2File.ReadSingle("MsslTrgtTurnLingering");
                    db2File.ReadSingle("MsslTrgtPitchLingering");
                    db2File.ReadSingle("MsslTrgtMouseLingering");
                    db2File.ReadSingle("MsslTrgtEndOpacity");
                    db2File.ReadSingle("MsslTrgtArcSpeed");
                    db2File.ReadSingle("MsslTrgtArcRepeat");
                    db2File.ReadSingle("MsslTrgtArcWidth");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("MsslTrgtImpactRadius", i);
                    db2File.ReadCString("MsslTrgtArcTexture");
                    db2File.ReadCString("MsslTrgtImpactTexture");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadCString("MsslTrgtImpactModel", i);
                    db2File.ReadSingle("CameraYawOffset");
                    db2File.ReadSingle("MsslTrgtImpactTexRadius");
                    for (int i = 0; i < 8; ++i)
                        db2File.ReadUInt16("SeatID", i);
                    db2File.ReadUInt16("VehicleUIIndicatorID");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt16("PowerDisplayID", i);
                    db2File.ReadByte("FlagsB");
                    db2File.ReadByte("UILocomotionType");
                    break;
                }
                case DB2Hash.VehicleSeat:
                {
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadUInt32("Flags", i);
                    db2File.ReadVector3("AttachmentOffset");
                    db2File.ReadSingle("EnterPreDelay");
                    db2File.ReadSingle("EnterSpeed");
                    db2File.ReadSingle("EnterGravity");
                    db2File.ReadSingle("EnterMinDuration");
                    db2File.ReadSingle("EnterMaxDuration");
                    db2File.ReadSingle("EnterMinArcHeight");
                    db2File.ReadSingle("EnterMaxArcHeight");
                    db2File.ReadSingle("ExitPreDelay");
                    db2File.ReadSingle("ExitSpeed");
                    db2File.ReadSingle("ExitGravity");
                    db2File.ReadSingle("ExitMinDuration");
                    db2File.ReadSingle("ExitMaxDuration");
                    db2File.ReadSingle("ExitMinArcHeight");
                    db2File.ReadSingle("ExitMaxArcHeight");
                    db2File.ReadSingle("PassengerYaw");
                    db2File.ReadSingle("PassengerPitch");
                    db2File.ReadSingle("PassengerRoll");
                    db2File.ReadSingle("VehicleEnterAnimDelay");
                    db2File.ReadSingle("VehicleExitAnimDelay");
                    db2File.ReadSingle("CameraEnteringDelay");
                    db2File.ReadSingle("CameraEnteringDuration");
                    db2File.ReadSingle("CameraExitingDelay");
                    db2File.ReadSingle("CameraExitingDuration");
                    db2File.ReadVector3("CameraOffset");
                    db2File.ReadSingle("CameraPosChaseRate");
                    db2File.ReadSingle("CameraFacingChaseRate");
                    db2File.ReadSingle("CameraEnteringZoom");
                    db2File.ReadSingle("CameraSeatZoomMin");
                    db2File.ReadSingle("CameraSeatZoomMax");
                    db2File.ReadUInt32("UISkinFileDataID");
                    db2File.ReadInt16("EnterAnimStart");
                    db2File.ReadInt16("EnterAnimLoop");
                    db2File.ReadInt16("RideAnimStart");
                    db2File.ReadInt16("RideAnimLoop");
                    db2File.ReadInt16("RideUpperAnimStart");
                    db2File.ReadInt16("RideUpperAnimLoop");
                    db2File.ReadInt16("ExitAnimStart");
                    db2File.ReadInt16("ExitAnimLoop");
                    db2File.ReadInt16("ExitAnimEnd");
                    db2File.ReadInt16("VehicleEnterAnim");
                    db2File.ReadInt16("VehicleExitAnim");
                    db2File.ReadInt16("VehicleRideAnimLoop");
                    db2File.ReadUInt16("EnterAnimKitID");
                    db2File.ReadUInt16("RideAnimKitID");
                    db2File.ReadUInt16("ExitAnimKitID");
                    db2File.ReadUInt16("VehicleEnterAnimKitID");
                    db2File.ReadUInt16("VehicleRideAnimKitID");
                    db2File.ReadUInt16("VehicleExitAnimKitID");
                    db2File.ReadUInt16("CameraModeID");
                    db2File.ReadSByte("AttachmentID");
                    db2File.ReadSByte("PassengerAttachmentID");
                    db2File.ReadSByte("VehicleEnterAnimBone");
                    db2File.ReadSByte("VehicleExitAnimBone");
                    db2File.ReadSByte("VehicleRideAnimLoopBone");
                    db2File.ReadByte("VehicleAbilityDisplay");
                    db2File.ReadUInt32("EnterUISoundID");
                    db2File.ReadUInt32("ExitUISoundID");
                    break;
                }
                case DB2Hash.VehicleUIIndSeat:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.VehicleUIIndicator:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.VideoHardware:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    db2File.ReadEntry("ID");
                    db2File.ReadInt32("15");
                    db2File.ReadInt32("16");
                    db2File.ReadInt32("17");
                    db2File.ReadInt32("18");
                    db2File.ReadInt32("19");
                    db2File.ReadInt32("20");
                    db2File.ReadInt32("21");
                    break;
                }
                case DB2Hash.Vignette:
                {
                    db2File.ReadCString("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadInt32("5");
                    db2File.ReadInt32("6");
                    break;
                }
                case DB2Hash.VocalUISounds:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("3", i);
                    break;
                }
                case DB2Hash.WMOAreaTable:
                {
                    db2File.ReadInt32("WMOGroupID");
                    db2File.ReadCString("AreaName");
                    db2File.ReadInt16("WMOID");
                    db2File.ReadUInt16("AmbienceID");
                    db2File.ReadUInt16("ZoneMusic");
                    db2File.ReadUInt16("IntroSound");
                    db2File.ReadUInt16("AreaTableID");
                    db2File.ReadUInt16("UWIntroSound");
                    db2File.ReadUInt16("UWAmbience");
                    db2File.ReadSByte("NameSet");
                    db2File.ReadByte("SoundProviderPref");
                    db2File.ReadByte("SoundProviderPrefUnderwater");
                    db2File.ReadByte("Flags");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("UWZoneMusic");
                    break;
                }
                case DB2Hash.WbAccessControlList:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.WbCertBlacklist:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 20; ++i)
                        db2File.ReadByte("1", i);
                    break;
                }
                case DB2Hash.WbCertWhitelist:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    break;
                }
                case DB2Hash.WbPermissions:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    break;
                }
                case DB2Hash.WeaponImpactSounds:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    for (int i = 0; i < 11; ++i)
                        db2File.ReadInt32("3", i);
                    for (int i = 0; i < 11; ++i)
                        db2File.ReadInt32("4", i);
                    for (int i = 0; i < 11; ++i)
                        db2File.ReadInt32("5", i);
                    for (int i = 0; i < 11; ++i)
                        db2File.ReadInt32("6", i);
                    break;
                }
                case DB2Hash.WeaponSwingSounds2:
                {
                    db2File.ReadByte("0");
                    db2File.ReadByte("1");
                    db2File.ReadInt32("2");
                    break;
                }
                case DB2Hash.WeaponTrail:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt32("4", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("5", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("6", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("7", i);
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("8", i);
                    break;
                }
                case DB2Hash.WeaponTrailModelDef:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.WeaponTrailParam:
                {
                    db2File.ReadSingle("0");
                    db2File.ReadSingle("1");
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    db2File.ReadSingle("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    break;
                }
                case DB2Hash.Weather:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadSingle("1");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("2", i);
                    db2File.ReadCString("3");
                    db2File.ReadSingle("4");
                    db2File.ReadSingle("5");
                    db2File.ReadSingle("6");
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadInt16("9");
                    db2File.ReadByte("10");
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadInt32("13");
                    break;
                }
                case DB2Hash.WindSettings:
                {
                    db2File.ReadSingle("0");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("1", i);
                    db2File.ReadSingle("2");
                    db2File.ReadSingle("3");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("4", i);
                    db2File.ReadSingle("5");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadSingle("6", i);
                    db2File.ReadSingle("7");
                    db2File.ReadSingle("8");
                    db2File.ReadByte("9");
                    break;
                }
                case DB2Hash.WmoMinimapTexture:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    break;
                }
                case DB2Hash.WorldBossLockout:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    break;
                }
                case DB2Hash.WorldChunkSounds:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    db2File.ReadByte("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    break;
                }
                case DB2Hash.WorldEffect:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadByte("4");
                    db2File.ReadByte("5");
                    break;
                }
                case DB2Hash.WorldElapsedTimer:
                {
                    db2File.ReadCString("0");
                    db2File.ReadByte("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.WorldMapArea:
                {
                    db2File.ReadCString("AreaName");
                    db2File.ReadSingle("LocLeft");
                    db2File.ReadSingle("LocRight");
                    db2File.ReadSingle("LocTop");
                    db2File.ReadSingle("LocBottom");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("AreaID");
                    db2File.ReadInt16("DisplayMapID");
                    db2File.ReadInt16("DefaultDungeonFloor");
                    db2File.ReadUInt16("ParentWorldMapID");
                    db2File.ReadUInt16("Flags");
                    db2File.ReadByte("LevelRangeMin");
                    db2File.ReadByte("LevelRangeMax");
                    db2File.ReadByte("BountySetID");
                    db2File.ReadByte("BountyBoardLocation");
                    db2File.ReadEntry("ID");
                    db2File.ReadUInt32("PlayerConditionID");
                    break;
                }
                case DB2Hash.WorldMapContinent:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadSingle("1");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("2", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("3", i);
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadByte("6");
                    db2File.ReadByte("7");
                    db2File.ReadByte("8");
                    db2File.ReadByte("9");
                    db2File.ReadByte("10");
                    break;
                }
                case DB2Hash.WorldMapOverlay:
                {
                    db2File.ReadCString("TextureName");
                    db2File.ReadUInt16("TextureWidth");
                    db2File.ReadUInt16("TextureHeight");
                    db2File.ReadUInt32("MapAreaID");
                    for (int i = 0; i < 4; ++i)
                        db2File.ReadUInt32("AreaID", i);
                    db2File.ReadUInt32("OffsetX");
                    db2File.ReadUInt32("OffsetY");
                    db2File.ReadUInt32("HitRectTop");
                    db2File.ReadUInt32("HitRectLeft");
                    db2File.ReadUInt32("HitRectBottom");
                    db2File.ReadUInt32("HitRectRight");
                    db2File.ReadUInt32("PlayerConditionID");
                    db2File.ReadUInt32("Flags");
                    break;
                }
                case DB2Hash.WorldMapTransforms:
                {
                    packet.ReadVector3("RegionMin");
                    packet.ReadVector3("RegionMax");
                    db2File.ReadVector3("RegionOffset");
                    db2File.ReadSingle("RegionScale");
                    db2File.ReadUInt16("MapID");
                    db2File.ReadUInt16("AreaID");
                    db2File.ReadUInt16("NewMapID");
                    db2File.ReadUInt16("NewDungeonMapID");
                    db2File.ReadUInt16("NewAreaID");
                    db2File.ReadByte("Flags");
                    break;
                }
                case DB2Hash.WorldStateExpression:
                {
                    db2File.ReadCString("0");
                    break;
                }
                case DB2Hash.WorldStateUI:
                {
                    db2File.ReadCString("0");
                    db2File.ReadCString("1");
                    db2File.ReadCString("2");
                    db2File.ReadCString("3");
                    db2File.ReadCString("4");
                    db2File.ReadCString("5");
                    db2File.ReadInt16("6");
                    db2File.ReadInt16("7");
                    db2File.ReadInt16("8");
                    db2File.ReadInt16("9");
                    for (int i = 0; i < 3; ++i)
                        db2File.ReadInt16("10", i);
                    db2File.ReadByte("11");
                    db2File.ReadByte("12");
                    db2File.ReadByte("13");
                    db2File.ReadByte("14");
                    db2File.ReadEntry("ID");
                    break;
                }
                case DB2Hash.WorldStateZoneSounds:
                {
                    db2File.ReadInt32("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadInt16("5");
                    db2File.ReadInt16("6");
                    db2File.ReadByte("7");
                    break;
                }
                case DB2Hash.World_PVP_Area:
                {
                    db2File.ReadInt16("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    db2File.ReadInt16("3");
                    db2File.ReadInt16("4");
                    db2File.ReadByte("5");
                    db2File.ReadByte("6");
                    break;
                }
                case DB2Hash.ZoneIntroMusicTable:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    db2File.ReadInt32("3");
                    break;
                }
                case DB2Hash.ZoneLight:
                {
                    db2File.ReadCString("0");
                    db2File.ReadInt16("1");
                    db2File.ReadInt16("2");
                    break;
                }
                case DB2Hash.ZoneLightPoint:
                {
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadSingle("0", i);
                    db2File.ReadInt16("1");
                    db2File.ReadByte("2");
                    break;
                }
                case DB2Hash.ZoneMusic:
                {
                    db2File.ReadCString("0");
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("1", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("2", i);
                    for (int i = 0; i < 2; ++i)
                        db2File.ReadInt32("3", i);
                    break;
                }
                default:
                {
                    db2File.AddValue("Unknown DB2 file type", string.Format("{0} (0x{0:x})", type));
                    for (int i = 0;; ++i)
                    {
                        if (db2File.Length - 4 >= db2File.Position)
                        {
                            UpdateField blockVal = db2File.ReadUpdateField();
                            string key = "Block Value " + i;
                            string value = blockVal.UInt32Value + "/" + blockVal.SingleValue;
                            packet.AddValue(key, value);
                        }
                        else
                        {
                            long left = db2File.Length - db2File.Position;
                            for (int j = 0; j < left; ++j)
                            {
                                string key = "Byte Value " + i;
                                byte value = db2File.ReadByte();
                                packet.AddValue(key, value);
                            }
                            break;
                        }
                    }
                    break;
                }
            }

            if (db2File.Length != db2File.Position)
            {
                packet.WriteLine(
                    $"Packet not fully read! Current position is {db2File.Position}, length is {db2File.Length}, and diff is {db2File.Length - db2File.Position}.");

                if (db2File.Length < 300) // If the packet isn't "too big" and it is not full read, print its hex table
                    packet.AsHex();

                packet.Status = ParsedStatus.WithErrors;
            }
        }
    }
}
