using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("The distance in units with which the enemy moves.")]
    private float _moveDistance = 0.5f;
    [SerializeField] private float _moveTime = .2f;
    [SerializeField] public Transform MovePoint;
    [SerializeField] private LayerMask _whatStopsMovement;

    public event Action FinishedMoving;

    //private delegate void moveDirection(Vector3 direction);
    private GameObject _player;
    private Transform _playerMovePoint;
    private IEnemy _enemyType;
    private bool _playerIsWithinLOS = false;
    private Vector3 rayOffset = new Vector3(0, .25f, 0);
    private Animator _animator;

    void Start()
    {
        //makes sure the movepoint won't move with the Enemy
        MovePoint.parent = null;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMovePoint = _player.GetComponent<TurnBasedPlayerMovement>().GetMovePoint();
        _enemyType = GetComponent<IEnemy>();
        _animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position + rayOffset, _player.transform.position - transform.position);
        if(Physics.Raycast(transform.position + rayOffset, _player.transform.position - transform.position, out hit))
        {
            _playerIsWithinLOS = hit.collider.gameObject.CompareTag("Player");
            Debug.Log(_playerIsWithinLOS);
            Debug.Log(hit.collider.gameObject.name);
        }
    }

    private void OnEnable()
    {
        TurnBasedPlayerMovement.PlayerMoved += Move;
    }

    private void OnDisable()
    {
        TurnBasedPlayerMovement.PlayerMoved -= Move;
    }

    public void Move()
    {
        if(_playerIsWithinLOS)
        {
            _enemyType.Move();
        }else
        {
            Debug.Log("Sleep " + gameObject.name);
        }
    }

    public void SetMovePoint(List<Direction> moveDirection, Vector3 dir, ref Transform movePoint)
    {
        if (moveDirection.Count == 0)
        {
            return;
        }
        Vector3 moveAmount;
        if (moveDirection[0] == Direction.RIGHT || moveDirection[0] == Direction.LEFT)
        {
            moveAmount = new Vector3(NormalizeSingleDirection(dir.x) * _moveDistance, 0f, 0f);
        }
        else
        {
             moveAmount = new Vector3(0f, 0f, NormalizeSingleDirection(dir.z) * _moveDistance);
        }

        if (CheckCollision(movePoint.position, moveAmount))
        {
            movePoint.position += moveAmount;
            movePoint.rotation = Quaternion.Euler(new Vector3(0, GetRotationFromDirection(moveDirection[0]), 0));
        }
        else
        {
            moveDirection.RemoveAt(0);
            SetMovePoint(moveDirection, dir, ref movePoint);
        }
    }

    public int NormalizeSingleDirection(float direction)
    {
        //ulgy solution but it works
        //ideas for improvement welcome
        if (direction > 0)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public bool CheckCollision(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, _whatStopsMovement).Length == 0;
    }

    public IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime)
    {
        _animator.SetBool("Moving", true);
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        _animator.SetBool("Moving", false);
        transform.position = target;
        FinishedMoving?.Invoke();
    }

    public IEnumerator RotateObject(Quaternion source, Quaternion target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform.rotation = Quaternion.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.rotation = target;
    }

    public Transform GetPlayerMovePoint()
    {
        return _playerMovePoint;
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    public Transform GetMovePoint()
    {
        return MovePoint;
    }

    public float GetMoveTime()
    {
        return _moveTime;
    }

    private float GetRotationFromDirection(Direction dir)
    {
        if(dir == Direction.UP)
        {
            return 0f;
        }
        if(dir == Direction.RIGHT)
        {
            return 90f;
        }
        if(dir == Direction.DOWN)
        {
            return 180f;
        }
        if(dir == Direction.LEFT)
        {
            return 270f;
        }
        return 0f;
    }
}

public enum Direction
{
    UP, DOWN, LEFT, RIGHT, EQ
}
