using System;
using System.Diagnostics;
using Tamir.SharpSsh.jsch;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public class MyUserInfo : UserInfo
    {
        private readonly string _passwd;

        public MyUserInfo(string password)
        {
            _passwd = password;
        }

        public string getPassword() { return _passwd; }
        public bool promptYesNo(string str) { return true; }
        public string getPassphrase() { return null; }
        public bool promptPassphrase(string message) { return true; }
        public bool promptPassword(string message) { return true; }
        public void showMessage(string message) { }
    }

    public static class SSHTunnel
    {
        [ThreadStatic]
        private static Session _session;
        public static bool Enabled = Settings.SSHEnabled;

        public static void Connect()
        {
            try
             {
                 JSch jsch = new JSch();

                 _session = jsch.getSession(Settings.SSHUsername, Settings.SSHHost, Settings.SSHPort);
                 _session.setHost(Settings.SSHHost);
                 _session.setPassword(Settings.SSHPassword);
                 UserInfo ui = new MyUserInfo(Settings.SSHPassword);
                 _session.setUserInfo(ui);
                 _session.connect();
                 int port;
                 if (!int.TryParse(Settings.Port, out port))
                     port = 3306;
                 _session.setPortForwardingL(Settings.SSHLocalPort, "localhost", port);
                 if (!_session.isConnected())
                    Enabled = false;
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
            return _session != null && _session.isConnected();
        }

        public static void Disconnect()
        {
            _session?.disconnect();
        }
    }
}
