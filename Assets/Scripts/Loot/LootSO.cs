using UnityEngine;

[CreateAssetMenu(fileName = "NewLoot", menuName = "Game/Loot")]
public class LootSO : ScriptableObject
{
    public int Health;
    public int Ammo;
    public int Exp;
}

