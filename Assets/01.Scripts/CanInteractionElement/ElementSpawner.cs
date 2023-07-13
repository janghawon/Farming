using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elementList = new List<GameObject>();
    TilemapRenderer _tileMap;

    private void Awake()
    {
        _tileMap = GameObject.Find("GridMap").transform.Find("SpawnRange").GetComponent<TilemapRenderer>();
    }

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
