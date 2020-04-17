using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using Cysharp.Diagnostics;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// Entrypoint, create from the .NET Core Console App.
class Program : ConsoleAppBase // inherit ConsoleAppBase
{
    private static string configFileName = ".actemp";
    private static string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ".actemp");

    static async Task Main(string[] args)
    {
        // target T as ConsoleAppBase.
        await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
    }

    [Command(new[] { "uk", "ushitapunikiakun" })]
    public void Ushitapunikiakun()
    {
        Console.Error.WriteLine("う　し　た　ぷ　に　き　あ　く　ん　（笑）");
    }

    [Command("install", "テンプレートのインストール")]
    public void InstallTemplate([Option(0, "テンプレートとなるプロジェクトへのパス")]string templatePath)
    {
        var invalidPathString = new string(Path.GetInvalidPathChars());
        if (invalidPathString.Any(invalidChar => templatePath.Contains(invalidChar)))
        {
            Console.Error.WriteLine("WA! 以下の文字は、パスに含むことができません");
            Console.Error.WriteLine($"{String.Join(" ", invalidPathString)}");
        }
        if (!Directory.Exists(templatePath))
        {
            Console.Error.WriteLine($"WA! {templatePath} は存在しません。");
            Console.Error.WriteLine("存在するパスを指定してください。");
            return;
        }

        if (Path.IsPathRooted(templatePath))
        {
            File.WriteAllText(configFilePath, templatePath);
        }
        else
        {
            File.WriteAllText(configFilePath, Path.GetFullPath(templatePath));
        }

        Console.WriteLine($"AC! {templatePath} をテンプレートとして登録しました。");
    }

    [Command("list", "インストールしたテンプレートの一覧")]
    public void ListTemplate()
    {
        if (!File.Exists(configFilePath) || File.ReadAllText(configFilePath) == "")
        {
            Console.Error.WriteLine($"WA! install コマンドで、利用するテンプレートプロジェクトのパスを指定してください。");
            return;
        }

        var templatePath = File.ReadAllText(configFilePath);

        Console.WriteLine(templatePath);
    }

    [Command("create", "コンテスト用のプロジェクト作成")]
    public void CreateContestProjects([Option(0, "コンテスト名")]string contestName)
    {
        Console.WriteLine("WJ... コンテスト名のディレクトリを作成します。");

        var invalidFileNameString = new string(Path.GetInvalidFileNameChars());
        if (invalidFileNameString.Any(invalidChar => contestName.Contains(invalidChar)))
        {
            Console.Error.WriteLine("WA! 以下の文字は、コンテスト名に含むことができません。");
            Console.Error.WriteLine($"{String.Join(" ", invalidFileNameString)}");
            return;
        }
        if (Directory.Exists(contestName))
        {
            Console.Error.WriteLine($"WA! {contestName} ディレクトリはすでに存在しています。別のコンテスト名を使用してください。");
            return;
        }

        if (!File.Exists(configFilePath) || File.ReadAllText(configFilePath) == "")
        {
            Console.Error.WriteLine($"WA! install コマンドで、利用するテンプレートプロジェクトのパスを指定してください。");
            return;
        }

        var templatePath = File.ReadAllText(configFilePath);

        Directory.CreateDirectory(contestName);
        foreach (var problemName in new List<string> { "A", "B", "C", "D", "E", "F" })
        {
            Console.WriteLine($"WJ... {problemName} ディレクトリを作成します。");

            var problemPath = Path.Combine(contestName, problemName);
            Task.Run(() => ProcessX.StartAsync($"cp -r {templatePath}  {problemPath}").WriteLineAllAsync());
        }

        Console.WriteLine("AC! コンテスト用の各問題プロジェクトの作成が完了しました。");
    }
}