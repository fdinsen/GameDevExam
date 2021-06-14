using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableItemHandler : MonoBehaviour, IInteractable
{
    InventoryObject inventory;
    [SerializeField]
    public ItemObject item;
    [SerializeField]
    public GameObject PoofEffect;
    [SerializeField]
    public GameObject TableItem;
    [SerializeField]
    public int quantity;
    [SerializeField] private AudioSource _pickup;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
        //TableItem = GameObject.FindGameObjectWithTag("TableItem");
    }

    public void Interact()
    {
        if(item != null)
        {
            _pickup?.Play();
            Loot();
            item = null;
            Instantiate(PoofEffect, transform.position, Quaternion.identity);
            Destroy(TableItem);
        }
    }

    public void Loot()
    {
        Item _item = new Item(item);
        inventory.AddItem(_item, quantity);
    }
}
