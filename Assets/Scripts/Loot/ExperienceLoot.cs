public class ExperienceLoot : Loot
{
    public override void ApplyEffect(PlayerModel playerModel)
    {
        playerModel.GetExpLoot(data.Exp);
    }
}
