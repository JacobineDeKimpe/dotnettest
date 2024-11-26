#!/bin/bash
set -euo pipefail

#clean up docker
docker system prune -f

mkdir -p tempdir
mkdir -p tempdir/Rise.Client
mkdir -p tempdir/Rise.Client.Tests
mkdir -p tempdir/Rise.Domain
mkdir -p tempdir/Rise.Domain.Tests
mkdir -p tempdir/Rise.Persistence
mkdir -p tempdir/Rise.PlaywrightTests
mkdir -p tempdir/Rise.Server
mkdir -p tempdir/Rise.Server.Tests
mkdir -p tempdir/Rise.Services
mkdir -p tempdir/Rise.Shared

#copy the content of the folder
declare -a folders=("Rise.Client" "Rise.Client.Tests" "Rise.Domain" "Rise.Domain.Tests" "Rise.Persistence" "Rise.PlaywrightTests" "Rise.Server" "Rise.Server.Tests" "Rise.Services" "Rise.Shared")

for folder in "${folders[@]}"; do
  echo "Copying $folder files"
  cp -r "$folder"/* "tempdir/$folder"
done

#copy the sln file to tempdir
cp Rise.sln tempdir

cat > tempdir/Dockerfile << _EOF_
#Use aspnet voor .net runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

EXPOSE 5000

# Use the official .NET 8 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory for the build process
WORKDIR /app

# Copy the .sln file and the project files into the container
COPY ["Rise.sln", "./"]
COPY ["Rise.Client/Rise.Client.csproj", "Rise.Client/"]
COPY ["Rise.Client.Tests/Rise.Client.Tests.csproj", "Rise.Client.Tests/"]
COPY ["Rise.Domain/Rise.Domain.csproj", "Rise.Domain/"]
COPY ["Rise.Domain.Tests/Rise.Domain.Tests.csproj", "Rise.Domain.Tests/"]
COPY ["Rise.Persistence/Rise.Persistence.csproj", "Rise.Persistence/"]
COPY ["Rise.PlaywrightTests/Rise.PlaywrightTests.csproj", "Rise.PlaywrightTests/"]
COPY ["Rise.Server/Rise.Server.csproj", "Rise.Server/"]
COPY ["Rise.Server.Tests/Rise.Server.Tests.csproj", "Rise.Server.Tests/"]
COPY ["Rise.Services/Rise.Services.csproj", "Rise.Services/"]
COPY ["Rise.Shared/Rise.Shared.csproj", "Rise.Shared/"]

# Restore as distinct layers
RUN dotnet restore "Rise.Server/Rise.Server.csproj"
RUN dotnet restore "Rise.Persistence/Rise.Persistence.csproj"

# copy remaining files
COPY . .

# Change repository
WORKDIR "/app/Rise.Server"

# Build the application
RUN dotnet build "Rise.Server.csproj" -c Release -o /app/build

WORKDIR "/app"

# install dotnet tools in build image
RUN dotnet tool install --global dotnet-ef

#var
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet-ef database update --startup-project Rise.Server --project Rise.Persistence

WORKDIR "/app/Rise.Server"

# publish the application
RUN dotnet publish "Rise.Server.csproj" -c Release -o /app/publish

FROM base AS FINAL
WORKDIR /app

#copy the published files
COPY --from=build /app/publish .

# .net tool path
ENV PATH="${PATH}:/root/.dotnet/tools"
ENV ASPNETCORE_URLS=http://+:5000


#start the application
ENTRYPOINT ["dotnet", "Rise.Server.dll"]
_EOF_

cd tempdir || exit
# Build the Docker image, specifying the current directory as the build context
docker build -t dotnet .
docker run -t -d -p 5000:5000 -p 5001:5001 --name dotnetapp dotnet

#remove tempdir
rm -rf tempdir

# List the running Docker containers
docker ps -a | grep dotnetapp
