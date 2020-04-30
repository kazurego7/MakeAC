# MakeAC
MakeAC は、競技プログラミング(AtCoder)用のテンプレート生成ツールです。

指定したコンテスト名のディレクトリを作成し、その中にA~D問題までのプロジェクトをテンプレートから作成します。

以下の3ステップで、MakeAC のインストール、テンプレートの登録、コンテスト用プロジェクトの作成が行えます。

1. MakeAC のインストール
    ```
    dotnet tool install --global MakeAC
    ```

    ![Install MakeAC](./screenshot/MakeACInstall.png)

2. テンプレートの登録
    ```
    mkac install [テンプレート名] [テンプレートのパス]
    ```

    ![Install template](./screenshot/TemplateInstall.png)

3. コンテスト用プロジェクトの作成
    ```
    mkac new [テンプレート名] [コンテスト名]
    ```

    ![Create New ContestProject](./screenshot/NewContest.png)

    ![Show Problems](./screenshot/Problems.png)

## その他機能

mkac コマンドでサブコマンドを指定しないと、ヘルプが表示されます。

![Show help main](./screenshot/Help.png)

サブコマンドも `-help` オプションをつけることで、ヘルプが表示されます。

![Show help sub](./screenshot/Help2.png)

## MakeAC のアップデート
```
dotnet tool update --global MakeAC
```

## MakeAC のアンインストール
```
dotnet tool uninstall --global MakeAC
```

## Nuget Package
https://www.nuget.org/packages/MakeAC/