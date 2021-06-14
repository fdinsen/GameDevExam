using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlime : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _enemy;

    void Start()
    {
        _enemy.SetActive(false);
    }

    public void Interact()
    {
        _enemy.SetActive(true);
    }
}
