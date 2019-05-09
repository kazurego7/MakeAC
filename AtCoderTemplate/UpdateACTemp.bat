@echo off
setlocal
pushd "%~dp0"
if exist *.nupkg (
    del *.nupkg
    )
nuget pack ./AtCoderContest.Template.CSharp.nuspec -Build -NoDefaultExcludes
dotnet new -i ./*.nupkg
popd