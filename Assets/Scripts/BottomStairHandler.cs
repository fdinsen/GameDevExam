using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomStairHandler : MonoBehaviour
{
    [SerializeField] private GameObject _bottomPos;
    [SerializeField] private GameObject _topCollider;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Player"))
        {
            if(other.transform.position.y > _bottomPos.transform.position.y + 0.051f)
            {
                TurnBasedPlayerMovement movement = other.GetComponent<TurnBasedPlayerMovement>();
                movement.currentlyTraveledStairs = gameObject;
                movement.SetMovePoint(_bottomPos.transform.position);
            }
            else
            {
                TurnBasedPlayerMovement movement = other.GetComponent<TurnBasedPlayerMovement>();
                movement.currentlyTraveledStairs = gameObject;
                movement.SetMovePoint(_topCollider.transform.position);
            }
        }
    }
}
