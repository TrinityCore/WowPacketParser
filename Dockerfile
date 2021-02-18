FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app/src
COPY . .
RUN dotnet build "WowPacketParser.sln" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/runtime:5.0 as final
WORKDIR /usr/src/app/build
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "WowPacketParser.dll"]
