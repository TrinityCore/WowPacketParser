using System;
using System.Collections.Generic;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class ClientVersion
    {
        public static ClientVersionBuild Version { get; set; }

        // Kept in sync with http://www.wowwiki.com/Public_client_builds
        private static readonly KeyValuePair<ClientVersionBuild, DateTime>[] _clientVersions = new KeyValuePair<ClientVersionBuild, DateTime>[]
        {
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
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V3_3_5a_12340, new DateTime(2010, 6, 29)),

            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_1_13164, new DateTime(2010, 10, 12)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_1_13205, new DateTime(2010, 10, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_3_13329, new DateTime(2010, 11, 23)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_6_13596, new DateTime(2011, 2, 8)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_0_6_13623, new DateTime(2011, 2, 11)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_1_0_13914, new DateTime(2011, 4, 26)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_1_0_14007, new DateTime(2011, 5, 5)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_2_0_14333, new DateTime(2011, 6, 28)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_2_0_14480, new DateTime(2011, 9, 8)),
            new KeyValuePair<ClientVersionBuild, DateTime>(ClientVersionBuild.V4_2_2_14545, new DateTime(2011, 9, 30)),
        };

        private static ClientVersionBuild GetCurrentVersion(DateTime time)
        {
            for (int i = 1; i < _clientVersions.Length; i++)
                if (_clientVersions[i].Value > time)
                    return _clientVersions[i - 1].Key;

            return _clientVersions[_clientVersions.Length - 1].Key;
        }

        public static void SetVersion(DateTime time)
        {
            Version = GetCurrentVersion(time);
        }
    }
}
