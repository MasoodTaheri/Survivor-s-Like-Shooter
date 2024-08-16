public class ExperienceLoot : Loot
{
    public override void ApplyEffect(PlayerModel playerModel)
    {
        playerModel.GetExpLoot(data.Exp);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        //collision.GetComponent<PlayerController>().UpdateModel(LootData);
    //        _lootManager.CollectLoot(ApplyEffect);
    //        transform.DOMove(collision.transform.position, 0.25f).OnComplete(() => { Destroy(gameObject); });

    //    }
    //}
}
