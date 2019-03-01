using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Potion")]
public class Potion : ItemData
{
    public enum PotionType { Healing, Mana };
    [Header("Potion Stuff")]
    public PotionType potionType;
    public int restoreAmount;

    public override ItemData GetClone()
    {
        Potion PotionClone = ScriptableObject.CreateInstance<Potion>();
        PotionClone.itemName = itemName;
        PotionClone.itemID = itemID;
        PotionClone.weight = weight;
        PotionClone.value = value;
        PotionClone.sprite = sprite;
        PotionClone.stackCount = stackCount;
        PotionClone.stackSize = stackSize;

        PotionClone.potionType = potionType;
        PotionClone.restoreAmount = restoreAmount;

        return PotionClone;
    }

}
