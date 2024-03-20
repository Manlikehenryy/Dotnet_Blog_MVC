# Use the .NET 6.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Copy the migration script
COPY rundb-migrations.sh /app
RUN chmod +x /app/rundb-migrations.sh

# Set the script as the entry point
ENTRYPOINT ["dotnet", "mvc.dll"]
