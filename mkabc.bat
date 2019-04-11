@echo off
if exist ABC%1 (
    echo "ABC%1 has exist. use other name."
) else (
mkdir ABC%1
cd ABC%1
dotnet new actemp -o A
dotnet new actemp -o B
dotnet new actemp -o C
dotnet new actemp -o D
cd ../
)