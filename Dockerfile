# Use the .NET 6.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:6.0.403 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0.403
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "mvc.dll"]
