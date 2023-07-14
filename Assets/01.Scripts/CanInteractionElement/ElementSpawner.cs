using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ElementSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _elementList = new List<GameObject>();
    [SerializeField] private LayerMask _rangeMask;
    [SerializeField] private LayerMask _eleMask;
    Tilemap _tileMap;
    BoundsInt bound;

    private void Awake()
    {
        _tileMap = GameObject.Find("GridMap").transform.Find("SpawnRange").GetComponent<Tilemap>();
        bound = _tileMap.cellBounds;
    }

    private void Start()
    {
        for(int j = 0; j < _elementList.Count; j++)
        {
            for (int i = 0; i < 7; i++)
            {
                SpawnElement(_elementList[j]);
            }
        }
    }


    private Vector3 RandomTileMapVector()
    {
        Vector2 randomCell = new Vector2();

        while (true)
        {
            randomCell = new Vector2(Random.Range(bound.min.x, bound.max.x),
                                         Random.Range(bound.min.y, bound.max.y));

            RaycastHit2D rangehit = Physics2D.Raycast(randomCell, Vector2.down, 1 << _rangeMask);
            RaycastHit2D elehit = Physics2D.Raycast(randomCell, Vector2.down, 1 << _eleMask);

            if (rangehit.collider != null && elehit.collider == null)
                break;
        }


        return randomCell;
    }

    private void SpawnElement(GameObject element)
    {
        GameObject ele = Instantiate(element);
        //ele.transform.position = RandomTileMapVector();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Vector2 randomCell = new Vector2(Random.Range(-22, 14),
                                             Random.Range(-9, 12));

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            Debug.Log(pos);

            RaycastHit2D rangehit = Physics2D.Raycast(pos, Vector2.down, 1 << _rangeMask);
            RaycastHit2D elehit = Physics2D.Raycast(pos, Vector2.down, 1 << _eleMask);

            //Debug.Log(randomCell);
            Debug.Log(rangehit.collider);
            Debug.Log(elehit.collider);
        }
        
    }
}
