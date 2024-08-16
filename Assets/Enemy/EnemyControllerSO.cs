using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyController", menuName = "Game/EnemyController")]
public class EnemyControllerSO : ScriptableObject
{
    public float SpawnDistance;
    public float EnemySpawnRate;
    public float MaxSpawnRate;
    public float IncreaseSpwanRate;
    //public List<EnemyBehavior> EnemyPrefabs;
    public List<ObjectWithChance<EnemyBehavior>> EnemyPrefabs;
}


