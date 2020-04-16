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
    public void MakeAC([Option(0, "コンテスト名")]string contestName)
    {
        var invalidFileNameString = new string(Path.GetInvalidFileNameChars());
        if (invalidFileNameString.Any(invalidChar => contestName.Contains(invalidChar)))
        {
            Console.Error.WriteLine($"WA! {contestName} に使えない文字が含まれています。");
            Console.Error.WriteLine("以下の文字列は、コンテスト名に含むことができません。");
            Console.Error.WriteLine($"{String.Join(" ", invalidFileNameString)}");
            return;
        }
        if (Directory.Exists(contestName))
        {
            Console.Error.WriteLine($"WA! {contestName} はすでに存在しています。別のディレクトリ名を使用してください。");
            return;
        }
        Directory.CreateDirectory(contestName);

        var templatePath = Environment.GetEnvironmentVariable("ATCODER_TEMPLATE");

        if (templatePath == "")
        {
            Console.Error.WriteLine($"WA! ユーザー環境変数 ATCODER_TEMPLATE に利用するテンプレートプロジェクトのパスを指定してください。");
            return;
        }

        if (!Directory.Exists(templatePath))
        {
            Console.Error.WriteLine($"WA! {templatePath} は存在しません。");
            Console.Error.WriteLine("ユーザー環境変数 ATCODER_TEMPLATE に利用するテンプレートプロジェクトのパスを指定してください。");
            return;
        }

        foreach (var problemName in new List<string> { "A", "B", "C", "D", "E", "F" })
        {
            var problemPath = Path.Combine(contestName, problemName);
            Task.Run(() => ProcessX.StartAsync($"cp -r {templatePath}  {problemPath}").WriteLineAllAsync());

            var binPath = Path.Combine(problemPath, "bin");
            if (Directory.Exists(binPath))
            {
                Directory.Delete(binPath, true);
            }
            var objPath = Path.Combine(problemPath, "obj");
            if (Directory.Exists(objPath))
            {
                Directory.Delete(objPath, true);
            }
        }

        Console.WriteLine("AC! コンテスト用の各問題プロジェクトの作成が完了しました。");
    }
}