using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rigid;
    [SerializeField] private float _pSpeed;
    Vector3 _dir;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    public void PMove(Vector3 dir)
    {
        _dir = dir;
    }

    private void Update()
    {
        _rigid.velocity =  _dir * _pSpeed;
    }

}
