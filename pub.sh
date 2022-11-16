

dotnet clean --nologo -v m

dotnet restore -v m

dotnet build --configuration -c Release --runtime linux-x64 --self-contained true --no-restore -p:PublishSingleFile=true -p:ImportByWildcardBeforeSolution=false

dotnet publish --runtime linux-x64 --self-contained true -c Release /p:PublishSingleFile=true -p:PublishTrimmed=True -p:TrimMode=CopyUsed -p:_TrimmerDefaultAction=copy --output /tmp/madhonpub


