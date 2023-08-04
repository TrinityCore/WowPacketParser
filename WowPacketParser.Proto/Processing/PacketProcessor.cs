using System;

namespace WowPacketParser.Proto.Processing
{
    public abstract class PacketProcessor<T> : IPacketProcessor<T>
    {
        public virtual T? Process(PacketHolder packet)
        {
            switch (packet.KindCase)
            {
                case PacketHolder.KindOneofCase.None:
                    return default;
                case PacketHolder.KindOneofCase.Chat:
                    return Process(packet.BaseData, packet.Chat);
                case PacketHolder.KindOneofCase.QueryCreatureResponse:
                    return Process(packet.BaseData, packet.QueryCreatureResponse);
                case PacketHolder.KindOneofCase.Emote:
                    return Process(packet.BaseData, packet.Emote);
                case PacketHolder.KindOneofCase.PlaySound:
                    return Process(packet.BaseData, packet.PlaySound);
                case PacketHolder.KindOneofCase.PlayMusic:
                    return Process(packet.BaseData, packet.PlayMusic);
                case PacketHolder.KindOneofCase.PlayObjectSound:
                    return Process(packet.BaseData, packet.PlayObjectSound);
                case PacketHolder.KindOneofCase.GossipHello:
                    return Process(packet.BaseData, packet.GossipHello);
                case PacketHolder.KindOneofCase.GossipMessage:
                    return Process(packet.BaseData, packet.GossipMessage);
                case PacketHolder.KindOneofCase.GossipSelect:
                    return Process(packet.BaseData, packet.GossipSelect);
                case PacketHolder.KindOneofCase.GossipClose:
                    return Process(packet.BaseData, packet.GossipClose);
                case PacketHolder.KindOneofCase.SpellStart:
                    return Process(packet.BaseData, packet.SpellStart);
                case PacketHolder.KindOneofCase.SpellGo:
                    return Process(packet.BaseData, packet.SpellGo);
                case PacketHolder.KindOneofCase.AuraUpdate:
                    return Process(packet.BaseData, packet.AuraUpdate);
                case PacketHolder.KindOneofCase.MonsterMove:
                    return Process(packet.BaseData, packet.MonsterMove);
                case PacketHolder.KindOneofCase.PhaseShift:
                    return Process(packet.BaseData, packet.PhaseShift);
                case PacketHolder.KindOneofCase.SpellClick:
                    return Process(packet.BaseData, packet.SpellClick);
                case PacketHolder.KindOneofCase.PlayerLogin:
                    return Process(packet.BaseData, packet.PlayerLogin);
                case PacketHolder.KindOneofCase.OneShotAnimKit:
                    return Process(packet.BaseData, packet.OneShotAnimKit);
                case PacketHolder.KindOneofCase.SetAnimKit:
                    return Process(packet.BaseData, packet.SetAnimKit);
                case PacketHolder.KindOneofCase.PlaySpellVisualKit:
                    return Process(packet.BaseData, packet.PlaySpellVisualKit);
                case PacketHolder.KindOneofCase.QuestGiverAcceptQuest:
                    return Process(packet.BaseData, packet.QuestGiverAcceptQuest);
                case PacketHolder.KindOneofCase.QuestGiverCompleteQuestRequest:
                    return Process(packet.BaseData, packet.QuestGiverCompleteQuestRequest);
                case PacketHolder.KindOneofCase.QuestGiverQuestComplete:
                    return Process(packet.BaseData, packet.QuestGiverQuestComplete);
                case PacketHolder.KindOneofCase.QuestGiverRequestItems:
                    return Process(packet.BaseData, packet.QuestGiverRequestItems);
                case PacketHolder.KindOneofCase.NpcText:
                    return Process(packet.BaseData, packet.NpcText);
                case PacketHolder.KindOneofCase.NpcTextOld:
                    return Process(packet.BaseData, packet.NpcTextOld);
                case PacketHolder.KindOneofCase.DbReply:
                    return Process(packet.BaseData, packet.DbReply);
                case PacketHolder.KindOneofCase.UpdateObject:
                    return Process(packet.BaseData, packet.UpdateObject);
                case PacketHolder.KindOneofCase.QueryGameObjectResponse:
                    return Process(packet.BaseData, packet.QueryGameObjectResponse);
                case PacketHolder.KindOneofCase.ClientAreaTrigger:
                    return Process(packet.BaseData, packet.ClientAreaTrigger);
                case PacketHolder.KindOneofCase.QueryPlayerNameResponse:
                    return Process(packet.BaseData, packet.QueryPlayerNameResponse);
                case PacketHolder.KindOneofCase.QuestComplete:
                    return Process(packet.BaseData, packet.QuestComplete);
                case PacketHolder.KindOneofCase.QuestFailed:
                    return Process(packet.BaseData, packet.QuestFailed);
                case PacketHolder.KindOneofCase.QuestAddKillCredit:
                    return Process(packet.BaseData, packet.QuestAddKillCredit);
                case PacketHolder.KindOneofCase.ClientUseItem:
                    return Process(packet.BaseData, packet.ClientUseItem);
                case PacketHolder.KindOneofCase.ClientQuestGiverChooseReward:
                    return Process(packet.BaseData, packet.ClientQuestGiverChooseReward);
                case PacketHolder.KindOneofCase.ClientMove:
                    return Process(packet.BaseData, packet.ClientMove);
                case PacketHolder.KindOneofCase.ClientUseGameObject:
                    return Process(packet.BaseData, packet.ClientUseGameObject);
                case PacketHolder.KindOneofCase.GossipPoi:
                    return Process(packet.BaseData, packet.GossipPoi);
                case PacketHolder.KindOneofCase.GameObjectCustomAnim:
                    return Process(packet.BaseData, packet.GameObjectCustomAnim);
                case PacketHolder.KindOneofCase.SpellCastFailed:
                    return Process(packet.BaseData, packet.SpellCastFailed);
                case PacketHolder.KindOneofCase.SpellFailure:
                    return Process(packet.BaseData, packet.SpellFailure);
                case PacketHolder.KindOneofCase.LoginSetTimeSpeed:
                    return Process(packet.BaseData, packet.LoginSetTimeSpeed);
                case PacketHolder.KindOneofCase.AuraUpdateAll:
                    return Process(packet.BaseData, packet.AuraUpdateAll);
                case PacketHolder.KindOneofCase.AiReaction:
                    return Process(packet.BaseData, packet.AiReaction);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected virtual T? Process(PacketBase basePacket, PacketQueryGameObjectResponse packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketClientAreaTrigger packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketPhaseShift packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketAuraUpdate packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketSpellGo packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketSpellStart packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketGossipClose packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketGossipSelect packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketGossipMessage packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketGossipHello packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketPlayObjectSound packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketPlayMusic packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketPlaySound packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketEmote packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketChat packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQueryCreatureResponse packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketMonsterMove packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketSpellClick packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketPlayerLogin packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketOneShotAnimKit packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketSetAnimKit packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketPlaySpellVisualKit packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestGiverAcceptQuest packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestGiverCompleteQuestRequest packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestGiverQuestComplete packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestGiverRequestItems packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketNpcText packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketNpcTextOld packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketDbReply packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketUpdateObject packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQueryPlayerNameResponseWrapper packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestComplete packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestFailed packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketQuestAddKillCredit packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketClientUseItem packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketClientQuestGiverChooseReward packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketClientMove packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketClientUseGameObject packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketGossipPoi packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketGameObjectCustomAnim packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketSpellCastFailed packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketSpellFailure packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketLoginSetTimeSpeed packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketAuraUpdateAll packet) => default;
        protected virtual T? Process(PacketBase basePacket, PacketAIReaction packet) => default;
    }
}
