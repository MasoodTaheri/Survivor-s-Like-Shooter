using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesController : MonoBehaviour
{
    //public List<EnemyBehavior> enemyList;
    //public List<EnemyBehavior> aliveEnemyList;
    public EnemyControllerSO EnemyControllerData;
    public Action<int> OnEnemyKilledChanged;
    public Action<Vector3> OnEnemyKilled;

    private GameObject _player;
    private bool _allowGenerateEnemy;
    private EnemyControllerModel _enemyControllerModel;
    private int _deadEnemyCount = 0;

    //public List<EnemyBehavior> EnemyPrefabs;
    //private List<EnemyBehavior> Enemylist = new List<EnemyBehavior>();
    //private List<ObjectPool<EnemyBehavior>> _enemypool;
    //public ExtendedPool<EnemyBehavior> pool1;
    private List<ExtendedPool<EnemyBehavior>> EnemiesPool;


    //public EnemyBehavior TempEnemy;
    //public ObjectPool<EnemyBehavior> PooledObject;
    public void Initialize(GameObject player)
    {
        _player = player;
        _enemyControllerModel = new EnemyControllerModel(EnemyControllerData);
        _deadEnemyCount = 0;


        EnemiesPool = new List<ExtendedPool<EnemyBehavior>>();
        for (int i = 0; i < _enemyControllerModel.EnemyPrefabs.Count; i++)
        {
            int currentIndex = i;
            EnemiesPool.Add(new ExtendedPool<EnemyBehavior>(
            createFunc: () => { return Instantiate(_enemyControllerModel.EnemyPrefabs[currentIndex].item); },//create
            enemy => enemy.gameObject.SetActive(true),//onget
            enemy => enemy.gameObject.SetActive(false),//onrelease
            enemy => Destroy(enemy.gameObject)//ondestroy
            ));
        }
    }

    void Update()
    {
        if (_enemyControllerModel == null)
            return;
        _enemyControllerModel.IncreaseSpawnRate();
    }

    IEnumerator Spwner()
    {
        while (_allowGenerateEnemy)
        {
            if (_enemyControllerModel.EnemyPrefabs.Count > 0)
            {
                var enemy = EnemiesPool[_enemyControllerModel.selectRandomEnemyID()].Get();

                var randomPos = Random.insideUnitCircle.normalized * _enemyControllerModel.SpawnDistance;
                enemy.transform.position = _player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
                enemy.Initialize(this, _player);

                enemy.gameObject.SetActive(true);
                //Enemylist.Add(enemy);
            }
            yield return new WaitForSeconds(_enemyControllerModel.MaxSpawnRate - _enemyControllerModel.EnemySpawnRate + 1);
        }
    }

    public List<GameObject> GetAliveEnemyList()
    {
        if (EnemiesPool==null)
            return new List<GameObject>();
        List<GameObject> list = new List<GameObject>();
        //foreach (EnemyBehavior enemy in Enemylist)
            //list.Add(enemy.gameObject);

        foreach (var pool in EnemiesPool)
            for (int i=0;i<pool.AvailableItems.Count;i++)
                list.Add(pool.AvailableItems[i].gameObject);

            return list;
    }

    public void DestroyAllEnemies()
    {
        _allowGenerateEnemy = false;

        foreach (var pool in EnemiesPool)
            pool.Clear();

        //    foreach (var enemy in Enemylist)
        //{
        //    Destroy(enemy.gameObject);
        //}
    }

    public void StartEnemySpawner()
    {
        _allowGenerateEnemy = true;
        StartCoroutine(Spwner());
    }

    public void EnemyDead(EnemyBehavior enemy)
    {
        _deadEnemyCount++;

        OnEnemyKilledChanged?.Invoke(_deadEnemyCount);
        OnEnemyKilled?.Invoke(enemy.gameObject.transform.position);
        //Enemylist.Remove(enemy);
        //pool1[0].Release(enemy);
        foreach (var pool in EnemiesPool)
            if (pool.IsInList(enemy))
                pool.Release(enemy);
        //Destroy(enemy.gameObject);
    }
}
