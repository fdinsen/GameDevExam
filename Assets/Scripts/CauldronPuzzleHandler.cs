using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(DoorHandler))]
public class CauldronPuzzleHandler : MonoBehaviour
{
    [SerializeField]
    GemReceiver[] cauldrons;
    [SerializeField]
    private int _id;

    private DoorHandler door;

    private void Awake()
    {
        door = GetComponent<DoorHandler>();
    }

    public void CheckCauldrons()
    {
        bool isSolved = true;
        foreach(GemReceiver cauldron in cauldrons)
        {
            if(!cauldron.isActive())
            {
                isSolved = false;
                break;
            }
        }
        if(isSolved)
        {
            door.UnlockDoor();
        }
        else
        {
            door.LockDoor();
        }
    }

    public int ID
    {
        get { return _id; }
        set { _id = value; }
    }
}
