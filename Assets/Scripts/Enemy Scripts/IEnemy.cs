using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public abstract bool IsWithinRange(Vector3 playerPos, Vector3 currentPosition);
    public abstract void DoAttack(Vector3 currentPosition, Vector3 target);
    public abstract void Move();
}
