FROM mono:4.8.0.524-onbuild

ENTRYPOINT [ "mono", "WowPacketParser.exe" ]
