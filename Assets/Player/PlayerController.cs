using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerModel
{
    private int _health;
    private int _experience;
    private int AmmoCount;
    private int MaxHealth;
    private int MaxExp;
    private int MaxAmmo;
    public int ShootRate { get; private set; }
    public float MoveSpeed { get; private set; }

    public float ShootingRange { get; private set; }
    public Bullet BulletPrefab;

    public Action<int, int> OnHealthChanged;
    public Action<int, int> OnExperienceChanged;
    public Action<int, int> OnAmmoChanged;


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

    //public int GetAmmoCount()
    //{
    //    return AmmoCount;
    //}

    //public void UpdatePlayerModel(PlayerModel newValues)
    //{
    //    if (newValues == null) return;
    //    _health = Mathf.Clamp(_health + newValues._health, 0, MaxHealth);
    //    _experience = Mathf.Clamp(_experience + newValues._experience, 0, MaxExp);
    //    AmmoCount = Mathf.Clamp(AmmoCount + newValues.AmmoCount, 0, MaxAmmo);
    //    OnAmmoChanged?.Invoke(AmmoCount, MaxAmmo);
    //    OnHealthChanged?.Invoke(_health, MaxHealth);
    //    OnExperienceChanged?.Invoke(_experience, MaxExp);
    //}

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
public class PlayerController : MonoBehaviour, IHealth
{
    private Rigidbody2D m_rigidbody;

    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    //public string jumpButton = "Jump";

    private float inputHorizontal;
    private float inputVertical;
    //public float MoveSpeed = 20.0f;
    public PlayerGunController playerGunController;
    private float f;
    public Transform playerGraphics;

    [SerializeField] private PlayerModel _playerModel;
    //public UIHandler UIHandler;



    //void Awake()
    //{
    //    Init();
    //    //UpdateUI();
    //}

    public void Init(PlayerModel playermodel, EnemiesController enemiesController)
    {
        _playerModel = playermodel;
        playerGunController.Initialize(_playerModel, enemiesController, playermodel.BulletPrefab);
        m_rigidbody = GetComponent<Rigidbody2D>();
        //ResetHealth();
    }

    void Update()
    {
        inputHorizontal = SimpleInput.GetAxis(horizontalAxis);
        inputVertical = SimpleInput.GetAxis(verticalAxis);

        //transform.Rotate(0f, inputHorizontal * 5f, 0f, Space.World);

        //if (SimpleInput.GetButtonDown(jumpButton) && IsGrounded())
        //    m_rigidbody.AddForce(0f, 10f, 0f, ForceMode.Impulse);


        f = playerGunController.ShootTarget.transform.localPosition.x - transform.position.x;
        if (f < 0)
            playerGraphics.localScale = new Vector3(-1, 1, 1);
        else
            playerGraphics.localScale = new Vector3(1, 1, 1);
    }

    void FixedUpdate()
    {
        if (inputHorizontal != 0 || inputVertical != 0)
            m_rigidbody.velocity = new Vector3(inputHorizontal, inputVertical, 0f) *
                _playerModel.MoveSpeed;
        else
            m_rigidbody.velocity = Vector3.zero;
    }

    //public void ResetHealth()
    //{
    //    //PlayerModel._health = PlayerModel.MaxHealth;
    //}

    public void TakeDamage(float damage)
    {
        //PlayerModel._health -= (int)damage;
        _playerModel.TakeDamage((int)damage);
        if (!_playerModel.IsPlayerAlive())
            Debug.Log("player is dead");

        //UIHandler.UpdateHealth(PlayerModel._health, PlayerModel.MaxHealth);
    }

    //public void UpdateModel(PlayerModel lootModel)
    //{
    //    _playerModel.UpdatePlayerModel(lootModel);
    //    //UpdateUI();
    //}

    //private void UpdateUI()
    //{
    //    UIHandler.ExperienceUpdate(PlayerModel._experience, PlayerModel.MaxExp);
    //    UIHandler.UpdateHealth(PlayerModel._health, PlayerModel.MaxHealth);
    //    UIHandler.UpdateAmmo(PlayerModel.AmmoCount);

    //}

    public bool isPlayerAlive()
    {
        return _playerModel.IsPlayerAlive();
    }
}
