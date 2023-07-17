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
    [SerializeField] private UnityEvent<Vector3> _moveEvent;
    [SerializeField] private UnityEvent<Vector3, PlayerDirection> _animationEvent;
    Vector3 dir;
    [SerializeField] private PlayerDirection _pDir;

    private void Awake()
    {
        _pDir = PlayerDirection.front;
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
    }

    private void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        SetPDir();
        _moveEvent?.Invoke(dir);
        _animationEvent?.Invoke(dir, _pDir);
    }
}
