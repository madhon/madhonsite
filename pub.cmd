@echo off
dotnet clean --nologo -v m

dotnet restore -v m

dotnet build --configuration Release --no-restore

dotnet publish --no-restore --no-build --configuration Release --self-contained=false --output ..\madhonpub
