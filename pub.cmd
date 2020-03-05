@echo off
dotnet clean --nologo -v m

dotnet restore -v m

dotnet build --nologo -v m --no-restore

dotnet publish --nologo -v m -c release -r linux-x64 -o ..\madhonpub --self-contained=false --no-build
