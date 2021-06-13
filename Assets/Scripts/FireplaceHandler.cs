using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private Item keyNeeded;
    [SerializeField] private Animator _anim;
    [SerializeField] public ItemObject _item;
    [SerializeField] private GameObject _interactEffect;

    private InventoryObject _inventory;

    // Start is called before the first frame update
    void Start()
    {
        _inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
        _anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        var key = _inventory.FindItemOnInventory(keyNeeded);
        if(key != null && _item != null)
        {
            _anim.SetTrigger("PickGem");
            key.RemoveItem();
            Item reward = new Item(_item);
            _inventory.AddItem(reward, 1) ;
            _item = null;
            Destroy(_interactEffect);
        }
    }
}
