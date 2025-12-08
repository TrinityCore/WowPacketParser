using System;
using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public static class HighGuid
    {
        public static HighGuidType ToGeneric(HighGuidTypeLegacy high)
        {
            return high switch
            {
                HighGuidTypeLegacy.None => HighGuidType.Null,
                HighGuidTypeLegacy.Player => HighGuidType.Player,
                HighGuidTypeLegacy.BattleGround1 => HighGuidType.PVPQueueGroup, // ?? unused in wpp
                HighGuidTypeLegacy.InstanceSave => HighGuidType.LFGList, // ?? unused in wpp
                HighGuidTypeLegacy.Group => HighGuidType.RaidGroup,
                HighGuidTypeLegacy.BattleGround2 => HighGuidType.PVPQueueGroup, // ?? unused in wpp
                HighGuidTypeLegacy.MOTransport => HighGuidType.Transport, // ?? unused in wpp
                HighGuidTypeLegacy.Guild => HighGuidType.Guild,
                HighGuidTypeLegacy.Item => HighGuidType.Item,
                HighGuidTypeLegacy.DynObject => HighGuidType.DynamicObject,
                HighGuidTypeLegacy.GameObject => HighGuidType.GameObject,
                HighGuidTypeLegacy.Transport => HighGuidType.Transport,
                HighGuidTypeLegacy.Unit => HighGuidType.Creature,
                HighGuidTypeLegacy.Pet => HighGuidType.Pet,
                HighGuidTypeLegacy.Vehicle => HighGuidType.Vehicle,
                HighGuidTypeLegacy.Unknown270 => HighGuidType.Null,
                _ => throw new ArgumentOutOfRangeException(nameof(high), high, "0x" + high.ToString("X"))
            };
        }

        public static HighGuidType ToGeneric(HighGuidType623 high)
        {
            return high switch
            {
                HighGuidType623.Null => HighGuidType.Null,
                HighGuidType623.Uniq => HighGuidType.Uniq,
                HighGuidType623.Player => HighGuidType.Player,
                HighGuidType623.Item => HighGuidType.Item,
                HighGuidType623.StaticDoor => HighGuidType.StaticDoor,
                HighGuidType623.Transport => HighGuidType.Transport,
                HighGuidType623.Conversation => HighGuidType.Conversation,
                HighGuidType623.Creature => HighGuidType.Creature,
                HighGuidType623.Vehicle => HighGuidType.Vehicle,
                HighGuidType623.Pet => HighGuidType.Pet,
                HighGuidType623.GameObject => HighGuidType.GameObject,
                HighGuidType623.DynamicObject => HighGuidType.DynamicObject,
                HighGuidType623.AreaTrigger => HighGuidType.AreaTrigger,
                HighGuidType623.Corpse => HighGuidType.Corpse,
                HighGuidType623.LootObject => HighGuidType.LootObject,
                HighGuidType623.SceneObject => HighGuidType.SceneObject,
                HighGuidType623.Scenario => HighGuidType.Scenario,
                HighGuidType623.AIGroup => HighGuidType.AIGroup,
                HighGuidType623.DynamicDoor => HighGuidType.DynamicDoor,
                HighGuidType623.ClientActor => HighGuidType.ClientActor,
                HighGuidType623.Vignette => HighGuidType.Vignette,
                HighGuidType623.CallForHelp => HighGuidType.CallForHelp,
                HighGuidType623.AIResource => HighGuidType.AIResource,
                HighGuidType623.AILock => HighGuidType.AILock,
                HighGuidType623.AILockTicket => HighGuidType.AILockTicket,
                HighGuidType623.ChatChannel => HighGuidType.ChatChannel,
                HighGuidType623.Party => HighGuidType.Party,
                HighGuidType623.Guild => HighGuidType.Guild,
                HighGuidType623.WowAccount => HighGuidType.WowAccount,
                HighGuidType623.BNetAccount => HighGuidType.BNetAccount,
                HighGuidType623.GMTask => HighGuidType.GMTask,
                HighGuidType623.MobileSession => HighGuidType.MobileSession,
                HighGuidType623.RaidGroup => HighGuidType.RaidGroup,
                HighGuidType623.Spell => HighGuidType.Spell,
                HighGuidType623.Mail => HighGuidType.Mail,
                HighGuidType623.WebObj => HighGuidType.WebObj,
                HighGuidType623.LFGObject => HighGuidType.LFGObject,
                HighGuidType623.LFGList => HighGuidType.LFGList,
                HighGuidType623.UserRouter => HighGuidType.UserRouter,
                HighGuidType623.PVPQueueGroup => HighGuidType.PVPQueueGroup,
                HighGuidType623.UserClient => HighGuidType.UserClient,
                HighGuidType623.PetBattle => HighGuidType.PetBattle,
                HighGuidType623.UniqueUserClient => HighGuidType.UniqUserClient,
                HighGuidType623.BattlePet => HighGuidType.BattlePet,
                _ => throw new ArgumentOutOfRangeException(nameof(high), high, "0x" + high.ToString("X"))
            };
        }

        public static HighGuidType ToGeneric(HighGuidType624 high)
        {
            return high switch
            {
                HighGuidType624.Null => HighGuidType.Null,
                HighGuidType624.Uniq => HighGuidType.Uniq,
                HighGuidType624.Player => HighGuidType.Player,
                HighGuidType624.Item => HighGuidType.Item,
                HighGuidType624.WorldTransaction => HighGuidType.WorldTransaction,
                HighGuidType624.StaticDoor => HighGuidType.StaticDoor,
                HighGuidType624.Transport => HighGuidType.Transport,
                HighGuidType624.Conversation => HighGuidType.Conversation,
                HighGuidType624.Creature => HighGuidType.Creature,
                HighGuidType624.Vehicle => HighGuidType.Vehicle,
                HighGuidType624.Pet => HighGuidType.Pet,
                HighGuidType624.GameObject => HighGuidType.GameObject,
                HighGuidType624.DynamicObject => HighGuidType.DynamicObject,
                HighGuidType624.AreaTrigger => HighGuidType.AreaTrigger,
                HighGuidType624.Corpse => HighGuidType.Corpse,
                HighGuidType624.LootObject => HighGuidType.LootObject,
                HighGuidType624.SceneObject => HighGuidType.SceneObject,
                HighGuidType624.Scenario => HighGuidType.Scenario,
                HighGuidType624.AIGroup => HighGuidType.AIGroup,
                HighGuidType624.DynamicDoor => HighGuidType.DynamicDoor,
                HighGuidType624.ClientActor => HighGuidType.ClientActor,
                HighGuidType624.Vignette => HighGuidType.Vignette,
                HighGuidType624.CallForHelp => HighGuidType.CallForHelp,
                HighGuidType624.AIResource => HighGuidType.AIResource,
                HighGuidType624.AILock => HighGuidType.AILock,
                HighGuidType624.AILockTicket => HighGuidType.AILockTicket,
                HighGuidType624.ChatChannel => HighGuidType.ChatChannel,
                HighGuidType624.Party => HighGuidType.Party,
                HighGuidType624.Guild => HighGuidType.Guild,
                HighGuidType624.WowAccount => HighGuidType.WowAccount,
                HighGuidType624.BNetAccount => HighGuidType.BNetAccount,
                HighGuidType624.GMTask => HighGuidType.GMTask,
                HighGuidType624.MobileSession => HighGuidType.MobileSession,
                HighGuidType624.RaidGroup => HighGuidType.RaidGroup,
                HighGuidType624.Spell => HighGuidType.Spell,
                HighGuidType624.Mail => HighGuidType.Mail,
                HighGuidType624.WebObj => HighGuidType.WebObj,
                HighGuidType624.LFGObject => HighGuidType.LFGObject,
                HighGuidType624.LFGList => HighGuidType.LFGList,
                HighGuidType624.UserRouter => HighGuidType.UserRouter,
                HighGuidType624.PVPQueueGroup => HighGuidType.PVPQueueGroup,
                HighGuidType624.UserClient => HighGuidType.UserClient,
                HighGuidType624.PetBattle => HighGuidType.PetBattle,
                HighGuidType624.UniqUserClient => HighGuidType.UniqUserClient,
                HighGuidType624.BattlePet => HighGuidType.BattlePet,
                HighGuidType624.CommerceObj => HighGuidType.CommerceObj,
                HighGuidType624.ClientSession => HighGuidType.ClientSession,
                _ => throw new ArgumentOutOfRangeException(nameof(high), high, "0x" + high.ToString("X"))
            };
        }

        public static HighGuidType ToGeneric(HighGuidType703 high)
        {
            return high switch
            {
                HighGuidType703.Null => HighGuidType.Null,
                HighGuidType703.Uniq => HighGuidType.Uniq,
                HighGuidType703.Player => HighGuidType.Player,
                HighGuidType703.Item => HighGuidType.Item,
                HighGuidType703.WorldTransaction => HighGuidType.WorldTransaction,
                HighGuidType703.StaticDoor => HighGuidType.StaticDoor,
                HighGuidType703.Transport => HighGuidType.Transport,
                HighGuidType703.Conversation => HighGuidType.Conversation,
                HighGuidType703.Creature => HighGuidType.Creature,
                HighGuidType703.Vehicle => HighGuidType.Vehicle,
                HighGuidType703.Pet => HighGuidType.Pet,
                HighGuidType703.GameObject => HighGuidType.GameObject,
                HighGuidType703.DynamicObject => HighGuidType.DynamicObject,
                HighGuidType703.AreaTrigger => HighGuidType.AreaTrigger,
                HighGuidType703.Corpse => HighGuidType.Corpse,
                HighGuidType703.LootObject => HighGuidType.LootObject,
                HighGuidType703.SceneObject => HighGuidType.SceneObject,
                HighGuidType703.Scenario => HighGuidType.Scenario,
                HighGuidType703.AIGroup => HighGuidType.AIGroup,
                HighGuidType703.DynamicDoor => HighGuidType.DynamicDoor,
                HighGuidType703.ClientActor => HighGuidType.ClientActor,
                HighGuidType703.Vignette => HighGuidType.Vignette,
                HighGuidType703.CallForHelp => HighGuidType.CallForHelp,
                HighGuidType703.AIResource => HighGuidType.AIResource,
                HighGuidType703.AILock => HighGuidType.AILock,
                HighGuidType703.AILockTicket => HighGuidType.AILockTicket,
                HighGuidType703.ChatChannel => HighGuidType.ChatChannel,
                HighGuidType703.Party => HighGuidType.Party,
                HighGuidType703.Guild => HighGuidType.Guild,
                HighGuidType703.WowAccount => HighGuidType.WowAccount,
                HighGuidType703.BNetAccount => HighGuidType.BNetAccount,
                HighGuidType703.GMTask => HighGuidType.GMTask,
                HighGuidType703.MobileSession => HighGuidType.MobileSession,
                HighGuidType703.RaidGroup => HighGuidType.RaidGroup,
                HighGuidType703.Spell => HighGuidType.Spell,
                HighGuidType703.Mail => HighGuidType.Mail,
                HighGuidType703.WebObj => HighGuidType.WebObj,
                HighGuidType703.LFGObject => HighGuidType.LFGObject,
                HighGuidType703.LFGList => HighGuidType.LFGList,
                HighGuidType703.UserRouter => HighGuidType.UserRouter,
                HighGuidType703.PVPQueueGroup => HighGuidType.PVPQueueGroup,
                HighGuidType703.UserClient => HighGuidType.UserClient,
                HighGuidType703.PetBattle => HighGuidType.PetBattle,
                HighGuidType703.UniqUserClient => HighGuidType.UniqUserClient,
                HighGuidType703.BattlePet => HighGuidType.BattlePet,
                HighGuidType703.CommerceObj => HighGuidType.CommerceObj,
                HighGuidType703.ClientSession => HighGuidType.ClientSession,
                HighGuidType703.Cast => HighGuidType.Cast,
                HighGuidType703.ClientConnection => HighGuidType.ClientConnection,
                HighGuidType703.ClubFinder => HighGuidType.ClubFinder,
                HighGuidType703.ToolsClient => HighGuidType.ToolsClient,
                HighGuidType703.WorldLayer => HighGuidType.WorldLayer,
                HighGuidType703.ArenaTeam => HighGuidType.ArenaTeam,
                HighGuidType703.LMMParty => HighGuidType.LMMParty,
                HighGuidType703.LMMLobby => HighGuidType.LMMLobby,
                HighGuidType703.Housing => HighGuidType.Housing,
                HighGuidType703.MeshObject => HighGuidType.MeshObject,
                HighGuidType703.Entity => HighGuidType.Entity,
                HighGuidType703.Invalid => HighGuidType.Invalid,
                _ => throw new ArgumentOutOfRangeException(nameof(high), high, "0x" + high.ToString("X"))
            };
        }
    }
}
