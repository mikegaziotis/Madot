FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["source/shell/Badop.Shell.API/Badop.Shell.API.csproj", "source/shell/Badop.Shell.API/"]
RUN dotnet restore "source/shell/Badop.Shell.API/Badop.Shell.API.csproj"
COPY . .
WORKDIR "/src/source/shell/Badop.Shell.API"
RUN dotnet build "Badop.Shell.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Badop.Shell.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Badop.Shell.API.dll"]
