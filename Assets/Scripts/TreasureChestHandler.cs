using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChestHandler : MonoBehaviour, IInteractable
{
    InventoryObject inventory;
    [SerializeField]
    public ItemObject item;
    private Animator _anim;
    [SerializeField]
    public int quantity = 1;
    [SerializeField] private GameObject _interactEffect;
    [SerializeField] private AudioSource _openSound;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if(item != null)
        {
            _openSound?.Play();
            Loot();
            _anim.SetTrigger("Open");
            item = null;
            _interactEffect?.SetActive(false);
        }
    }

    public void Loot()
    {
        Item _item = new Item(item);
        inventory.AddItem(_item, quantity);
    }
}
