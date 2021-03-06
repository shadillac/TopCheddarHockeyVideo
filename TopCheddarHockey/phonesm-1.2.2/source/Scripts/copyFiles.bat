pushd "%~dp0..\Source\"

copy Libraries\SM.Media\bin\Debug\SM.Media.dll ..\Distribution\bin\Debug\
copy Libraries\SM.Media\bin\Debug\SM.Media.pdb ..\Distribution\bin\Debug\
copy Libraries\SM.Media\bin\Debug\SM.TsParser.dll ..\Distribution\bin\Debug\
copy Libraries\SM.Media\bin\Debug\SM.TsParser.pdb ..\Distribution\bin\Debug\
copy Libraries\SM.Media\bin\Release\SM.Media.dll ..\Distribution\bin\Release\
copy Libraries\SM.Media\bin\Release\SM.Media.pdb ..\Distribution\bin\Release\
copy Libraries\SM.Media\bin\Release\SM.TsParser.dll ..\Distribution\bin\Release\
copy Libraries\SM.Media\bin\Release\SM.TsParser.pdb ..\Distribution\bin\Release\

copy Libraries\SM.Media.Legacy\bin\Debug\SM.Media.Legacy.dll ..\Distribution\bin\Debug\
copy Libraries\SM.Media.Legacy\bin\Debug\SM.Media.Legacy.pdb ..\Distribution\bin\Debug\
copy Libraries\SM.Media.Legacy\bin\Debug\SM.TsParser.Legacy.dll ..\Distribution\bin\Debug\
copy Libraries\SM.Media.Legacy\bin\Debug\SM.TsParser.Legacy.pdb ..\Distribution\bin\Debug\
copy Libraries\SM.Media.Legacy\bin\Release\SM.Media.Legacy.dll ..\Distribution\bin\Release\
copy Libraries\SM.Media.Legacy\bin\Release\SM.Media.Legacy.pdb ..\Distribution\bin\Release\
copy Libraries\SM.Media.Legacy\bin\Release\SM.TsParser.Legacy.dll ..\Distribution\bin\Release\
copy Libraries\SM.Media.Legacy\bin\Release\SM.TsParser.Legacy.pdb ..\Distribution\bin\Release\

copy Libraries\SM.Media.Platform.WP7\bin\Debug\SM.Media.Platform.WP7.dll ..\Distribution\bin\Debug\WP7\
copy Libraries\SM.Media.Platform.WP7\bin\Debug\SM.Media.Platform.WP7.pdb ..\Distribution\bin\Debug\WP7\
copy Libraries\SM.Media.Platform.WP8\bin\ARM\Debug\SM.Media.Platform.WP8.dll ..\Distribution\bin\Debug\WP8\ARM\
copy Libraries\SM.Media.Platform.WP8\bin\ARM\Debug\SM.Media.Platform.WP8.pdb ..\Distribution\bin\Debug\WP8\ARM\
copy Libraries\SM.Media.Platform.WP8\bin\x86\Debug\SM.Media.Platform.WP8.dll ..\Distribution\bin\Debug\WP8\x86\
copy Libraries\SM.Media.Platform.WP8\bin\x86\Debug\SM.Media.Platform.WP8.pdb ..\Distribution\bin\Debug\WP8\x86\
copy Libraries\SM.Media.Platform.Silverlight\bin\Debug\SM.Media.Platform.Silverlight.dll ..\Distribution\bin\Debug\Silverlight\
copy Libraries\SM.Media.Platform.Silverlight\bin\Debug\SM.Media.Platform.Silverlight.pdb ..\Distribution\bin\Debug\Silverlight\
copy bin\ARM\Debug\SM.Media.Platform.Win81.dll ..\Distribution\bin\Debug\WinRT\ARM\
copy bin\ARM\Debug\SM.Media.Platform.Win81.pdb ..\Distribution\bin\Debug\WinRT\ARM\
copy bin\x86\Debug\SM.Media.Platform.Win81.dll ..\Distribution\bin\Debug\WinRT\x86\
copy bin\x86\Debug\SM.Media.Platform.Win81.pdb ..\Distribution\bin\Debug\WinRT\x86\
copy bin\x64\Debug\SM.Media.Platform.Win81.dll ..\Distribution\bin\Debug\WinRT\x64\
copy bin\x64\Debug\SM.Media.Platform.Win81.pdb ..\Distribution\bin\Debug\WinRT\x64\

copy Libraries\SM.Media.Platform.WP7\bin\Release\SM.Media.Platform.WP7.dll ..\Distribution\bin\Release\WP7\
copy Libraries\SM.Media.Platform.WP7\bin\Release\SM.Media.Platform.WP7.pdb ..\Distribution\bin\Release\WP7\
copy Libraries\SM.Media.Platform.WP8\bin\ARM\Release\SM.Media.Platform.WP8.dll ..\Distribution\bin\Release\WP8\ARM\
copy Libraries\SM.Media.Platform.WP8\bin\ARM\Release\SM.Media.Platform.WP8.pdb ..\Distribution\bin\Release\WP8\ARM\
copy Libraries\SM.Media.Platform.WP8\bin\x86\Release\SM.Media.Platform.WP8.dll ..\Distribution\bin\Release\WP8\x86\
copy Libraries\SM.Media.Platform.WP8\bin\x86\Release\SM.Media.Platform.WP8.pdb ..\Distribution\bin\Release\WP8\x86\
copy Libraries\SM.Media.Platform.Silverlight\bin\Release\SM.Media.Platform.Silverlight.dll ..\Distribution\bin\Release\Silverlight\
copy Libraries\SM.Media.Platform.Silverlight\bin\Release\SM.Media.Platform.Silverlight.pdb ..\Distribution\bin\Release\Silverlight\
copy bin\ARM\Release\SM.Media.Platform.Win81.dll ..\Distribution\bin\Release\WinRT\ARM\
copy bin\ARM\Release\SM.Media.Platform.Win81.pdb ..\Distribution\bin\Release\WinRT\ARM\
copy bin\x86\Release\SM.Media.Platform.Win81.dll ..\Distribution\bin\Release\WinRT\x86\
copy bin\x86\Release\SM.Media.Platform.Win81.pdb ..\Distribution\bin\Release\WinRT\x86\
copy bin\x64\Release\SM.Media.Platform.Win81.dll ..\Distribution\bin\Release\WinRT\x64\
copy bin\x64\Release\SM.Media.Platform.Win81.pdb ..\Distribution\bin\Release\WinRT\x64\

copy Libraries\SM.Media.Builder\bin\Debug\SM.Media.Builder.dll ..\Distribution\bin\Debug\
copy Libraries\SM.Media.Builder\bin\Debug\SM.Media.Builder.pdb ..\Distribution\bin\Debug\
copy Libraries\SM.Media.Builder\bin\Release\SM.Media.Builder.dll ..\Distribution\bin\Release\
copy Libraries\SM.Media.Builder\bin\Release\SM.Media.Builder.pdb ..\Distribution\bin\Release\

copy Global\GlobalAssemblyInfo.cs ..\Distribution\Global\
copy Global\SM.MediaVersion.cs ..\Distribution\Global\
copy Global\readme.txt ..\Distribution\
copy Global\LICENSE.txt ..\Distribution\

copy Phone\HlsView\*.xaml ..\Distribution\Phone\HlsView\
copy Phone\HlsView\*.cs  ..\Distribution\Phone\HlsView\
copy Phone\HlsView\app.config  ..\Distribution\Phone\HlsView\
copy Phone\HlsView\*.png  ..\Distribution\Phone\HlsView\
copy Phone\HlsView\*.jpg  ..\Distribution\Phone\HlsView\
copy Phone\HlsView\HlsView.csproj  ..\Distribution\Phone\HlsView\
copy Phone\HlsView\packages.config ..\Distribution\Phone\HlsView\
copy Phone\HlsView\Properties\*.cs ..\Distribution\Phone\HlsView\Properties\
copy Phone\HlsView\Properties\*.xml ..\Distribution\Phone\HlsView\Properties\

copy Phone\HlsView.WP8\*.xaml ..\Distribution\Phone\HlsView.WP8\
copy Phone\HlsView.WP8\*.cs  ..\Distribution\Phone\HlsView.WP8\
copy Phone\HlsView.WP8\Assets\*.png  ..\Distribution\Phone\HlsView.WP8\Assets\
copy Phone\HlsView.WP8\Assets\Tiles\*.png  ..\Distribution\Phone\HlsView.WP8\Assets\Tiles\
copy Phone\HlsView.WP8\HlsView.WP8.csproj  ..\Distribution\Phone\HlsView.WP8\
copy Phone\HlsView.WP8\packages.config ..\Distribution\Phone\HlsView.WP8\
copy Phone\HlsView.WP8\Properties\*.cs ..\Distribution\Phone\HlsView.WP8\Properties\
copy Phone\HlsView.WP8\Properties\*.xml ..\Distribution\Phone\HlsView.WP8\Properties\
copy Phone\HlsView.WP8\Resources\*.cs ..\Distribution\Phone\HlsView.WP8\Resources\
copy Phone\HlsView.WP8\Resources\*.resx ..\Distribution\Phone\HlsView.WP8\Resources\

copy Libraries\SM.Media.Platform.WP7\MediaElementManager.cs ..\Distribution\Libraries\SM.Media.Platform.WP7\

copy Libraries\SM.Media.MediaPlayer.WP7\*.config ..\Distribution\Libraries\SM.Media.MediaPlayer.WP7\
copy Libraries\SM.Media.MediaPlayer.WP7\*.cs ..\Distribution\Libraries\SM.Media.MediaPlayer.WP7\
copy Libraries\SM.Media.MediaPlayer.WP7\SM.Media.MediaPlayer.WP7.csproj ..\Distribution\Libraries\SM.Media.MediaPlayer.WP7\
copy Libraries\SM.Media.MediaPlayer.WP7\Properties\*.cs ..\Distribution\Libraries\SM.Media.MediaPlayer.WP7\Properties\

copy Libraries\SM.Media.MediaPlayer.WP8\*.config ..\Distribution\Libraries\SM.Media.MediaPlayer.WP8\
copy Libraries\SM.Media.MediaPlayer.WP8\*.cs ..\Distribution\Libraries\SM.Media.MediaPlayer.WP8\
copy Libraries\SM.Media.MediaPlayer.WP8\SM.Media.MediaPlayer.WP8.csproj ..\Distribution\Libraries\SM.Media.MediaPlayer.WP8\
copy Libraries\SM.Media.MediaPlayer.WP8\Properties\*.cs ..\Distribution\Libraries\SM.Media.MediaPlayer.WP8\Properties\

copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\packages.config ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\
copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\*.cs ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\
copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\SM.Media.BackgroundAudioStreamingAgent.WP7.csproj ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\
copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\Properties\*.cs ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP7\Properties\

copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\packages.config ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\
copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\*.cs ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\
copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\SM.Media.BackgroundAudioStreamingAgent.WP8.csproj ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\
copy Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\Properties\*.cs ..\Distribution\Libraries\SM.Media.BackgroundAudioStreamingAgent.WP8\Properties\

copy Phone\SamplePlayer.WP7\*.xaml ..\Distribution\Phone\SamplePlayer.WP7\
copy Phone\SamplePlayer.WP7\*.cs  ..\Distribution\Phone\SamplePlayer.WP7\
copy Phone\SamplePlayer.WP7\*.png  ..\Distribution\Phone\SamplePlayer.WP7\
copy Phone\SamplePlayer.WP7\*.jpg  ..\Distribution\Phone\SamplePlayer.WP7\
copy Phone\SamplePlayer.WP7\SamplePlayer.WP7.csproj  ..\Distribution\Phone\SamplePlayer.WP7\
copy Phone\SamplePlayer.WP7\packages.config ..\Distribution\Phone\SamplePlayer.WP7\
copy Phone\SamplePlayer.WP7\Properties\*.cs ..\Distribution\Phone\SamplePlayer.WP7\Properties\
copy Phone\SamplePlayer.WP7\Properties\*.xml ..\Distribution\Phone\SamplePlayer.WP7\Properties\

copy Phone\SamplePlayer.WP8\*.xaml ..\Distribution\Phone\SamplePlayer.WP8\
copy Phone\SamplePlayer.WP8\*.cs  ..\Distribution\Phone\SamplePlayer.WP8\
copy Phone\SamplePlayer.WP8\Assets\*.png  ..\Distribution\Phone\SamplePlayer.WP8\Assets\
copy Phone\SamplePlayer.WP8\Assets\Tiles\*.png  ..\Distribution\Phone\SamplePlayer.WP8\Assets\Tiles\
copy Phone\SamplePlayer.WP8\SamplePlayer.WP8.csproj  ..\Distribution\Phone\SamplePlayer.WP8\
copy Phone\SamplePlayer.WP8\packages.config ..\Distribution\Phone\SamplePlayer.WP8\
copy Phone\SamplePlayer.WP8\Properties\*.cs ..\Distribution\Phone\SamplePlayer.WP8\Properties\
copy Phone\SamplePlayer.WP8\Properties\*.xml ..\Distribution\Phone\SamplePlayer.WP8\Properties\
copy Phone\SamplePlayer.WP8\Resources\*.cs ..\Distribution\Phone\SamplePlayer.WP8\Resources\
copy Phone\SamplePlayer.WP8\Resources\*.resx ..\Distribution\Phone\SamplePlayer.WP8\Resources\

copy Phone\BackgroundAudio.Sample.WP7\*.xaml ..\Distribution\Phone\BackgroundAudio.Sample.WP7\
copy Phone\BackgroundAudio.Sample.WP7\*.cs ..\Distribution\Phone\BackgroundAudio.Sample.WP7\
copy Phone\BackgroundAudio.Sample.WP7\*.jpg ..\Distribution\Phone\BackgroundAudio.Sample.WP7\
copy Phone\BackgroundAudio.Sample.WP7\*.png ..\Distribution\Phone\BackgroundAudio.Sample.WP7\
copy Phone\BackgroundAudio.Sample.WP7\BackgroundAudio.Sample.WP7.csproj ..\Distribution\Phone\BackgroundAudio.Sample.WP7\
copy Phone\BackgroundAudio.Sample.WP7\Images\*.png ..\Distribution\Phone\BackgroundAudio.Sample.WP7\Images\
copy Phone\BackgroundAudio.Sample.WP7\packages.config ..\Distribution\Phone\BackgroundAudio.Sample.WP7\
copy Phone\BackgroundAudio.Sample.WP7\Properties\*.cs ..\Distribution\Phone\BackgroundAudio.Sample.WP7\Properties\
copy Phone\BackgroundAudio.Sample.WP7\Properties\*.xml ..\Distribution\Phone\BackgroundAudio.Sample.WP7\Properties\

copy Phone\BackgroundAudio.Sample.WP8\*.xaml ..\Distribution\Phone\BackgroundAudio.Sample.WP8\
copy Phone\BackgroundAudio.Sample.WP8\*.cs ..\Distribution\Phone\BackgroundAudio.Sample.WP8\
copy Phone\BackgroundAudio.Sample.WP8\Assets\*.png ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Assets\
copy Phone\BackgroundAudio.Sample.WP8\Assets\Tiles\*.png ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Assets\Tiles\
copy Phone\BackgroundAudio.Sample.WP8\Images\*.png ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Images\
copy Phone\BackgroundAudio.Sample.WP8\BackgroundAudio.Sample.WP8.csproj ..\Distribution\Phone\BackgroundAudio.Sample.WP8\
copy Phone\BackgroundAudio.Sample.WP8\packages.config ..\Distribution\Phone\BackgroundAudio.Sample.WP8\
copy Phone\BackgroundAudio.Sample.WP8\Properties\*.cs ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Properties\
copy Phone\BackgroundAudio.Sample.WP8\Properties\*.xml ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Properties\
copy Phone\BackgroundAudio.Sample.WP8\Resources\*.cs ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Resources\
copy Phone\BackgroundAudio.Sample.WP8\Resources\*.resx ..\Distribution\Phone\BackgroundAudio.Sample.WP8\Resources\

copy App\WinRT\HlsView.Win81\*.xaml ..\Distribution\App\WinRT\HlsView.Win81\
copy App\WinRT\HlsView.Win81\*.cs ..\Distribution\App\WinRT\HlsView.Win81\
copy App\WinRT\HlsView.Win81\HlsView.Win81.csproj ..\Distribution\App\WinRT\HlsView.Win81\
copy App\WinRT\HlsView.Win81\Assets\*.png ..\Distribution\App\WinRT\HlsView.Win81\Assets\
copy App\WinRT\HlsView.Win81\Properties\*.cs ..\Distribution\App\WinRT\HlsView.Win81\Properties\
copy App\WinRT\HlsView.Win81\HlsView.Win81_TemporaryKey.pfx ..\Distribution\App\WinRT\HlsView.Win81\
copy App\WinRT\HlsView.Win81\Package.appxmanifest ..\Distribution\App\WinRT\HlsView.Win81\
copy App\WinRT\HlsView.Win81\packages.config ..\Distribution\App\WinRT\HlsView.Win81\

copy App\Silverlight\HlsView.Silverlight\*.xaml ..\Distribution\App\Silverlight\HlsView.Silverlight\
copy App\Silverlight\HlsView.Silverlight\*.cs ..\Distribution\App\Silverlight\HlsView.Silverlight\
copy App\Silverlight\HlsView.Silverlight\HlsView.Silverlight.csproj ..\Distribution\App\Silverlight\HlsView.Silverlight\
copy App\Silverlight\HlsView.Silverlight\Properties\*.cs ..\Distribution\App\Silverlight\HlsView.Silverlight\Properties\
copy App\Silverlight\HlsView.Silverlight\Properties\*.xml ..\Distribution\App\Silverlight\HlsView.Silverlight\Properties\
copy App\Silverlight\HlsView.Silverlight\app.config ..\Distribution\App\Silverlight\HlsView.Silverlight\
copy App\Silverlight\HlsView.Silverlight\packages.config ..\Distribution\App\Silverlight\HlsView.Silverlight\

copy .nuget\NuGet.exe ..\Distribution\.nuget\
copy .nuget\NuGet.targets ..\Distribution\.nuget\
copy .nuget\NuGet.config ..\Distribution\.nuget\

copy smf\readme.txt ..\Distribution\smf\

copy Sample*.sln ..\Distribution\

popd
