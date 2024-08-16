using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy")]
public class EnemySO : ScriptableObject
{
    public float initHealth;
    public int DamagePerSecond;
    public int Speed;
}


