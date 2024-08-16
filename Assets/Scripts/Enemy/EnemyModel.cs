using UnityEngine;

public class EnemyModel
{
    public float Health { get; private set; }
    public int DamagePerSecond { get; private set; }
    public int Speed { get; private set; }

    public EnemyModel(EnemySO data)
    {
        Health = data.initHealth;
        DamagePerSecond = data.DamagePerSecond;
        Speed = data.Speed;
    }

    public void TakeDamage(float damage)
    {
        Health = Mathf.Clamp(Health - damage, 0, 100);
    }

}
