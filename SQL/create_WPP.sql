-- Create DB
CREATE DATABASE `WPP`;

-- Table structure for table `SniffData`
CREATE TABLE IF NOT EXISTS `WPP`.`SniffData` (
  `Build` INT(10) UNSIGNED NOT NULL DEFAULT '0',
  `SniffName` TEXT NOT NULL,
  `ObjectType` ENUM('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player', 'Achievement') NOT NULL DEFAULT 'None', -- StoreNameType.cs enum
  `Id` INT(10) NOT NULL DEFAULT '0',
  `Data` TEXT NOT NULL,
  UNIQUE KEY `SniffName` (`ObjectType`,`Id`,`Data`(255))
) ENGINE=INNODB DEFAULT CHARSET=latin1;

-- Table structure for table `ObjectNames`
CREATE TABLE IF NOT EXISTS `WPP`.`ObjectNames` (
  `ObjectType` enum('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','CreatureDifficulty','Item','Quest','Opcode','PageText','NpcText','BroadcastText','Gossip','Zone','Area','AreaTrigger','Phase','Player', 'Achievement') NOT NULL DEFAULT 'None', -- StoreNameType.cs enum
  `Id` int(10) NOT NULL,
  `Name` text NOT NULL,
  PRIMARY KEY (`ObjectType`,`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Table structure for table `map_difficulty`
CREATE TABLE `map_difficulty` (
  `ID` int(11) NOT NULL DEFAULT '0',
  `MapID` int(11) NOT NULL DEFAULT '0',
  `DifficultyID` int(11) NOT NULL DEFAULT '0',
  `VerifiedBuild` smallint(5) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
