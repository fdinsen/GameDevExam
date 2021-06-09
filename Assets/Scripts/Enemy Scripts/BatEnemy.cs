using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour, IEnemy, IAttackable
{
    [SerializeField] private AttackDefinition batAttack;
    [SerializeField] private int minIntervalBetweenCharges = 5;
    [SerializeField] private int maxIntervalBetweenCharges = 10;
    [SerializeField] private int turnsStunnedAfterCharge = 2;

    private bool isCharging = false;
    private Vector3 chargeDirection = new Vector3(0, 0, 0);
    private int countdownToCharge;

    private CharacterStats _stats;
    private Animator _animator;
    private EnemyMovement _enemyMovement;

    void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _stats = GetComponent<CharacterStats>();
        _animator = GetComponent<Animator>();
        countdownToCharge = Random.Range(minIntervalBetweenCharges, maxIntervalBetweenCharges);
    }

    public void DoAttack(Vector3 currentPosition, Vector3 target)
    {
        RaycastHit hit;
        transform.LookAt(target);
        if (Physics.Raycast(currentPosition, target, out hit, batAttack.Range))
        {
            bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                GameObject targetObj = hit.collider.gameObject;
                if (targetObj.CompareTag("Player"))
                    Debug.Log("ATTACCCCKKKK!!!!!");
                {
                    var attack = batAttack.CreateAttack(_stats, targetObj.GetComponent<CharacterStats>());

                    var attackables = targetObj.GetComponentsInChildren(typeof(IAttackable));

                    foreach (IAttackable attackable in attackables)
                    {
                        attackable.OnAttacked(gameObject, attack);
                    }
                }
            }
        }
       // _animator.SetTrigger("Attack");
    }

    public bool IsWithinRange(Vector3 playerPos, Vector3 currentPosition)
    {
        if(countdownToCharge <= 0)
        {
            return true;
        }
        return false;
    }

    public void Move()
    {
        countdownToCharge--;
        if (!isCharging)
        {
            if(!IsWithinRange(transform.position, _enemyMovement.GetPlayerMovePoint().position))
            {
                // flipped target/source to have bat move away from player
                var dir = _enemyMovement.GetMoveDir(_enemyMovement.GetPlayerMovePoint().position, this.transform.position);
                _enemyMovement.SetMovePointFromDirectionList(_enemyMovement.GetPrioritizedListOfDirections(dir), dir, ref _enemyMovement.MovePoint);

                StartCoroutine(
                    _enemyMovement.MoveObject(transform.position, _enemyMovement.MovePoint.position, _enemyMovement.GetMoveTime())
                );
                StartCoroutine(
                    _enemyMovement.RotateObject(transform.rotation, _enemyMovement.MovePoint.rotation, _enemyMovement.GetMoveTime())
                );
                transform.rotation = _enemyMovement.MovePoint.rotation;
            } 
            else
            {
                var dir = _enemyMovement.GetMoveDir(this.transform.position, _enemyMovement.GetPlayerMovePoint().position);
                chargeDirection = CalculateChargeDirection(_enemyMovement.GetPrioritizedListOfDirections(dir)[0], dir);
                isCharging = true;
                _enemyMovement.SetIgnoreLOS(true);
                DoCharge();
            }
        } 
        else
        {
            DoCharge();
        }
    }

    public void DoCharge()
    {
        _animator.SetBool("Charging", true);
        if(_enemyMovement.CheckCollision(transform.position, chargeDirection))
        {
            if(_enemyMovement.CheckCollision(transform.position, chargeDirection * 2))
            {
                _enemyMovement.SetMovePoint(chargeDirection * 2, ref _enemyMovement.MovePoint);
            }
            else if(_enemyMovement.CheckCollision(transform.position, chargeDirection))
            {
                _enemyMovement.SetMovePoint(chargeDirection, ref _enemyMovement.MovePoint);
                EndCharge();
            } else
            {
                EndCharge();
            }
            StartCoroutine(
                    _enemyMovement.MoveObject(transform.position, _enemyMovement.MovePoint.position, _enemyMovement.GetMoveTime())
            );
            StartCoroutine(
                _enemyMovement.RotateObject(transform.rotation, _enemyMovement.MovePoint.rotation, _enemyMovement.GetMoveTime())
            );
        } 
        else
        {
            EndCharge();
        }
    }

    private void EndCharge()
    {
        DoAttack(transform.position, transform.position + chargeDirection);
        _enemyMovement.SetStunTime(turnsStunnedAfterCharge);
        Debug.Log("End Charge");
        _enemyMovement.SetIgnoreLOS(false);
        _animator.SetBool("Charging", false);
        countdownToCharge = Random.Range(minIntervalBetweenCharges, maxIntervalBetweenCharges);
        isCharging = false;
    }

    public Vector3 CalculateChargeDirection(Direction moveDirection, Vector3 dir)
    {
        if (moveDirection == Direction.RIGHT || moveDirection == Direction.LEFT)
        {
            return new Vector3(_enemyMovement.NormalizeSingleDirection(dir.x) * _enemyMovement.GetMoveDistance(), 0f, 0f);
        }
        else
        {
            return new Vector3(0f, 0f, _enemyMovement.NormalizeSingleDirection(dir.z) * _enemyMovement.GetMoveDistance());
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
        if(_stats.GetHealth() <= 0)
        {
            Die();
        } 
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }
}
