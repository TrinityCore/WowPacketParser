using WowPacketParser.Misc;

namespace WowPacketParserModule.V5_4_8_18291.Enums
{
    public sealed class PlayerMovementInfo
    {
        public MovementStatusElements [] PlayerMove = // 5.4.8 18291
        {
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEOneBit,                 // 42
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasMovementFlags,       // 6
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEMovementFlags,          // 6
            MovementStatusElements.MSEHasMovementFlags2,      // 7
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasTimestamp,           // 8
            MovementStatusElements.MSEMovementFlags2,         // 7
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSECounterCount,           // 38
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportTime2,         // 88 (22)
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime,          // 84 (21)
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime3,         // 24
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSETimestamp,              // 8
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 29
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementFallLand = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementHeartBeat = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementJump = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementSetFacing = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementSetPitch = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartBackward = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartForward = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartStrafeLeft = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartStrafeRight = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartTurnLeft = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartTurnRight = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStop = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStopStrafe = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStopTurn = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartAscend = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartDescend = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartSwim = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStopSwim = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStopAscend = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStopPitch = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartPitchDown = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements [] MovementStartPitchUp = // 5.4.8 18291
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEZeroBit,                // 148
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEZeroBit,                // 149
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEZeroBit,                // 172
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MoveRoot = // 5.4.8 18291
        {
            MovementStatusElements.MSEHasGuidByte0,
            MovementStatusElements.MSEHasGuidByte3,
            MovementStatusElements.MSEHasGuidByte4,
            MovementStatusElements.MSEHasGuidByte1,
            MovementStatusElements.MSEHasGuidByte5,
            MovementStatusElements.MSEHasGuidByte2,
            MovementStatusElements.MSEHasGuidByte6,
            MovementStatusElements.MSEHasGuidByte7,
            MovementStatusElements.MSEGuidByte4,
            MovementStatusElements.MSEGuidByte7,
            MovementStatusElements.MSEGuidByte1,
            MovementStatusElements.MSEGuidByte2,
            MovementStatusElements.MSEGuidByte6,
            MovementStatusElements.MSEGuidByte5,
            MovementStatusElements.MSECount,
            MovementStatusElements.MSEGuidByte0,
            MovementStatusElements.MSEGuidByte3,
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MoveUnroot = // 5.4.8 18291
        {
            MovementStatusElements.MSEHasGuidByte3,
            MovementStatusElements.MSEHasGuidByte5,
            MovementStatusElements.MSEHasGuidByte7,
            MovementStatusElements.MSEHasGuidByte1,
            MovementStatusElements.MSEHasGuidByte0,
            MovementStatusElements.MSEHasGuidByte2,
            MovementStatusElements.MSEHasGuidByte4,
            MovementStatusElements.MSEHasGuidByte6,
            MovementStatusElements.MSEGuidByte0,
            MovementStatusElements.MSEGuidByte7,
            MovementStatusElements.MSECount,
            MovementStatusElements.MSEGuidByte5,
            MovementStatusElements.MSEGuidByte4,
            MovementStatusElements.MSEGuidByte2,
            MovementStatusElements.MSEGuidByte1,
            MovementStatusElements.MSEGuidByte3,
            MovementStatusElements.MSEGuidByte6,
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MoveTeleport = // 5.4.8 18414
        {
            MovementStatusElements.MSEHasGuidByte0,
            MovementStatusElements.MSEHasGuidByte6,
            MovementStatusElements.MSEHasGuidByte5,
            MovementStatusElements.MSEHasGuidByte7,
            MovementStatusElements.MSEHasGuidByte2,
            MovementStatusElements.MSEHasTransportData,
            MovementStatusElements.MSEHasGuidByte4,
            MovementStatusElements.MSEHasTransportGuidByte1,
            MovementStatusElements.MSEHasTransportGuidByte3,
            MovementStatusElements.MSEHasTransportGuidByte6,
            MovementStatusElements.MSEHasTransportGuidByte4,
            MovementStatusElements.MSEHasTransportGuidByte5,
            MovementStatusElements.MSEHasTransportGuidByte2,
            MovementStatusElements.MSEHasTransportGuidByte0,
            MovementStatusElements.MSEHasTransportGuidByte7,
            MovementStatusElements.MSEHasGuidByte3,
            MovementStatusElements.MSEHasGuidByte1,
            MovementStatusElements.MSEHasUnkBitA,
            MovementStatusElements.MSEUnkBitABit,
            MovementStatusElements.MSEUnkBitABit,
            MovementStatusElements.MSEUnkBitAByte,
            MovementStatusElements.MSETransportGuidByte4,
            MovementStatusElements.MSETransportGuidByte3,
            MovementStatusElements.MSETransportGuidByte7,
            MovementStatusElements.MSETransportGuidByte1,
            MovementStatusElements.MSETransportGuidByte6,
            MovementStatusElements.MSETransportGuidByte0,
            MovementStatusElements.MSETransportGuidByte2,
            MovementStatusElements.MSETransportGuidByte5,
            MovementStatusElements.MSEGuidByte4,
            MovementStatusElements.MSEGuidByte7,
            MovementStatusElements.MSEPositionZ,
            MovementStatusElements.MSEPositionY,
            MovementStatusElements.MSEGuidByte2,
            MovementStatusElements.MSEGuidByte3,
            MovementStatusElements.MSEGuidByte5,
            MovementStatusElements.MSEPositionX,
            MovementStatusElements.MSECount,
            MovementStatusElements.MSEGuidByte0,
            MovementStatusElements.MSEGuidByte6,
            MovementStatusElements.MSEGuidByte1,
            MovementStatusElements.MSEOrientation,
            MovementStatusElements.MSEEnd
        };
    }
}
