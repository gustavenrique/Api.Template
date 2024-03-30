#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["./src/RestApi.Template.Api/RestApi.Template.Api.csproj", "./RestApi.Template.Api/"]
COPY ["./src/RestApi.Template.Application/RestApi.Template.Application.csproj", "./RestApi.Template.Application/"]
COPY ["./src/RestApi.Template.Domain/RestApi.Template.Domain.csproj", "./RestApi.Template.Domain/"]
COPY ["./src/RestApi.Template.Infra/RestApi.Template.Infra.csproj", "./RestApi.Template.Infra/"]
COPY ["./src/RestApi.Common/RestApi.Common.csproj" "./RestApi.Common/"]
COPY ./Directory.Build.props ./
COPY ./.editorconfig ./

RUN dotnet restore "./RestApi.Template.Api/RestApi.Template.Api.csproj"

COPY ./src/ ./

WORKDIR "/src/RestApi.Template.Api"

RUN dotnet build "./RestApi.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# TODO: adicionar dotnet test

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG KEYVAULT_URL

ENV KeyVault__Url=$KEYVAULT_URL

RUN dotnet publish "./RestApi.Template.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

EXPOSE 8080
EXPOSE 8081

USER app
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestApi.Template.Api.dll"]