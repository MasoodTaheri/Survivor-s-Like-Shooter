using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int maxHealth = 100;
    public int maxAmmo = 50;
    public int maxExp = 50;
    public int initialHealth = 100;
    public int initialAmmo = 50;
    public int initialExperience = 0;
    public float ShootingRange;
    public int ShootRate;
    public float MoveSpeed=5;
    public Bullet BulletPrefab;
}

