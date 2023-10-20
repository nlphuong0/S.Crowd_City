using System;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;
using UnityEngine;

public enum TypePools
{
    None = 1,
    Box = 2
}

public class PoolingManager : Singleton<PoolingManager>
{
    [SerializeField] private Dictionary<TypePools, GameObject> pools = new Dictionary<TypePools, GameObject>();


    #region Get Name and Get Prefabs from Dictionary Pools

    private GameObject GetPrefabs(TypePools typePools)
    {
        foreach (var pool in pools)
        {
            if (pool.Key == typePools) return pool.Value;
        }

        return null;
    }

    private string GetNamePrefabs(TypePools typePools)
    {
        return typePools.ToString();
    }

    #endregion

    #region Methods Handle Spawn Prefabs from PoolsManager

    public void SpawnPrefab(TypePools typePools, Vector3 positionSpawn, Quaternion rotation)
    {
        PoolManager.Pools[GetNamePrefabs(typePools)].Spawn(GetPrefabs(typePools), positionSpawn, rotation);
    }

    public Transform SpawnPrefab(TypePools typePools, Transform parentTransform)
    {
        if (!PoolManager.Pools.ContainsKey(GetNamePrefabs(typePools)))
        {
            PoolManager.Pools.Create(GetNamePrefabs(typePools));
        }

        return PoolManager.Pools[GetNamePrefabs(typePools)].Spawn(GetPrefabs(typePools), parentTransform);
    }

    public void SpawnPrefab(TypePools typePools, Vector3 positionSpawn, Quaternion rotation, Transform parentTransform)
    {
        PoolManager.Pools[GetNamePrefabs(typePools)]
            .Spawn(GetPrefabs(typePools), positionSpawn, rotation, parentTransform);
    }

    #endregion

    #region Methods Handle Despawn Prefabs from PoolsManager

    public void DespawnPrefab(TypePools typePools, Transform prefabs)
    {
        PoolManager.Pools[GetNamePrefabs(typePools)].Despawn(prefabs);
    }

    #endregion
}