@echo off

dotnet publish -r win-x64 -c Release /p:PublishAot=true
