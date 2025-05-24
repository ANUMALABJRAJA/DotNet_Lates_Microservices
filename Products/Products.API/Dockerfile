FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5236

ENV ASPNETCORE_URLS=http://+:5236

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Products.API/Products.API.csproj", "Products.API/"]
RUN dotnet restore "Products.API/Products.API.csproj"
COPY . .
WORKDIR "/src/Products.API"
RUN dotnet build "Products.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Products.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV MYSQL_HOST=localhost
ENV MYSQL_PASSWORD=admin1234
ENTRYPOINT ["dotnet", "Products.API.dll"]
