using System;
using System.Data;
using Tamir.SharpSsh.jsch;
using WowPacketParser.Misc;

namespace WowPacketParser.SQL
{
    public class MyUserInfo : UserInfo
    {
        private readonly String passwd;

        public MyUserInfo(string password)
        {
            passwd = password;
        }

        public String getPassword() { return passwd; }
        public bool promptYesNo(String str) { return true; }
        public String getPassphrase() { return null; }
        public bool promptPassphrase(String message) { return true; }
        public bool promptPassword(String message) { return true; }
        public void showMessage(String message) { }
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
                 var jsch = new JSch();

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
                Console.WriteLine(ex.Message + " at ssh connect.");
                Disconnect();
            }
        }

        public static bool Connected()
        {
            return _session != null && _session.isConnected();
        }

        public static void Disconnect()
        {
            if (_session != null)
                _session.disconnect();
        }
    }
}
