using System;
using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class ClientVersion
    {
        // Kept in sync with http://www.wowwiki.com/Public_client_builds
        private static readonly KeyValuePair<ClientVersionBuild, DateTime>[] _clientBuilds = new []
        {
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
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_5_16048 , new DateTime(2012, 9, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_5a_16057, new DateTime(2012, 9, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_0_5b_16135, new DateTime(2012, 10, 14)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_1_0_16309, new DateTime(2012, 11, 13)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V5_1_0a_16357, new DateTime(2012, 12, 3))
        };

        private static ClientType _expansion;

        public static ClientVersionBuild Build { get; private set; }

        public static int BuildInt
        {
            get { return (int) Build; }
        }

        public static string VersionString
        {
            get { return Build.ToString(); }
        }

        private static ClientType GetExpansion(ClientVersionBuild build)
        {
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
            if (time < _clientBuilds[0].Value)
                return ClientVersionBuild.Zero;

            for (var i = 1; i < _clientBuilds.Length; i++)
                if (_clientBuilds[i].Value > time)
                    return _clientBuilds[i - 1].Key;

            return _clientBuilds[_clientBuilds.Length - 1].Key;
        }

        public static void SetVersion(ClientVersionBuild version)
        {
            Build = version;
            _expansion = GetExpansion(version);
        }

        public static void SetVersion(DateTime time)
        {
            SetVersion(GetVersion(time));
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
