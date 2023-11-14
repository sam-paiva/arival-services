#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ArivalBankServices.API/ArivalBankServices.API.csproj", "ArivalBankServices.API/"]
COPY ["ArivalBankServices.Application/ArivalBankServices.Application.csproj", "ArivalBankServices.Application/"]
COPY ["ArivalBankServices.Core/ArivalBankServices.Core.csproj", "ArivalBankServices.Core/"]
COPY ["ArivalBankServices.IoC/ArivalBankServices.IoC.csproj", "ArivalBankServices.IoC/"]
COPY ["ArivalBankServices.Infra.DataAccess/ArivalBankServices.Infra.DataAccess.csproj", "ArivalBankServices.Infra.DataAccess/"]
RUN dotnet restore "ArivalBankServices.API/ArivalBankServices.API.csproj"
COPY . .
WORKDIR "/src/ArivalBankServices.API"
RUN dotnet build "ArivalBankServices.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ArivalBankServices.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ArivalBankServices.API.dll"]