FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
#EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExchangeRates.API/ExchangeRates.API.csproj", "ExchangeRates.API/"]
RUN dotnet restore "ExchangeRates.API/ExchangeRates.API.csproj"
COPY . .
WORKDIR "/src/ExchangeRates.API"
RUN dotnet build "ExchangeRates.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExchangeRates.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeRates.API.dll"]