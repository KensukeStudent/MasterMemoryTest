using System;
using UnityEngine;
using MessagePack;

/// <summary>
/// ファイル読み込みクラス
/// </summary>
public class FileLoad
{
    ////////////////////////////////////
    ///     Jsonファイル読み込み     ///
    ////////////////////////////////////

    /// <summary>
    /// Jsonロード
    /// </summary>
    /// <typeparam name="T">取得するデータモデル</typeparam>
    /// <typeparam name="K">ファイル読み込み名</typeparam>
    /// <returns></returns>
    public static T[] JsonLoad<T>()
    {
        try
        {
            string downloadPath = $"MasterData/{typeof(T).Name}";
            string json = Resources.Load<TextAsset>(downloadPath).text;
            Debug.Log($"downloadPath : {downloadPath} / json : {json}");
            var bytes = MessagePackSerializer.ConvertFromJson(json);
            return MessagePackSerializer.Deserialize<T[]>(bytes);
        }
        catch (Exception)
        {
            UnityEngine.Debug.LogError(string.Format("Jsonデータ : {0}のエラー", typeof(T).Name));
            UnityEngine.Debug.LogError("ファイルが入っていない可能性");
            throw;
        }
    }
}
