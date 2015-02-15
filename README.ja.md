ApplicationRegistries
======================

[To English](https://github.com/banban525/ApplicationRegistries/blob/master/README.md)

ApplicaitonRegistries はアプリケーションの設定を抽象化する.NETライブラリです。

## 説明

多くのアプリケーションは、レジストリや環境変数といった外部設定にしたがって挙動を変えるようになっています。
しかし、テスト容易性を考えるとこれらの外部設定は動的に変更できるようにしておくべきです。
このライブラリは、レジストリや環境変数を抽象化することでこの問題を解決します。
外部設定の定義をXMLに記述することで、ApplicationRegistriesは外部設定にアクセスするラッパークラスを生成します。
このラッパークラスを使うことで、アプリケーションコードを変更せずに外部設定の参照先を変更することができます。

## デモ

![Demo Animation](https://github.com/banban525/ApplicationRegistries/blob/master/Samples/ReadmeContents/Readme_images.ja.gif?raw=true) 


## 依存

ApplicationRegistries.dll は、.NET Framework 4.0 のみに依存します。

ApplicationRegistries.Generator.exe は次のライブラリに依存します。

* Antlr4.StringTemplate
* Command Line Parser Library


## 使い方

XML定義の例は以下の通り。

    <?xml version="1.0" encoding="utf-8" ?>
    <ApplicationRegistryDefine
      xmlns="https://github.com/banban525/ApplicationRegistries/schemas/1.0.0/ApplicationRegistryDefine.xsd">
      <Entry id="InstallDir" Type="string">
        <Description>インストール先のフォルダ</Description>
        <Registory>
          <Key>HKLM\SOFTWARE\banban525\ApplicationRegistries\Install</Key>
          <Name>Directory</Name>
          <DefaultValue>None</DefaultValue>
        </Registory>
      </Entry>
      <Entry id="ApplicationName" Type="string">
        <Description>アプリケーション名</Description>
        <StaticValue>
          <Value>ApplicationRegistries</Value>
        </StaticValue>
      </Entry>
      <Entry id="IsDebug" Type="bool">
        <Description>デバッグモードかどうか</Description>
        <CommandLineArgument ignoreCase="true">
          <ArgumentName>/Debug</ArgumentName>
          <DefaultValue>0</DefaultValue>
        </CommandLineArgument>
      </Entry>
      <Entry id="LogginUser" Type="string">
        <Description>ログイン中ユーザ</Description>
        <EnvironmentVariable>
          <VariableName>USERNAME</VariableName>
          <DefaultValue>Unknown</DefaultValue>
        </EnvironmentVariable>
      </Entry>
    </ApplicationRegistryDefine>

ラッパークラスの出力方法

    ApplicationRegistries.Generator.exe --mode Code --input define.xml --output Registries.cs

以下のようなラッパークラスが生成されます。

     public class Registries
    {
        public Registries()
        {
            ...
        }

        /// <summary>
        /// インストール先のフォルダ
        /// </summary>
        public String InstallDir
        {
            get
            {
                ...
            }
        }
    ...
    
    }


### 生成ツールの使い方

    Usage: ApplicationRegistries.Generator.exe <options>
    
    options:
    
      -m, --mode         Required. (Default: Code) Select Mode for generation type.
      -o, --output       Required. Output file path.
      -i, --input        Required. Input file path.
      -c, --classname    (Default: Registries) class name for *.cs Code
      -n, --namespace    (Default: ApplicationRegistries) namespace for *.cs Code.
      -t, --template     (Default: ) template name for --Mode Other
      --help             Display this help screen.

## インストール方法

以下のコマンドでnugetからインストールできます。

    PM> Install-Package ApplicationRegistries

## 開発方法

1. フォークしてください ( https://github.com/banban525/ApplicationRegistries/fork )
2. 機能ブランチを作ってください (git checkout -b my-new-feature)
3. コミットしてください (git commit -am 'Add some feature')
4. ブランチをプッシュしてください (git push origin my-new-feature)
5. プルリクエストを送ってください


## ライセンス

[MIT](https://github.com/banban525/ApplicationRegistries/blob/master/LICENSE)

## 開発者

[banban525](https://github.com/banban525)
