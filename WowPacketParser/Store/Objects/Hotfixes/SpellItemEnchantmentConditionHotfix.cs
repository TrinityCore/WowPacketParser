using WowPacketParser.Misc;
using WowPacketParser.SQL;

namespace WowPacketParser.Store.Objects
{
    [Hotfix]
    [DBTableName("spell_item_enchantment_condition")]
    public sealed record SpellItemEnchantmentConditionHotfix1000: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LtOperandType", 5)]
        public byte?[] LtOperandType;

        [DBFieldName("LtOperand", 5)]
        public uint?[] LtOperand;

        [DBFieldName("Operator", 5)]
        public byte?[] Operator;

        [DBFieldName("RtOperandType", 5)]
        public byte?[] RtOperandType;

        [DBFieldName("RtOperand", 5)]
        public byte?[] RtOperand;

        [DBFieldName("Logic", 5)]
        public byte?[] Logic;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }

    [Hotfix]
    [DBTableName("spell_item_enchantment_condition")]
    public sealed record SpellItemEnchantmentConditionHotfix340: IDataModel
    {
        [DBFieldName("ID", true)]
        public uint? ID;

        [DBFieldName("LtOperandType", 5)]
        public byte?[] LtOperandType;

        [DBFieldName("LtOperand", 5)]
        public uint?[] LtOperand;

        [DBFieldName("Operator", 5)]
        public byte?[] Operator;

        [DBFieldName("RtOperandType", 5)]
        public byte?[] RtOperandType;

        [DBFieldName("RtOperand", 5)]
        public byte?[] RtOperand;

        [DBFieldName("Logic", 5)]
        public byte?[] Logic;

        [DBFieldName("VerifiedBuild")]
        public int? VerifiedBuild = ClientVersion.BuildInt;
    }
}
