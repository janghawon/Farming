using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elementList = new List<GameObject>();

    private void Start()
    {
        for(int j = 0; j < _elementList.Count; j++)
        {
            for (int i = 0; i < 6; i++)
            {
                SpawnElement(_elementList[j]);
            }
        }
    }

    private void SpawnElement(GameObject element)
    {
        GameObject ele = Instantiate(element);
    }

    private void Update()
    {
        
    }
}
