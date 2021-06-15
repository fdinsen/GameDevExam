using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _moveTime = .2f;
    [SerializeField] private LayerMask _whatStopsMovement;
    [SerializeField] private GameObject explosion;
    [SerializeField] private AudioSource explosionSound;

    public AttackDefinition Attack { get; set; }
    public CharacterStats AttackerStats { get; set; }
    public Vector3 MoveAmount { get; set; }

    private void OnEnable()
    {
        TurnBasedPlayerMovement.PlayerMoved += Move;
    }

    private void OnDisable()
    {
        TurnBasedPlayerMovement.PlayerMoved -= Move;
    }

    void Move()
    {
        if(CheckCollision(transform.position, MoveAmount))
        {
            StartCoroutine(MoveObject(transform.position, transform.position + MoveAmount, _moveTime));
        }
        else
        {
            DoDamage();
        }
    }

    void DoDamage()
    {
        Vector3 halfExtents = new Vector3(Attack.Range, Attack.Range, Attack.Range);
        var colliders = Physics.OverlapBox(transform.position, halfExtents);
        foreach(Collider col in colliders)
        {
            bool isAttackable = col.GetComponent(typeof(IAttackable)) != null;
            if (isAttackable)
            {
                GameObject targetObj = col.gameObject;
                var attack = Attack.CreateAttack(AttackerStats, targetObj.GetComponent<CharacterStats>());

                var attackables = targetObj.GetComponentsInChildren(typeof(IAttackable));

                foreach (IAttackable attackable in attackables)
                {
                    attackable.OnAttacked(gameObject, attack);
                }
            }
        }

        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        explosionSound?.Play();
        yield return new WaitForSeconds(.2f);
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DoDamage();
    }

    public IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.position = target;
    }

    public bool CheckCollision(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, _whatStopsMovement).Length == 0;
    }
}
