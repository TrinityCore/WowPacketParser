using System;

namespace WowPacketParser.Enums
{
    [Flags]
    public enum PlayerFieldBytesMiscFlags // 4.x - first byte of PLAYER_FIELD_BYTES
    {
        Unk1                        = 0x1,
        TrackStealthed              = 0x2,
        Unk2                        = 0x4,
        UseReleaseSpiritTimer       = 0x8,
        PreventReleaseSpiritAtDeath = 0x10,
        HasTempPetBar               = 0x20
    }
}
