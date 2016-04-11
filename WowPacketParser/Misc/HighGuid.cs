using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public abstract class HighGuid
    {
        protected HighGuidType highGuidType;

        public HighGuidType GetHighGuidType()
        {
            return highGuidType;
        }

    }

    public class HighGuidLegacy : HighGuid
    {
        private HighGuidTypeLegacy high;
        private static readonly Dictionary<HighGuidTypeLegacy, HighGuidType> HighLegacyToHighType
            = new Dictionary<HighGuidTypeLegacy, HighGuidType>
        {
            { HighGuidTypeLegacy.None, HighGuidType.Null },
            { HighGuidTypeLegacy.Player, HighGuidType.Player },
            { HighGuidTypeLegacy.BattleGround1, HighGuidType.PVPQueueGroup }, // ?? unused in wpp
            { HighGuidTypeLegacy.InstanceSave, HighGuidType.LFGList }, // ?? unused in wpp
            { HighGuidTypeLegacy.Group, HighGuidType.RaidGroup },
            { HighGuidTypeLegacy.BattleGround2, HighGuidType.PVPQueueGroup }, // ?? unused in wpp
            { HighGuidTypeLegacy.MOTransport, HighGuidType.Transport }, // ?? unused in wpp
            { HighGuidTypeLegacy.Guild, HighGuidType.Guild },
            { HighGuidTypeLegacy.Item, HighGuidType.Item },
            { HighGuidTypeLegacy.DynObject, HighGuidType.DynamicObject },
            { HighGuidTypeLegacy.GameObject, HighGuidType.GameObject },
            { HighGuidTypeLegacy.Transport, HighGuidType.Transport },
            { HighGuidTypeLegacy.Unit, HighGuidType.Creature },
            { HighGuidTypeLegacy.Pet, HighGuidType.Pet },
            { HighGuidTypeLegacy.Vehicle, HighGuidType.Vehicle },
        };

        public HighGuidLegacy(HighGuidTypeLegacy high)
        {
            this.high = high;
            if (!HighLegacyToHighType.ContainsKey(high))
                throw new ArgumentOutOfRangeException("0x" + high.ToString("X"));

            highGuidType = HighLegacyToHighType[high];
        }
    }


    public class HighGuid623 : HighGuid
    {
        protected byte high;
        private static readonly Dictionary<HighGuidType623, HighGuidType> High623ToHighType
            = new Dictionary<HighGuidType623, HighGuidType>
        {
            { HighGuidType623.Null, HighGuidType.Null },
            { HighGuidType623.Uniq, HighGuidType.Uniq },
            { HighGuidType623.Player, HighGuidType.Player },
            { HighGuidType623.Item, HighGuidType.Item },
            { HighGuidType623.StaticDoor, HighGuidType.StaticDoor },
            { HighGuidType623.Transport, HighGuidType.Transport },
            { HighGuidType623.Conversation, HighGuidType.Conversation },
            { HighGuidType623.Creature, HighGuidType.Creature },
            { HighGuidType623.Vehicle, HighGuidType.Vehicle },
            { HighGuidType623.Pet, HighGuidType.Pet },
            { HighGuidType623.GameObject, HighGuidType.GameObject },
            { HighGuidType623.DynamicObject, HighGuidType.DynamicObject },
            { HighGuidType623.AreaTrigger, HighGuidType.AreaTrigger },
            { HighGuidType623.Corpse, HighGuidType.Corpse },
            { HighGuidType623.LootObject, HighGuidType.LootObject },
            { HighGuidType623.SceneObject, HighGuidType.SceneObject },
            { HighGuidType623.Scenario, HighGuidType.Scenario },
            { HighGuidType623.AIGroup, HighGuidType.AIGroup },
            { HighGuidType623.DynamicDoor, HighGuidType.DynamicDoor },
            { HighGuidType623.ClientActor, HighGuidType.ClientActor },
            { HighGuidType623.Vignette, HighGuidType.Vignette },
            { HighGuidType623.CallForHelp, HighGuidType.CallForHelp },
            { HighGuidType623.AIResource, HighGuidType.AIResource },
            { HighGuidType623.AILock, HighGuidType.AILock },
            { HighGuidType623.AILockTicket, HighGuidType.AILockTicket },
            { HighGuidType623.ChatChannel, HighGuidType.ChatChannel },
            { HighGuidType623.Party, HighGuidType.Party },
            { HighGuidType623.Guild, HighGuidType.Guild },
            { HighGuidType623.WowAccount, HighGuidType.WowAccount },
            { HighGuidType623.BNetAccount, HighGuidType.BNetAccount },
            { HighGuidType623.GMTask, HighGuidType.GMTask },
            { HighGuidType623.MobileSession, HighGuidType.MobileSession },
            { HighGuidType623.RaidGroup, HighGuidType.RaidGroup },
            { HighGuidType623.Spell, HighGuidType.Spell },
            { HighGuidType623.Mail, HighGuidType.Mail },
            { HighGuidType623.WebObj, HighGuidType.WebObj },
            { HighGuidType623.LFGObject, HighGuidType.LFGObject },
            { HighGuidType623.LFGList, HighGuidType.LFGList },
            { HighGuidType623.UserRouter, HighGuidType.UserRouter },
            { HighGuidType623.PVPQueueGroup, HighGuidType.PVPQueueGroup },
            { HighGuidType623.UserClient, HighGuidType.UserClient },
            { HighGuidType623.PetBattle, HighGuidType.PetBattle },
            { HighGuidType623.UniqueUserClient, HighGuidType.UniqUserClient },
            { HighGuidType623.BattlePet, HighGuidType.BattlePet }
        };

        public HighGuid623(byte high)
        {
            this.high = high;
            if (!High623ToHighType.ContainsKey((HighGuidType623)high))
                throw new ArgumentOutOfRangeException("0x" + high.ToString("X"));

            highGuidType = High623ToHighType[(HighGuidType623)high];
        }
    }

    public class HighGuid624 : HighGuid
    {
        protected byte high;

        public HighGuid624(byte high)
        {
            this.high = high;
            highGuidType = (HighGuidType)high;
        }
    }
}
