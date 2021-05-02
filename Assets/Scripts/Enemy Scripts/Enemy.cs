using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy
{
    public abstract bool isWithinRange(Vector3 playerPos, Vector3 currentPosition);
    public abstract void doAttack(Vector3 currentPosition, Vector3 target);
}
