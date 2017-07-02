FROM mono:5.0.1.1

RUN mkdir -p /usr/src/app/source /usr/src/app/build
WORKDIR /usr/src/app/source

COPY . /usr/src/app/source
RUN nuget restore -NonInteractive
RUN xbuild /property:Configuration=Release /property:OutDir=/usr/src/app/build/
WORKDIR /usr/src/app/build

ENTRYPOINT [ "mono", "WowPacketParser.exe" ]
