# Usando a imagem base do .NET runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base  
WORKDIR /app

# Usando a imagem SDK do .NET para a construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copie a solução .sln e os arquivos dos projetos
COPY ["Api.Certification.Issuer.sln", "./"]  # Ajuste o nome para sua solução real

# Copie os projetos (controller e as dependências/bibliotecas) para o contêiner
COPY ["Api.Certification.AppDomain/Api.Certification.AppDomain.csproj", "Api.Certification.AppDomain/"]
COPY ["Api.Certification.Controller/Api.Certification.Controller.csproj", "Api.Certification.Controller/"]
COPY ["Api.Certification.Infra/Api.Certification.Infra.csproj", "Api.Certification.Infra/"]

# Restaure as dependências da solução
RUN dotnet restore "Api.Certification.Issuer.sln"

# Copie todos os arquivos restantes (código-fonte, dependências, etc.)
COPY . .

# Compile a solução completa
RUN dotnet build "Api.Certification.Issuer.sln" -c $BUILD_CONFIGURATION -o /app/build

# Publique a aplicação final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Api.Certification.Issuer.sln" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Etapa final: copia a aplicação publicada para a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Define o ponto de entrada para a aplicação
ENTRYPOINT ["dotnet", "Api.Certification.Controller.dll"]
