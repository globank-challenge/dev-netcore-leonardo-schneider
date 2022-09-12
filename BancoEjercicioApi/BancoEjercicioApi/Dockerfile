#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BancoEjercicioApi/BancoEjercicioApi.WebApi.csproj", "BancoEjercicioApi/"]
COPY ["BancoEjercicioApi.BusinessLogic/BancoEjercicioApi.Services.csproj", "BancoEjercicioApi.BusinessLogic/"]
COPY ["BancoEjercicioApi.Abstractions/BancoEjercicioApi.Abstractions.csproj", "BancoEjercicioApi.Abstractions/"]
COPY ["BancoEjercicioApi.Entities/BancoEjercicioApi.Entities.csproj", "BancoEjercicioApi.Entities/"]
COPY ["BancoEjercicioApi.DataAccess/BancoEjercicioApi.DataAccess.csproj", "BancoEjercicioApi.DataAccess/"]
COPY ["BancoEjercicioApi.Exceptions/BancoEjercicioApi.Exceptions.csproj", "BancoEjercicioApi.Exceptions/"]
RUN dotnet restore "BancoEjercicioApi/BancoEjercicioApi.WebApi.csproj"
COPY . .
WORKDIR "/src/BancoEjercicioApi"
RUN dotnet build "BancoEjercicioApi.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BancoEjercicioApi.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BancoEjercicioApi.WebApi.dll"]