using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemReceiver : MonoBehaviour, IInteractable
{
    [SerializeField]
    public ItemObject keyItem;
    [SerializeField]
    private int _puzzleId;

    private bool activated = false;
    private CauldronPuzzleHandler puzzle;
    private InventoryObject inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
    }

    void Awake()
    {
        puzzle = FindConnectedPuzzle();
        if(puzzle == null)
        {
            Debug.LogError("No lever puzzle found with id: " + _puzzleId 
                + ". Have you set the id on the door and set the tag to LeverPuzzleDoor?");
        }
    }

    public void Interact()
    {
        if(keyItem != null)
        {
            var item = inventory.FindItemOnInventory(keyItem.data);
            if(item != null)
            {
                item.RemoveItem();
                activated = true;
                puzzle.CheckCauldrons();
                return;
            }
        }
    }

    public bool isActive()
    {
        return activated;
    }

    private CauldronPuzzleHandler FindConnectedPuzzle()
    {
        var puzzles = GameObject.FindGameObjectsWithTag("CauldronPuzzleDoor");
        foreach (GameObject door in puzzles)
        {
            var cauldron = door.GetComponent<CauldronPuzzleHandler>();
            if (cauldron.ID == _puzzleId)
            {
                return cauldron;
            }
        }
        return null;
    }
}
