FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ExchangeRates.API/ExchangeRates.API.csproj ExchangeRates.API/
COPY ExchangeRates.Domain/ExchangeRates.Domain.csproj ExchangeRates.Domain/
COPY ExchangeRates.Infrastructure/ExchangeRates.Infrastructure.csproj ExchangeRates.Infrastructure/
COPY ExchangeRates.Test/ExchangeRates.Test.csproj ExchangeRates.Test/
RUN dotnet restore
COPY . .
WORKDIR /src/ExchangeRates.API
RUN dotnet build "ExchangeRates.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExchangeRates.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ExchangeRates.API.dll