C#アプリケーションでレジストリを抽象化して扱いたい
====================================================

## この記事を一言で言うと

アプリケーション開発時に
レジストリや環境変数、設定ファイルといった外部設定値を抽象化して扱いたい。

## 想定読者

* アプリケーションを1から設計する人
* 動けばいいではなく、長期的な保守コストを考えたい人
* Windows、C#で開発する人 (には限らない話ですが、サンプルコードはこの辺です)

## 課題

Windowsで動くようなアプリケーションを開発する際、
タイムアウト時間といったスペック依存なものや、待ち受けするポートNoといったPC全体の資源を使うものは
後から変更できるよう実装すると思います。
その際使われる主なものを上げると次があります。
* 設定ファイル
* 環境変数
* コマンドライン引数
* レジストリ (Windowsなら)
* アプリケーション構成ファイル(*.exe.config)

どれも守備範囲が違いますので、自由に選んだらよいと思いますが、
アプリケーションからすると、どれであろうと同じでアプリケーションの外から値を変更できる「外部設定値」ですよね？

それに対して、実装はそれぞれ違っていて、例えばコマンドライン引数なんかはパーサーを自分で書かないといけなかったり、
レジストリはnullチェックが必要だったりと、気軽にパッと使えるものではありません。

また、特にポリシーなくプログラミング中に思いつくまま外部設定値の読み込みを実装すると
コード上にバラバラに実装されることになり、外部設置値が何があるのかわからなくなります。
また、レジストリのキーや設定ファイルの使い方が微妙に違ったりとか一貫性が保てません。


以上より、アプリケーション開発における外部設定値について、次の課題があると思います。
1. アプリケーション側から見ると同じなのに実装方法がまちまち
2. ポリシーなく実装するだけでは一覧性や一貫性が無い

## 解決策

上記の課題を解決できるOSSライブラリがあれば、と思い探していますが、
良いものは見つかりません。
需要がないのか、単に探し方が悪いのか・・・

C#の例ですが、次のコードみたいな感じで、優先順位付きで簡単に外部設定を取得することができたらな、と思い、
GitHubで開発してみています。


```C#
// C#

[ApplicationRegistry(
    BuiltInAccessors.CommandlineArguments,
    BuiltInAccessors.EnvironmenetVariable,
    BuiltInAccessors.UserRegistry,
    BuiltInAccessors.MachineRegistry)]
)]
public interface ISettings
{
    /// <Summary>
    /// 待ち受けするポートNo
    /// </Summary>
    int PortNo { get; }

    /// <Summary>
    /// バックアップの保存先
    /// </Summary>
    string BackupFolder { get; }
}


    static void Main()
    {
        int portNo = ApplicationRegistry.Get<ISettings>().PortNo;

    }

```

この例だと、コマンドライン引数→環境変数→レジストリ(HKCU)→レジストリ(HKLM)の順に
以下の場所を探します。
無ければ、下へ下へと探索していく。

* コマンドライン引数: --ISettings-PortNo=(数値) の値
* 環境変数: MYAPP_ISETTINGS_PORTNO の値
* レジストリ: HKCU\Software\MyApp\ISettings の PortNo
* レジストリ: HKLM\Software\MyApp\ISettings の PortNo


また、適当なコマンドで

```
Format-ApplicationRegistry.exe --input MyApp.exe --output "ExternalSettings.html"
```
