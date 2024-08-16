using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerGunController : MonoBehaviour
{
    public Action OnShoot;
    private Bullet _bulletPrefab;
    public Transform BulletSpawnPoint;
    public GameObject ShootTarget;
    private EnemiesController _enemiesController;
    private ObjectPool<Bullet> _bulletpool;
    private PlayerModel _playerModel;
    //public Bullet Bullet;
    //public float distanceToShoot;
    //public PlayerController playerController;
    //public UIHandler UIhandler;
    //public int ShootRate;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    public void Initialize(PlayerModel playerModel,
        EnemiesController enemiesController, Bullet bulletPrefab)
    {
        _playerModel = playerModel;
        _enemiesController = enemiesController; 
        _bulletPrefab = bulletPrefab;
        CreateBulletPool();
        StartCoroutine(AutomaticShooting());
    }

    private void CreateBulletPool()
    {
        //_bulletpool = new ObjectPool<Bullet>(
        //    () => { return Instantiate(_bulletPrefab); },
        //    bullet => bullet.gameObject.SetActive(true),
        //    Bullet => Bullet.gameObject.SetActive(false),
        //    Bullet => Destroy(Bullet.gameObject),
        //    false, 15, 20);
    }

    public void ReleaseBulletFromPool(Bullet bullet)
    {
        _bulletpool.Release(bullet);
        //_bulletpool.
    }

    private void OnEnable()
    {
        OnShoot += ShootProjectile;
    }

    private void OnDisable()
    {
        OnShoot -= ShootProjectile;
    }

    private void ShootProjectile()
    {
        if (!_playerModel.GetBullet())
            return;
        //UIhandler.UpdateAmmo(_playerModel.GetAmmoCount());
        //Bullet = Instantiate(BulletPrefab);
        var Bullet = _bulletpool.Get();
        Bullet.Init(this, BulletSpawnPoint.position, ShootTarget.transform.position);
    }

    IEnumerator AutomaticShooting()
    {
        int nearestEnemyId = -1;
        while (_playerModel.IsPlayerAlive())
        {
            var allEnemies = _enemiesController.GetAliveEnemyList();
            nearestEnemyId = FindNearestObject(_playerModel.ShootingRange, allEnemies);
            if (nearestEnemyId >= 0)
            {
                ShootTarget.transform.position = allEnemies[nearestEnemyId].transform.position;
                Shoot();
            }
            if (_playerModel.ShootRate > 0)
                yield return new WaitForSeconds(1.0f / _playerModel.ShootRate);
            else
                yield return new WaitForSeconds(1.0f);
        }
    }

    private void Shoot()
    {
        OnShoot?.Invoke();
    }

    public int FindNearestObject(float range, List<GameObject> objList)
    {
        int nearestID = -1;
        float nearestDistance = Mathf.Infinity;
        float distancetoEnemy = 0;
        for (int i = 0; i < objList.Count; i++)
        {
            distancetoEnemy = Vector3.Distance(transform.position, objList[i].transform.position);
            if (distancetoEnemy < nearestDistance)
            {
                nearestID = i;
                nearestDistance = distancetoEnemy;
            }
        }
        if (nearestDistance < _playerModel.ShootingRange)
            return nearestID;
        else return -1;
    }
}

