#! /bin/bash

if [ -d $1 ]; then
    echo "$1 already exists. use other name."
else
mkdir $1
cd $1
dotnet new actemp -o A
dotnet new actemp -o B
dotnet new actemp -o C
dotnet new actemp -o D
dotnet new actemp -o E
dotnet new actemp -o F
cd ../
fi