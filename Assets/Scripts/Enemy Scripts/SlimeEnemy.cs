using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class SlimeEnemy : MonoBehaviour, IEnemy, IAttackable
{
    [SerializeField] private AttackDefinition slimeAttack;
    [SerializeField] private LayerMask attackable;

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
        if (Physics.Raycast(currentPosition, transform.forward, out hit, slimeAttack.Range, attackable))
        {
            bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                GameObject targetObj = hit.collider.gameObject;
                var attack = slimeAttack.CreateAttack(_stats, targetObj.GetComponent<CharacterStats>());

                var attackables = targetObj.GetComponentsInChildren(typeof(IAttackable));

                foreach (IAttackable attackable in attackables)
                {
                    attackable.OnAttacked(gameObject, attack);
                }
            }
        }
        _animator.SetTrigger("Attack");
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
            var dir = _enemyMovement.GetMoveDir(this.transform.position, _enemyMovement.GetPlayerMovePoint().position);
            _enemyMovement.SetMovePointFromDirectionList(_enemyMovement.GetPrioritizedListOfDirections(dir), dir, ref _enemyMovement.MovePoint);

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
        if (_stats.GetHealth() <= 0)
        {
            StartCoroutine(Die());
        }
    }

    public IEnumerator Die()
    {
        _enemyMovement.Die();
        _animator.SetBool("Dead", true);
        gameObject.layer = 0; // default
        yield return new WaitForSeconds(5.0f);
        Destroy(gameObject);
    }
}
