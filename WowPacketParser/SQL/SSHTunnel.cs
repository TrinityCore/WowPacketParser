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
        public string[] PromptKeyboardInteractive(string destination, string name, string instruction, string[] prompt, bool[] echo)
        {
            return new string[] { passwd };
        }
    }

    public static class SSHTunnel
    {
        private static Session _session;
        public static bool Enabled = Settings.GetBoolean("SSHEnabled");

        public static void Connect()
        {
            try
             {
                 var jsch = new JSch();
                 var host = Settings.GetString("SSHHost");
                 var user = Settings.GetString("SSHUsername");
                 var pass = Settings.GetString("SSHPassword");
                 var port = Settings.GetInt32("SSHPort");
                 var rPort = Settings.GetInt32("Port");  // Taken from Database settings
                 var lPort = Settings.GetInt32("SSHLocalPort");

                 _session = jsch.getSession(user, host, port);
                 _session.setHost(host);
                 _session.setPassword(pass);
                 UserInfo ui = new MyUserInfo(pass);
                 _session.setUserInfo(ui);
                 _session.connect();
                 _session.setPortForwardingL(lPort, "localhost", rPort);
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
