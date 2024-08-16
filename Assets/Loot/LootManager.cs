using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootManager : MonoBehaviour
{
    public List<Loot> lootList;
    public PlayerModel _playerModel;
    //[SerializeField] private int lootCount;
    public float SpawnDistance;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _delayForRespawn;

    public void Initialize(PlayerModel playerModel, GameObject player, EnemiesController enemiesController)
    {
        _playerModel = playerModel;
        _player = player;
        GenerateNewRandomLoot();
        StartCoroutine(Spwner());
        enemiesController.OnEnemyKilled += SpawnLoot;
    }
    public void SpawnLoot(Vector3 position)
    {
        int r = Random.Range(0, lootList.Count);
        var l = Instantiate(lootList[r]);
        l.Initialize(this);
        l.transform.position = position;
        //lootCount++;
    }

    public void CollectLoot(Action<PlayerModel> ApplyEffect)
    {
        ApplyEffect(_playerModel);
        //lootCount--;
    }

    private void GenerateNewRandomLoot()
    {
        var randomPos = Random.insideUnitCircle.normalized * SpawnDistance;
        Vector3 lootPos = _player.transform.position + new Vector3(randomPos.x, randomPos.y, 0);
        SpawnLoot(lootPos);
    }

    IEnumerator Spwner()
    {
        while (true)
        {
            yield return new WaitForSeconds(_delayForRespawn);
            GenerateNewRandomLoot();
        }
    }
}
