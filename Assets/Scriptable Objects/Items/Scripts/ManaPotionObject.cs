using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Mana Potion Object", menuName = "Inventory System/Items/Mana Potion")]
public class ManaPotionObject : ItemObject
{
    public int restoreManaValue;
    public void Awake() 
    {
        type = ItemType.ManaPotion;
    }

}
