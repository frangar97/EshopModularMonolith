# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Bootstrapper/API/API.csproj", "Bootstrapper/API/"]
COPY ["Modules/Basket/Basket/Basket.csproj", "Modules/Basket/Basket/"]
COPY ["Shared/Shared/Shared.csproj", "Shared/Shared/"]
COPY ["Modules/Catalog/Catalog/Catalog.csproj", "Modules/Catalog/Catalog/"]
COPY ["Modules/Ordering/Ordering/Ordering.csproj", "Modules/Ordering/Ordering/"]
RUN dotnet restore "./Bootstrapper/API/API.csproj"
COPY . .
WORKDIR "/src/Bootstrapper/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]