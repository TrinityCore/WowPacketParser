using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    //! Assortment of suspected related data. Required for proper movementflags implementation research
    public sealed class MovementRelatedData
    {
        public MovementFlag MovementFlags;
        public MovementFlagExtra MovementFlagsExtra;
        public int Bytes1;              //! UNIT_FIELD_BYTES_1
        public int Bytes2;              //! UNIT_FIELD_BYTES_2
        public UnitFlags Flags;         //! UNIT_FIELD_FLAGS
        public UnitFlags2 Flags2;       //! UNIT_FIELD_FLAGS_2
    }
}
