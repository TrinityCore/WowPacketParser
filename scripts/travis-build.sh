#!/bin/bash

set -ev

if [ "${DOTNETCORE}" == "1" ]; then
    dotnet restore;
    dotnet build;
else
    xbuild /p:TargetFrameworkVersion="v4.6";
    mono ./testrunner/NUnit.ConsoleRunner.3.6.1/tools/nunit3-console.exe ./WowPacketParser.Tests/bin/Debug/WowPacketParser.Tests.dll
fi

exit 0;
