# Use the .NET 6.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else
COPY . ./

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy the built files from build-env
COPY --from=build-env /app/out .

# Copy the migration script
COPY rundb-migrations.sh .

# Grant execution permissions to the script
RUN chmod +x rundb-migrations.sh

# Run the migration script during container startup
CMD ["/bin/bash", "rundb-migrations.sh"]

# Set the entry point to start the ASP.NET Core application
ENTRYPOINT ["dotnet", "mvc.dll"]

