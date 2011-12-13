--
-- Table structure for table `SniffData`
--
 
CREATE TABLE IF NOT EXISTS `SniffData` (
  `Build` INT(10) UNSIGNED NOT NULL DEFAULT '0',
  `SniffName` TEXT NOT NULL,
  `Timestamp` INT(10) NOT NULL DEFAULT '0',
  `ObjectType` ENUM('None','Spell','Map','LFGDungeon','Battleground','Unit','GameObject','Item','Quest','Opcode','PageText','NpcText','Gossip') NOT NULL DEFAULT 'None', -- StoreNameType.cs enum
  `Data1` TEXT NOT NULL,
  `Data2` TEXT NOT NULL,
  UNIQUE KEY `SniffName` (`Timestamp`,`ObjectType`,`Data1`(255),`Data2`(255))
) ENGINE=INNODB DEFAULT CHARSET=latin1;
