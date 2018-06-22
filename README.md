WowPacketParser (WPP)
=====================

[![GitHub license](https://img.shields.io/github/license/TrinityCore/WowPacketParser.svg?style=flat-square)](https://github.com/TrinityCore/WowPacketParser/blob/WowPacketParser/COPYING)
[![Coverity Scan Build Status](https://img.shields.io/coverity/scan/2618.svg?style=flat-square)](https://scan.coverity.com/projects/2618)
[![Build Status TravisCI](https://img.shields.io/travis/TrinityCore/WowPacketParser/master.svg?style=flat-square)](https://travis-ci.org/TrinityCore/WowPacketParser)
[![Build Status AppVeyor](https://img.shields.io/appveyor/ci/DDuarte/wowpacketparser-191/master.svg?style=flat-square)](https://ci.appveyor.com/project/DDuarte/wowpacketparser-191)
[![Docker Pulls](https://img.shields.io/docker/pulls/trinitycore/wpp.svg?style=flat-square)](https://hub.docker.com/r/trinitycore/wpp/)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/0154074816634467883a601e6c23188c)](https://www.codacy.com/app/TrinityCore/WowPacketParser?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=TrinityCore/WowPacketParser&amp;utm_campaign=Badge_Grade)

Usage
-----

* Compile WowPacketParser using Visual Studio 2015 or higher (Windows) or Mono 4.4 or higher (Linux/OSX).
  Alternatively you can download compiled binaries from the links [below](#nightly-builds).
* Edit `WowPacketParser.exe.config` to fit your needs.
* Drag one or more files (.pkt or .bin) to `WowPacketParser.exe`.
* Command line usage: `WowPacketParser.exe [--ConfigFile path --Option1 value1 ...] filetoparse1 ...`

##### Databases

Optionally, WPP can connect to two kinds of MySQL databases: `world` from [TrinityCore](https://github.com/TrinityCore/TrinityCore)
and its own database, `WPP`. This can be enabled by setting `<add key="DBEnabled" value="true" />`
in the `.config` file. Remember to set `<add key="TargetedDatabase" value="1"/>` in accordance with the targeted version of the core. 

The `world` database is used when creating SQL files after parsing to produce the minimum number of
changes needed to update the database. For example, if only the faction of a creature that appears
in the sniff needs to be updated, the produced SQL files will contain an UPDATE query, instead of
a full INSERT to the table `creature_template`. WPP does not modify this database directly, all the
tentative changes will be written to the output SQL files.

The `WPP` database is used to feed additional data that WPP may use while parsing. For example, in
the output text files, the spell name can be displayed next to spell ids:

> ServerToClient: SMSG_SPELL_START (0x2BB8) Length: 96 ConnIdx: 0 Time: 01/01/2016 00:22:33.235 Number: 701  
> (Cast) CasterGUID: Full: 0x03691F00000000000000000000000001 Player/0 R3558/S0 Map: 0 Low: 1  
> (Cast) SpellID: **2479 (Honorless Target)**  
> (Cast) CastFlags: 15

This is available for a lot of other named entities (achievements, creatures, quests, etc.). The SQL
files required for this database is in the `SQL` directory. `create_WPP.sql` creates the database
and `wpp_data_objectnames.sql` has some data to fill the database.

Nightly Builds
--------------

##### .NET 4.5 (AnyCPU) by AppVeyor

- Visual Studio 2015
  - [Debug](https://ci.appveyor.com/api/projects/DDuarte/wowpacketparser-191/artifacts/WowPacketParser/WPP.zip?job=Image:%20Visual%20Studio%202015;%20Configuration:%20Debug&branch=master)
  - [Release](https://ci.appveyor.com/api/projects/DDuarte/wowpacketparser-191/artifacts/WowPacketParser/WPP.zip?job=Image:%20Visual%20Studio%202015;%20Configuration:%20Release&branch=master)

- Visual Studio 2017
  - [Debug](https://ci.appveyor.com/api/projects/DDuarte/wowpacketparser-191/artifacts/WowPacketParser/WPP.zip?job=Image:%20Visual%20Studio%202017;%20Configuration:%20Debug&branch=master)
  - [Release](https://ci.appveyor.com/api/projects/DDuarte/wowpacketparser-191/artifacts/WowPacketParser/WPP.zip?job=Image:%20Visual%20Studio%202017;%20Configuration:%20Release&branch=master)
  
Docker (experimental)
---------------------

It is possible run WPP on Docker using the `trinitycore/wpp` image:

```
docker run -v /place/where/sniffs/are/kept:/usr/src/app/build/sniffs trinitycore/wpp sniffs/sniffname.pkt
```

*/place/where/sniffs/are/kept* should your local directory containing the .pkt file and *sniffname.pkt* the file to be parsed.

Output (.txt/.sql) of the parser will be added to */place/where/sniffs/are/kept*.
