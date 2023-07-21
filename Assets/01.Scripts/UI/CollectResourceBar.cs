using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CollectResourceBar : MonoBehaviour
{
    public static CollectResourceBar Instance;
    [SerializeField] bool systemStart;
    PlayerCheckElement pce;
    [SerializeField] private float _maxSpeed;
    public float MaxSpeed => _maxSpeed;

    Image _collectRangeBar;
    RectTransform _collectRange;
    [SerializeField] private Color _normaalRed;
    [SerializeField] private Color _sucessGreen;
    Image _collectRangeImg;

    [SerializeField] private Sprite _normalKeyImage;
    [SerializeField] private Sprite _pressKeyImage;
    
    Image _keyImage;
    RectTransform _keyTrans;

    float _range;
    float _speed;
    [SerializeField] float _maxCount;
    [SerializeField] float _dCount;
    bool isMove;
    public bool isFinish;

    [SerializeField] private UnityEvent _attackMotionEvent;
    [SerializeField] private UnityEvent _endEvent;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!!!!");
            return;
        }
        Instance = this;
        pce = GameObject.Find("Player").GetComponentInChildren<PlayerCheckElement>();
        _collectRangeBar = GetComponent<Image>();
        _collectRange = transform.Find("CorrectRange").GetComponent<RectTransform>();
        _keyImage = transform.Find("Key").GetComponent<Image>();
        _keyTrans = _keyImage.GetComponent<RectTransform>();
        _collectRangeImg = _collectRange.GetComponent<Image>();
    }

    private void Start()
    {
        _keyTrans.localPosition = Vector3.zero;

        _collectRangeBar.enabled = false;
        _collectRangeImg.enabled = false;
        _keyImage.enabled = false;

        isMove = false;
    }

    public void SetAndStart(float rangeArea, float speed, float destroyCount, float maxCount)
    {
        if(!isFinish)
        {
            _collectRangeImg.color = _normaalRed;
            _collectRangeBar.enabled = true;
            _collectRangeImg.enabled = true;
            _keyImage.enabled = true;

            _range = rangeArea;
            _speed = speed;
            _dCount = destroyCount;
            _maxCount = maxCount;
            _keyImage.sprite = _normalKeyImage;
            _collectRange.sizeDelta = new Vector2(rangeArea, 50);
            float dex = Random.Range(rangeArea * 0.5f, -rangeArea * 0.5f);
            _collectRange.localPosition = new Vector3(dex, 0, 0);
            _maxSpeed = speed;

            isMove = true;
            systemStart = true;
        }
    }

    float t;
    private void MoveKey()
    {
        if(isMove)
        {
            _keyTrans.localPosition = new Vector3(Mathf.Sin(t) * 150, _keyTrans.localPosition.y);
            t += Time.fixedDeltaTime;
        }
    }

    public void StopKey()
    {
        _keyImage.sprite = _pressKeyImage;
        _collectRangeImg.color = _normaalRed;
        if(IsOverlappingUI() && systemStart)
        {
            _attackMotionEvent?.Invoke();
            _collectRangeImg.color = _sucessGreen;
            isMove = false;
            _dCount--;
        }
        _maxCount--;

        if(_maxCount == 0)
        {
            StartCoroutine(TurmCo());
            if (_dCount == 0)
            {
                pce.selectIb.DropItem();
            }
        }
    }

    IEnumerator TurmCo()
    {
        yield return new WaitForSeconds(1);
        _endEvent?.Invoke();
        pce.canInteraction = true;
        isMove = false;
        isFinish = true;
        yield return null;
        systemStart = false;
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
        if(Input.GetKeyDown(KeyCode.E) && systemStart)
        {
            StopKey();
            systemStart = false;
            isMove = false;
            StartCoroutine(SystemCool());
        }
        MoveKey();
    }

    IEnumerator SystemCool()
    {
        if(!isFinish)
        {
            yield return new WaitForSeconds(1);
            systemStart = true;
            isMove = true;
            _collectRangeImg.color = _normaalRed;
            SetAndStart(_range, _speed, _dCount, _maxCount);
        }
    }
}
