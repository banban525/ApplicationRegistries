ApplicationRegistries
======================

[To Original(English)](https://github.com/banban525/ApplicationRegistries/blob/master/README.md)

ApplicaitonRegistries はアプリケーションの外部設定値のプロキシとなる.NETライブラリです。

## 説明

Windowsアプリケーションを開発する場合、後で変更できるようにいくつかのパラメータが実装されています。
例えば、スペックの影響を受けるパラメータであるタイムアウト時間や、PC全体のリソースを利用するパラメータであるポート番号があります。
一般的な方法は次のとおりです。
* 設定ファイル
* 環境変数
* コマンドライン引数
* レジストリ (Windowsの場合)
* アプリケーション構成ファイル(*.exe.config)

自由に選択することができますが、これらはアプリケーションにとっては同じもので「外部設定値」です。

ただし、実装する方法はそれぞれ異なります。
たとえば、コマンドライン引数パーザを自分で実装する必要があります。
レジストリにはヌルチェックが必要です。
気軽に使えるものではありません。

また、読み込み処理をさまざまな場所に実装すると、外部設定をリストするのが難しくなります。
それ以外に、実装方法が毎回同じにならないことがあります。

ApplicationRegistries は上記に挙げた「外部設定値」のプロキシとして機能します。
簡単な使い方で、かつ、優先順位付きで、「外部設定値」を読み込むことができます。
また、「外部設定値」の一覧レポートを出力することもできます。

## 使用例

次のコードは使用例です。

```csharp
[ApplicationRegistry]
public interface ISettings
{
  /// <Summary>
  /// Listen Port Number
  /// </Summary>
  [DefaultValue(80)]
  int PortNo { get; }

  /// <Summary>
  /// Backup to
  /// </Summary>
  string BackupFolder { get; }
}

class Program
{
  static void Main()
  {
    int portNo = ApplicationRegistry.Get<ISettings>().PortNo;
  }
}

```

## 依存関係

ApplicationRegistries2.dll は、.NET Framework 4.5 のみに依存します。

## インストール方法

以下のコマンドでnugetからインストールできます。 (未実装)

```
PM> Install-Package ApplicationRegistries2
```


## 使い方

1. ApplicationRegistries2.dll を参照します。
2. 外部設定値に対応するインターフェイスを定義します。
   なお、インターフェイスには次の制約があります。
    * getプロパティのみを定義すること
    * プロパティの型は次の型であること
      * Int32
      * string
3. 外部設定値インターフェイスに属性[ApplicationRegistry]を付けます。
4. `ApplicationRegistry.Get<T>()`を使って、プロパティにアクセスすることで外部設定値を読み込むことができます。
  `T`には2で宣言したインターフェイス型を指定します。

ApplicationRegistriesはデフォルトでは次の優先順位で外部設定を読み込みます。
* (1)コマンドライン引数
* (2)環境変数
* (3)レジストリ(HKCU)
* (4)XMLファイル
* (5)レジストリ(HKLM)

なお、[ApplicationRegistry]属性の引数に、外部設定を読み込む場所を指定することで
優先順位や有効・無効を設定できます。


### コマンドライン引数

デフォルトでは、以下のコマンドライン引数が読み込まれます。

```
--(インターフェイス名)_(プロパティ名)="設定値"
```

例
``` csharp
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

```
--ISettings_PortNo=80
```

属性[CommandlineArgumentPrefix]と[CommandlineArgumentName]をつけることで
インターフェイス名とプロパティ名を変更することができます。

例
``` csharp
  [ApplicationRegistry]
  [CommandlineArgumentPrefix("Settings")]
  public interface ISettings
  {
    [CommandlineArgumentName("ListenPort")]
    int PortNo {get;}
  }
```

### 環境変数

デフォルトでは、以下の環境変数が読み込まれます。

```
set (アセンブリ名)_(インターフェイス名)_(プロパティ名)=設定値
```

例
``` csharp
  // ApplicationRegistries.Sample.dll
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

```
set ApplicationRegistries.Sample‗ISettings_PortNo=80
```

属性[EnvironmentVariablePrefix]と[EnvironmentVariableName]をつけることで
アセンブリ名とインターフェイス名、プロパティ名を変更することができます。

例
``` csharp
  [ApplicationRegistry]
  [EnvironmentVariablePrefix("Settings")]
  public interface ISettings
  {
    [EnvironmentVariableName("ListenPort")]
    int PortNo {get;}
  }
```

### レジストリ

デフォルトでは、以下のレジストリが読み込まれます。

HKCUの場合
```
[HKCU\Software\ApplicationRegistries\(アセンブリ名)\(インターフェイス名)]
"プロパティ名"=型:設定値
```
HKLMの場合
```
[HKLM\Software\ApplicationRegistries\(アセンブリ名)\(インターフェイス名)]
"プロパティ名"=型:設定値
```

例
``` csharp
  // ApplicationRegistries.Sample.dll
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

```
[HKCU\Software\ApplicationRegistries\ApplicationRegistries.Sample\ISettings]
"PortNo"=dword:00000050

[HKLM\Software\ApplicationRegistries\ApplicationRegistries.Sample\ISettings]
"PortNo"=dword:00000050
```

属性[RegistryKey]と[RegistryName]をつけることで
インターフェイス名とプロパティ名を変更することができます。

例
``` csharp
  [ApplicationRegistry]
  [RegistryKey(@"Software\MyCompany\MySoftware")]
  public interface ISettings
  {
    [RegistryName("ListenPort")]
    int PortNo {get;}
  }
```

### XMLファイル

デフォルトでは、次のXMLファイルが読み込まれます。

ファイルパス: (ApplicationRegistries2.dllのフォルダ)\ApplicationRegisties.xml



``` xml
<?xml version="1.0" encoding="utf-8" ?>
<ApplicationRegisties>
  <インターフェイス名>
    <プロパティ名>設定値</プロパティ名>
  </インターフェイス名>
</ApplicationRegisties>
```

例
``` csharp
  // ApplicationRegistries.Sample.dll
  [ApplicationRegistry]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

``` xml
<?xml version="1.0" encoding="utf-8" ?>
<ApplicationRegisties>
  <ISettings>
    <PortNo>80</PortNo>
  </ISettings>
</ApplicationRegisties>
```

属性[XmlFile]と[XmlName]をつけることで
ファイル名やXMLの構造を指定することができます。

例
``` csharp
  [ApplicationRegistry]
  [XmlFile(@".\Settings.xml", "/Settings")]
  public interface ISettings
  {
    [XmlName("ListenPort")]
    int PortNo {get;}
  }
```

``` xml
<?xml version="1.0" encoding="utf-8" ?>
<Settings>
  <ListenPort>80</ListenPort>
</Settings>
```

### デフォルト値

指定された外部設定値すべてでデータが取得できない場合に返すデフォルト値を属性[DefaultValue]で指定できます。

``` csharp
  [ApplicationRegistry]
  public interface ISettings
  {
    [DefaultValue(80)]
    int PortNo {get;}
  }
```

### 外部設定の優先順位と無効化

外部設定のデフォルトの優先順位は次の表の通りです。
[ApplicationRegistry]の引数にキーを配列で指定することで、優先順位を変更したり、無効化することができます。


| 外部設定          | デフォルトの優先順位 | 指定するキー                           |
|------------------|---------------------|---------------------------------------|
| コマンドライン引数 | 1                  | BuiltInAccessors.CommandlineArguments |
| 環境変数          | 2                  | BuiltInAccessors.EnvironmenetVariable |
| レジストリ(HKCU)  | 3                  | BuiltInAccessors.UserRegistry         |
| XMLファイル       | 4                  | BuiltInAccessors.XmlFile              |
| レジストリ(HKLM)  | 5                  | BuiltInAccessors.MachineRegistry      |

``` csharp
  [ApplicationRegistry(
    BuiltInAccessors.CommandlineArguments,
    BuiltInAccessors.EnvironmenetVariable,
    BuiltInAccessors.UserRegistry,
    BuiltInAccessors.MachineRegistry
  )]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

### ユーザー定義の外部設定

アプリケーション固有のデータベースや設定ファイルから設定値を読み込みたい場合は、
読み込み処理をユーザー定義することができます。

1. IAccessor を実装した外部設定値読み込み用のでユーザー定義クラスを作成します
2. `ApplicationRegistry.RegistCustomAccessor()`でユーザー定義クラスを登録します。
   この時第1引数としてキーを指定します。
``` csharp
  ApplicationRegistry.RegistCustomAccessor("CUSTOM", new CustomAccessor());
```
3. 2で登録したキーを属性[ApplicationRegistry]の引数に指定します。
``` csharp
  [ApplicationRegistry(
    "CUSTOM",
    BuiltInAccessors.CommandlineArguments,
    BuiltInAccessors.EnvironmenetVariable,
    BuiltInAccessors.UserRegistry,
    BuiltInAccessors.MachineRegistry
  )]
  public interface ISettings
  {
    int PortNo {get;}
  }
```

### レポート出力

(Coming Soon)

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
