using System;
using System.IO;
using WowPacketParser.Enums;


namespace WowPacketParser.Misc
{
    public static class ClientLocale
    {
        public static string ClientLocaleString;

        public static string PacketLocaleString;

        public static LocaleConstant PacketLocale => (LocaleConstant)Enum.Parse(typeof(LocaleConstant), PacketLocaleString);

        public static void SetLocale(string locale)
        {
            if (locale == string.Empty)
                throw new InvalidDataException("No Locale in packet");

            ClientLocaleString = locale;

            // enGB contains same data as enUS
            if (locale == "enGB")
                PacketLocaleString = "enUS";
            else
                PacketLocaleString = locale;
        }
    }
}
