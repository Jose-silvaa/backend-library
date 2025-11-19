FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/library.Application/library.Application.csproj", "library.Application/"]
COPY ["src/library.API/library.API.csproj", "library.API/"]
COPY ["src/library.Domain/library.Domain.csproj", "library.Domain/"]

RUN dotnet restore "library.Application/library.Application.csproj"
COPY . .
WORKDIR "/src/library.Application"
RUN dotnet build "./library.Application.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./library.Application.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet library.Application.dll"]
