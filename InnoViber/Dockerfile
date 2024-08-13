#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7060

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InnoViber/InnoViber.API.csproj", "InnoViber/"]
COPY ["InnoViber.BLL/InnoViber.BLL.csproj", "InnoViber.BLL/"]
COPY ["InnoViber.DAL/InnoViber.DAL.csproj", "InnoViber.DAL/"]
COPY ["InnoViber.Domain/InnoViber.Domain.csproj", "InnoViber.Domain/"]
COPY ["InnoViber.User.DAL/InnoViber.User.DAL.csproj", "InnoViber.User.DAL/"]
COPY ["SharedModels/SharedModels.csproj", "SharedModels/"]
COPY ["InnoViber.Test.Service/InnoViber.Test.Service.csproj", "InnoViber.Test.Service/"]
RUN dotnet restore "./InnoViber/InnoViber.API.csproj"
COPY . .
WORKDIR "/src/InnoViber"
RUN dotnet build "./InnoViber.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InnoViber.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InnoViber.API.dll"]