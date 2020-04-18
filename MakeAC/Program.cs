using ConsoleAppFramework;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
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
                var actemplateCofig = new ACTemplateConfig();
                actemplateCofig.templates = new Dictionary<string, ACTemplate>();
                File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACTemplateConfig>(actemplateCofig));
            }
        });
        // target T as ConsoleAppBase.
        await Host.CreateDefaultBuilder().RunConsoleAppFrameworkAsync<Program>(args);
    }

    [Command(new[] { "ushitapunikiakun", "uk", }, "????????????")]
    public void UshitapunikiakunCommand()
    {
        Console.Error.WriteLine("う　し　た　ぷ　に　き　あ　く　ん　（笑）");
    }

    [Command(new[] { "install", "i", }, "テンプレートのインストール")]
    public void InstallCommand([Option(0, "テンプレート名")]string templateName, [Option(1, "テンプレートへのパス")]string templatePath)
    {
        var templateConfig = JsonSerializer.Deserialize<ACTemplateConfig>(File.ReadAllText(configFilePath));
        var invalidPathString = new string(Path.GetInvalidPathChars());
        if (invalidPathString.Any(invalidChar => templatePath.Contains(invalidChar)))
        {
            Console.Error.WriteLine($"WA! 以下の文字は、テンプレートへのパスに含むことができません。");
            Console.Error.WriteLine($"    {String.Join(" ", invalidPathString)}");
            return;
        }

        if (!Directory.Exists(templatePath))
        {
            Console.Error.WriteLine($"WA! {templatePath} は存在しません。");
            Console.Error.WriteLine($"    存在するテンプレートへのパスを指定してください。");
            return;
        }
        var templateAbsolutePath = Path.GetFullPath(templatePath);
        if (!templateConfig.templates.ContainsKey(templateName))
        {
            templateConfig.templates.Add(templateName, new ACTemplate { path = templateAbsolutePath, removeFlag = false });
            File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACTemplateConfig>(templateConfig));
            Console.WriteLine($"AC! テンプレート名 {templateName} に、{templateAbsolutePath} をインストールしました。");
        }
        else if (templateConfig.templates[templateName].removeFlag)
        {
            templateConfig.templates[templateName].removeFlag = false;
            File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACTemplateConfig>(templateConfig));
            Console.WriteLine($"AC! テンプレート名 {templateName} に、{templateAbsolutePath} をインストールしました。");
        }
        else
        {
            Console.WriteLine($"テンプレート名 {templateName} には、テンプレートへのパス {templateConfig.templates[templateName]} が既にインストールされています。");
            Console.WriteLine("テンプレートへのパスを上書きしますか？ (yes/no)");
            var input = Console.ReadLine();
            if (input == "yes")
            {
                templateConfig.templates[templateName] = new ACTemplate { path = templateAbsolutePath, removeFlag = false };
                File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACTemplateConfig>(templateConfig));
                Console.WriteLine($"AC! テンプレート名 {templateName} に、{templateAbsolutePath} を上書きインストールしました。");
            }
            else
            {
                Console.WriteLine($"テンプレート名 {templateName} を上書きせず終了しました。");
            }
        }
    }

    [Command(new[] { "uninstall", "un", "remove", "rm" }, "テンプレートのアンインストール")]
    public void RemoveCommand([Option(0, "テンプレート名")]string templateName)
    {
        var templateConfig = JsonSerializer.Deserialize<ACTemplateConfig>(File.ReadAllText(configFilePath));
        if (!templateConfig.templates.ContainsKey(templateName) || templateConfig.templates[templateName].removeFlag)
        {
            Console.Error.WriteLine($"WA! {templateName} がテンプレート名に存在しません。");
            Console.Error.WriteLine($"    テンプレート一覧を確認してください。");

            Console.WriteLine($"テンプレート名 : テンプレートへのパス");
            foreach (var template in templateConfig.templates.Where(keyValue => !keyValue.Value.removeFlag))
            {
                Console.WriteLine($"{template.Key} : {template.Value.path}");
            }
            return;
        }

        templateConfig.templates[templateName].removeFlag = true;
        File.WriteAllText(configFilePath, JsonSerializer.Serialize<ACTemplateConfig>(templateConfig));
        Console.WriteLine($"AC! テンプレート名 {templateName} をアンインストールしました。");
    }

    [Command(new[] { "list", "ls", }, "インストールしたテンプレートの一覧")]
    public void ListCommand([Option("r", "アンインストールしたテンプレートを表示するか")]bool remove = false)
    {
        var templateConfig = JsonSerializer.Deserialize<ACTemplateConfig>(File.ReadAllText(configFilePath));
        if (remove)
        {
            Console.WriteLine($"テンプレート名 : テンプレートへのパス");
            foreach (var template in templateConfig.templates.Where(keyValue => !keyValue.Value.removeFlag))
            {
                Console.WriteLine($"{template.Key} : {template.Value.path}");
            }
        }
        else
        {
            Console.WriteLine($"削除されたテンプレート名 : テンプレートへのパス");
            foreach (var template in templateConfig.templates.Where(keyValue => keyValue.Value.removeFlag))
            {
                Console.WriteLine($"{template.Key} : {template.Value.path}");
            }
        }
    }

    [Command(new[] { "new", "n", }, "コンテスト用のプロジェクト作成")]
    public void CreateCommand([Option(0, "利用するテンプレート名")]string templateName, [Option(1, "作成するコンテスト名")]string contestName)
    {
        var templateConfig = JsonSerializer.Deserialize<ACTemplateConfig>(File.ReadAllText(configFilePath));
        if (!templateConfig.templates.ContainsKey(templateName) || templateConfig.templates[templateName].removeFlag)
        {
            Console.Error.WriteLine($"WA! {templateName} がテンプレート名に存在しません。");
            Console.Error.WriteLine($"    テンプレート一覧を確認してください。");

            Console.WriteLine($"テンプレート名 : テンプレートへのパス");
            foreach (var template in templateConfig.templates.Where(keyValue => !keyValue.Value.removeFlag))
            {
                Console.WriteLine($"{template.Key} : {template.Value.path}");
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
        if (Directory.Exists(contestName) || File.Exists(contestName))
        {
            Console.Error.WriteLine($"WA! {contestName} はすでに存在しています。別のコンテスト名を使用してください。");
            return;
        }

        if (!Directory.Exists(templateConfig.templates[templateName].path))
        {
            Console.Error.WriteLine($"CE! テンプレート名 {templateName} のパス {templateConfig.templates[templateName].path} に、テンプレートが存在しません。");
            Console.Error.WriteLine($"    install コマンドで、テンプレートへのパスを修正してください。");
            return;
        }

        Console.WriteLine("WJ... コンテスト名のディレクトリを作成します。");

        Directory.CreateDirectory(contestName);
        foreach (var problemName in new List<string> { "A", "B", "C", "D", "E", "F" })
        {
            Console.WriteLine($"WJ... {problemName} ディレクトリを作成します。");

            var problemPath = Path.Combine(contestName, problemName);
            DirectoryEx.Copy(templateConfig.templates[templateName].path, problemPath);
        }

        Console.WriteLine("AC! コンテスト用の各問題プロジェクトの作成が完了しました。");
    }
}