@echo off
del src\packages.lock.json
dotnet publish -r win-x64 -c Release /p:RestoreLockedMode=true --self-contained AvaloniaApp.csproj