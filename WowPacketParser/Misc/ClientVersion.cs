using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WowPacketParser.Misc
{
    public static class ClientVersion
    {
        public static DateTime Time { private get; set; }

        public static int Version { get; private set; }

        private static readonly List<KeyValuePair<int, DateTime>> _clientVersions = new List<KeyValuePair<int, DateTime>>();

        public static void ComputeDateTime()
        {
            // Kept in sync with http://www.wowwiki.com/Public_client_builds
            // ...
            _clientVersions.Add(new KeyValuePair<int, DateTime>(10958, new DateTime(2010, 12, 2))); // 3.3.0
            _clientVersions.Add(new KeyValuePair<int, DateTime>(11159, new DateTime(2010, 12, 14))); // 3.3.0a
            _clientVersions.Add(new KeyValuePair<int, DateTime>(11685, new DateTime(2010, 3, 23))); // 3.3.3
            _clientVersions.Add(new KeyValuePair<int, DateTime>(12340, new DateTime(2010, 6, 29))); // 3.3.5

            _clientVersions.Add(new KeyValuePair<int, DateTime>(13164, new DateTime(2010, 10, 12))); // 4.0.1
            _clientVersions.Add(new KeyValuePair<int, DateTime>(13205, new DateTime(2010, 10, 26))); // 4.0.1
            _clientVersions.Add(new KeyValuePair<int, DateTime>(13329, new DateTime(2010, 11, 23))); // 4.0.3
            _clientVersions.Add(new KeyValuePair<int, DateTime>(13596, new DateTime(2011, 2, 8))); // 4.0.6
            _clientVersions.Add(new KeyValuePair<int, DateTime>(13623, new DateTime(2011, 2, 11))); // 4.0.6
            _clientVersions.Add(new KeyValuePair<int, DateTime>(13914, new DateTime(2011, 4, 26))); // 4.1.0
            _clientVersions.Add(new KeyValuePair<int, DateTime>(14007, new DateTime(2011, 5, 5))); // 4.1.0
            _clientVersions.Add(new KeyValuePair<int, DateTime>(14333, new DateTime(2011, 6, 28))); // 4.2.0
            _clientVersions.Add(new KeyValuePair<int, DateTime>(14480, new DateTime(2011, 9, 8))); // 4.2.0
            _clientVersions.Add(new KeyValuePair<int, DateTime>(14545, new DateTime(2011, 9, 30))); // 4.2.2
            
            foreach (var clientVersion in _clientVersions)
            {
                if (clientVersion.Value < Time)
                    Version = clientVersion.Key;
            }
        }
    }
}
