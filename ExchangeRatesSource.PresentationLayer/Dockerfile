FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ExchangeRatesSource.PresentationLayer/ExchangeRatesSource.PresentationLayer.csproj", "ExchangeRatesSource.PresentationLayer/"]
RUN dotnet restore "ExchangeRatesSource.PresentationLayer/ExchangeRatesSource.PresentationLayer.csproj"
COPY . .
WORKDIR "/src/ExchangeRatesSource.PresentationLayer"
RUN dotnet build "ExchangeRatesSource.PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExchangeRatesSource.PresentationLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExchangeRatesSource.PresentationLayer.dll"]
