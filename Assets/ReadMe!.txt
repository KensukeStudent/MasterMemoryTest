実行時はdotnet必須となります。
以下のコマンドをたたいて、インストールされているかの確認をお願いいたします。
尚、インストールするdotnetバージョンはマスターメモリと互換性がある必要があります。今回のプロジェクトではver8.0
dotnet install : https://dotnet.microsoft.com/ja-jp/download
参考資料 : https://light11.hatenadiary.com/entry/2023/11/20/194245

dotnetコマンド確認
$ dotnet tool list --global

マスターメモリ関連のデータ作成
$ dotnet-mmgen.exe -i .\Assets\Scripts\Master\ -o .\Assets\Scripts\Generated\ -n "Example" -addImmutableConstructor

i : マスターデータクラスが存在する読み込みディレクトリ
o : マスターデータクラスの生成先ディレクトリ
n : namespace
-addImmutableConstructor : コンストラクター生成

メッセージパック関連のデータ作成
$ mpc -i .\Assets\Scripts\Master\ -o .\Assets\Scripts\Generated\

i : マスターデータクラスが存在する読み込みディレクトリ
o : マスターデータクラスの生成先ディレクトリ

=================================================================================================================================

Usage: MasterMemory.Generator [options...]
Options:
  -i, -inputDirectory <String>              Input file directory(search recursive). (Required)
  -o, -outputDirectory <String>             Output file directory. (Required)
  -n, -usingNamespace <String>              Namespace of generated files. (Required)
  -p, -prefixClassName <String>             Prefix of class names. (Default: )
  -c, -addImmutableConstructor <Boolean>    Add immutable constructor to MemoryTable class. (Default: False)
  -t, -returnNullIfKeyNotFound <Boolean>    Return null if key not found on unique find method. (Default: False)

=================================================================================================================================