using System.IO;
using Example;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor;

public static class BinaryGenerator
{
    [MenuItem("Example/Generate Binary")]
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

        // Csvとかからデータを入れる（今回はテストのためコードで入れる）
        var personMasters = new Person[]
        {
            new Person(1, 15, Gender.Female, "まもこ"),
            new Person(2, 10, Gender.Male, "まもる"),
            new Person(3, 25, Gender.Unknown, "なぞる"),
        };

        var characterMasters = new CharacterMaster[]
        {
            new CharacterMaster(1, "ひとかげ", 10),
            new CharacterMaster(2, "ふしぎだね", 10),
            new CharacterMaster(3, "銭亀", 10),
        };

        // DatabaseBuilderを使ってバイナリデータを生成する
        var databaseBuilder = new DatabaseBuilder();
        databaseBuilder.Append(personMasters);
        databaseBuilder.Append(characterMasters);
        var binary = databaseBuilder.Build();

        // できたバイナリは永続化しておく
        var path = "Assets/Binary/MasterDataArray.bytes";
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        File.WriteAllBytes(path, binary);
        AssetDatabase.Refresh();
    }
}