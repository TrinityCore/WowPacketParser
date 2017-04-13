cov-build --dir cov-int C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe WowPacketParser.sln /t:Rebuild
tar czvf wpp.tgz cov-int
REM upload wpp.tgz to https://scan.coverity.com/projects/2618/builds/new?tab=upload
