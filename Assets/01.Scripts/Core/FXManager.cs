using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FXType
{
    smoke = 0
}

public class FXManager : MonoBehaviour
{
    public static FXManager Instance;
    [SerializeField] private GameObject[] _fxPrefabs;

    GameObject _fx;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("!!!!");
            return;
        }
        Instance = this;
    }

    public void SetFx(FXType type, Vector2 pos, int count, float range)
    {
        Vector2 randomPos;
        for(int i = 0; i < count; i++)
        {
            randomPos = Random.insideUnitCircle * range;
            _fx = PoolManager.Instance.Pop(_fxPrefabs[(int)type].name).gameObject;
            _fx = Instantiate(_fxPrefabs[(int)type]);

            _fx.transform.position = pos + randomPos;
        }
    }
}
