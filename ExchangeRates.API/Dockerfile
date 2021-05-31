FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExchangeRates.API.csproj", "ExchangeRates/"]
RUN dotnet restore "./ExchangeRates/ExchangeRates.API.csproj"
COPY ["." "./ExchangeRates"]
WORKDIR "/src/ExchangeRates"
RUN dotnet build "ExchangeRates.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./ExchangeRates.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ExchangeRates.API.dll