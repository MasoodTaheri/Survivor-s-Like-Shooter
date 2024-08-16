using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    [SerializeField] protected LootSO data;
    [SerializeField]  LootManager _lootManager;
    public abstract void ApplyEffect(PlayerModel playerModel);
    public void Initialize(LootManager lootManager)
    {
        _lootManager = lootManager;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerController>().UpdateModel(LootData);
            _lootManager.CollectLoot(ApplyEffect);
            transform.DOMove(collision.transform.position, 0.25f).OnComplete(() => { Destroy(gameObject); });

        }
    }
}
public class HealthLoot : Loot
{
    public override void ApplyEffect(PlayerModel playerModel)
    {
        playerModel.GetHealthLoot(data.Health);
    }


}
