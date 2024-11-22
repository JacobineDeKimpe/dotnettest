#!/bin/bash
set -euo pipefail

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

#COPY the content of the folder
echo "COPYing Ris.Client files"
cp -r Rise.Client/* tempdir/Rise.Client
echo "COPYing Ris.Client.Tests files"
cp -r Rise.Client.Tests/* tempdir/Rise.Client.Tests
echo "COPYing Ris.Domain files"
cp -r Rise.Domain/* tempdir/Rise.Domain
echo "COPYing Ris.Domain.Tests files"
cp -r Rise.Domain.Tests/* tempdir/Rise.Domain.Tests
echo "COPYing Ris.Persistence files"
cp -r Rise.Persistence/* tempdir/Rise.Persistence
echo "COPYing Ris.PlaywrightTests files"
cp -r Rise.PlaywrightTests/* tempdir/Rise.PlaywrightTests
echo "COPYing Ris.Server files"
cp -r Rise.Server/* tempdir/Rise.Server
echo "COPYing Ris.Server.Tests files"
#cp -r Rise.Server.Tests/* tempdir/Rise.Server.Tests
echo "COPYing Ris.Services files"
cp -r Rise.Services/* tempdir/Rise.Services
echo "COPYing Ris.Shared files"
cp -r Rise.Shared/* tempdir/Rise.Shared

#COPY the sln file to tempdir
cp Rise.sln tempdir

cat > tempdir/Dockerfile << _EOF_
COPY ./Rise.client /home/app/Rise.Client/
COPY ./Rise.Client.Tests /home/app/Rise.Client.Tests/
COPY ./Rise.Domain /home/app/Rise.Domain/
COPY ./Rise.Domain.Tests /home/app/Rise.Domain.Tests/
COPY ./Rise.Persistence /home/app/Rise.Persistence/
COPY ./Rise.PlaywrightTests /home/app/Rise.PlaywrightTests/
COPY ./Rise.Server /home/app/Rise.Server/
COPY ./Rise.Server.Tests /home/app/Rise.Server.Tests/
COPY ./Rise.Services /home/app/Rise.Services/
COPY ./Rise.Shared /home/app/Rise.Shared/
COPY ./Rise.sln /home/app/Rise.sln
EXPOSE 5001
CMD dotnet build /home/app/
_EOF_

cd tempdir || exit
docker build -t rise-dotnet .
docker run -t -p 5001:5001 --name dotnet-running rise-dotnet
docker ps -a
