using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

using ACIgnore = System.Collections.Generic.Dictionary<System.String, System.String>;

// Entrypoint, create from the .NET Core Console App.
class Program : ConsoleAppBase // inherit ConsoleAppBase
{
    private static string configFileName = ".actemp";
    private static string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), configFileName);

    static async Task Main(string[] args)
    {
        await Task.Run(() =>
        {
            if (!File.Exists(configFilePath))
            {
                File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACIgnore>(new ACIgnore()));
            }
        });
        // target T as ConsoleAppBase.
        await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
    }

    [Command(new[] { "uk", "ushitapunikiakun" })]
    public void Ushitapunikiakun()
    {
        Console.Error.WriteLine("う　し　た　ぷ　に　き　あ　く　ん　（笑）");
    }

    [Command("install", "テンプレートのインストール")]
    public void InstallTemplate([Option(0, "テンプレート名")]string templateName, [Option(1, "テンプレートとなるプロジェクトへのパス")]string templatePath)
    {
        var invalidPathString = new string(Path.GetInvalidPathChars());
        if (invalidPathString.Any(invalidChar => templatePath.Contains(invalidChar)))
        {
            Console.Error.WriteLine($"WA! 以下の文字は、テンプレートとなるプロジェクトへのパスに含むことができません。");
            Console.Error.WriteLine($"    {String.Join(" ", invalidPathString)}");
            return;
        }

        if (!Directory.Exists(templatePath))
        {
            Console.Error.WriteLine($"WA! {templatePath} は存在しません。");
            Console.Error.WriteLine($"    存在するパスを指定してください。");
            return;
        }

        var templateAbsolutePath = Path.GetFullPath(templatePath);
        var templates = JsonSerializer.Deserialize<ACIgnore>(File.ReadAllText(configFilePath));
        if (!templates.ContainsKey(templateName))
        {
            templates.Add(templateName, templateAbsolutePath);
            File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACIgnore>(templates));
            Console.WriteLine($"AC! テンプレート名 {templateName} に、{templateAbsolutePath} をインストールしました。");
        }
        else
        {
            Console.WriteLine($"テンプレート名 {templateName} には、{templates[templateName]} が既にインストールされています。");
            Console.WriteLine("パスを上書きしますか？ (yes/no)");
            var input = Console.ReadLine();
            if (input == "yes")
            {
                templates[templateName] = templateAbsolutePath;
                File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACIgnore>(templates));
                Console.WriteLine($"AC! テンプレート名 {templateName} に、{templateAbsolutePath} を上書きインストールしました。");
            }
            else
            {
                Console.WriteLine($"テンプレート名 {templateName} を上書きせず終了しました。");
            }
        }
    }

    [Command("list", "インストールしたテンプレートの一覧")]
    public void ListTemplate()
    {
        Console.WriteLine($"テンプレート名 : テンプレートへのパス");
        foreach (var template in JsonSerializer.Deserialize<ACIgnore>(File.ReadAllText(configFilePath)))
        {
            Console.WriteLine($"{template.Key} : {template.Value}");
        }
    }

    [Command(new[] { "new", "create" }, "コンテスト用のプロジェクト作成")]
    public void CreateContestProjects([Option(0, "利用するテンプレート名")]string templateName, [Option(1, "作成するコンテスト名")]string contestName)
    {
        var templates = JsonSerializer.Deserialize<ACIgnore>(File.ReadAllText(configFilePath));
        if (!templates.ContainsKey(templateName))
        {
            Console.Error.WriteLine($"WA! {templateName} がテンプレートに存在しません。");
            Console.Error.WriteLine($"    テンプレート一覧を確認してください。");

            Console.WriteLine($"テンプレート名 : テンプレートへのパス");
            foreach (var template in templates)
            {
                Console.WriteLine($"{template.Key} : {template.Value}");
            }
            return;
        }

        var invalidFileNameString = new string(Path.GetInvalidFileNameChars());
        if (invalidFileNameString.Any(invalidChar => contestName.Contains(invalidChar)))
        {
            Console.Error.WriteLine($"WA! 以下の文字は、コンテスト名に含むことができません。");
            Console.Error.WriteLine($"    {String.Join(" ", invalidFileNameString)}");
            return;
        }
        if (Directory.Exists(contestName))
        {
            Console.Error.WriteLine($"WA! {contestName} ディレクトリはすでに存在しています。別のコンテスト名を使用してください。");
            return;
        }

        if (!Directory.Exists(templates[templateName]))
        {
            Console.Error.WriteLine($"CE! テンプレート名 {templateName} のパス {templates[templateName]} にテンプレートとなるディレクトリが存在しません。");
            Console.Error.WriteLine($"    install コマンドでパスを修正してください。");
            return;
        }

        Console.WriteLine("WJ... コンテスト名のディレクトリを作成します。");

        Directory.CreateDirectory(contestName);
        foreach (var problemName in new List<string> { "A", "B", "C", "D", "E", "F" })
        {
            Console.WriteLine($"WJ... {problemName} ディレクトリを作成します。");

            var problemPath = Path.Combine(contestName, problemName);
            DirectoryEx.Copy(templates[templateName], problemPath);
        }

        Console.WriteLine("AC! コンテスト用の各問題プロジェクトの作成が完了しました。");
    }
}