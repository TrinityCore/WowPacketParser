#!/bin/bash

set -ev

if [ ${DOTNETCORE} = "1" ]; then
    dotnet restore;
    dotnet build;
else
    xbuild /p:TargetFrameworkVersion="v4.6";
fi

exit 0;
