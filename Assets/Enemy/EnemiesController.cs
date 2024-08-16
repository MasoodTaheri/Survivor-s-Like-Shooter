using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemiesController : MonoBehaviour
{
    public List<EnemyBehavior> enemyList;
    public List<EnemyBehavior> aliveEnemyList;
    public EnemyControllerSO EnemyControllerData;
    public Action<int> OnEnemyKilledChanged;
    public Action<Vector3> OnEnemyKilled;

    private GameObject _player;
    private bool _allowGenerateEnemy;
    private EnemyControllerModel _enemyControllerModel;
    private int _deadEnemyCount = 0;


    public void Initialize(GameObject player)
    {
        _player = player;
        //_uiHandler = uiHandler;
        //_lootManager = lootManager;
        _enemyControllerModel = new EnemyControllerModel(EnemyControllerData);
        _deadEnemyCount = 0;
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
            if (enemyList.Count > 0)
            {
                var enemy = enemyList[Random.Range(0, enemyList.Count)];
                var randomPos = Random.insideUnitCircle.normalized * _enemyControllerModel.SpawnDistance;
                enemy.transform.position = _player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
                enemy.Initialize(this, _player);

                enemy.gameObject.SetActive(true);
                aliveEnemyList.Add(enemy);
                enemyList.Remove(enemy);
            }
            yield return new WaitForSeconds(_enemyControllerModel.MaxSpawnRate - _enemyControllerModel.EnemySpawnRate + 1);
        }
    }

    public List<GameObject> GetAliveEnemyList()
    {
        //aliveEnemyList.RemoveAll(x => x._currentHealth <= 0);
        //foreach (var enemy in aliveEnemyList)
        //{
        //    if (enemy._health <= 0)
        //        enemyList.Add(enemy);
        //}

        foreach (var enemy in enemyList)
            aliveEnemyList.Remove(enemy);

        List<GameObject> list = new List<GameObject>();
        foreach (EnemyBehavior enemy in aliveEnemyList)
            list.Add(enemy.gameObject);
        return list;
    }

    public void DestroyAllEnemies()
    {
        _allowGenerateEnemy = false;
        foreach (var enemy in aliveEnemyList)
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
        aliveEnemyList.Remove(enemy);
        enemyList.Add(enemy);
        _deadEnemyCount++;

        OnEnemyKilledChanged?.Invoke(_deadEnemyCount);
        //_uiHandler.IncKillCount();
        OnEnemyKilled?.Invoke(enemy.gameObject.transform.position);
        //_lootManager.SpawnLoot(enemy.gameObject.transform.position);
    }
}
