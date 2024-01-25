using Example;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor;
using UnityEngine;

public static class BinaryLoader
{
    [MenuItem("Example/Load Binary")]
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

        // ロード（テスト用にAssetDatabaseを使っているが実際にはAddressableなどで）
        var path = "Assets/Binary/Person.bytes";
        var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
        var binary = asset.bytes;

        // MemoryDatabaseをバイナリから作成
        var memoryDatabase = new MemoryDatabase(binary);
        // テーブルからデータを検索
        var stage = memoryDatabase.PersonTable.FindByPersonId(1);
        Debug.Log(stage.Name);
    }
}