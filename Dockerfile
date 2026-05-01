FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/FinCashly.API/FinCashly.API.csproj src/FinCashly.API/
COPY src/FinCashly.Application/FinCashly.Application.csproj src/FinCashly.Application/
COPY src/FinCashly.Domain/FinCashly.Domain.csproj src/FinCashly.Domain/
COPY src/FinCashly.Infrastructure/FinCashly.Infrastructure.csproj src/FinCashly.Infrastructure/

RUN dotnet restore src/FinCashly.API/FinCashly.API.csproj

COPY src/ ./src/

WORKDIR /src/src/FinCashly.API

RUN dotnet publish FinCashly.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinCashly.API.dll"]