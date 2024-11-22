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

#copy the content of the folder
echo "Copying Ris.Client files"
cp -r Rise.Client/* tempdir/Rise.Client
echo "Copying Ris.Client.Tests files"
cp -r Rise.Client.Tests/* tempdir/Rise.Client.Tests
echo "Copying Ris.Domain files"
cp -r Rise.Domain/* tempdir/Rise.Domain
echo "Copying Ris.Domain.Tests files"
cp -r Rise.Domain.Tests/* tempdir/Rise.Domain.Tests
echo "Copying Ris.Persistence files"
cp -r Rise.Persistence/* tempdir/Rise.Persistence
echo "Copying Ris.PlaywrightTests files"
cp -r Rise.PlaywrightTests/* tempdir/Rise.PlaywrightTests
echo "Copying Ris.Server files"
cp -r Rise.Server/* tempdir/Rise.Server
echo "Copying Ris.Server.Tests files"
#cp -r Rise.Server.Tests/* tempdir/Rise.Server.Tests
echo "Copying Ris.Services files"
cp -r Rise.Services/* tempdir/Rise.Services
echo "Copying Ris.Shared files"
cp -r Rise.Shared/* tempdir/Rise.Shared

#copy the sln file to tempdir
cp Rise.sln tempdir

cat > tempdir/Dockerfile << _EOF_
copy ./Rise.client /home/app/Rise.Client/
copy ./Rise.Client.Tests /home/app/Rise.Client.Tests/
copy ./Rise.Domain /home/app/Rise.Domain/
copy ./Rise.Domain.Tests /home/app/Rise.Domain.Tests/
copy ./Rise.Persistence /home/app/Rise.Persistence/
copy ./Rise.PlaywrightTests /home/app/Rise.PlaywrightTests/
copy ./Rise.Server /home/app/Rise.Server/
copy ./Rise.Server.Tests /home/app/Rise.Server.Tests/
copy ./Rise.Services /home/app/Rise.Services/
copy ./Rise.Shared /home/app/Rise.Shared/
copy ./Rise.sln /home/app/Rise.sln
expose 5001
cmd dotnet build /home/app/
_EOF_

cd tempdir || exit
docker build -t rise-dotnet .
docker run -t -p 5001:5001 --name dotnet-running rise-dotnet
docker ps -a
