using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    [SerializeField] private AttackDefinition primaryAttack;
    [SerializeField] private AttackDefinition secondaryAttack;

    private PlayerAttackControls m_playerAttackControls;
    private CharacterStats stats;
    private TurnBasedPlayerMovement playerMovement;
    private Animator _animator;

    void OnEnable()
    {
        m_playerAttackControls.DefaultInput.Enable();
    }

    void OnDisable()
    {
        m_playerAttackControls.DefaultInput.Disable();
    }

    void Awake()
    {
        m_playerAttackControls = new PlayerAttackControls();
        m_playerAttackControls.DefaultInput.BaseAttack.performed += ctx => PrimaryAttack();
        m_playerAttackControls.DefaultInput.AlternateAttack.performed += ctx => SecondaryAttack();
        stats = GetComponent<CharacterStats>();
        playerMovement = GetComponent<TurnBasedPlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward +transform.right);
    }

    void PrimaryAttack()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, primaryAttack.Range))
        {
            DoAttack(primaryAttack, hit);
        }
        playerMovement.InvokePlayerAction();
        _animator.SetTrigger("PrimaryMeleeAttack");
    }

    void SecondaryAttack()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        if(Physics.Raycast(transform.position, transform.forward, out hit1, secondaryAttack.Range))
        {
            DoAttack(secondaryAttack, hit1);
        }
        if(Physics.Raycast(transform.position, transform.forward + transform.right, out hit2, secondaryAttack.Range * 1.5f))
        {
            DoAttack(secondaryAttack, hit2);
        }
        if(Physics.Raycast(transform.position, transform.forward - transform.right, out hit3, secondaryAttack.Range * 1.5f))
        {
            DoAttack(secondaryAttack, hit3);
        }
        playerMovement.InvokePlayerAction();
        _animator.SetTrigger("SecondaryMeleeAttack");
    }

    private void DoAttack(AttackDefinition attackType, RaycastHit hit)
    {
        bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
        if (isAttackable)
        {
            GameObject target = hit.collider.gameObject;
            var attack = attackType.CreateAttack(stats, target.GetComponent<CharacterStats>());

            var attackables = target.GetComponentsInChildren(typeof(IAttackable));

            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttack(gameObject, attack);
            }
        }
    }
}
