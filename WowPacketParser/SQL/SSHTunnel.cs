using Renci.SshNet;
using System;
using System.Diagnostics;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public static class SSHTunnel
    {
        [ThreadStatic]
        private static SshClient _session;
        public static bool Enabled = Settings.SSHEnabled;

        public static void Connect()
        {
            try
            {
                _session = new SshClient(Settings.SSHHost, Settings.SSHPort, Settings.SSHUsername, Settings.SSHPassword);
                _session.Connect();
                uint port;
                if (!uint.TryParse(Settings.Port, out port))
                    port = 3306;

                var portForward = new ForwardedPortLocal((uint)Settings.SSHLocalPort, "127.0.0.1", port);

                _session.AddForwardedPort(portForward);

                portForward.Start();

                Enabled = _session.IsConnected;
            }
            catch (Exception ex)
            {
                Enabled = false;
                Trace.WriteLine(ex.Message + " at ssh connect.");
                Disconnect();
            }
        }

        public static bool Connected()
        {
            return _session != null && _session.IsConnected;
        }

        public static void Disconnect()
        {
            if (_session != null)
            {
                _session.Disconnect();
                _session.Dispose();
                _session = null;
            }
        }
    }
}
