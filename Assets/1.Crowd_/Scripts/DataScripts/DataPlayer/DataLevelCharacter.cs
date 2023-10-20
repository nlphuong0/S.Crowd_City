using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;


[CreateAssetMenu(menuName = "Survivor/DataLevelPlayer")]
public class DataLevelCharacter : SerializedScriptableObject
{
    #region Load Data From Folder DatalevelPlayer

    [FolderPath(ParentFolder = "$folderName")]
    public string folderName;

    [Sirenix.OdinInspector.FilePath(Extensions = "cs")]
    public string test;


    [Button(ButtonStyle.CompactBox, Expanded = true)]
    public void UploadData()
    {
        string[] assetsPaths = AssetDatabase.FindAssets("", new[] { folderName });

        foreach (var dataLevel in assetsPaths)
        {
        }
    }
    // public DataCharacter ConvertAsset(string assetGame)
    // {
    //     string assetPath = AssetDatabase.GUIDToAssetPath(assetGame);
    //     DataCharacter asset = AssetDatabase.LoadAssetAtPath<DataCharacter>(assetPath);
    //     return asset;
    //
    // public string AddKeyNamePlayer(string assetGame)
    // {
    //     string[] parts = assetGame.Split('_');
    //     string lastPart = parts[parts.Length - 1];
    //     string desiredPart = parts[1];
    //     string finalPart = desiredPart.Split(' ')[0];
    //     return finalPart;
    // }
    //
    // public NamePlayer UpdateNamePlayerEnum(string assetGame)
    // {
    //     List<string> lines = File.ReadAllLines(test).ToList();
    //     if (!lines.Contains("    " + AddKeyNamePlayer(assetGame) + ","))
    //     {
    //         lines.RemoveAt(lines.Count - 1);
    //         lines.Add("    " + AddKeyNamePlayer(assetGame) + ",");
    //         lines.Add("}");
    //         NamePlayer namePlayer = (NamePlayer)Enum.Parse(typeof(NamePlayer), AddKeyNamePlayer(assetGame));
    //         File.WriteAllLines(test, lines);
    //         AssetDatabase.Refresh();
    //         return namePlayer;
    //     }
    //     else
    //     {
    //         return NamePlayer.None;
    //     }
    // }

    #endregion

    [DictionaryDrawerSettings(KeyLabel = "NAME CHARACTER", ValueLabel = "INFO LEVEL")]
    public Dictionary<NamePlayer, DataCharacter> dictLevelCharacters = new Dictionary<NamePlayer, DataCharacter>();

    public PlayerInfo GetDataCharacter(NamePlayer namePlayer, int level)
    {
        foreach (var dataCharacter in dictLevelCharacters)
        {
            if (dataCharacter.Key == namePlayer)
            {
                return dataCharacter.Value.GetDataPlayerInfo(level);
            }
        }

        return null;
    }
}

public enum NamePlayer
{
    None = 0,
}