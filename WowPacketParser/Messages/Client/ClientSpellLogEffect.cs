using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellLogEffect
    {
        public int Effect;
        public List<ClientSpellLogEffectPowerDrainParams> PowerDrainTargets;
        public List<ClientSpellLogEffectExtraAttacksParams> ExtraAttacksTargets;
        public List<ClientSpellLogEffectDurabilityDamageParams> DurabilityDamageTargets;
        public List<ClientSpellLogEffectGenericVictimParams> GenericVictimTargets;
        public List<ClientSpellLogEffectTradeSkillItemParams> TradeSkillTargets;
        public List<ClientSpellLogEffectFeedPetParams> FeedPetTargets;
    }
}
