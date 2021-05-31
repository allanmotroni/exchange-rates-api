FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY *.sln .
COPY ExchangeRates.API/*.csproj ./Exchange.API/
COPY ExchangeRates.Domain/*.csproj ./Exchange.Domain/
COPY ExchangeRates.Infrastructure/*.csproj ./Exchange.Infrastructure/

RUN dotnet restore "./Exchange.API/Exchange.API.csproj"

#COPY . .
#WORKDIR "/src"
#RUN dotnet build "ExchangeRates.API.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "./ExchangeRates.API.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .

#CMD ASPNETCORE_URLS="http://*:$PORT" dotnet ExchangeRates.API.dll