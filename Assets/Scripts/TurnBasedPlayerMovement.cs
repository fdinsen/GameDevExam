using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBasedPlayerMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("The speed with which the player moves from square to square.")]
    private float _moveSpeed = 5f;
    [SerializeField] [Tooltip("Local Empty Gameobject that defines the next move-point.")]
    private Transform _movePoint;
    [SerializeField] [Tooltip("Which layers that should stop movement.")]
    private LayerMask _whatStopsMovement;
    [SerializeField] [Range(0,1)] [Tooltip("The time, in seconds, between inputs being registred when holding down a move key.")] 
    private float _timeBetweenInputs = .1f;

    public static event Action PlayerMoved;

    private PlayerControls m_playerControls;
    private float _moveCooldown;

    private void Start()
    {
        //makes sure the movepoint won't move with the player
        _movePoint.parent = null;
        _moveCooldown = _timeBetweenInputs;
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
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _movePoint.position, _moveSpeed * Time.deltaTime);

        if(_moveCooldown <= 0) 
        {
            _moveCooldown = _timeBetweenInputs;
            if(Vector3.Distance(transform.position, _movePoint.position) <= .05f)
            {
                var input = m_playerControls.DefaultInput.Move.ReadValue<Vector2>();
                if (Mathf.Abs(input.x) == 1f)
                {
                    if(CheckCollision(_movePoint.position, new Vector3(input.x, 0f, 0f)) )
                    {
                        _movePoint.position += new Vector3(input.x, 0f, 0f);
                        PlayerMoved?.Invoke();
                    }
                }
                if (Mathf.Abs(input.y) == 1f)
                {
                    if (CheckCollision(_movePoint.position, new Vector3(0f, 0f, input.y)) )
                    {
                        _movePoint.position += new Vector3(0f, 0f, input.y);
                        PlayerMoved?.Invoke();
                    }
                }
            }
        }
        else
        {
            _moveCooldown -= Time.deltaTime;
        }
    }

    private bool CheckCollision(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, _whatStopsMovement).Length == 0;
    }
}
