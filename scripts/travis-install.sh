#!/bin/bash

set -ev

if [ "${DOTNETCORE}" == "1" ]; then
    true;
else
    nuget restore WowPacketParser.sln;
    nuget install NUnit.Console -Version 3.6.1 -OutputDirectory testrunner
fi

exit 0;
