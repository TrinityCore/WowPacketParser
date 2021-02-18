FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app/src
COPY . .
RUN dotnet build "WowPacketParser.sln" -c Release

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS final
WORKDIR /usr/src/app/build
COPY --from=build /app/src/WowPacketParser/bin/Release .
ENTRYPOINT ["dotnet", "WowPacketParser.dll"]
