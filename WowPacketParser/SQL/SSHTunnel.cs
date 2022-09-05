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
        public static bool Enabled = Settings.Instance.SSHEnabled;

        public static void Connect()
        {
            try
            {
                _session = new SshClient(Settings.Instance.SSHHost, Settings.Instance.SSHPort, Settings.Instance.SSHUsername, Settings.Instance.SSHPassword);
                _session.Connect();
                uint port;
                if (!uint.TryParse(Settings.Instance.Port, out port))
                    port = 3306;

                var portForward = new ForwardedPortLocal((uint)Settings.Instance.SSHLocalPort, "127.0.0.1", port);

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
