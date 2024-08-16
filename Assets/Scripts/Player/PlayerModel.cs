using System;

using UnityEngine;

public class PlayerModel
{
    public int ShootRate { get; private set; }
    public float MoveSpeed { get; private set; }

    public float ShootingRange { get; private set; }
    public Bullet BulletPrefab;
    public Action<int, int> OnHealthChanged;
    public Action<int, int> OnExperienceChanged;
    public Action<int, int> OnAmmoChanged;


    private int _health;
    private int _experience;
    private int AmmoCount;
    private int MaxHealth;
    private int MaxExp;
    private int MaxAmmo;


    public PlayerModel(PlayerData playerData)
    {
        _health = playerData.initialHealth;
        _experience = playerData.initialExperience;
        AmmoCount = playerData.initialAmmo;
        MaxHealth = playerData.maxHealth;
        MaxAmmo = playerData.maxAmmo;
        MaxExp = playerData.maxExp;
        ShootingRange = playerData.ShootingRange;
        ShootRate = playerData.ShootRate;
        BulletPrefab = playerData.BulletPrefab;
        MoveSpeed= playerData.MoveSpeed;
    }

    public void GetUpdatedValue()
    {
        OnAmmoChanged?.Invoke(AmmoCount, MaxAmmo);
        OnHealthChanged?.Invoke(_health, MaxHealth);
        OnExperienceChanged?.Invoke(_experience, MaxExp);
    }

    public bool GetBullet()
    {
        if (AmmoCount <= 0)
            return false;

        AmmoCount--;
        OnAmmoChanged?.Invoke(AmmoCount, MaxAmmo);
        return true;
    }


    public bool IsPlayerAlive()
    {
        return (_health > 0);
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Clamp(_health - damage, 0, MaxHealth);
        OnHealthChanged?.Invoke(_health, MaxHealth);
    }

    public void GetHealthLoot(int amount)
    {
        _health = Mathf.Clamp(_health + amount, 0, MaxHealth);
        OnHealthChanged?.Invoke(_health, MaxHealth);
    }

    public void GetExpLoot(int amount)
    {
        _experience = Mathf.Clamp(_experience + amount, 0, MaxExp);
        OnExperienceChanged?.Invoke(_experience, MaxExp);
    }
    public void GetAmmoLoot(int amount)
    {
        AmmoCount = Mathf.Clamp(AmmoCount + amount, 0, MaxAmmo);
        OnAmmoChanged?.Invoke(AmmoCount, MaxAmmo);
    }

}
