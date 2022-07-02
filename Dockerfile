FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
COPY . ./
#WORKDIR /src/ExchangeRates.API
#RUN dotnet build "ExchangeRates.API.csproj" -c Release -o /app/build
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS release
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ExchangeRates.API.dll
