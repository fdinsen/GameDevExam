using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronPotionGiver : MonoBehaviour, IInteractable
{

    InventoryObject inventory;
    [SerializeField]
    public ItemObject item;
    [SerializeField]
    public int quantity;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
    }

    public void Interact()
    {
        if(item != null)
        {
        Loot();
        item = null;
        }
    }

    public void Loot()
    {
        Item _item = new Item(item);
        inventory.AddItem(_item, quantity);
    }
}
