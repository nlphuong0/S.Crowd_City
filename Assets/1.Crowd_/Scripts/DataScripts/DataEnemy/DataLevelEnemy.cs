using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(menuName = "Survivor/DataLevelEnemy")]
public class DataLevelEnemy : SerializedScriptableObject
{
    [DictionaryDrawerSettings(KeyLabel = "NAME ENEMY", ValueLabel = "INFO LEVEL")]
    public Dictionary<NameEnemy, DataEnemy> dictLevelEnemys = new Dictionary<NameEnemy, DataEnemy>();

    public EnemyInfo GetDataEnemy(NameEnemy nameEnemy, int level)
    {
        foreach (var dataEnemy in dictLevelEnemys)
        {
            if (dataEnemy.Key == nameEnemy)
            {
                return dataEnemy.Value.GetDataEnemyInfo(level);
            }
        }

        return null;
    }
}

public enum NameEnemy
{
    None = 0,
    Bat = 1,
    Bee = 2,
}