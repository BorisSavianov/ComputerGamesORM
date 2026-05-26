FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# This container target is for the legacy console interface only.
# The production WinForms UI is an interactive Windows desktop app and is run on Windows.
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore src/ComputerGamesORM.Presentation/ComputerGamesORM.Presentation.csproj

COPY . .
RUN dotnet publish src/ComputerGamesORM.Presentation/ComputerGamesORM.Presentation.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ComputerGamesORM.Presentation.dll"]
