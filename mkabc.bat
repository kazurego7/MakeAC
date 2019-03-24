@echo off 
mkdir ABC%1
cd ABC%1
dotnet new actemp -o A
dotnet new actemp -o B
dotnet new actemp -o C
dotnet new actemp -o D