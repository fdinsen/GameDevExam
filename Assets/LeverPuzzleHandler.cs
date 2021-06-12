using System;
using UnityEngine;
[RequireComponent(typeof(DoorHandler))]
public class LeverPuzzleHandler : MonoBehaviour
{
    [SerializeField] LeverHandler[] levers;
    [SerializeField] private int _id;

    private DoorHandler door;

    private void Awake()
    {
        door = GetComponent<DoorHandler>();
    }
    public void CheckLevers()
    {
        bool isSolved = true;
        foreach(LeverHandler lever in levers)
        {
            if(!lever.IsCorrect())
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
