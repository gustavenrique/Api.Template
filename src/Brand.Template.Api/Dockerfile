#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["./src/Brand.Template.Api/Brand.Template.Api.csproj", "./Brand.Template.Api/"]
COPY ["./src/Brand.Template.Application/Brand.Template.Application.csproj", "./Brand.Template.Application/"]
COPY ["./src/Brand.Template.Domain/Brand.Template.Domain.csproj", "./Brand.Template.Domain/"]
COPY ["./src/Brand.Template.Infra/Brand.Template.Infra.csproj", "./Brand.Template.Infra/"]
COPY ["./src/Brand.SharedKernel/Brand.SharedKernel.csproj" "./Brand.SharedKernel/"]
COPY ./Directory.Build.props ./
COPY ./.editorconfig ./

RUN dotnet restore "./Brand.Template.Api/Brand.Template.Api.csproj"

COPY ./src/ ./

WORKDIR "/src/Brand.Template.Api"

RUN dotnet build "./Brand.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# TODO: adicionar dotnet test

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG KEYVAULT_URL

ENV KeyVault__Url=$KEYVAULT_URL

RUN dotnet publish "./Brand.Template.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

EXPOSE 8080
EXPOSE 8081

USER app
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Brand.Template.Api.dll"]