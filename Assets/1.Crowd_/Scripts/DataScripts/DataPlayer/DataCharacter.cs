using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(menuName = "Survivor/DataPlayer")]
public class DataCharacter : SerializedScriptableObject
{
    [DictionaryDrawerSettings(KeyLabel = "LEVEL", ValueLabel = "INFO CHARACTER")]
    public Dictionary<int, PlayerInfo> infoLevelPlayers = new Dictionary<int, PlayerInfo>();

    public PlayerInfo GetDataPlayerInfo(int level)
    {
        foreach (var playerInfo in infoLevelPlayers)
        {
            if (playerInfo.Key == level)
            {
                PlayerInfo temp = new PlayerInfo(playerInfo.Value.hp, playerInfo.Value.damage, playerInfo.Value.speed,
                    playerInfo.Value.weaponLengh, playerInfo.Value.rotationWeapon);
                return temp;
            }
        }

        return null;
    }
    
}

[Serializable]
public class PlayerInfo : DataCharacterBase
{
    public int weaponLengh;
    public float rotationWeapon;

    public PlayerInfo(int hp, float damage, float speed, int weaponLengh, float rotationWeapon) :
        base(hp, damage, speed)
    {
        this.weaponLengh = weaponLengh;
        this.rotationWeapon = rotationWeapon;
    }
}