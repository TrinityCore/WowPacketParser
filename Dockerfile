FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet build -c Release

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /src/WowPacketParser/bin/Release .
COPY --chmod=755 ./docker-entrypoint.sh /
ENTRYPOINT ["/docker-entrypoint.sh"]
