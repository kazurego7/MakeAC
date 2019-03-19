@echo off 
mkdir ABC%1
cd ABC%1
dotnet new abctemp -o A
dotnet new abctemp -o B
dotnet new abctemp -o C
dotnet new abctemp -o D