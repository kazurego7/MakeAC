#! /bin/bash

if [ -f *.nupkg]; then
    rm *.nupkg
fi
if [ -d ./content/bin/]; then
    rm -r ./content/bin/
fi
if [ -d ./content/obj/]; then
    rm -r ./content/obj/
fi
nuget pack ./AtCoderContest.Template.CSharp.nuspec -Build
dotnet new -i ./*.nupkg