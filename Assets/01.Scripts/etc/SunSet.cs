using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSet : MonoBehaviour
{
    [SerializeField] private Color _afternoon;
    [SerializeField] private Color _day;
    [SerializeField] private float _rotationSpeed;

    private void Update()
    {
        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.z += _rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(currentRotation);

        if (transform.rotation.z >= 360)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
        
    }
}
