using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _pSpeed;
    public void PMove(Vector3 dir)
    {
        transform.position += dir * _pSpeed * Time.deltaTime;
    }
}
