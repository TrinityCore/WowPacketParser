using System;
using System.Diagnostics;
using Tamir.SharpSsh.jsch;
using PacketParser.Misc;

namespace PacketParser.SQL
{
    public class MyUserInfo : UserInfo
    {
        private readonly String _passwd;

        public MyUserInfo(string password)
        {
            _passwd = password;
        }

        public String getPassword() { return _passwd; }
        public bool promptYesNo(String str) { return true; }
        public String getPassphrase() { return null; }
        public bool promptPassphrase(String message) { return true; }
        public bool promptPassword(String message) { return true; }
        public void showMessage(String message) { }
    }

    public static class SSHTunnel
    {
        [ThreadStatic]
        private static Session Session;
        public static bool Enabled = ParserSettings.SSHTunnel.Enabled;

        public static void Connect()
        {
            try
             {
                 var jsch = new JSch();

                 Session = jsch.getSession(ParserSettings.SSHTunnel.Username, ParserSettings.SSHTunnel.Host, ParserSettings.SSHTunnel.Port);
                 Session.setHost(ParserSettings.SSHTunnel.Host);
                 Session.setPassword(ParserSettings.SSHTunnel.Password);
                 UserInfo ui = new MyUserInfo(ParserSettings.SSHTunnel.Password);
                 Session.setUserInfo(ui);
                 Session.connect();
                 int port;
                 if (!int.TryParse(ParserSettings.MySQL.Port, out port))
                     port = 3306;
                 Session.setPortForwardingL(ParserSettings.SSHTunnel.LocalPort, "localhost", port);
                 if (!Session.isConnected())
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
            return Session != null && Session.isConnected();
        }

        public static void Disconnect()
        {
            if (Session != null)
                Session.disconnect();
        }
    }
}
