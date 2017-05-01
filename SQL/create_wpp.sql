-- Create DB
CREATE DATABASE `wpp`;

-- Table structure for table `sniff_data`
CREATE TABLE IF NOT EXISTS `wpp`.`sniff_data` (
  `Build` INT(10) UNSIGNED NOT NULL DEFAULT '0',
  `SniffName` TEXT NOT NULL,
  `ObjectType` ENUM('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player', 'Achievement') NOT NULL DEFAULT 'None', -- StoreNameType.cs enum
  `Id` INT(10) NOT NULL DEFAULT '0',
  `Data` TEXT NOT NULL,
  UNIQUE KEY `SniffName` (`ObjectType`,`Id`,`Data`(255))
) ENGINE=INNODB DEFAULT CHARSET=latin1;

-- Table structure for table `object_names`
CREATE TABLE IF NOT EXISTS `wpp`.`object_names` (
  `ObjectType` enum('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player', 'Achievement') NOT NULL DEFAULT 'None', -- StoreNameType.cs enum
  `Id` int(10) NOT NULL,
  `Name` text NOT NULL,
  PRIMARY KEY (`ObjectType`,`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='WPP''s ObjectTypes Names DataBase';
