using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour, Enemy
{
    public void doAttack(Vector3 currentPosition, Vector3 target)
    {
        Debug.Log("Did attack");
    }

    public bool isWithinRange(Vector3 currentPosition, Vector3 playerPos)
    {
        Vector3 distance = playerPos - currentPosition;
        return Mathf.Abs(Mathf.Abs(distance.x) - Mathf.Abs(distance.z)) == .5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
