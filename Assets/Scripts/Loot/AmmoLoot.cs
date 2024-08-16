public class AmmoLoot : Loot
{
    public override void ApplyEffect(PlayerModel playerModel)
    {
        playerModel.GetAmmoLoot(data.Ammo);
    }
}
