@echo off
dotnet clean --nologo -v m

dotnet restore -v m

dotnet build --nologo -c release -v m --no-restore

dotnet publish --nologo -v m -c release -o ..\madhonpub --self-contained=false --no-build
