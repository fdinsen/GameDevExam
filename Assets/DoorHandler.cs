using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _door;
    [SerializeField] private Animator _anim;
    [SerializeField] public bool Locked = false;
    
    private bool _open = false;

    public void Interact()
    {
        if(!Locked)
        {
            AccessDoor();
        }
        else
        {
            //Check for key
            //If player has key, AccessDoor(); 
            //If not, play locked effect
            _anim.SetTrigger("Locked");
        }
    }

    private void AccessDoor()
    {
        _open = !_open;
        _anim.SetBool("Open", _open);

        if (_open)
        {
            gameObject.layer = 0;
        }
        else
        {
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
