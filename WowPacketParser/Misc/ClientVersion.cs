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
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22267, new DateTime(2016, 07, 19)),
            //new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_0_3_22277, new DateTime(2016, 07, 20)), no time known
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

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_2_0_23706, new DateTime(2017, 03, 05)),
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
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_2_25549, new DateTime(2017, 11, 22)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25848, new DateTime(2018, 01, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25860, new DateTime(2018, 01, 16, 08, 59, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25864, new DateTime(2018, 01, 16, 16, 11, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25875, new DateTime(2018, 01, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25881, new DateTime(2018, 01, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25901, new DateTime(2018, 01, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25928, new DateTime(2018, 01, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25937, new DateTime(2018, 01, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25944, new DateTime(2018, 01, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25946, new DateTime(2018, 01, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25950, new DateTime(2018, 01, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25961, new DateTime(2018, 02, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_25996, new DateTime(2018, 02, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26124, new DateTime(2018, 02, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26365, new DateTime(2018, 04, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26654, new DateTime(2018, 05, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26755, new DateTime(2018, 05, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26822, new DateTime(2018, 06, 12)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26899, new DateTime(2018, 06, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V7_3_5_26972, new DateTime(2018, 06, 29)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27101, new DateTime(2018, 07, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27144, new DateTime(2018, 07, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27165, new DateTime(2018, 07, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27178, new DateTime(2018, 07, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27219, new DateTime(2018, 08, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27291, new DateTime(2018, 08, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27326, new DateTime(2018, 08, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27355, new DateTime(2018, 08, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27356, new DateTime(2018, 08, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27366, new DateTime(2018, 08, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27377, new DateTime(2018, 08, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27404, new DateTime(2018, 08, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27481, new DateTime(2018, 08, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27547, new DateTime(2018, 08, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27602, new DateTime(2018, 09, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27791, new DateTime(2018, 09, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27843, new DateTime(2018, 09, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_27980, new DateTime(2018, 10, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_0_1_28153, new DateTime(2018, 10, 18)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_28724, new DateTime(2018, 12, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_28768, new DateTime(2018, 12, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_28807, new DateTime(2018, 12, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_28822, new DateTime(2018, 12, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_28833, new DateTime(2018, 12, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29088, new DateTime(2019, 01, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29139, new DateTime(2019, 01, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29235, new DateTime(2019, 01, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29285, new DateTime(2019, 02, 05, 17, 15, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29297, new DateTime(2019, 02, 05, 20, 40, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29482, new DateTime(2019, 02, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29600, new DateTime(2019, 03, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_0_29621, new DateTime(2019, 03, 06)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29683, new DateTime(2019, 03, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29701, new DateTime(2019, 03, 12, 11, 00, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29704, new DateTime(2019, 03, 12, 15, 45, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29705, new DateTime(2019, 03, 13, 13, 30, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29718, new DateTime(2019, 03, 13, 20, 00, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29732, new DateTime(2019, 03, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29737, new DateTime(2019, 03, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29814, new DateTime(2019, 03, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29869, new DateTime(2019, 03, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29896, new DateTime(2019, 04, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_29981, new DateTime(2019, 04, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_30477, new DateTime(2019, 05, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_1_5_30706, new DateTime(2019, 06, 05)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_30898, new DateTime(2019, 06, 25, 16, 05, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_30918, new DateTime(2019, 06, 25, 18, 48, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_30920, new DateTime(2019, 06, 26, 0, 35, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_30948, new DateTime(2019, 06, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_30993, new DateTime(2019, 07, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_31229, new DateTime(2019, 07, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_31429, new DateTime(2019, 08, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_0_31478, new DateTime(2019, 08, 16)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_31921, new DateTime(2019, 09, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_31958, new DateTime(2019, 09, 24, 9, 14, 32)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_31960, new DateTime(2019, 09, 24, 13, 25, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_31961, new DateTime(2019, 09, 24, 16, 31, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_31984, new DateTime(2019, 09, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32028, new DateTime(2019, 09, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32144, new DateTime(2019, 10, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32185, new DateTime(2019, 10, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32265, new DateTime(2019, 10, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32294, new DateTime(2019, 10, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32305, new DateTime(2019, 10, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32494, new DateTime(2019, 11, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32580, new DateTime(2019, 11, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32638, new DateTime(2019, 11, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32722, new DateTime(2019, 12, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32750, new DateTime(2019, 12, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_2_5_32978, new DateTime(2020, 01, 10)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33062, new DateTime(2020, 01, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33073, new DateTime(2020, 01, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33084, new DateTime(2020, 01, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33095, new DateTime(2020, 01, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33115, new DateTime(2020, 01, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33169, new DateTime(2020, 01, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33237, new DateTime(2020, 02, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33369, new DateTime(2020, 02, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33528, new DateTime(2020, 03, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33724, new DateTime(2020, 03, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33775, new DateTime(2020, 03, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_33941, new DateTime(2020, 04, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_34220, new DateTime(2020, 04, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_34601, new DateTime(2020, 06, 03)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_34769, new DateTime(2020, 06, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_0_34963, new DateTime(2020, 07, 01)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_7_35249, new DateTime(2020, 07, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_7_35284, new DateTime(2020, 07, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_7_35435, new DateTime(2020, 08, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V8_3_7_35662, new DateTime(2020, 08, 26)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36216, new DateTime(2020, 10, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36228, new DateTime(2020, 10, 13, 09, 28, 51)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36230, new DateTime(2020, 10, 13, 13, 24, 51)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36247, new DateTime(2020, 10, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36272, new DateTime(2020, 10, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36322, new DateTime(2020, 10, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36372, new DateTime(2020, 10, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36492, new DateTime(2020, 11, 03)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_1_36577, new DateTime(2020, 11, 10)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36639, new DateTime(2020, 11, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36665, new DateTime(2020, 11, 17, 09, 04, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36671, new DateTime(2020, 11, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36710, new DateTime(2020, 11, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36734, new DateTime(2020, 11, 23, 16, 10, 49)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36751, new DateTime(2020, 11, 24, 14, 25, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36753, new DateTime(2020, 11, 24, 18, 20, 43)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36839, new DateTime(2020, 12, 07)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_36949, new DateTime(2020, 12, 15, 17, 10, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_37106, new DateTime(2021, 01, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_37142, new DateTime(2021, 01, 08, 20, 00, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_37176, new DateTime(2021, 01, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_2_37474, new DateTime(2021, 02, 04)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_37862, new DateTime(2021, 03, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_37864, new DateTime(2021, 03, 09, 12, 0, 0)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_37893, new DateTime(2021, 03, 10)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_37899, new DateTime(2021, 03, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_37988, new DateTime(2021, 03, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_38134, new DateTime(2021, 03, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_0_5_38556, new DateTime(2021, 05, 11, 19, 00, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39185, new DateTime(2021, 06, 29)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39226, new DateTime(2021, 06, 29, 22, 08, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39229, new DateTime(2021, 06, 30, 06, 12, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39262, new DateTime(2021, 07, 02, 06, 14, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39282, new DateTime(2021, 07, 02, 18, 24, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39289, new DateTime(2021, 07, 03, 05, 30, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39291, new DateTime(2021, 07, 04, 05, 40, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39318, new DateTime(2021, 07, 06, 23, 40, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39335, new DateTime(2021, 07, 08, 19, 22, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39427, new DateTime(2021, 07, 16, 20, 06, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39498, new DateTime(2021, 07, 23, 19, 25, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39497, new DateTime(2021, 07, 24, 20, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39584, new DateTime(2021, 07, 29, 02, 55, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39617, new DateTime(2021, 07, 31, 01, 45, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39653, new DateTime(2021, 08, 5, 01, 45, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_39804, new DateTime(2021, 08, 19, 01, 22, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_40000, new DateTime(2021, 09, 01, 19, 25, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_40120, new DateTime(2021, 09, 10, 01, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_40443, new DateTime(2021, 10, 06, 23, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_40593, new DateTime(2021, 10, 13, 06, 45, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_0_40725, new DateTime(2021, 10, 26, 21, 36, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_40772, new DateTime(2021, 11, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_40871, new DateTime(2021, 11, 02, 15, 14, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_40906, new DateTime(2021, 11, 03, 18, 42, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_40944, new DateTime(2021, 11, 06, 02, 25, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_40966, new DateTime(2021, 11, 10, 22, 16, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41031, new DateTime(2021, 11, 12, 00, 30, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41079, new DateTime(2021, 11, 18, 20, 05, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41288, new DateTime(2021, 12, 02, 03, 58, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41323, new DateTime(2021, 12, 03, 23, 35, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41359, new DateTime(2021, 12, 07, 21, 31, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41488, new DateTime(2021, 12, 16, 23, 36, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_41793, new DateTime(2022, 01, 08, 01, 21, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_1_5_42010, new DateTime(2022, 01, 22, 01, 14, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42423, new DateTime(2022, 02, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42488, new DateTime(2022, 02, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42521, new DateTime(2022, 03, 01)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42538, new DateTime(2022, 03, 02, 03, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42560, new DateTime(2022, 03, 03, 00, 55, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42614, new DateTime(2022, 03, 08, 15, 15, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42698, new DateTime(2022, 03, 12, 03, 18, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42852, new DateTime(2022, 03, 24, 21, 10, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42937, new DateTime(2022, 03, 29, 16, 20, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_42979, new DateTime(2022, 03, 30, 04, 05, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_43114, new DateTime(2022, 04, 08, 20, 00, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_43206, new DateTime(2022, 04, 14, 02, 10, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_43340, new DateTime(2022, 04, 22, 19, 32, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_0_43345, new DateTime(2022, 05, 03, 04, 18, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_43903, new DateTime(2022, 06, 01, 02, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_43971, new DateTime(2022, 05, 31, 16, 18, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44015, new DateTime(2022, 06, 06, 21, 06, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44061, new DateTime(2022, 06, 07, 16, 04, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44127, new DateTime(2022, 06, 15, 03, 12, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44232, new DateTime(2022, 06, 18, 02, 46, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44325, new DateTime(2022, 06, 27, 20, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44730, new DateTime(2022, 07, 22, 20, 05, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_5_44908, new DateTime(2022, 08, 02, 16, 05, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_7_45114, new DateTime(2022, 08, 16, 17, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_7_45161, new DateTime(2022, 08, 19, 03, 24, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_7_45338, new DateTime(2022, 09, 08, 01, 05, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V9_2_7_45745, new DateTime(2022, 09, 22, 03, 04, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46181, new DateTime(2022, 10, 20, 21, 30, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46293, new DateTime(2022, 10, 25, 16, 20, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46313, new DateTime(2022, 10, 26, 04, 45, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46340, new DateTime(2022, 10, 28, 00, 20, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46366, new DateTime(2022, 10, 30, 00, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46455, new DateTime(2022, 11, 04, 00, 50, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46547, new DateTime(2022, 11, 08, 18, 20, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46549, new DateTime(2022, 11, 09, 01, 00, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_0_46597, new DateTime(2022, 11, 10, 21, 34, 00)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46479, new DateTime(2022, 11, 12)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46658, new DateTime(2022, 11, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46689, new DateTime(2022, 11, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46702, new DateTime(2022, 11, 18)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46741, new DateTime(2022, 11, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46801, new DateTime(2022, 11, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46879, new DateTime(2022, 11, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_46924, new DateTime(2022, 12, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_47067, new DateTime(2022, 12, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_47187, new DateTime(2022, 12, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_47213, new DateTime(2022, 12, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_2_47631, new DateTime(2023, 01, 17)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47777, new DateTime(2023, 01, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47799, new DateTime(2023, 01, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47825, new DateTime(2023, 01, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47849, new DateTime(2023, 01, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47871, new DateTime(2023, 01, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47884, new DateTime(2023, 01, 31)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47936, new DateTime(2023, 02, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_47967, new DateTime(2023, 02, 07)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_48001, new DateTime(2023, 02, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_48069, new DateTime(2023, 02, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_48317, new DateTime(2023, 02, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_48397, new DateTime(2023, 03, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_5_48526, new DateTime(2023, 03, 10)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48676, new DateTime(2023, 03, 21)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48749, new DateTime(2023, 03, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48838, new DateTime(2023, 03, 31, 05, 33, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48865, new DateTime(2023, 03, 31, 23, 06, 00)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48892, new DateTime(2023, 04, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48966, new DateTime(2023, 04, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_48999, new DateTime(2023, 04, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_49267, new DateTime(2023, 04, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_0_7_49343, new DateTime(2023, 04, 28)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49407, new DateTime(2023, 05, 02, 13, 59, 02)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49426, new DateTime(2023, 05, 03)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49444, new DateTime(2023, 05, 04)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49474, new DateTime(2023, 05, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49570, new DateTime(2023, 05, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49679, new DateTime(2023, 05, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49741, new DateTime(2023, 05, 25)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49801, new DateTime(2023, 05, 27)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_49890, new DateTime(2023, 06, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_0_50000, new DateTime(2023, 06, 12)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50401, new DateTime(2023, 07, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50438, new DateTime(2023, 07, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50469, new DateTime(2023, 07, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50504, new DateTime(2023, 07, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50585, new DateTime(2023, 07, 22)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50622, new DateTime(2023, 07, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50747, new DateTime(2023, 08, 03)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_50791, new DateTime(2023, 08, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_5_51130, new DateTime(2023, 08, 31)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51187, new DateTime(2023, 09, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51237, new DateTime(2023, 09, 06)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51261, new DateTime(2023, 09, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51313, new DateTime(2023, 09, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51421, new DateTime(2023, 09, 20)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51485, new DateTime(2023, 09, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51536, new DateTime(2023, 09, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51754, new DateTime(2023, 10, 17)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51886, new DateTime(2023, 10, 24)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_1_7_51972, new DateTime(2023, 11, 01)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52038, new DateTime(2023, 11, 06)), // background download only
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52068, new DateTime(2023, 11, 07)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52095, new DateTime(2023, 11, 08)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52106, new DateTime(2023, 11, 09)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52129, new DateTime(2023, 11, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52148, new DateTime(2023, 11, 15)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52188, new DateTime(2023, 11, 16)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52301, new DateTime(2023, 11, 30)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52393, new DateTime(2023, 12, 05)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52485, new DateTime(2023, 12, 12)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52545, new DateTime(2023, 12, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52607, new DateTime(2023, 12, 19)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V10_2_0_52649, new DateTime(2023, 12, 21)),

            // no classic info, pkt contain build in header
        };

        private static ClientType _expansion;
        private static ClientBranch _branch;

        public static ClientVersionBuild Build { get; private set; }

        // Returns the build that will define opcodes/updatefields/handlers for given Build
        public static ClientVersionBuild GetVersionDefiningBuild(ClientVersionBuild build)
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
                    return ClientVersionBuild.V2_4_3_8606;
                case ClientVersionBuild.V3_0_2_9056:
                case ClientVersionBuild.V3_0_3_9183:
                case ClientVersionBuild.V3_0_8_9464:
                case ClientVersionBuild.V3_0_8a_9506:
                case ClientVersionBuild.V3_0_9_9551:
                    return ClientVersionBuild.V3_0_9_9551;
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
                    return ClientVersionBuild.V4_3_0_15005;
                case ClientVersionBuild.V4_3_2_15211:
                    return ClientVersionBuild.V4_3_2_15211;
                case ClientVersionBuild.V4_3_3_15354:
                    return ClientVersionBuild.V4_3_3_15354;
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
                    return ClientVersionBuild.V7_0_3_22248;
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
                    return ClientVersionBuild.V8_0_1_27101;
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
                case ClientVersionBuild.V8_2_0_30898:
                case ClientVersionBuild.V8_2_0_30918:
                case ClientVersionBuild.V8_2_0_30920:
                case ClientVersionBuild.V8_2_0_30948:
                case ClientVersionBuild.V8_2_0_30993:
                case ClientVersionBuild.V8_2_0_31229:
                case ClientVersionBuild.V8_2_0_31429:
                case ClientVersionBuild.V8_2_0_31478:
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
                    return ClientVersionBuild.V8_0_1_27101;
                case ClientVersionBuild.V9_0_1_36216:
                case ClientVersionBuild.V9_0_1_36228:
                case ClientVersionBuild.V9_0_1_36230:
                case ClientVersionBuild.V9_0_1_36247:
                case ClientVersionBuild.V9_0_1_36272:
                case ClientVersionBuild.V9_0_1_36322:
                case ClientVersionBuild.V9_0_1_36372:
                case ClientVersionBuild.V9_0_1_36492:
                case ClientVersionBuild.V9_0_1_36577:
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
                case ClientVersionBuild.V9_0_5_37503:
                case ClientVersionBuild.V9_0_5_37862:
                case ClientVersionBuild.V9_0_5_37864:
                case ClientVersionBuild.V9_0_5_37893:
                case ClientVersionBuild.V9_0_5_37899:
                case ClientVersionBuild.V9_0_5_37988:
                case ClientVersionBuild.V9_0_5_38134:
                case ClientVersionBuild.V9_0_5_38556:
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
                case ClientVersionBuild.V9_2_5_43903:
                case ClientVersionBuild.V9_2_5_43971:
                case ClientVersionBuild.V9_2_5_44015:
                case ClientVersionBuild.V9_2_5_44061:
                case ClientVersionBuild.V9_2_5_44127:
                case ClientVersionBuild.V9_2_5_44232:
                case ClientVersionBuild.V9_2_5_44325:
                case ClientVersionBuild.V9_2_5_44730:
                case ClientVersionBuild.V9_2_5_44908:
                case ClientVersionBuild.V9_2_7_45114:
                case ClientVersionBuild.V9_2_7_45161:
                case ClientVersionBuild.V9_2_7_45338:
                case ClientVersionBuild.V9_2_7_45745:
                    return ClientVersionBuild.V9_0_1_36216;
                case ClientVersionBuild.V10_0_0_46181:
                case ClientVersionBuild.V10_0_0_46293:
                case ClientVersionBuild.V10_0_0_46313:
                case ClientVersionBuild.V10_0_0_46340:
                case ClientVersionBuild.V10_0_0_46366:
                case ClientVersionBuild.V10_0_0_46455:
                case ClientVersionBuild.V10_0_0_46547:
                case ClientVersionBuild.V10_0_0_46549:
                case ClientVersionBuild.V10_0_0_46597:
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
                case ClientVersionBuild.V10_0_7_48676:
                case ClientVersionBuild.V10_0_7_48749:
                case ClientVersionBuild.V10_0_7_48838:
                case ClientVersionBuild.V10_0_7_48865:
                case ClientVersionBuild.V10_0_7_48892:
                case ClientVersionBuild.V10_0_7_48966:
                case ClientVersionBuild.V10_0_7_48999:
                case ClientVersionBuild.V10_0_7_49267:
                case ClientVersionBuild.V10_0_7_49343:
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
                    return ClientVersionBuild.V10_0_0_46181;
                //Classic
                case ClientVersionBuild.V1_13_2_31446:
                case ClientVersionBuild.V1_13_2_31650:
                case ClientVersionBuild.V1_13_2_31687:
                case ClientVersionBuild.V1_13_2_31727:
                case ClientVersionBuild.V1_13_2_31830:
                case ClientVersionBuild.V1_13_2_31882:
                case ClientVersionBuild.V1_13_2_32089:
                case ClientVersionBuild.V1_13_2_32421:
                case ClientVersionBuild.V1_13_2_32600:
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
                case ClientVersionBuild.V1_13_6_36324:
                case ClientVersionBuild.V1_13_6_36497:
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
                    return ClientVersionBuild.V1_13_2_31446;
                case ClientVersionBuild.V1_14_0_39802:
                case ClientVersionBuild.V1_14_0_39958:
                case ClientVersionBuild.V1_14_0_40140:
                case ClientVersionBuild.V1_14_0_40179:
                case ClientVersionBuild.V1_14_0_40237:
                case ClientVersionBuild.V1_14_0_40347:
                case ClientVersionBuild.V1_14_0_40441:
                case ClientVersionBuild.V1_14_0_40618:
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
                case ClientVersionBuild.V2_5_3_41812:
                case ClientVersionBuild.V2_5_3_42083:
                case ClientVersionBuild.V2_5_3_42328:
                case ClientVersionBuild.V2_5_3_42598:
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
                    return ClientVersionBuild.V2_5_1_38707;
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
                case ClientVersionBuild.V3_4_2_50063:
                case ClientVersionBuild.V3_4_2_50129:
                case ClientVersionBuild.V3_4_2_50172:
                case ClientVersionBuild.V3_4_2_50250:
                case ClientVersionBuild.V3_4_2_50375:
                case ClientVersionBuild.V3_4_2_50664:
                case ClientVersionBuild.V3_4_3_51505:
                case ClientVersionBuild.V3_4_3_51572:
                case ClientVersionBuild.V3_4_3_51666:
                case ClientVersionBuild.V3_4_3_51739:
                case ClientVersionBuild.V3_4_3_51831:
                case ClientVersionBuild.V3_4_3_51943:
                case ClientVersionBuild.V3_4_3_52237:
                    return ClientVersionBuild.V3_4_0_45166;
                case ClientVersionBuild.BattleNetV37165:
                    return ClientVersionBuild.BattleNetV37165;
                case ClientVersionBuild.Zero:
                    return build;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static ClientVersionBuild VersionDefiningBuild
        {
            get
            {
                return GetVersionDefiningBuild(Build);
            }
        }

        public static ClientVersionBuild FallbackVersionDefiningBuild(ClientVersionBuild definingbuild, ClientVersionBuild originalDefiningBuild)
        {
            switch (definingbuild)
            {
                case ClientVersionBuild.V1_13_2_31446:
                    return ClientVersionBuild.V8_0_1_27101;
                case ClientVersionBuild.V2_5_1_38707:
                    return ClientVersionBuild.V9_0_1_36216;
                case ClientVersionBuild.V3_4_0_45166:
                    return ClientVersionBuild.V2_5_1_38707;

                case ClientVersionBuild.V7_0_3_22248:
                    return ClientVersionBuild.V6_0_2_19033;
                case ClientVersionBuild.V8_0_1_27101:
                    return ClientVersionBuild.V7_0_3_22248;
                case ClientVersionBuild.V9_0_1_36216:
                    if (IsClassicClientVersionBuild(originalDefiningBuild))
                        return ClientVersionBuild.V1_13_2_31446;
                    return ClientVersionBuild.V8_0_1_27101;
                case ClientVersionBuild.V10_0_0_46181:
                    return ClientVersionBuild.V9_0_1_36216;
                default:
                    return ClientVersionBuild.Zero;
            }
        }

        public static int BuildInt => (int) Build;

        public static string VersionString => Build.ToString();

        public static ClientType Expansion => GetExpansion(Build);

        private static ClientType GetExpansion(ClientVersionBuild build)
        {
            if (IsClassicVanillaClientVersionBuild(build))
                return ClientType.Classic;
            if (IsClassicSeasonOfMasteryClientVersionBuild(build))
                return ClientType.ClassicSoM;
            if (IsBurningCrusadeClassicClientVersionBuild(build))
                return ClientType.BurningCrusadeClassic;
            if (IsWotLKClientVersionBuild(build))
                return ClientType.WotLKClassic;

            if (build >= ClientVersionBuild.V10_0_0_46181)
                return ClientType.Dragonflight;
            if (build >= ClientVersionBuild.V9_0_1_36216)
                return ClientType.Shadowlands;
            if (build >= ClientVersionBuild.V8_0_1_27101)
                return ClientType.BattleForAzeroth;
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

        public static ClientBranch Branch => _branch;

        private static ClientBranch GetBranch(ClientVersionBuild build)
        {
            if (IsClassicVanillaClientVersionBuild(build) || IsClassicSeasonOfMasteryClientVersionBuild(build))
                return ClientBranch.Classic;
            if (IsBurningCrusadeClassicClientVersionBuild(build))
                return ClientBranch.TBC;
            if (IsWotLKClientVersionBuild(build))
                return ClientBranch.WotLK;

            return ClientBranch.Retail;
        }

        public static ClientVersionBuild GetVersion(DateTime time)
        {
            if (time < ClientBuilds[0].Value)
                return ClientVersionBuild.Zero;

            return ClientBuilds.Last(a => a.Value <= time).Key;
        }

        public static void SetVersion(ClientVersionBuild version)
        {
            if (Build == version)
                return;

            ClientVersionBuild prevBuild = Build;
            Build = version;
            _branch = GetBranch(version);

            Opcodes.InitializeOpcodeDictionary();

            if (Opcodes.GetOpcodeDefiningBuild(prevBuild) != Opcodes.GetOpcodeDefiningBuild(Build) || prevBuild == ClientVersionBuild.Zero)
            {
                _expansion = GetExpansion(version);
                Handler.ResetHandlers();
                UpdateFields.ResetUFDictionaries();

                ClientVersionBuild tmpFallback = FallbackVersionDefiningBuild(VersionDefiningBuild, VersionDefiningBuild);

                while (tmpFallback != ClientVersionBuild.Zero)
                {
                    try
                    {
                        var asm = Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + $"/Parsers/WowPacketParserModule.{tmpFallback}.dll");
                        Trace.WriteLine($"Loading module WowPacketParserModule.{tmpFallback}.dll (fallback)");

                        Handler.LoadHandlers(asm, tmpFallback);
                    }
                    catch (FileNotFoundException)
                    {
                    }
                    tmpFallback = FallbackVersionDefiningBuild(tmpFallback, VersionDefiningBuild);
                }

                try
                {
                    var asm = Assembly.LoadFrom(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + $"/Parsers/WowPacketParserModule.{VersionDefiningBuild}.dll");
                    Trace.WriteLine($"Loading module WowPacketParserModule.{VersionDefiningBuild}.dll");

                    HotfixStoreMgr.LoadStores(asm);
                    Handler.LoadHandlers(asm, VersionDefiningBuild);
                    BattlenetHandler.LoadBattlenetHandlers(asm);

                    // This is a huge hack to handle the abnormal situation that appeared with builds 6.0 and 6.1 having mostly the same packet structures
                    if (!UpdateFields.LoadUFDictionaries(asm, version))
                        UpdateFields.LoadUFDictionaries(asm, VersionDefiningBuild);

                    UpdateFields.LoadUFHandlers(asm, VersionDefiningBuild);
                }
                catch (FileNotFoundException)
                {
                    // No dll found, try to load the data in the executable itself
                    UpdateFields.LoadUFDictionaries(Assembly.GetExecutingAssembly(), Build);
                }
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

        public static bool InVersion(ClientBranch branch, ClientVersionBuild build1, ClientVersionBuild build2)
        {
            return _branch == branch && InVersion(build1, build2);
        }

        public static bool AddedInVersion(ClientVersionBuild build)
        {
            return Build >= build;
        }

        public static bool AddedInVersion(ClientBranch branch, ClientVersionBuild build)
        {
            return _branch == branch && AddedInVersion(build);
        }

        public static bool AddedInVersion(ClientType expansion)
        {
            return _expansion >= expansion;
        }

        public static bool RemovedInVersion(ClientVersionBuild build)
        {
            return Build < build;
        }

        public static bool RemovedInVersion(ClientBranch branch, ClientVersionBuild build)
        {
            return _branch == branch && RemovedInVersion(build);
        }

        public static bool RemovedInVersion(ClientType expansion)
        {
            return _expansion < expansion;
        }

        public static bool IsUndefined()
        {
            return Build == ClientVersionBuild.Zero;
        }

        public static bool IsClassicClientVersionBuild(ClientVersionBuild build)
        {
            return IsClassicVanillaClientVersionBuild(build) ||
                   IsClassicSeasonOfMasteryClientVersionBuild(build) ||
                   IsBurningCrusadeClassicClientVersionBuild(build) ||
                   IsWotLKClientVersionBuild(build);
        }

        public static bool IsClassicVanillaClientVersionBuild(ClientVersionBuild build)
        {
            switch (build)
            {
                case ClientVersionBuild.V1_13_2_31446:
                case ClientVersionBuild.V1_13_2_31650:
                case ClientVersionBuild.V1_13_2_31687:
                case ClientVersionBuild.V1_13_2_31727:
                case ClientVersionBuild.V1_13_2_31830:
                case ClientVersionBuild.V1_13_2_31882:
                case ClientVersionBuild.V1_13_2_32089:
                case ClientVersionBuild.V1_13_2_32421:
                case ClientVersionBuild.V1_13_2_32600:
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
                case ClientVersionBuild.V1_13_6_36324:
                case ClientVersionBuild.V1_13_6_36497:
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
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsClassicSeasonOfMasteryClientVersionBuild(ClientVersionBuild build)
        {
            switch (build)
            {
                case ClientVersionBuild.V1_14_0_39802:
                case ClientVersionBuild.V1_14_0_39958:
                case ClientVersionBuild.V1_14_0_40140:
                case ClientVersionBuild.V1_14_0_40179:
                case ClientVersionBuild.V1_14_0_40237:
                case ClientVersionBuild.V1_14_0_40347:
                case ClientVersionBuild.V1_14_0_40441:
                case ClientVersionBuild.V1_14_0_40618:
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
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBurningCrusadeClassicClientVersionBuild(ClientVersionBuild build)
        {
            switch (build)
            {
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
                case ClientVersionBuild.V2_5_3_41812:
                case ClientVersionBuild.V2_5_3_42083:
                case ClientVersionBuild.V2_5_3_42328:
                case ClientVersionBuild.V2_5_3_42598:
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
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsWotLKClientVersionBuild(ClientVersionBuild build)
        {
            switch (build)
            {
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
                case ClientVersionBuild.V3_4_2_50063:
                case ClientVersionBuild.V3_4_2_50129:
                case ClientVersionBuild.V3_4_2_50172:
                case ClientVersionBuild.V3_4_2_50250:
                case ClientVersionBuild.V3_4_2_50375:
                case ClientVersionBuild.V3_4_2_50664:
                case ClientVersionBuild.V3_4_3_51505:
                case ClientVersionBuild.V3_4_3_51572:
                case ClientVersionBuild.V3_4_3_51666:
                case ClientVersionBuild.V3_4_3_51739:
                case ClientVersionBuild.V3_4_3_51831:
                case ClientVersionBuild.V3_4_3_51943:
                case ClientVersionBuild.V3_4_3_52237:
                    return true;
                default:
                    return false;
            }
        }
    }
}
