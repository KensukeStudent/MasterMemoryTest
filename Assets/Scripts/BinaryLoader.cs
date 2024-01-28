using Example;
using MessagePack;
using MessagePack.Resolvers;
using UnityEditor;
using UnityEngine;

public static class BinaryLoader
{
    [MenuItem("Tools/Master/Load Binary")]
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

        // ロード (AssetBudleやAddressableで読み込みも有)
        var asset = Resources.Load<TextAsset>("Binary/MasterBytes");
        var binary = asset.bytes;

        // MemoryDatabaseをバイナリから作成
        var memoryDatabase = new MemoryDatabase(binary);

        // テーブルからデータを検索
        var character = memoryDatabase.CharacterMasterTable.FindById(1);
        Debug.Log($"キャラ名 : {character.Name}");
    }
}