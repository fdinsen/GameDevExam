using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _door;
     private Animator _anim;
    [SerializeField] public bool Locked = false;
    [SerializeField] private ItemObject keyNeeded;
    [SerializeField] private GameObject _interactEffect;
    [SerializeField] private AudioSource _openSound;
    [SerializeField] private AudioSource _lockedSound;

    private bool _open = false;
    private InventoryObject inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
    }

    public void Interact()
    {
        if(!Locked)
        {
            AccessDoor();
        }
        else
        {
            if(keyNeeded != null)
            {
                var item = inventory.FindItemOnInventory(keyNeeded.data);
                if (item != null)
                {
                    item.RemoveItem();
                    Locked = false;
                    AccessDoor();
                    return;
                } 
            }
            _lockedSound?.Play();
            _anim.SetTrigger("Locked");
        }
    }

    private void AccessDoor()
    {
        _open = !_open;
        _anim.SetBool("Open", _open);
        _openSound?.Play();

        if (_open)
        {
            _interactEffect?.SetActive(false);
            gameObject.layer = 0;
        }
        else
        {
            _interactEffect?.SetActive(true);
            gameObject.layer = 3; //BlocksMovement
        }
    }

    void Awake()
    {
        if(_anim == null) 
            _anim = GetComponent<Animator>();
    }

    public void UnlockDoor()
    {
        Locked = false;
        AccessDoor();
    }

    public void LockDoor()
    {
        Locked = true;
        gameObject.layer = 3; //BlocksMovement
        _anim.SetBool("Open", false);
        _open = false;
    }
    
}
