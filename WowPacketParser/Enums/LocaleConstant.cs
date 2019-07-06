using System;
using System.Diagnostics.CodeAnalysis;

namespace WowPacketParser.Enums
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum LocaleConstant
    {
        enUS            = 0,
        koKR            = 1,
        frFR            = 2,
        deDE            = 3,
        zhCN            = 4,
        zhTW            = 5,
        esES            = 6,
        esMX            = 7,
        ruRU            = 8,
        none            = 9,
        ptBR            = 10,
        itIT            = 11
    }

    [Flags]
    public enum LocaleConstantFlags : uint
    {
        enUS = (1 << 0),
        koKR = (1 << 1),
        frFR = (1 << 2),
        deDE = (1 << 3),
        zhCN = (1 << 4),
        zhTW = (1 << 5),
        esES = (1 << 6),
        esMX = (1 << 7),
        ruRU = (1 << 8),
        none = (1 << 9),
        ptBR = (1 << 10),
        itIT = (1 << 11),
    }
}
