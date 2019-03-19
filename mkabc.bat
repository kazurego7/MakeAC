@echo off 
mkdir ABC%1
cd ABC%1
dotnet new console -o A
dotnet new console -o B
dotnet new console -o C
dotnet new console -o D