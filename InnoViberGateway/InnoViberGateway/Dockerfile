#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 49698

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY . .
RUN dotnet restore "InnoViberGateway.csproj"
RUN dotnet build --no-restore -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InnoViberGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /app/certificates
COPY ./certificates/aspnetapp.pfx /app/certificates
ENV ASPNETCORE_Kestrel__Certificates__Default__Password="leeyaoi"
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/certificates/aspnetapp.pfx

ENTRYPOINT ["dotnet", "InnoViberGateway.dll"]