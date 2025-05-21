FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5196

ENV ASPNETCORE_URLS=http://+:5196

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["eCommerce.API/eCommerce.API.csproj", "eCommerce.API/"]
RUN dotnet restore "eCommerce.API/eCommerce.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.API"
RUN dotnet build "eCommerce.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "eCommerce.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.API.dll"]
