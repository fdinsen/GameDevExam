using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopStairHandler : MonoBehaviour
{
    [SerializeField] private GameObject _topPos;
    [SerializeField] private GameObject _bottomCollider;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if (other.transform.position.y < _topPos.transform.position.y - 0.051f)
            {
                TurnBasedPlayerMovement movement = other.GetComponent<TurnBasedPlayerMovement>();
                movement.currentlyTraveledStairs = gameObject;
                movement.SetMovePoint(_topPos.transform.position);
            }
            else
            {
                TurnBasedPlayerMovement movement = other.GetComponent<TurnBasedPlayerMovement>();
                movement.currentlyTraveledStairs = gameObject;
                movement.SetMovePoint(_bottomCollider.transform.position);
            }
        }
    }
}
