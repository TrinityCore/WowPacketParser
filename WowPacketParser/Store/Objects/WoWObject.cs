using System.Collections.Generic;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Store.Objects
{
    public class WoWObject
    {
        public ObjectType Type;

        public MovementInfo Movement;

        public uint Map;

        public int Area;

        public Dictionary<int, UpdateField> UpdateFields; // SMSG_UPDATE_OBJECT - CreateObject

        public ICollection<Dictionary<int, UpdateField>> ChangedUpdateFieldsList; // SMSG_UPDATE_OBJECT - Values

        public uint PhaseMask;

        public virtual bool IsTemporarySpawn()
        {
            return false;
        }

        public bool IsOnTransport()
        {
            return Movement.TransportGuid != Guid.Empty;
        }

        public uint GetMapOrTransportMap()
        {
            if (!IsOnTransport())
                return Map;

            switch (Movement.TransportGuid.GetEntry())
            {
                case 20808:
                    return 593; // Ship (The Maiden's Fancy)
                case 164871:
                    return 591; // Zeppelin (The Thundercaller)
                case 175080:
                    return 589; // Zeppelin (The Iron Eagle)
                case 176231:
                    return 584; // Ship (The Lady Mehley)
                case 176244:
                    return 582; // Ship, Night Elf (Moonspray)
                case 176310:
                    return 588; // Ship (The Bravery)
                case 176495:
                    return 590; // Zeppelin (The Purple Princess)
                case 177233:
                    return 587; // Ship, Night Elf (Feathermoon Ferry)
                case 181056:
                    return 0; // Naxxramas
                case 181646:
                    return 586; // Ship, Night Elf (Elune's Blessing)
                case 181688:
                    return 612; // Ship, Icebreaker (Northspear)
                case 181689:
                    return 610; // Zeppelin, Horde (Cloudkisser)
                case 186238:
                    return 613; // Zeppelin, Horde (The Mighty Wind)
                case 187568:
                    return 620; // Turtle (Walker of Waves)
                case 190536:
                    return 614; // Ship, Icebreaker (Stormwind's Pride)
                case 188511:
                    return 621; // Turtle (Green Island)
                case 186371:
                    return 0; // Zeppelin
                case 187038:
                    return 594; // Sister Mercy
                case 192241:
                    return 622; // Orgrim's Hammer
                case 192242:
                    return 623; // The Skybreaker
                case 195121:
                    return 641; // Alliance Gunship
                case 195276:
                    return 642; // Horde Gunship
                case 190549:
                    return 647; // The Zephyr
                case 201599:
                    return 713; // Orgrim's Hammer
                case 201811:
                    return 672; // The Skybreaker
                case 201812:
                    return 673; // Orgrim's Hammer
                case 201598:
                    return 712; // The Skybreaker
                case 201580:
                    return 672; // The Skybreaker
                case 201581:
                    return 673; // Orgrim's Hammer
                case 201834:
                    return 718; // Zeppelin, Horde (The Mighty Wind) (Icecrown Raid)
                case 203621:
                    return 739; // Horde Submarine to Leviathan Cave
                case 203620:
                    return 740; // Alliance Submarine to Leviathan Cave
                case 203626:
                    return 741; // The Spear of Durotar
                case 203730:
                    return 742; // Horde Submarine circling Abyssal Maw
                case 203729:
                    return 743; // Alliance Submarine circling Abyssal Maw
                case 197195:
                    return 674; // Ship to Vashj'ir
                case 204018:
                    return 747; // Alliance Gunship
                case 203428:
                    return 749; // Orc Gunship
                case 204423:
                    return 0; // Orc Gunship
                case 206328:
                    return 762; // Krazzworks to Dragonmaw Port
                case 206329:
                    return 763; // Dragonmaw Port to Krazzworks
                case 207227:
                    return 765; // Krazzworks Attack Zeppelin
                case 203466:
                    return 738; // Ship to Vashj'ir            
            }
            return 0;            
        }

        public int GetDefaultSpawnTime()
        {
            // If map is Eastern Kingdoms, Kalimdor, Outland, Northrend or Ebon Hold use a lower respawn time
            // TODO: Rank and if npc is needed for quest kill should change spawntime as well
            return (Map == 0 || Map == 1 || Map == 530 || Map == 571 || Map == 609) ? 120 : 7200;
        }

        public virtual void LoadValuesFromUpdateFields() { }
    }
}
