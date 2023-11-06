FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["sag.api/sag.api.csproj", "sag.api/"]
COPY ["sag.Application/sag.Application.csproj", "sag.Application/"]
COPY ["sag.Domain/sag.Domain.csproj", "sag.Domain/"]
COPY ["sag.Infrastructure/sag.Infrastructure.csproj", "sag.Infrastructure/"]
COPY ["sag.Persistence/sag.Persistence.csproj", "sag.Persistence/"]
RUN dotnet restore "sag.api/sag.api.csproj"
COPY . .
WORKDIR "/src/sag.api"
RUN dotnet build "sag.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sag.api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sag.api.dll"]
