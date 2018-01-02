using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Hotfix;
using WowPacketParser.Parsing;

namespace WowPacketParser.Misc
{
    public static class ClientVersion
    {
        // Kept in sync with http://www.wowwiki.com/Public_client_builds
        private static readonly KeyValuePair<ClientVersionBuild, DateTime>[] ClientBuilds = {
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V1_12_1_5875, new DateTime(2006, 9, 26)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_0_1_6180, new DateTime(2006, 12, 5)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_0_3_6299, new DateTime(2007, 1, 9)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_0_6_6337, new DateTime(2007, 1, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_1_0_6692, new DateTime(2007, 5, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_1_1_6739, new DateTime(2007, 6, 5)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_1_2_6803, new DateTime(2007, 6, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_1_3_6898, new DateTime(2007, 7, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_2_0_7272, new DateTime(2007, 9, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_2_2_7318, new DateTime(2007, 10, 3)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_2_3_7359, new DateTime(2007, 10, 9)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_3_0_7561, new DateTime(2007, 11, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_3_2_7741, new DateTime(2008, 1, 8)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_3_3_7799, new DateTime(2008, 1, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_4_0_8089, new DateTime(2008, 3, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_4_1_8125, new DateTime(2008, 4, 1)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_4_2_8209, new DateTime(2008, 5, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V2_4_3_8606, new DateTime(2008, 7, 15)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_0_2_9056, new DateTime(2008, 10, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_0_3_9183, new DateTime(2008, 11, 4)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_0_8_9464, new DateTime(2009, 1, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_0_8a_9506, new DateTime(2009, 1, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_0_9_9551, new DateTime(2009, 2, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_1_0_9767, new DateTime(2009, 4, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_1_1_9806, new DateTime(2009, 4, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_1_1a_9835, new DateTime(2009, 4, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_1_2_9901, new DateTime(2009, 5, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_1_3_9947, new DateTime(2009, 6, 2)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_2_0_10192, new DateTime(2009, 8, 4)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_2_0a_10314, new DateTime(2009, 8, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_2_2_10482, new DateTime(2009, 9, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_2_2a_10505, new DateTime(2009, 9, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_0_10958, new DateTime(2009, 12, 2)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_0a_11159, new DateTime(2009, 12, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_3_11685, new DateTime(2010, 3, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_3a_11723, new DateTime(2010, 3, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_5_12213, new DateTime(2010, 6, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_5a_12340, new DateTime(2010, 6, 29)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_1_13164, new DateTime(2010, 10, 12)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_1a_13205, new DateTime(2010, 10, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_3_13329, new DateTime(2010, 11, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_6_13596, new DateTime(2011, 2, 8)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_6a_13623, new DateTime(2011, 2, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_1_0_13914, new DateTime(2011, 4, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_1_0a_14007, new DateTime(2011, 5, 5)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_2_0_14333, new DateTime(2011, 6, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_2_0a_14480, new DateTime(2011, 9, 8)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_2_2_14545, new DateTime(2011, 9, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_3_0_15005, new DateTime(2011, 11, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_3_0a_15050, new DateTime(2011, 12, 2)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_3_2_15211, new DateTime(2012, 1, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_3_3_15354, new DateTime(2012, 2, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_3_4_15595, new DateTime(2012, 4, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_4_16016, new DateTime(2012, 8, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_5_16048, new DateTime(2012, 9, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_5a_16057, new DateTime(2012, 9, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_5b_16135, new DateTime(2012, 10, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_1_0_16309, new DateTime(2012, 11, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_1_0a_16357, new DateTime(2012, 12, 3)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16650, new DateTime(2013, 02, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16669, new DateTime(2013, 03, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16683, new DateTime(2013, 03, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16685, new DateTime(2013, 03, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16701, new DateTime(2013, 03, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16709, new DateTime(2013, 03, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16716, new DateTime(2013, 03, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16733, new DateTime(2013, 03, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16769, new DateTime(2013, 03, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_2_0_16826, new DateTime(2013, 04, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_3_0_16981, new DateTime(2013, 05, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_3_0_16983, new DateTime(2013, 05, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_3_0_16992, new DateTime(2013, 05, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_3_0_17055, new DateTime(2013, 06, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_3_0_17116, new DateTime(2013, 06, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_3_0_17128, new DateTime(2013, 06, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_0_17359, new DateTime(2013, 09, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_0_17371, new DateTime(2013, 09, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_0_17399, new DateTime(2013, 09, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_1_17538, new DateTime(2013, 10, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_2_17658, new DateTime(2013, 12, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_2_17688, new DateTime(2013, 12, 12)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_7_17898, new DateTime(2014, 02, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_7_17930, new DateTime(2014, 02, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_7_17956, new DateTime(2014, 02, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_7_18019, new DateTime(2014, 03, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_8_18291, new DateTime(2014, 05, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_8_18414, new DateTime(2014, 06, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_4_8_18414, new DateTime(2014, 06, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_0_2_19033, new DateTime(2014, 10, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_0_2_19034, new DateTime(2014, 10, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_0_3_19103, new DateTime(2014, 10, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_0_3_19116, new DateTime(2014, 10, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_0_3_19243, new DateTime(2014, 11, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_0_3_19342, new DateTime(2014, 12, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_1_0_19678, new DateTime(2015, 02, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_1_0_19702, new DateTime(2015, 02, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_1_2_19802, new DateTime(2015, 03, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_1_2_19831, new DateTime(2015, 03, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_1_2_19865, new DateTime(2015, 04, 03)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_0_20173, new DateTime(2015, 06, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_0_20182, new DateTime(2015, 06, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_0_20201, new DateTime(2015, 06, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_0_20216, new DateTime(2015, 07, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_0_20253, new DateTime(2015, 07, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_0_20338, new DateTime(2015, 07, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_2_20444, new DateTime(2015, 09, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_2a_20490, new DateTime(2015, 09, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_2a_20574, new DateTime(2015, 10, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_3_20726, new DateTime(2015, 11, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_3_20779, new DateTime(2015, 12, 1)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_3_20886, new DateTime(2016, 01, 5)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21336, new DateTime(2016, 03, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21343, new DateTime(2016, 03, 22, 15, 03, 43)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21345, new DateTime(2016, 03, 22, 17, 41, 51)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21348, new DateTime(2016, 03, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21355, new DateTime(2016, 03, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21463, new DateTime(2016, 04, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21676, new DateTime(2016, 05, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V6_2_4_21742, new DateTime(2016, 05, 18, 18, 0, 0)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22248, new DateTime(2016, 07, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22280, new DateTime(2016, 07, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22289, new DateTime(2016, 07, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22293, new DateTime(2016, 07, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22345, new DateTime(2016, 08, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22396, new DateTime(2016, 08, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22410, new DateTime(2016, 08, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22423, new DateTime(2016, 08, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22445, new DateTime(2016, 08, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22498, new DateTime(2015, 08, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22522, new DateTime(2016, 08, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22566, new DateTime(2016, 09, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22594, new DateTime(2016, 09, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22624, new DateTime(2016, 09, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22747, new DateTime(2016, 10, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22810, new DateTime(2016, 10, 12)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_22900, new DateTime(2016, 10, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_22908, new DateTime(2016, 10, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_22950, new DateTime(2016, 11, 03)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_22989, new DateTime(2016, 11, 07)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_22995, new DateTime(2016, 11, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_22996, new DateTime(2016, 11, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_23171, new DateTime(2016, 12, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_0_23222, new DateTime(2016, 12, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_5_23360, new DateTime(2017, 01, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_1_5_23420, new DateTime(2017, 01, 18)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23826, new DateTime(2017, 03, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23835, new DateTime(2017, 03, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23836, new DateTime(2017, 03, 28, 18, 09, 34)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23846, new DateTime(2017, 03, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23852, new DateTime(2017, 03, 29, 23, 13, 43)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23857, new DateTime(2017, 03, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23877, new DateTime(2017, 04, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23911, new DateTime(2017, 04, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23937, new DateTime(2017, 04, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_24015, new DateTime(2017, 04, 27)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24330, new DateTime(2017, 06, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24367, new DateTime(2017, 06, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24414, new DateTime(2017, 06, 22, 21, 09, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24415, new DateTime(2017, 06, 22, 21, 41, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24430, new DateTime(2017, 06, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24461, new DateTime(2017, 06, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_5_24742, new DateTime(2017, 08, 03)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_24920, new DateTime(2017, 08, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_24931, new DateTime(2017, 08, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_24956, new DateTime(2017, 09, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_24970, new DateTime(2017, 09, 05, 16, 07, 47)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_24974, new DateTime(2017, 09, 05, 10, 39, 36)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_25021, new DateTime(2017, 09, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_0_25195, new DateTime(2017, 10, 02)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25383, new DateTime(2017, 10, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25442, new DateTime(2017, 11, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25455, new DateTime(2017, 11, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25477, new DateTime(2017, 11, 09, 12, 53, 41)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25480, new DateTime(2017, 11, 09, 15, 38, 45)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25497, new DateTime(2017, 11, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25549, new DateTime(2017, 11, 22))
        };

        private static ClientType _expansion;

        public static ClientVersionBuild Build { get; private set; }

        // Returns the build that will define opcodes/updatefields/handlers for given Build
        public static ClientVersionBuild VersionDefiningBuild
        {
            get
            {
                switch (Build)
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
                    case ClientVersionBuild.V3_0_2_9056:
                    case ClientVersionBuild.V3_0_3_9183:
                    case ClientVersionBuild.V3_0_8_9464:
                    case ClientVersionBuild.V3_0_8a_9506:
                    case ClientVersionBuild.V3_0_9_9551:
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
                        return ClientVersionBuild.V3_3_0a_11159;
                    case ClientVersionBuild.V3_3_3_11685:
                    case ClientVersionBuild.V3_3_3a_11723:
                    case ClientVersionBuild.V3_3_5_12213:
                    case ClientVersionBuild.V3_3_5a_12340:
                        return ClientVersionBuild.V3_3_5a_12340;
                    case ClientVersionBuild.V4_0_1_13164:
                    case ClientVersionBuild.V4_0_1a_13205:
                    case ClientVersionBuild.V4_0_3_13329:
                    case ClientVersionBuild.V4_0_6_13596:
                    case ClientVersionBuild.V4_0_6a_13623:
                    case ClientVersionBuild.V4_1_0_13914:
                    case ClientVersionBuild.V4_1_0a_14007:
                        return ClientVersionBuild.V4_0_6_13596;
                    case ClientVersionBuild.V4_2_0_14333:
                    case ClientVersionBuild.V4_2_0a_14480:
                    case ClientVersionBuild.V4_2_2_14545:
                        return ClientVersionBuild.V4_2_0_14333;
                    case ClientVersionBuild.V4_3_0_15005:
                    case ClientVersionBuild.V4_3_0a_15050:
                    case ClientVersionBuild.V4_3_2_15211:
                    case ClientVersionBuild.V4_3_3_15354:
                    case ClientVersionBuild.V4_3_4_15595:
                        return ClientVersionBuild.V4_3_4_15595;
                    case ClientVersionBuild.V5_0_4_16016:
                    case ClientVersionBuild.V5_0_5_16048:
                    case ClientVersionBuild.V5_0_5a_16057:
                    case ClientVersionBuild.V5_0_5b_16135:
                        return ClientVersionBuild.V5_0_5_16048;
                    case ClientVersionBuild.V5_1_0_16309:
                    case ClientVersionBuild.V5_1_0a_16357:
                        return ClientVersionBuild.V5_1_0_16309;
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
                        return ClientVersionBuild.V5_2_0_16826;
                    case ClientVersionBuild.V5_3_0_16981:
                    case ClientVersionBuild.V5_3_0_16983:
                    case ClientVersionBuild.V5_3_0_16992:
                    case ClientVersionBuild.V5_3_0_17055:
                    case ClientVersionBuild.V5_3_0_17116:
                    case ClientVersionBuild.V5_3_0_17128:
                        return ClientVersionBuild.V5_3_0_16981;
                    case ClientVersionBuild.V5_4_0_17359:
                    case ClientVersionBuild.V5_4_0_17371:
                    case ClientVersionBuild.V5_4_0_17399:
                        return ClientVersionBuild.V5_4_0_17359;
                    case ClientVersionBuild.V5_4_1_17538:
                        return ClientVersionBuild.V5_4_1_17538;
                    case ClientVersionBuild.V5_4_2_17658:
                    case ClientVersionBuild.V5_4_2_17688:
                        return ClientVersionBuild.V5_4_2_17658;
                    case ClientVersionBuild.V5_4_7_17898:
                    case ClientVersionBuild.V5_4_7_17930:
                    case ClientVersionBuild.V5_4_7_17956:
                    case ClientVersionBuild.V5_4_7_18019:
                        return ClientVersionBuild.V5_4_7_17898;
                    case ClientVersionBuild.V5_4_8_18291:
                    case ClientVersionBuild.V5_4_8_18414:
                        return ClientVersionBuild.V5_4_8_18291;
                    case ClientVersionBuild.V6_0_2_19033:
                    case ClientVersionBuild.V6_0_2_19034:
                    case ClientVersionBuild.V6_0_3_19103:
                    case ClientVersionBuild.V6_0_3_19116:
                    case ClientVersionBuild.V6_0_3_19243:
                    case ClientVersionBuild.V6_0_3_19342:
                    case ClientVersionBuild.V6_1_0_19678:
                    case ClientVersionBuild.V6_1_0_19702:
                    case ClientVersionBuild.V6_1_2_19802:
                    case ClientVersionBuild.V6_1_2_19831:
                    case ClientVersionBuild.V6_1_2_19865:
                    case ClientVersionBuild.V6_2_0_20173:
                    case ClientVersionBuild.V6_2_0_20182:
                    case ClientVersionBuild.V6_2_0_20201:
                    case ClientVersionBuild.V6_2_0_20216:
                    case ClientVersionBuild.V6_2_0_20253:
                    case ClientVersionBuild.V6_2_0_20338:
                    case ClientVersionBuild.V6_2_2_20444:
                    case ClientVersionBuild.V6_2_2a_20490:
                    case ClientVersionBuild.V6_2_2a_20574:
                    case ClientVersionBuild.V6_2_3_20726:
                    case ClientVersionBuild.V6_2_3_20779:
                    case ClientVersionBuild.V6_2_3_20886:
                    case ClientVersionBuild.V6_2_4_21315:
                    case ClientVersionBuild.V6_2_4_21336:
                    case ClientVersionBuild.V6_2_4_21343:
                    case ClientVersionBuild.V6_2_4_21345:
                    case ClientVersionBuild.V6_2_4_21348:
                    case ClientVersionBuild.V6_2_4_21355:
                    case ClientVersionBuild.V6_2_4_21463:
                    case ClientVersionBuild.V6_2_4_21676:
                    case ClientVersionBuild.V6_2_4_21742:
                        return ClientVersionBuild.V6_0_2_19033;
                    case ClientVersionBuild.V7_0_3_22248:
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
                    case ClientVersionBuild.V7_1_0_22900:
                    case ClientVersionBuild.V7_1_0_22908:
                    case ClientVersionBuild.V7_1_0_22950:
                    case ClientVersionBuild.V7_1_0_22989:
                    case ClientVersionBuild.V7_1_0_22995:
                    case ClientVersionBuild.V7_1_0_22996:
                    case ClientVersionBuild.V7_1_0_23171:
                    case ClientVersionBuild.V7_1_0_23222:
                    case ClientVersionBuild.V7_1_5_23360:
                    case ClientVersionBuild.V7_1_5_23420:
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
                    case ClientVersionBuild.V7_2_5_24330:
                    case ClientVersionBuild.V7_2_5_24367:
                    case ClientVersionBuild.V7_2_5_24414:
                    case ClientVersionBuild.V7_2_5_24415:
                    case ClientVersionBuild.V7_2_5_24430:
                    case ClientVersionBuild.V7_2_5_24461:
                    case ClientVersionBuild.V7_2_5_24742:
                    case ClientVersionBuild.V7_3_0_24920:
                    case ClientVersionBuild.V7_3_0_24931:
                    case ClientVersionBuild.V7_3_0_24956:
                    case ClientVersionBuild.V7_3_0_24970:
                    case ClientVersionBuild.V7_3_0_24974:
                    case ClientVersionBuild.V7_3_0_25021:
                    case ClientVersionBuild.V7_3_0_25195:
                    case ClientVersionBuild.V7_3_2_25383:
                    case ClientVersionBuild.V7_3_2_25442:
                    case ClientVersionBuild.V7_3_2_25455:
                    case ClientVersionBuild.V7_3_2_25477:
                    case ClientVersionBuild.V7_3_2_25480:
                    case ClientVersionBuild.V7_3_2_25497:
                    case ClientVersionBuild.V7_3_2_25549:
                        return ClientVersionBuild.V7_0_3_22248;
                    case ClientVersionBuild.BattleNetV37165:
                        return ClientVersionBuild.BattleNetV37165;
                    case ClientVersionBuild.Zero:
                        return Build;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public static ClientVersionBuild FallbackVersionDefiningBuild
        {
            get
            {
                switch (VersionDefiningBuild)
                {
                    case ClientVersionBuild.V7_0_3_22248:
                        return ClientVersionBuild.V6_0_2_19033;
                    default:
                        return ClientVersionBuild.Zero;
                }
            }
        }

        public static int BuildInt => (int) Build;

        public static string VersionString => Build.ToString();

        public static ClientType Expansion => GetExpansion(Build);

        private static ClientType GetExpansion(ClientVersionBuild build)
        {
            if (build >= ClientVersionBuild.V7_0_3_22248)
                return ClientType.Legion;
            if (build >= ClientVersionBuild.V6_0_2_19033)
                return ClientType.WarlordsOfDraenor;
            if (build >= ClientVersionBuild.V5_0_4_16016)
                return ClientType.MistsOfPandaria;
            if (build >= ClientVersionBuild.V4_0_3_13329)
                return ClientType.Cataclysm;
            if (build >= ClientVersionBuild.V3_0_3_9183)
                return ClientType.WrathOfTheLichKing;
            if (build >= ClientVersionBuild.V2_0_3_6299)
                return ClientType.TheBurningCrusade;

            return ClientType.WorldOfWarcraft;
        }

        private static ClientVersionBuild GetVersion(DateTime time)
        {
            if (time < ClientBuilds[0].Value)
                return ClientVersionBuild.Zero;

            return ClientBuilds.Last(a => a.Value <= time).Key;
        }

        public static void SetVersion(ClientVersionBuild version)
        {
            if (Build == version)
                return;

            Build = version;
            _expansion = GetExpansion(version);

            Opcodes.InitializeOpcodeDictionary();
            Handler.ResetHandlers();
            UpdateFields.ResetUFDictionaries();

            if (FallbackVersionDefiningBuild != ClientVersionBuild.Zero)
            {
                try
                {
                    var asm = Assembly.Load($"WowPacketParserModule.{FallbackVersionDefiningBuild}");
                    Trace.WriteLine($"Loading module WowPacketParserModule.{FallbackVersionDefiningBuild}.dll (fallback)");

                    Handler.LoadHandlers(asm, FallbackVersionDefiningBuild);
                }
                catch (FileNotFoundException)
                {
                }
            }

            try
            {
                var asm = Assembly.Load($"WowPacketParserModule.{VersionDefiningBuild}");
                Trace.WriteLine($"Loading module WowPacketParserModule.{VersionDefiningBuild}.dll");

                HotfixStoreMgr.LoadStores(asm);
                Handler.LoadHandlers(asm, VersionDefiningBuild);
                BattlenetHandler.LoadBattlenetHandlers(asm);

                // This is a huge hack to handle the abnormal situation that appeared with builds 6.0 and 6.1 having mostly the same packet structures
                if (!UpdateFields.LoadUFDictionaries(asm, version))
                    UpdateFields.LoadUFDictionaries(asm, VersionDefiningBuild);
            }
            catch (FileNotFoundException)
            {
                // No dll found, try to load the data in the executable itself
                UpdateFields.LoadUFDictionaries(Assembly.GetExecutingAssembly(), Build);
            }
        }

        public static void SetVersion(DateTime time)
        {
            SetVersion(GetVersion(time));
        }

        public static bool InVersion(ClientVersionBuild build1, ClientVersionBuild build2)
        {
            return AddedInVersion(build1) && RemovedInVersion(build2);
        }

        public static bool AddedInVersion(ClientVersionBuild build)
        {
            return Build >= build;
        }

        public static bool AddedInVersion(ClientType expansion)
        {
            return _expansion >= expansion;
        }

        public static bool RemovedInVersion(ClientVersionBuild build)
        {
            return Build < build;
        }

        public static bool RemovedInVersion(ClientType expansion)
        {
            return _expansion < expansion;
        }

        public static bool IsUndefined()
        {
            return Build == ClientVersionBuild.Zero;
        }
    }
}
