using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class SlimeEnemy : MonoBehaviour, IEnemy, IAttackable
{
    [SerializeField] private AttackDefinition slimeAttack;

    private CharacterStats _stats;
    private Animator _animator;
    private EnemyMovement _enemyMovement;
     

    void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _stats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();
    }
    public void DoAttack(Vector3 currentPosition, Vector3 target)
    {
        RaycastHit hit;
        transform.LookAt(target);
        if(Physics.Raycast(currentPosition, target, out hit, slimeAttack.Range))
        {
            bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                GameObject targetObj = hit.collider.gameObject;
                var attack = slimeAttack.CreateAttack(_stats, targetObj.GetComponent<CharacterStats>());

                var attackables = targetObj.GetComponentsInChildren(typeof(IAttackable));

                foreach (IAttackable attackable in attackables)
                {
                    attackable.OnAttack(gameObject, attack);
                }
            }
        }
        _animator.SetTrigger("Attack");
    }

    void Update()
    {
        Debug.DrawLine(transform.position, _enemyMovement.GetPlayerMovePoint().position * (slimeAttack.Range * 1.5f));
    }

    public bool IsWithinRange(Vector3 currentPosition, Vector3 playerPos)
    {
        Vector3 halfExtents = new Vector3(slimeAttack.Range, slimeAttack.Range, slimeAttack.Range);
        var colliders = Physics.OverlapBox(currentPosition, halfExtents);
        foreach(Collider col in colliders)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void Move()
    {
        if (!IsWithinRange(transform.position, _enemyMovement.GetPlayerMovePoint().position))
        {
            var dir = GetMoveDir(this.transform.position, _enemyMovement.GetPlayerMovePoint().position);
            _enemyMovement.SetMovePoint(GetPrioritizedListOfDirections(dir), dir, ref _enemyMovement.MovePoint);

            StartCoroutine(
                _enemyMovement.MoveObject(transform.position, _enemyMovement.MovePoint.position, _enemyMovement.GetMoveTime())
            );
            StartCoroutine(
                _enemyMovement.RotateObject(transform.rotation, _enemyMovement.MovePoint.rotation, _enemyMovement.GetMoveTime())
            );
        }
        else
        {
            DoAttack(transform.position, _enemyMovement.GetPlayer().transform.position);
        }
    }

    private Vector3 GetMoveDir(Vector3 source, Vector3 target)
    {
        return (target - source).normalized;
    }

    private List<Direction> GetPrioritizedListOfDirections(Vector3 dir)
    {
        //UGLY SOLUTION; SHOULD BE IMPROVED
        Direction[] movePriority = new Direction[4];
        if (Mathf.Abs(dir.x) < Mathf.Abs(dir.z))
        {
            if (dir.z > 0)
            {
                movePriority[0] = Direction.UP;
                movePriority[3] = Direction.DOWN;
            }
            else
            {
                movePriority[0] = Direction.DOWN;
                movePriority[3] = Direction.UP;
            }
            if (dir.x > 0)
            {
                movePriority[1] = Direction.RIGHT;
                movePriority[2] = Direction.LEFT;
            }
            else
            {
                movePriority[1] = Direction.LEFT;
                movePriority[2] = Direction.RIGHT;
            }
        }
        else
        {
            if (dir.x > 0)
            {
                movePriority[0] = Direction.RIGHT;
                movePriority[3] = Direction.LEFT;
            }
            else
            {
                movePriority[0] = Direction.LEFT;
                movePriority[3] = Direction.RIGHT;
            }
            if (dir.z > 0)
            {
                movePriority[1] = Direction.UP;
                movePriority[2] = Direction.DOWN;
            }
            else
            {
                movePriority[1] = Direction.DOWN;
                movePriority[2] = Direction.UP;
            }
        }
        return new List<Direction>(movePriority);
    }

    public void OnAttack(GameObject attacker, Attack attack)
    {
        if (attack.IsCritical)
        {
            Debug.Log("Critical Damage!!");
        }

        Debug.LogFormat("{0} attacked {1} for {2} damage.", attacker.name, name, attack.Damage);
    }
}
