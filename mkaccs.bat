@echo off
if exist %1 (
    echo "%1 has exist. use other name."
) else (
mkdir %1
cd %1
dotnet new accs -o A
dotnet new accs -o B
dotnet new accs -o C
dotnet new accs -o D
dotnet new accs -o E
dotnet new accs -o F
cd ../
)