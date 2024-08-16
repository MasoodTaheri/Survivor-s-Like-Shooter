using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class LootManager : MonoBehaviour
{
    public List<Loot> lootList;
    public PlayerModel _playerModel;
    public float SpawnDistance;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _delayForRespawn;
    public List<ExtendedPool<Loot>> LootPools;


    public void Initialize(PlayerModel playerModel, GameObject player, EnemiesController enemiesController)
    {
        _playerModel = playerModel;
        _player = player;
        StartCoroutine(Spwner());
        enemiesController.OnEnemyKilled += SpawnLoot;



        LootPools = new List<ExtendedPool<Loot>>();
        for (int i = 0; i < lootList.Count; i++)
        {
            int currentIndex = i;
            LootPools.Add(new ExtendedPool<Loot>(
            createFunc: () => { return Instantiate(lootList[currentIndex]); },//create
            loot => loot.gameObject.SetActive(true),//onget
            loot => loot.gameObject.SetActive(false),//onrelease
            loot => Destroy(loot.gameObject)//ondestroy
            ));
        }

        GenerateNewRandomLoot();

    }
    public void SpawnLoot(Vector3 position)
    {
        int r = Random.Range(0, LootPools.Count);
        var l = LootPools[r].Get();//Instantiate(lootList[r]);
        l.Initialize(this);
        l.transform.position = position;
    }

    public void CollectLoot(Action<PlayerModel> ApplyEffect)
    {
        ApplyEffect(_playerModel);
    }

    public void ReleaseLoot(Loot obj)
    {
        foreach (var pool in LootPools)
            if (pool.IsInList(obj))
                pool.Release(obj);
    }

    private void GenerateNewRandomLoot()
    {
        var randomPos = Random.insideUnitCircle.normalized * SpawnDistance;
        Vector3 lootPos = _player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
        SpawnLoot(lootPos);
    }

    IEnumerator Spwner()
    {
        while (_playerModel.IsPlayerAlive())
        {
            yield return new WaitForSeconds(_delayForRespawn);
            GenerateNewRandomLoot();
        }
    }
}
