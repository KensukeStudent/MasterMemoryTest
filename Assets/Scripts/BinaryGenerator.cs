using System.IO;
using Example;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor;

public static class BinaryGenerator
{
    [MenuItem("Tools/Master/Generate Binary")]
    private static void Run()
    {
        // MessagePackの初期化（ボイラープレート）
        var messagePackResolvers = CompositeResolver.Create(
            MasterMemoryResolver.Instance, // 自動生成されたResolver（Namespaceごとに作られる）
            GeneratedResolver.Instance, // 自動生成されたResolver
            StandardResolver.Instance // MessagePackの標準Resolver
        );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        // DatabaseBuilderを使ってバイナリデータを生成する
        var databaseBuilder = new DatabaseBuilder();
        databaseBuilder.Append(FileLoad.JsonLoad<CharacterMaster>());
        var binary = databaseBuilder.Build();

        // できたバイナリは永続化しておく
        var path = "Assets//Resources/Binary/MasterBytes.bytes";
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        File.WriteAllBytes(path, binary);
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Master/JsonLoad")]
    private static void LoadJson()
    {
        // MessagePackの初期化（ボイラープレート）
        var messagePackResolvers = CompositeResolver.Create(
            MasterMemoryResolver.Instance, // 自動生成されたResolver（Namespaceごとに作られる）
            GeneratedResolver.Instance, // 自動生成されたResolver
            StandardResolver.Instance // MessagePackの標準Resolver
        );
        var options = MessagePackSerializerOptions.Standard.WithResolver(messagePackResolvers);
        MessagePackSerializer.DefaultOptions = options;

        // オブジェクトからデシリアライズできるか調査 --------------------------------------------------
        var characterMasters = new CharacterMaster[]
        {
            new CharacterMaster(1, "ひとかげ", 10),
            new CharacterMaster(2, "ふしぎだね", 10),
            new CharacterMaster(3, "銭亀", 10),
        };
        var bytes = MessagePackSerializer.Serialize(characterMasters);
        var datas1 = MessagePackSerializer.Deserialize<CharacterMaster[]>(bytes);

        // オブジェクトからJson化したものからデシリアライズできるか調査 --------------------------------
        var json = MessagePackSerializer.ConvertToJson(bytes);
        var bytes2 = MessagePackSerializer.ConvertFromJson(json);
        var datas2 = MessagePackSerializer.Deserialize<CharacterMaster[]>(bytes2);

        // Json読み込みからデシリアライズできるか調査 -------------------------------------------------
        var data3 = FileLoad.JsonLoad<CharacterMaster>();
    }
}