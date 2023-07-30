using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PlayerDirection
{
    front,
    back,
    left,
    right
}

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _col;
    [SerializeField] private UnityEvent<Vector3> _moveEvent;
    [SerializeField] private UnityEvent<Vector3, PlayerDirection> _animationEvent;
    [SerializeField] private UnityEvent _attackEvent;
    [SerializeField] private UnityEvent<bool> _invenEvent;
    [SerializeField] private UnityEvent<PlayerDirection> _fishingEvent;
    Vector3 dir;
    [SerializeField] private PlayerDirection _pDir;
    private PlayerDirection _beforePdir;
    [SerializeField] private LayerMask _layerMask;

    bool isInvenActive;

    private void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        _pDir = PlayerDirection.front;
        isInvenActive = false;
    }
    private void ColliderOffset(PlayerDirection _p)
    {
        if (_beforePdir == _p)
            return;
        
        _col.offset = new Vector2(0.035f, -0.8f);
        _col.size = new Vector2(1, 0.2f);

        if (_p == PlayerDirection.left || _p == PlayerDirection.right)
        {
            _col.offset = new Vector2(0.38f, -0.85f);
            _col.size = new Vector2(1, 0.2f);
        }
         
    }
    private void SetPDir(Vector3 _dir)
    {
        if(_dir.x > 0)
        {
            _pDir = PlayerDirection.right;
        }
        else if(_dir.x < 0)
        {
            _pDir = PlayerDirection.left;
        }
        else if(_dir.y > 0)
        {
            _pDir = PlayerDirection.back;
        }
        else if(_dir.y < 0)
        {
            _pDir = PlayerDirection.front;
        }
    }
    private void CheckWhere(Vector2 dir)
    {
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            if(dir.x > 0)
            {
                _pDir = PlayerDirection.right;
            }
            else
            {
                _pDir = PlayerDirection.left;
            }
        }
        else if (Mathf.Abs(dir.x) < Mathf.Abs(dir.y))
        {
            if (dir.y > 0)
            {
                _pDir = PlayerDirection.back;
            }
            else
            {
                _pDir = PlayerDirection.front;
            }
        }
        else
        {
            _pDir = PlayerDirection.back;
        }
    }
    private void GetAttackKey()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Collider2D col = Physics2D.OverlapCircle(transform.position, 0.3f, _layerMask);
            if (col != null)
            {
                Vector2 dir = (col.gameObject.transform.position - transform.position).normalized;
                CheckWhere(dir);
                _attackEvent?.Invoke();
            }
        }
    }
    private void GetInvenKey()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            _invenEvent?.Invoke(!isInvenActive);
            isInvenActive = !isInvenActive;
        }
    }
    private void GetFishingKey()
    {
        _fishingEvent?.Invoke(_pDir);
    }

    private void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        SetPDir(dir);
        ColliderOffset(_pDir);
        _moveEvent?.Invoke(dir);
        _animationEvent?.Invoke(dir, _pDir);
        Detection();
        GetAttackKey();
        GetInvenKey();
        GetFishingKey();
    }

    private void Detection()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);

        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject.CompareTag("Element"))
            {
                if(colliders[i].gameObject.transform.position.y < transform.position.y)
                {
                    InteractionBase ib = colliders[i].gameObject.GetComponent<InteractionBase>();
                    ib.SetSortingOrder(5);
                }
                else
                {
                    InteractionBase ib = colliders[i].gameObject.GetComponent<InteractionBase>();
                    ib.SetSortingOrder(3);
                }
            }
        }
    }
}
