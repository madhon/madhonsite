ARG VERSION=9.0-noble-chiseled
ARG SDKVERSION=9.0-noble

FROM mcr.microsoft.com/dotnet/sdk:$SDKVERSION  AS build
WORKDIR /src

COPY ["Romulus.Web.csproj", "Romulus.Web.csproj"]

RUN dotnet restore "Romulus.Web.csproj"
COPY . .

WORKDIR /src
RUN dotnet build "Romulus.Web.csproj" -c Release --runtime linux-x64 --self-contained true --no-restore -p:PublishSingleFile=true -o /app/build

FROM build AS publish
RUN dotnet publish \
  "Romulus.Web.csproj" \
  --runtime linux-x64\
  --self-contained true \
  -c Release \
  /p:PublishSingleFile=true \
  -o /app/publish


FROM  mcr.microsoft.com/dotnet/runtime-deps:$VERSION
#HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --silent --fail http://localhost:8080/healthz || exit 1

USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_HTTP_PORTS=8080 \
  ASPNETCORE_ENVIRONMENT="Production"

EXPOSE 8080
ENTRYPOINT ["./Romulus.Web"]
