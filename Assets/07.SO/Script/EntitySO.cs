using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EntitySO", fileName = "New EntitySO")]
public class EntitySO : ScriptableObject
{
    public Vector2 range_speed;
    public float destroyCount;
}
