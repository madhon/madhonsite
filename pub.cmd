@echo off
dotnet clean --nologo -v m

dotnet restore -v m

dotnet build --configuration -c Release --runtime win10-x64 --self-contained true --no-restore -p:PublishSingleFile=true -p:ImportByWildcardBeforeSolution=false

dotnet publish --runtime win10-x64  --self-contained true -c Release /p:PublishSingleFile=true -p:PublishTrimmed=True -p:TrimMode=CopyUsed --output ..\madhonpub


rem dotnet publish --no-restore --no-build --configuration Release --self-contained=false --output ..\madhonpub
