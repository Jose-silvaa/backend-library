FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar csprojs
COPY ["library.Application/library.Application.csproj", "library.Application/"]
COPY ["library.API/library.API.csproj", "library.API/"]
COPY ["library.Domain/library.Domain.csproj", "library.Domain/"]

RUN dotnet restore "library.Application/library.Application.csproj"

# Copiar tudo
COPY . .

# Instalar EF CLI (somente no SDK)
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Build
WORKDIR "/src/library.Application"
RUN dotnet build "library.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação + Migrations
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# Rodar migrations no build (seguro)
RUN dotnet ef database update

RUN dotnet publish "library.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "library.Application.dll"]

