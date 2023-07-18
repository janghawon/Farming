using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectResourceBar : MonoBehaviour
{
    [SerializeField] private float _maxSpeed;
    public float MaxSpeed => _maxSpeed;

    Image _collectRangeBar;
    RectTransform _collectRange;
    Image _keyImage;
    RectTransform _keyTrans;

    bool isMove;

    private void Awake()
    {
        _collectRangeBar = GetComponent<Image>();
        _collectRange = transform.Find("CorrectRange").GetComponent<RectTransform>();
        _keyImage = transform.Find("Key").GetComponent<Image>();
        _keyTrans = _keyImage.GetComponent<RectTransform>();
    }

    private void Start()
    {
        _keyTrans.localPosition = Vector3.zero;
        _collectRangeBar.enabled = false;
        isMove = false;

        SetAndStart(100, 1);
    }

    private void SetAndStart(float rangeArea, float speed)
    {
        _collectRangeBar.enabled = true;
        _collectRange.sizeDelta = new Vector2(rangeArea, 50);
        float dex = Random.Range(rangeArea * 0.5f, -rangeArea * 0.5f);
        _collectRange.localPosition = new Vector3(dex, 0, 0);
        _maxSpeed = speed;
        isMove = true;
    }

    private void MoveKey()
    {
        if(isMove)
        {
            Vector3 v = _keyTrans.localPosition;
            v.x += 150 * Mathf.Sin(Time.fixedDeltaTime * _maxSpeed);
            _keyTrans.localPosition = v;
        }
    }

    [ContextMenu("StopKey")]
    public void StopKey()
    {
        if(IsOverlappingUI())
        {
            Debug.Log("Sucess");
        }
    }

    public bool IsOverlappingUI()
    {
        if(_keyTrans.localPosition.x > _collectRange.localPosition.x - _collectRange.sizeDelta.x * 0.5f - _keyTrans.sizeDelta.x * 0.5f &&
           _keyTrans.localPosition.x < _collectRange.localPosition.x + _collectRange.sizeDelta.x * 0.5f + _keyTrans.sizeDelta.x * 0.5f)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        MoveKey();

        if(Input.GetKeyDown(KeyCode.E))
        {
            StopKey();
        }
    }
}
