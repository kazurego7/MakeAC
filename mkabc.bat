@echo off
pushd "%~dp0"
mkdir ABC%1
cd ABC%1

mkdir A
cd ./A
dotnet new actemp -o content
dotnet new actest -o test
cd ./test
dotnet add reference ../content
cd ../../

mkdir B
cd ./B
dotnet new actemp -o content
dotnet new actest -o test
cd ./test
dotnet add reference ../content
cd ../../

mkdir C
cd ./C
dotnet new actemp -o content
dotnet new actest -o test
cd ./test
dotnet add reference ../content
cd ../../

mkdir D
cd ./D
dotnet new actemp -o content
dotnet new actest -o test
cd ./test
dotnet add reference ../content
cd ../../

popd