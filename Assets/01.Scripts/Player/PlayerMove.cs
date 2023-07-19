using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rigid;
    [SerializeField] private float _pSpeed;
    Vector3 _dir;
    [field: SerializeField] public bool canMove { get; set; }

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody2D>();
        canMove = true;
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
