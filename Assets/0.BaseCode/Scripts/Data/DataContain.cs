using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DataContain : MonoBehaviour
{
    [SerializeField] private DataLevelCharacter _datalevelCharacter;
    [SerializeField] private DataLevelEnemy _dataLevelEnemy;

    public DataLevelCharacter DataLevelCharacter => _datalevelCharacter;
    public DataLevelEnemy DataLevelEnemy => _dataLevelEnemy;
}