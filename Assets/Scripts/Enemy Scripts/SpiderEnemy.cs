using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class SpiderEnemy : MonoBehaviour, IEnemy, IAttackable
{
    [SerializeField] Vector3[] possibleMoves;
    [SerializeField] Vector3[] longMoves;
    [SerializeField] private AttackDefinition spiderAttack;

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
        if (Physics.Raycast(currentPosition, target, out hit, spiderAttack.Range))
        {
            bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                GameObject targetObj = hit.collider.gameObject;
                var attack = spiderAttack.CreateAttack(_stats, targetObj.GetComponent<CharacterStats>());

                var attackables = targetObj.GetComponentsInChildren(typeof(IAttackable));

                foreach (IAttackable attackable in attackables)
                {
                    attackable.OnAttacked(gameObject, attack);
                }
            }
        }
        _animator.SetTrigger("Attack");
    }

    public bool IsWithinRange(Vector3 playerPos, Vector3 currentPosition)
    {
        //Debug.Log((playerPos - currentPosition).magnitude <= spiderAttack.Range);
        return (playerPos - currentPosition).magnitude <= spiderAttack.Range;
    }

    public void Move()
    {
        if(!IsWithinRange(_enemyMovement.GetPlayerMovePoint().position, transform.position))
        {
            Vector3 dir = transform.position - _enemyMovement.GetPlayer().transform.position;
            Vector3 target;
            if (IsPlayerFarAway(transform.position, _enemyMovement.GetPlayerMovePoint().position))
            {
                target = GetClosestMove(dir, longMoves) * _enemyMovement.GetMoveDistance();
            }
            else
            {
                target = GetClosestMove(dir, possibleMoves) * _enemyMovement.GetMoveDistance();
            }

            _enemyMovement.SetMovePoint(target, ref _enemyMovement.MovePoint);

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

    public void OnAttacked(GameObject attacker, Attack attack)
    {
        _animator.SetTrigger("Hit");
        if (attack.IsCritical)
        {
            Debug.Log("Critical Damage!!");
        }
        _stats.TakeDamage(attack.Damage);
        _enemyMovement.SetStunTime(attack.StunTime);
        Debug.LogFormat("{0} attacked {1} for {2} damage.", attacker.name, name, attack.Damage);
    }

    private bool IsPlayerFarAway(Vector3 currentPos, Vector3 playerPos)
    {
        Debug.Log(Vector3.Distance(currentPos, playerPos));
        return Vector3.Distance(currentPos, playerPos) > (spiderAttack.Range * 4);
    }

    private Vector3 GetClosestMove(Vector3 dir, Vector3[] moves)
    {
        Vector3 closest = new Vector3(0, 0, 0);
        foreach(Vector3 pos in moves)
        {
            Vector3 closestDif = closest - dir;
            Vector3 posDif = pos - dir;
            if(closestDif.magnitude <= posDif.magnitude)
            {
                if(_enemyMovement.CheckCollision(transform.position, pos))
                    closest = pos;
            }
        }
        return closest;
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
