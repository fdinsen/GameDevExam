using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronPotionGiver : MonoBehaviour, IInteractable
{

    InventoryObject inventory;
    [SerializeField]
    private ItemObject item;
    [SerializeField]
    private int quantity;
    [SerializeField] private GameObject[] _content;
    [SerializeField] private AudioSource _pickup;

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
            _pickup?.Play();
            foreach(var content in _content)
            {
                content.SetActive(false);
            }
        }
    }

    public void Loot()
    {
        Item _item = new Item(item);
        inventory.AddItem(_item, quantity);
    }
}
