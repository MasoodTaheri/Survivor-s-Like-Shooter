public class HealthLoot : Loot
{
    public override void ApplyEffect(PlayerModel playerModel)
    {
        playerModel.GetHealthLoot(data.Health);
    }


}
