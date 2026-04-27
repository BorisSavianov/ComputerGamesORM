FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy csproj and restore as distinct layers
COPY src/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file src/${file%.*}/; done
RUN dotnet restore src/ComputerGamesORM.Presentation/ComputerGamesORM.Presentation.csproj

# Copy everything else and build
COPY . .
RUN dotnet publish src/ComputerGamesORM.Presentation/ComputerGamesORM.Presentation.csproj -c Release -o /app

# Final stage/image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ComputerGamesORM.Presentation.dll"]
