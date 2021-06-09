using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverHandler : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator anim;

    private bool activated = false;
    public void Interact()
    {
        activated = !activated;
        anim.SetBool("Activated", activated);
    }
}
