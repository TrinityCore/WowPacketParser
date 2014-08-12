using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParserModule.V5_4_8_18414.Misc
{
    public sealed class PlayerMovementInfo
    {
        public MovementStatusElements [] PlayerMove = // 5.4.8 18291
        {
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasUnkTime,             // 168
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
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSECounterCount,           // 152
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
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
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

        public MovementStatusElements[] MovementForceFlightSpeedChangeAck = // 09DA
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEExtraFloat,             // 184
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136

            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MovementForceRunBackSpeedChangeAck = // 0158
        {
            MovementStatusElements.MSEExtraFloat,             // 184
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136

            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MovementForceRunSpeedChangeAck = // 10F3
        {
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEExtraFloat,             // 184
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasOrientation,         // 48  30h
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasPitch,               // 112 70h
            MovementStatusElements.MSEHasSplineElevation,     // 144 90h
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEMovementFlags2,         // 28

            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSESplineElevation,        // 144 90h
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEOrientation,            // 48  30h
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEPitch,                  // 112 70h
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MovementForceSwimSpeedChangeAck = // 1853
        {
            MovementStatusElements.MSEExtraFloat,             // 184
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasSplineElevation,     // 144 90h
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasPitch,               // 112 70h
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasOrientation,         // 48  30h
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEMovementFlags,          // 24

            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144 90h
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEPitch,                  // 112 70h
            MovementStatusElements.MSEOrientation,            // 48  30h
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] MovementForceWalkSpeedChangeAck = // 00DB
        {
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEExtraFloat,             // 184
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136

            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEPitch,                  // 112
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

        public MovementStatusElements[] MovementRoot = // 5.4.8 18291
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
            MovementStatusElements.MSEEnd,
        };

        public MovementStatusElements[] MovementUnroot = // 5.4.8 18291
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
            MovementStatusElements.MSEEnd,
        };

        public MovementStatusElements[] Unk023B =
        {
            MovementStatusElements.MSEHasGuidByte5,
            MovementStatusElements.MSEHasGuidByte6,
            MovementStatusElements.MSEHasGuidByte4,
            MovementStatusElements.MSEHasGuidByte1,
            MovementStatusElements.MSEHasGuidByte3,
            MovementStatusElements.MSEHasGuidByte2,
            MovementStatusElements.MSEHasGuidByte7,
            MovementStatusElements.MSEHasGuidByte0,
            MovementStatusElements.MSEGuidByte1,
            MovementStatusElements.MSEGuidByte7,
            MovementStatusElements.MSEGuidByte3,
            MovementStatusElements.MSEExtraFloat,
            MovementStatusElements.MSEGuidByte6,
            MovementStatusElements.MSEGuidByte0,
            MovementStatusElements.MSEGuidByte4,
            MovementStatusElements.MSEGuidByte2,
            MovementStatusElements.MSEGuidByte5,
            MovementStatusElements.MSEEnd,
        };

        public MovementStatusElements[] Unk09DB = 
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28

            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] CUnk1052 = // 1052
        {
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasSplineElevation,     // 144 90h
            MovementStatusElements.MSEHasOrientation,         // 48  30h
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasPitch,               // 112 70h
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136

            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSEOrientation,            // 48  30h
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSESplineElevation,        // 144 90h
            MovementStatusElements.MSEPitch,                  // 112 70h
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk10F2 = // 10F2
        {
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasPitch,               // 112 70h
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasSplineElevation,     // 144 90h
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasOrientation,         // 48  30h
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEMovementFlags,          // 24
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28

            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEOrientation,            // 48  30h
            MovementStatusElements.MSEPitch,                  // 112 70h
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144 90h
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk11D8 = 
        {
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSECount,
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEMovementFlags,          // 24

            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] CUnk11DB =
        {
            MovementStatusElements.MSECount,                  // 176
            MovementStatusElements.MSEPositionX,              // 36
            MovementStatusElements.MSEPositionY,              // 40
            MovementStatusElements.MSEPositionZ,              // 44
            MovementStatusElements.MSEHasMovementFlags2,      // 28
            MovementStatusElements.MSEHasPitch,               // 112
            MovementStatusElements.MSECounterCount,           // 152
            MovementStatusElements.MSEbit149,                 // 149
            MovementStatusElements.MSEHasGuidByte7,           // 23
            MovementStatusElements.MSEbit172,                 // 172
            MovementStatusElements.MSEHasGuidByte1,           // 17
            MovementStatusElements.MSEbit148,                 // 148
            MovementStatusElements.MSEHasGuidByte6,           // 22
            MovementStatusElements.MSEHasMovementFlags,       // 24
            MovementStatusElements.MSEHasUnkTime,             // 168
            MovementStatusElements.MSEHasTimestamp,           // 32
            MovementStatusElements.MSEHasOrientation,         // 48
            MovementStatusElements.MSEHasGuidByte0,           // 16
            MovementStatusElements.MSEHasGuidByte2,           // 18
            MovementStatusElements.MSEHasGuidByte3,           // 19
            MovementStatusElements.MSEHasGuidByte5,           // 21
            MovementStatusElements.MSEHasFallData,            // 140
            MovementStatusElements.MSEHasTransportData,       // 104
            MovementStatusElements.MSEHasGuidByte4,           // 20
            MovementStatusElements.MSEHasSplineElevation,     // 144
            MovementStatusElements.MSEHasTransportTime3,      // 100
            MovementStatusElements.MSEHasTransportGuidByte0,  // 56
            MovementStatusElements.MSEHasTransportGuidByte1,  // 57
            MovementStatusElements.MSEHasTransportGuidByte6,  // 62
            MovementStatusElements.MSEHasTransportTime2,      // 92
            MovementStatusElements.MSEHasTransportGuidByte5,  // 61
            MovementStatusElements.MSEHasTransportGuidByte7,  // 63
            MovementStatusElements.MSEHasTransportGuidByte4,  // 60
            MovementStatusElements.MSEHasTransportGuidByte3,  // 59
            MovementStatusElements.MSEHasTransportGuidByte2,  // 58
            MovementStatusElements.MSEMovementFlags2,         // 28
            MovementStatusElements.MSEHasFallDirection,       // 136
            MovementStatusElements.MSEMovementFlags,          // 24

            MovementStatusElements.MSEGuidByte0,              // 16
            MovementStatusElements.MSECounter,                // 156
            MovementStatusElements.MSEGuidByte5,              // 21
            MovementStatusElements.MSEGuidByte2,              // 18
            MovementStatusElements.MSEGuidByte3,              // 19
            MovementStatusElements.MSEGuidByte6,              // 22
            MovementStatusElements.MSEGuidByte4,              // 20
            MovementStatusElements.MSEGuidByte7,              // 23
            MovementStatusElements.MSEGuidByte1,              // 17
            MovementStatusElements.MSEFallSinAngle,           // 128
            MovementStatusElements.MSEFallHorizontalSpeed,    // 132
            MovementStatusElements.MSEFallCosAngle,           // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 120
            MovementStatusElements.MSEFallTime,               // 116
            MovementStatusElements.MSETransportGuidByte4,     // 60
            MovementStatusElements.MSETransportGuidByte6,     // 62
            MovementStatusElements.MSETransportPositionX,     // 64
            MovementStatusElements.MSETransportTime3,         // 96
            MovementStatusElements.MSETransportGuidByte1,     // 57
            MovementStatusElements.MSETransportPositionY,     // 68
            MovementStatusElements.MSETransportGuidByte7,     // 63
            MovementStatusElements.MSETransportGuidByte0,     // 56
            MovementStatusElements.MSETransportOrientation,   // 76
            MovementStatusElements.MSETransportGuidByte3,     // 59
            MovementStatusElements.MSETransportTime,          // 84
            MovementStatusElements.MSETransportGuidByte5,     // 61
            MovementStatusElements.MSETransportSeat,          // 80
            MovementStatusElements.MSETransportPositionZ,     // 72
            MovementStatusElements.MSETransportTime2,         // 88
            MovementStatusElements.MSETransportGuidByte2,     // 58
            MovementStatusElements.MSEPitch,                  // 112
            MovementStatusElements.MSEOrientation,            // 48
            MovementStatusElements.MSETimestamp,              // 32
            MovementStatusElements.MSESplineElevation,        // 144
            MovementStatusElements.MSEUnkTime,                // 168
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk0047 = // 0047 +8
        {
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEHasGuidByte5,           // 29
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSEbit172,                 // 180
            MovementStatusElements.MSEHasGuidByte1,           // 25
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEMovementFlags2,         // 36
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSEExtraFloat,             // 16  10h
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 128 80h
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140 8ch
            MovementStatusElements.MSEFallCosAngle,           // 132 84h
            MovementStatusElements.MSEFallSinAngle,           // 136 88h
            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk00E1 = // 00E1 +8
        {
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEbit172,                 // 180
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEHasGuidByte5,           // 29
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEMovementFlags2,         // 36
            MovementStatusElements.MSEHasGuidByte1,           // 25

            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 128 80h
            MovementStatusElements.MSEFallSinAngle,           // 136 88h
            MovementStatusElements.MSEFallCosAngle,           // 132 84h
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140 8ch
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSEExtraFloat,             // 16  10h
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk01E2 = // 01E2 +8
        {
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEbit172,                 // 180
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSEMovementFlags2,         // 36
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEHasGuidByte5,           // 29
            MovementStatusElements.MSEHasGuidByte1,           // 25

            MovementStatusElements.MSEFallSinAngle,           // 136 88h
            MovementStatusElements.MSEFallCosAngle,           // 132 84h
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140 8ch
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSEFallVerticalSpeed,      // 128 80h
            MovementStatusElements.MSEExtraFloat,             // 16  10h
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk08A3 = // 08A3 +8
        {
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSEExtraFloat,             // 16  10h
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEHasGuidByte1,           // 25
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEMovementFlags2,         // 36
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEbit172,                 // 180
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEHasGuidByte5,           // 29

            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEFallSinAngle,           // 136 88h
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140 8ch
            MovementStatusElements.MSEFallCosAngle,           // 132 84h
            MovementStatusElements.MSEFallVerticalSpeed,      // 128 80h
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] CUnk09FB = // 09FB +8
        {
            MovementStatusElements.MSEExtraInt32,             // 196
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEExtraFloat,             // 16  10h
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSECount,                  // 184
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSEbit172,                 // 180
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEExtra2Bits,             // 192
            MovementStatusElements.MSEHasGuidByte5,           // 29
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEHasGuidByte1,           // 25
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEMovementFlags2,         // 36

            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSEFallVerticalSpeed,      // 128 80h
            MovementStatusElements.MSEFallSinAngle,           // 136 88h
            MovementStatusElements.MSEFallCosAngle,           // 132 84h
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140 8ch
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk158E = // 158E +8
        {
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEHasGuidByte1,           // 25
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEHasGuidByte5,           // 29
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSEMovementFlags2,         // 36
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEbit172,                 // 180

            MovementStatusElements.MSEFallVerticalSpeed,      // 128
            MovementStatusElements.MSEFallSinAngle,           // 136
            MovementStatusElements.MSEFallCosAngle,           // 132
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSEExtraFloat,             // 16
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSEEnd
        };

        public MovementStatusElements[] Unk1812 = // 1812 +8
        {
            MovementStatusElements.MSEHasGuidByte7,           // 31
            MovementStatusElements.MSEHasGuidByte3,           // 27
            MovementStatusElements.MSEHasMovementFlags2,      // 36
            MovementStatusElements.MSEbit149,                 // 157
            MovementStatusElements.MSEHasGuidByte4,           // 28
            MovementStatusElements.MSEHasGuidByte2,           // 26
            MovementStatusElements.MSEHasOrientation,         // 56  38h
            MovementStatusElements.MSEHasGuidByte5,           // 29
            MovementStatusElements.MSEbit148,                 // 156
            MovementStatusElements.MSEHasSplineElevation,     // 152 98h
            MovementStatusElements.MSEHasMovementFlags,       // 32
            MovementStatusElements.MSEHasTimestamp,           // 40
            MovementStatusElements.MSEHasPitch,               // 120 78h
            MovementStatusElements.MSEHasGuidByte6,           // 30
            MovementStatusElements.MSEHasGuidByte1,           // 25
            MovementStatusElements.MSEHasUnkTime,             // 176
            MovementStatusElements.MSEMovementFlags,          // 32
            MovementStatusElements.MSEbit172,                 // 180
            MovementStatusElements.MSECounterCount,           // 160
            MovementStatusElements.MSEHasFallData,            // 148
            MovementStatusElements.MSEHasFallDirection,       // 144
            MovementStatusElements.MSEHasTransportData,       // 112
            MovementStatusElements.MSEHasTransportGuidByte5,  // 69
            MovementStatusElements.MSEHasTransportGuidByte2,  // 66
            MovementStatusElements.MSEHasTransportGuidByte7,  // 71
            MovementStatusElements.MSEHasTransportGuidByte3,  // 67
            MovementStatusElements.MSEHasTransportTime2,      // 100
            MovementStatusElements.MSEHasTransportGuidByte6,  // 70
            MovementStatusElements.MSEHasTransportGuidByte4,  // 68
            MovementStatusElements.MSEHasTransportTime3,      // 108
            MovementStatusElements.MSEHasTransportGuidByte1,  // 65
            MovementStatusElements.MSEHasTransportGuidByte0,  // 64
            MovementStatusElements.MSEHasGuidByte0,           // 24
            MovementStatusElements.MSEMovementFlags2,         // 36
            MovementStatusElements.MSETransportOrientation,   // 84  54h
            MovementStatusElements.MSETransportGuidByte7,     // 71
            MovementStatusElements.MSETransportPositionZ,     // 80  50h
            MovementStatusElements.MSETransportSeat,          // 88
            MovementStatusElements.MSETransportGuidByte3,     // 67
            MovementStatusElements.MSETransportTime,          // 92
            MovementStatusElements.MSETransportTime3,         // 104
            MovementStatusElements.MSETransportGuidByte6,     // 70
            MovementStatusElements.MSETransportGuidByte1,     // 65
            MovementStatusElements.MSETransportGuidByte2,     // 66
            MovementStatusElements.MSETransportPositionY,     // 76  4ch
            MovementStatusElements.MSETransportGuidByte0,     // 64
            MovementStatusElements.MSETransportTime2,         // 96
            MovementStatusElements.MSETransportGuidByte4,     // 68
            MovementStatusElements.MSETransportGuidByte5,     // 69
            MovementStatusElements.MSETransportPositionX,     // 72  48h
            MovementStatusElements.MSESplineElevation,        // 152 98h
            MovementStatusElements.MSEPositionZ,              // 52  34h
            MovementStatusElements.MSEPitch,                  // 120 78h
            MovementStatusElements.MSEExtraFloat,             // 16  10h
            MovementStatusElements.MSEGuidByte5,              // 29
            MovementStatusElements.MSEGuidByte7,              // 31
            MovementStatusElements.MSEGuidByte4,              // 28
            MovementStatusElements.MSEGuidByte3,              // 27
            MovementStatusElements.MSEUnkTime,                // 176
            MovementStatusElements.MSEPositionX,              // 44  2ch
            MovementStatusElements.MSEFallCosAngle,           // 132 84h
            MovementStatusElements.MSEFallHorizontalSpeed,    // 140 8ch
            MovementStatusElements.MSEFallSinAngle,           // 136 88h
            MovementStatusElements.MSEFallVerticalSpeed,      // 128 80h
            MovementStatusElements.MSEFallTime,               // 124
            MovementStatusElements.MSEOrientation,            // 56  38h
            MovementStatusElements.MSEGuidByte1,              // 25
            MovementStatusElements.MSEPositionY,              // 48  30h
            MovementStatusElements.MSECounter,                // 164
            MovementStatusElements.MSEGuidByte0,              // 24
            MovementStatusElements.MSEExtraFloat,             // 20  14h
            MovementStatusElements.MSEGuidByte6,              // 30
            MovementStatusElements.MSETimestamp,              // 40
            MovementStatusElements.MSEGuidByte2,              // 26
            MovementStatusElements.MSEEnd
        };
    }
}
