public class EnemyControllerModel
{
    public float SpawnDistance { get; private set; }
    public float EnemySpawnRate { get; private set; }
    public float MaxSpawnRate { get; private set; }
    public float IncreaseSpwanRate { get; private set; }


    public EnemyControllerModel(EnemyControllerSO data)
    {
        SpawnDistance = data.SpawnDistance;
        EnemySpawnRate = data.EnemySpawnRate;
        MaxSpawnRate = data.MaxSpawnRate;
        IncreaseSpwanRate = data.IncreaseSpwanRate;
    }

    public void IncreaseSpawnRate()
    {
        if (EnemySpawnRate < MaxSpawnRate)
        {
            EnemySpawnRate += IncreaseSpwanRate;
        }
    }
}
