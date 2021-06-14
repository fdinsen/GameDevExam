using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator _anim;
    [SerializeField] private bool _correctOrientation = false;
    [SerializeField] private int _puzzleId;

    private bool activated = false;
    private LeverPuzzleHandler puzzle;
    public void Interact()
    {
        activated = !activated;
        _anim.SetBool("Activated", activated);
        puzzle.CheckLevers();
    }

    public bool IsCorrect()
    {
        return activated == _correctOrientation;
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

    private LeverPuzzleHandler FindConnectedPuzzle()
    {
        var puzzles = GameObject.FindGameObjectsWithTag("LeverPuzzleDoor");
        foreach (GameObject door in puzzles)
        {
            var lever = door.GetComponent<LeverPuzzleHandler>();
            if (lever.ID == _puzzleId)
            {
                return lever;
            }
        }
        return null;
    }
}
