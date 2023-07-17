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
    BoxCollider2D _col;
    [SerializeField] private UnityEvent<Vector3> _moveEvent;
    [SerializeField] private UnityEvent<Vector3, PlayerDirection> _animationEvent;
    Vector3 dir;
    [SerializeField] private PlayerDirection _pDir;

    private void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        _pDir = PlayerDirection.front;
    }

    private void ColliderOffset(PlayerDirection _p)
    {
        if(_p == PlayerDirection.left)
        {
            _col.offset = new Vector2(0.37f, -0.6f);
        }
        else
        {
            _col.offset = new Vector2(-0.38f, -0.6f);
        }    
    }

    private void SetPDir()
    {
        if(dir.x > 0)
        {
            _pDir = PlayerDirection.right;
        }
        else if(dir.x < 0)
        {
            _pDir = PlayerDirection.left;
        }
        else if(dir.y > 0)
        {
            _pDir = PlayerDirection.back;
        }
        else if(dir.y < 0)
        {
            _pDir = PlayerDirection.front;
        }
        ColliderOffset(_pDir);
    }

    private void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        SetPDir();
        _moveEvent?.Invoke(dir);
        _animationEvent?.Invoke(dir, _pDir);
        Detection();
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
