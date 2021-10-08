#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SkeletonFramework.csproj", ""]
RUN dotnet restore "./SkeletonFramework.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SkeletonFramework.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkeletonFramework.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkeletonFramework.dll"]