﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Migrator/Migrator.csproj", "Migrator/"]
RUN dotnet restore "Migrator/Migrator.csproj"
COPY . .
WORKDIR "/src/Migrator"
RUN dotnet build "Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Migrator.dll"]
