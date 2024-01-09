FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CribQuest.Backend/CribQuest.Backend.csproj", "CribQuest.Backend/"]
RUN dotnet restore "CribQuest.Backend/CribQuest.Backend.csproj"
COPY . .
WORKDIR "/src/CribQuest.Backend"
RUN dotnet build "CribQuest.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CribQuest.Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CribQuest.Backend.dll"]
