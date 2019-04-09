setlocal
pushd "%~dp0"
del *.nupkg
nuget pack ./AtCoderContest.Template.CSharp.nuspec -Build -NoDefaultExcludes
dotnet new -i ./*.nupkg
popd