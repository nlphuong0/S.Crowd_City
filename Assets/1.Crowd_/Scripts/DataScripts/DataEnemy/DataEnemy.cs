using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Survivor/DataEnemy")]
public class DataEnemy : SerializedScriptableObject
{
    [DictionaryDrawerSettings(KeyLabel = "LEVEL", ValueLabel = "INFO ENEMY")]
    public Dictionary<int, EnemyInfo> enemyInfos = new Dictionary<int, EnemyInfo>();

    public EnemyInfo GetDataEnemyInfo(int level)
    {
        foreach (var playerInfo in enemyInfos)
        {
            if (playerInfo.Key == level)
            {
                EnemyInfo temp = new EnemyInfo(playerInfo.Value.hp, playerInfo.Value.damage, playerInfo.Value.speed);
                return temp;
            }
        }

        return null;
    }
}

[Serializable]
public class EnemyInfo : DataCharacterBase
{
    public EnemyInfo(int hp, float damage, float speed) : base(hp, damage, speed)
    {
    }
}