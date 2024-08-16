using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyControllerModel
{
    public float SpawnDistance { get; private set; }
    public float EnemySpawnRate { get; private set; }
    public float MaxSpawnRate { get; private set; }
    public float IncreaseSpwanRate { get; private set; }
    public List<ObjectWithChance<EnemyBehavior>> EnemyPrefabs { get; private set; }


    public EnemyControllerModel(EnemyControllerSO data)
    {
        SpawnDistance = data.SpawnDistance;
        EnemySpawnRate = data.EnemySpawnRate;
        MaxSpawnRate = data.MaxSpawnRate;
        IncreaseSpwanRate = data.IncreaseSpwanRate;
        EnemyPrefabs=new List<ObjectWithChance<EnemyBehavior>>();
        EnemyPrefabs.AddRange(data.EnemyPrefabs);        
    }

    public void IncreaseSpawnRate()
    {
        if (EnemySpawnRate < MaxSpawnRate)
        {
            EnemySpawnRate += IncreaseSpwanRate;
        }
    }

    
    public int selectRandomEnemyID()
    {
        float totalChance = EnemyPrefabs.Sum(o => o.chance);
        float accumulatedChance = 0;
        for (int i=0;i<EnemyPrefabs.Count; i++)
        {
            EnemyPrefabs[i].SetRandomRange(accumulatedChance, accumulatedChance + EnemyPrefabs[i].chance);
            accumulatedChance += EnemyPrefabs[i].chance;
        }
        float randomValue= Random.Range(0, totalChance);
        for (int i = 0; i < EnemyPrefabs.Count; i++)
            if (EnemyPrefabs[i].CheckChanse(randomValue))
                return i;

        return -1;
    }
}
