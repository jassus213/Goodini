FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Goodini/Goodini.csproj", "Goodini/"]
COPY ["User.StartUp/User.StartUp.csproj", "User.StartUp/"]
COPY ["User.Dal/User.Dal.csproj", "User.Dal/"]
COPY ["User.Core/User.Core.csproj", "User.Core/"]
COPY ["User.Dal.PostgreSql/User.Dal.PostgreSql.csproj", "User.Dal.PostgreSql/"]
RUN dotnet restore "Goodini/Goodini.csproj"
COPY . .
WORKDIR "/src/Goodini"
RUN dotnet build "Goodini.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Goodini.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Goodini.dll"]
