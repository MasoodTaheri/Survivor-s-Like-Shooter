using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

[Serializable]
public class ExtendedPool<T> where T : class
{
    public List<T> AvailableItems = new List<T>();
    [SerializeField] private ObjectPool<T> _pool;

    public ExtendedPool(Func<T> createFunc,
        Action<T> actionOnGet = null,
        Action<T> actionOnRelease = null,
             Action<T> actionOndestroy = null)
    {
        _pool = new ObjectPool<T>(
         createFunc,
         actionOnGet,
         actionOnRelease,
         actionOndestroy,
         false, 15, 20);
    }

    public T Get()
    {
        T temp = _pool.Get();
        AvailableItems.Add(temp);
        return temp;
    }

    public void Release(T item)
    {
        AvailableItems.Remove(item);
        _pool.Release(item);
    }
}

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
    private List<EnemyBehavior> Enemylist;
    //private List<ObjectPool<EnemyBehavior>> _enemypool;
    //public ExtendedPool<EnemyBehavior> pool1;

    public void Initialize(GameObject player)
    {
        _player = player;
        //_uiHandler = uiHandler;
        //_lootManager = lootManager;
        _enemyControllerModel = new EnemyControllerModel(EnemyControllerData);
        _deadEnemyCount = 0;
        //EnemyPrefabs = new List<EnemyBehavior>();
        //EnemyPrefabs.AddRange(_enemyControllerModel.EnemyPrefabs);



        // for (int i = 0; i < EnemyPrefabs.Count; i++)
        // {
        //     var enemypool = new ObjectPool<EnemyBehavior>(
        //() => { return Instantiate(EnemyPrefabs[i]); },//create
        //enemy => enemy.gameObject.SetActive(true),//onget
        //enemy => enemy.gameObject.SetActive(false),//onrelease
        //enemy => Destroy(enemy.gameObject),//ondestroy
        //false, 15, 20);
        //     _enemypool.Add(enemypool);

        //     //_enemypool[0].Get();
        // }

      //  pool1 = new ExtendedPool<EnemyBehavior>(
      //createFunc: () => { return Instantiate(_enemyControllerModel.EnemyPrefabs[0].item); },
      //actionOnGet: enemy => { }

      //)
      //  {

      //  }

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
                //var enemy = Instantiate(_enemyControllerModel.EnemyPrefabs[Random.Range(0, _enemyControllerModel.EnemyPrefabs.Count)]);
                var enemy = Instantiate(_enemyControllerModel.selectRandomEnemy());

                var randomPos = Random.insideUnitCircle.normalized * _enemyControllerModel.SpawnDistance;
                enemy.transform.position = _player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
                enemy.Initialize(this, _player);

                enemy.gameObject.SetActive(true);
                Enemylist.Add(enemy);
            }
            yield return new WaitForSeconds(_enemyControllerModel.MaxSpawnRate - _enemyControllerModel.EnemySpawnRate + 1);
        }
    }

    public List<GameObject> GetAliveEnemyList()
    {
        List<GameObject> list = new List<GameObject>();
        foreach (EnemyBehavior enemy in Enemylist)
            list.Add(enemy.gameObject);
        return list;
    }

    public void DestroyAllEnemies()
    {
        _allowGenerateEnemy = false;
        foreach (var enemy in Enemylist)
        {
            Destroy(enemy.gameObject);
        }
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
        Enemylist.Remove(enemy);
        Destroy(enemy.gameObject);
    }
}
