using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] [Tooltip("The distance in units with which the enemy moves.")]
    private float _moveDistance = 0.5f;
    [SerializeField] private float _moveTime = .2f;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private LayerMask _whatStopsMovement;

    public event Action FinishedMoving;

    //private delegate void moveDirection(Vector3 direction);
    private GameObject _player;
    private Transform _playerMovePoint;
    private Enemy _enemyType;
    private bool _playerIsWithinLOS = false;
    private Vector3 rayOffset = new Vector3(0, .25f, 0);

    void Start()
    {
        //makes sure the movepoint won't move with the Enemy
        _movePoint.parent = null;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerMovePoint = _player.GetComponent<TurnBasedPlayerMovement>().GetMovePoint();
        _enemyType = GetComponent<Enemy>();
    }
    private void FixedUpdate()
    {
        RaycastHit hit;
        //Debug.DrawRay(transform.position + rayOffset, GetMoveDir(transform.position, _player.transform.position));
        if(Physics.Raycast(transform.position + rayOffset, GetMoveDir(transform.position, _player.transform.position), out hit))
        {
            _playerIsWithinLOS = hit.collider.gameObject.CompareTag("Player");
            //Debug.Log(_playerIsWithinLOS);
            //Debug.Log(hit.collider.gameObject.name);
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
            if (_enemyType == null || !_enemyType.isWithinRange(transform.position, _playerMovePoint.transform.position))
            {
                var dir = GetMoveDir(this.transform.position, _playerMovePoint.transform.position).normalized;
                SetMovePoint(GetPrioritizedListOfDirections(dir), dir, ref _movePoint);

                StartCoroutine(
                    MoveObject(transform.position, _movePoint.position, _moveTime)
                );
            }
            else
            {
                _enemyType.doAttack(transform.position, _player.transform.position);
            }
        }else
        {
            Debug.Log("Sleep " + gameObject.name);
        }
        

    }

    private Vector3 GetMoveDir(Vector3 source, Vector3 target)
    {
        return (target - source);
    }

    private List<Direction> GetPrioritizedListOfDirections(Vector3 dir)
    {
        //UGLY SOLUTION; SHOULD BE IMPROVED
        Direction[] movePriority = new Direction[4];
        if(Mathf.Abs(dir.x) < Mathf.Abs(dir.z))
        {
            if(dir.z > 0)
            {
                movePriority[0] = Direction.UP;
                movePriority[3] = Direction.DOWN;
            } 
            else
            {
                movePriority[0] = Direction.DOWN;
                movePriority[3] = Direction.UP;
            }
            if(dir.x > 0)
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

    private void SetMovePoint(List<Direction> moveDirection, Vector3 dir, ref Transform movePoint)
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
            
        }
        else
        {
            moveDirection.RemoveAt(0);
            SetMovePoint(moveDirection, dir, ref movePoint);
        }
    }

    /*private void SetMovePoint(List<Direction> moveDirection, Vector3 dir, ref Transform movePoint, Vector3 nextPosition)
    {
        if (moveDirection.Count == 0)
        {
            return;
        }
        Vector3 moveAmount;
        if (moveDirection[0] == Direction.RIGHT || moveDirection[0] == Direction.LEFT)
        {
            moveAmount = new Vector3(NormalizeSingleDirection(dir.x), 0f, 0f);
        }
        else
        {
            moveAmount = new Vector3(0f, 0f, NormalizeSingleDirection(dir.z));
        }

        if (CheckCollision(movePoint.position, moveAmount))
        {
            movePoint.position = nextPosition + moveAmount;

        }
        else
        {
            moveDirection.RemoveAt(0);
            SetMovePoint(moveDirection, dir, ref movePoint, nextPosition);
        }
    }*/

    private int NormalizeSingleDirection(float direction)
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

    private bool CheckCollision(Vector3 currentPosition, Vector3 moveBy)
    {
        return Physics.OverlapSphere(currentPosition + moveBy, .2f, _whatStopsMovement).Length == 0;
    }

    private IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime)
    {
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            transform.position = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);
            yield return null;
        }
        transform.position = target;
        FinishedMoving?.Invoke();
    }
}

enum Direction
{
    UP, DOWN, LEFT, RIGHT, EQ
}
