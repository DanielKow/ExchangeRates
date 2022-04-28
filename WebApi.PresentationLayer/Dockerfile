FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["WebApi.PresentationLayer/WebApi.PresentationLayer.csproj", "WebApi.PresentationLayer/"]
RUN dotnet restore "WebApi.PresentationLayer/WebApi.PresentationLayer.csproj"
COPY . .
WORKDIR "/src/WebApi.PresentationLayer"
RUN dotnet build "WebApi.PresentationLayer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.PresentationLayer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.PresentationLayer.dll"]
