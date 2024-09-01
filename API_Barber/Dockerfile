# Fase base usada para execução
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Fase de build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiando os arquivos .csproj e restaurando as dependências
COPY ["API_Barber/Barber.API.csproj", "API_Barber/"]
COPY ["Barber.Application/Barber.Application.csproj", "Barber.Application/"]
COPY ["Barber.Domain/Barber.Domain.csproj", "Barber.Domain/"]
COPY ["Barber.Infrastructure.IoC/Barber.Infrastructure.IoC.csproj", "Barber.Infrastructure.IoC/"]
COPY ["Barber.Infrastructure.Data/Barber.Infrastructure.Data.csproj", "Barber.Infrastructure.Data/"]
# Não é necessário copiar os projetos de teste para o build de produção
# COPY ["Barber.Tests/Barber.Tests.csproj", "Barber.Tests/"]

RUN dotnet restore "./API_Barber/Barber.API.csproj"

# Copiando o restante do código e buildando a aplicação
COPY . .
WORKDIR "/src/API_Barber"
RUN dotnet build "./Barber.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Fase de publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Barber.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish


# Fase final de execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Barber.API.dll"]
