using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSet : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Z 회전 값을 증가시킴
        currentRotation.z += _rotationSpeed * Time.deltaTime;

        // 새로운 회전 값을 적용
        transform.rotation = Quaternion.Euler(currentRotation);
        if (transform.rotation.z >= 360)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
