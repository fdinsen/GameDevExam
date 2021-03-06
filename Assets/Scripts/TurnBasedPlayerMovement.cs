using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Tooltip("The distance in units with which the player moves.")]
    private readonly float _moveDistance = 0.5f;
    [SerializeField] [Tooltip("The speed with which the player moves from square to square.")]
    private float _moveSpeed = 5f;
    [SerializeField] [Tooltip("The speed with which the player rotates when moving from square to square")]
    private float _rotationSpeed = 5f;
    [SerializeField] [Range(0, 1)]
    [Tooltip("The time, in seconds, between inputs being registred when holding down a move key.")]
    private float _timeBetweenInputs = .1f;
    [SerializeField] [Tooltip("Which layers that should stop movement.")]
    private LayerMask _whatStopsMovement;

    [Header("Object references")]
    [SerializeField] [Tooltip("Local Empty Gameobject that defines the next move-point.")]
    private Transform _movePoint;
    [SerializeField] [Tooltip("Local Camera")]
    private Transform _camera;

    [SerializeField] private AudioSource _footsteps;

    public static event Action PlayerMoved;

    private PlayerControls m_playerControls;
    private float _moveCooldown;
    private Animator _animator;
    private CharacterStats stats;
    public GameObject currentlyTraveledStairs = null;
    private void Start()
    {
        //makes sure the movepoint and camera won't move with the player
        _movePoint.parent = null;
        _camera.parent = null;
        _moveCooldown = _timeBetweenInputs;
        _animator = GetComponent<Animator>();
        stats = GetComponent<CharacterStats>();
        stats.ApplyHealth(5);
    }
    void OnEnable()
    {
        m_playerControls.DefaultInput.Enable();
    }

    void OnDisable()
    {
        m_playerControls.DefaultInput.Disable();
    }

    void Awake()
    {
        m_playerControls = new PlayerControls();
        m_playerControls.DefaultInput.TurnLeft.performed += ctx => TurnLeft();
        m_playerControls.DefaultInput.TurnRight.performed += ctx => TurnRight();
        m_playerControls.DefaultInput.Interact.performed += ctx => Interact();
    }

    public void SetInput(PlayerControls playerControls)
    {
        playerControls.DefaultInput.Enable();
    }

    public void UnsetInput(PlayerControls playerControls)
    {
        playerControls.DefaultInput.Disable();
    }

    public void ToggleMovementControls(bool toggleOn)
    {
        if(toggleOn)
        {
            SetInput(m_playerControls);
        }
        else
        {
            UnsetInput(m_playerControls);
        }
    }

    private void Update()
    {
        Move();
    }
    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, _movePoint.rotation, _rotationSpeed * Time.deltaTime);

        if (currentlyTraveledStairs != null)
        {
            PlayerMoved?.Invoke();
            currentlyTraveledStairs = null;
        }
        else if (_moveCooldown <= 0) 
        {
            _moveCooldown = _timeBetweenInputs;
            if(Vector3.Distance(transform.position, _movePoint.position) <= .05f)
            {
                var input = m_playerControls.DefaultInput.Move.ReadValue<Vector2>();
                if (Mathf.Abs(input.x) == 1f)
                {
                    if (CheckCollision(_movePoint.position, new Vector3(input.x * _moveDistance, 0f, 0f)) )
                    {
                        _movePoint.position += new Vector3(input.x * _moveDistance, 0f, 0f);
                        _movePoint.rotation = Quaternion.Euler(0, 90 * input.x, 0);
                        if (!_footsteps.isPlaying)
                        {
                            _footsteps?.Play();
                        }
                        PlayerMoved?.Invoke();
                        _animator?.SetFloat("MoveSpeed", Math.Abs(input.x));
                    }
                }
                if (Mathf.Abs(input.y) == 1f)
                {
                    if (CheckCollision(_movePoint.position, new Vector3(0f, 0f, input.y * _moveDistance)) )
                    {
                        _movePoint.position += new Vector3(0f, 0f, input.y * _moveDistance);
                        _movePoint.rotation = Quaternion.Euler(0, 180 * Mathf.Clamp(input.y * 2, 1, 2), 0);
                        if(!_footsteps.isPlaying)
                        {
                            _footsteps?.Play();
                        }
                        PlayerMoved?.Invoke();
                        _animator?.SetFloat("MoveSpeed", Math.Abs(input.y));
                    }

                }
            }
        }
        else
        {
            _moveCooldown -= Time.deltaTime;
        }

        if(transform.position.Equals(_movePoint.position))
        {
            _animator?.SetFloat("MoveSpeed", 0f);
        }
    }

    private bool CheckCollision(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, _whatStopsMovement).Length == 0;
    }

    private bool CheckForStairs(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, LayerMask.GetMask("Stairs")).Length > 0;
    }

    private Collider GetStairs(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, LayerMask.GetMask("Stairs"))[0];
    }

    public Transform GetMovePoint()
    {
        return _movePoint;
    }

    public void TurnLeft()
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y - 90, 0));
        transform.rotation = targetRotation;
        _movePoint.rotation = targetRotation;
    }

    public void TurnRight()
    {
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y + 90, 0));
        transform.rotation = targetRotation;
        _movePoint.rotation = targetRotation;
    }

    public void Interact()
    {
        _animator.SetTrigger("Interact");
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward * _moveDistance, out hit, LayerMask.GetMask("Interactable")))
        {
            bool isInteractable = hit.collider.GetComponent(typeof(IInteractable)) != null;
            if(isInteractable)
            {
                GameObject target = hit.collider.gameObject;

                var interactables = target.GetComponentsInChildren(typeof(IInteractable));

                foreach (IInteractable interactable in interactables)
                {
                    interactable.Interact();
                }
            }
        }
    }

    public void InvokePlayerAction()
    {
        PlayerMoved.Invoke();
    }

    public float GetMoveDistance()
    {
        return _moveDistance;
    }

    public void SetMovePoint(Vector3 position)
    {
        _movePoint.position = position;
    }
}
