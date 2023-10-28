FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["sag.web/sag.web.csproj", "sag.web/"]
RUN dotnet restore "sag.web/sag.web.csproj"
COPY . .
WORKDIR "/src/sag.web"
RUN dotnet build "sag.web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sag.web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sag.web.dll"]
