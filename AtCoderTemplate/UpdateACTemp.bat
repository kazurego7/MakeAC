@echo off
setlocal
pushd "%~dp0"
if exist *.nupkg (
    del *.nupkg
    )
if exist .\content\bin\ (
    rd /s /q .\content\bin\
)
if exist .\content\obj\ (
    rd /s /q .\content\obj\
)
nuget pack ./AtCoderContest.Template.CSharp.nuspec -Build
dotnet new -i ./*.nupkg
popd