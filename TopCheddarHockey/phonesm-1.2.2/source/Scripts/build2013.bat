%~dp0..\Source\.nuget\NuGet.exe restore %~dp0..\Source\HlsPlayer.VS2013.sln

msbuild /m %~dp0build2013.proj
