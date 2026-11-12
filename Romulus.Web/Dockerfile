ARG VERSION=11.0-resolute-chiseled
ARG SDKVERSION=11.0-resolute

FROM mcr.microsoft.com/dotnet/sdk:$SDKVERSION  AS publish

WORKDIR /src

COPY ["Romulus.Web.csproj", "Romulus.Web.csproj"]

RUN dotnet restore "Romulus.Web.csproj"
COPY . .

RUN dotnet publish "Romulus.Web.csproj" \
  --runtime linux-x64 \
  --self-contained true \
  -c Release \
  -p:PublishSingleFile=true \
  --no-restore \
  -o /app/publish


FROM  mcr.microsoft.com/dotnet/runtime-deps:$VERSION

USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_HTTP_PORTS=8080 \
  ASPNETCORE_ENVIRONMENT="Production"

EXPOSE 8080
ENTRYPOINT ["./Romulus.Web"]
