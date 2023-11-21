ARG VERSION=8.0-alpine

FROM mcr.microsoft.com/dotnet/sdk:$VERSION  AS build
WORKDIR /src

COPY ["Romulus.Web.csproj", "Romulus.Web.csproj"]

RUN dotnet restore "Romulus.Web.csproj"
COPY . .

WORKDIR /src
RUN dotnet build "Romulus.Web.csproj" -c Release --runtime linux-musl-x64 --self-contained true --no-restore -p:PublishSingleFile=true -o /app/build

FROM build AS publish
RUN dotnet publish \
  "Romulus.Web.csproj" \
  --runtime linux-musl-x64\
  --self-contained true \
  -c Release \
  /p:PublishSingleFile=true -p:PublishTrimmed=True -p:TrimMode=CopyUsed -p:_TrimmerDefaultAction=copy \
  -o /app/publish


FROM  mcr.microsoft.com/dotnet/runtime-deps:$VERSION
RUN addgroup -g 2000 romulus \
  && adduser \
  --disabled-password \
  --home /romulus \
  --uid 1000 --ingroup romulus \
  --gecos '' romulus \
  && chown -R romulus /romulus \
  && apk add --upgrade --no-cache apk-tools curl \
  && apk add --no-cache icu-libs tzdata libsecret

HEALTHCHECK --interval=5s --timeout=10s --retries=3 CMD curl --silent --fail http://localhost:8080/healthz || exit 1

USER romulus
WORKDIR /romulus
COPY --from=publish /app/publish .
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1 \
  DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_HTTP_PORTS=8080 \
  ASPNETCORE_ENVIRONMENT="Production"

EXPOSE 8080
ENTRYPOINT ["./Romulus.Web"]
