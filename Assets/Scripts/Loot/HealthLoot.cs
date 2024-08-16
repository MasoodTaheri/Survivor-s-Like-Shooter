using DG.Tweening;
using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    [SerializeField] protected LootSO data;
    [SerializeField] LootManager _lootManager;
    public abstract void ApplyEffect(PlayerModel playerModel);
    public void Initialize(LootManager lootManager)
    {
        _lootManager = lootManager;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _lootManager.CollectLoot(ApplyEffect);
            transform.DOMove(collision.transform.position, 0.25f).
                OnComplete(() =>
                {
                    _lootManager.ReleaseLoot(this);
                });
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
