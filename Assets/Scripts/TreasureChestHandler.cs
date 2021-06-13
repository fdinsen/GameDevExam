using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestHandler : MonoBehaviour, IInteractable
{
    InventoryObject inventory;
    [SerializeField]
    public ItemObject item;
    private Animator _anim;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if(item != null)
        {
            Loot();
        _anim.SetTrigger("Open");
        item = null;
        }
    }

    public void Loot()
    {
        Item _item = new Item(item);
        inventory.AddItem(_item, 1);
    }
}
