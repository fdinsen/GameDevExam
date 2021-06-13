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
    [SerializeField] private LayerMask _blocksLOS;

    public event Action FinishedMoving;

    //private delegate void moveDirection(Vector3 direction);
    private GameObject _player;
    private Transform _playerMovePoint;
    private IEnemy _enemyType;
    private bool _playerIsWithinLOS = false;
    private bool _ignoreLOS = false;
    public int stunTimer = 0;
    private Vector3 rayOffset = new Vector3(0, .25f, 0);
    private Animator _animator;
    private bool alive = true;

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
        if(!alive)
        {
            _playerIsWithinLOS = false;
        }
        else if(Physics.Raycast(transform.position + rayOffset, _player.transform.position - transform.position, out hit, _blocksLOS))
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
        if((_playerIsWithinLOS || _ignoreLOS) && stunTimer <= 0)
        {
            _animator.SetBool("Dizzy", false);
            _enemyType.Move();
        }else if (stunTimer > 0)
        {
            stunTimer--;
        }
    }

    public void SetMovePointFromDirectionList(List<Direction> moveDirection, Vector3 dir, ref Transform movePoint)
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
            SetMovePointFromDirectionList(moveDirection, dir, ref movePoint);
        }
    }

    public void SetMovePoint(Vector3 moveAmount, ref Transform movePoint)
    {
       movePoint.position += moveAmount;
        if(moveAmount.x != 0)
        {
            movePoint.rotation = Quaternion.Euler(new Vector3(0, 90 * moveAmount.x, 0));
        } else if(moveAmount.z != 0)
        {
            movePoint.rotation = Quaternion.Euler(new Vector3(0, 180 * Mathf.Clamp(moveAmount.x * 2, 1, 2), 0));
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

    public Vector3 GetMoveDir(Vector3 source, Vector3 target)
    {
        return (target - source).normalized;
    }

    public List<Direction> GetPrioritizedListOfDirections(Vector3 dir)
    {
        //UGLY SOLUTION; SHOULD BE IMPROVED
        Direction[] movePriority = new Direction[4];
        if (Mathf.Abs(dir.x) < Mathf.Abs(dir.z))
        {
            if (dir.z > 0)
            {
                movePriority[0] = Direction.UP;
                movePriority[3] = Direction.DOWN;
            }
            else
            {
                movePriority[0] = Direction.DOWN;
                movePriority[3] = Direction.UP;
            }
            if (dir.x > 0)
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

    public bool CheckCollision(Vector3 currentPosition, Vector3 moveBy)
    {
        return 
            Physics.OverlapSphere(currentPosition + moveBy, .2f, _whatStopsMovement).Length == 0
            || _playerMovePoint.position == currentPosition + moveBy;
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

    public float GetMoveDistance()
    {
        return _moveDistance;
    }

    public void SetIgnoreLOS(bool ignore)
    {
        _ignoreLOS = ignore;
    }

    public void SetStunTime(int stunTime)
    {
        if(stunTime > 0)
        {
            stunTimer = stunTime;
            _animator.SetBool("Dizzy", true);
        }
    }

    public void Die()
    {
        alive = false;
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
