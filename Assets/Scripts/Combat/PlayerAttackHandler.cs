using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DigitalRuby.LightningBolt;
using TMPro;

public class PlayerAttackHandler : MonoBehaviour, IAttackable
{
    [SerializeField] private AttackDefinition primaryAttack;
    [SerializeField] private AttackDefinition secondaryAttack;
    [SerializeField] private AttackDefinition modifiedPrimaryAttack;
    [SerializeField] private AttackDefinition modifiedSecondaryAttack;

    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject _lightningBolt;
    [SerializeField] private float lightningDuration = 2f;
    [SerializeField] private float lightningDelay = 1f;

    [SerializeField] private AudioSource deathSound;

    private PlayerAttackControls m_playerAttackControls;
    private CharacterStats _stats;
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
        m_playerAttackControls.DefaultInput.BaseAttack.performed += ctx => HandlePrimaryAttack();
        m_playerAttackControls.DefaultInput.AlternateAttack.performed += ctx => HandleSecondaryAttack();
        _stats = GetComponent<CharacterStats>();
        playerMovement = GetComponent<TurnBasedPlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward +transform.right);
    }

    void HandlePrimaryAttack()
    {
        //If player is holding attack modifier
        bool attackModifierIsDown 
            = m_playerAttackControls.DefaultInput.AttackModifier.ReadValue<float>() > 0.5f;
        if (attackModifierIsDown)
        {
            ProjectileAttack();
        }
        else
        {
            PrimaryAttack();
        }
    }

    void HandleSecondaryAttack()
    {
        //If player is holding attack modifier
        bool attackModiferIsDown 
            = m_playerAttackControls.DefaultInput.AttackModifier.ReadValue<float>() > 0.5f;
        if (attackModiferIsDown)
        {
            StunAttack();
        }
        else
        {
            SecondaryAttack();
        }
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

    void StunAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, modifiedSecondaryAttack.Range))
        {
            Debug.Log("Found enemy " + hit.collider.gameObject.name);
            DoAttack(modifiedSecondaryAttack, hit);
        }
        StartCoroutine(CreateLightningBolt());
        playerMovement.InvokePlayerAction();
        _animator.SetTrigger("PrimaryMeleeAttack");
    }

    void ProjectileAttack()
    {
        playerMovement.InvokePlayerAction();
        var projectileInst
            = Instantiate(
                projectile,
                (transform.position + (transform.forward * playerMovement.GetMoveDistance())) + new Vector3(0, 0.25f, 0), 
                Quaternion.identity
                );
        Projectile projScript = projectileInst.GetComponent<Projectile>();
        projScript.MoveAmount = transform.forward * playerMovement.GetMoveDistance();
        projScript.Attack = modifiedPrimaryAttack;
        projScript.AttackerStats = _stats;

        _animator.SetTrigger("PrimaryMeleeAttack");
    }

    private void DoAttack(AttackDefinition attackType, RaycastHit hit)
    {
        bool isAttackable = hit.collider.GetComponent(typeof(IAttackable)) != null;
        if (isAttackable)
        {
            GameObject target = hit.collider.gameObject;
            var attack = attackType.CreateAttack(_stats, target.GetComponent<CharacterStats>());

            var attackables = target.GetComponentsInChildren(typeof(IAttackable));

            foreach (IAttackable attackable in attackables)
            {
                attackable.OnAttacked(gameObject, attack);
            }
        }
    }

    private IEnumerator CreateLightningBolt()
    {
        Debug.Log("Doing lightning");

        float startTime = Time.time;
        while (Time.time < startTime + lightningDuration)
        {
            if(Time.time > startTime + lightningDelay)
            {
                _lightningBolt.SetActive(true);
            }
            yield return null;
        }
        _lightningBolt.SetActive(false);
    }

    public void ToggleAttackControls(bool toggleOn)
    {
        if (toggleOn)
        {
            SetInput(m_playerAttackControls);
        }
        else
        {
            UnsetInput(m_playerAttackControls);
        }
    }

    private void SetInput(PlayerAttackControls controls)
    {
        controls.DefaultInput.Enable();
    }

    private void UnsetInput(PlayerAttackControls controls)
    {
        controls.DefaultInput.Disable();
    }

    public void OnAttacked(GameObject attacker, Attack attack)
    {
        _animator.SetTrigger("Hit");
        if (attack.IsCritical)
        {
            Debug.Log("Critical Damage!!");
        }
        _stats.TakeDamage(attack.Damage);
        if (_stats.GetHealth() <= 0)
        {
            StartCoroutine(Die());
        }
    }

    private IEnumerator Die()
    {
        deathSound?.Play();
        _animator.SetBool("Dead", true);
        UnsetInput(m_playerAttackControls);
        playerMovement.ToggleMovementControls(false);
        GameObject.FindGameObjectWithTag("Canvas").SetActive(false);
        GameObject.FindGameObjectWithTag("DeathText")
            .GetComponent<TextMeshProUGUI>()
            .SetText("You Died!");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }
}
