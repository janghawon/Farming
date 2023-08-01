using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishingSystem : MonoBehaviour
{
    Transform _player;
    [SerializeField] private Transform _dummyTrans;
    FishingDummy _fishingDum;
    Vector2 _startPos;
    private float _waitingTime;
    [SerializeField] [Range(1, 10)] private float _minWatingTime;
    [SerializeField] [Range(2, 11)] private float _maxWatingTime;

    [SerializeField] private int _completeValue;
    [SerializeField] private int _currentValue = -1;
    [SerializeField] [Range(10, 20)] private int _minComValue;
    [SerializeField] [Range(21, 40)] private int _maxComValue;

    public bool isFishing;
    public bool isFIshingStart { get; set; }

    [SerializeField] private DropTable _dropTable;
    [SerializeField] private UnityEvent _fishingEndEvent;

    private void Start()
    {
        _player = GameManager.Instance.Player;
        _fishingDum = _dummyTrans.GetComponent<FishingDummy>();
    }

    public void FishingStart()
    {
        _waitingTime = Random.Range(_minWatingTime, _maxWatingTime);
        _completeValue = Random.Range(_minComValue, _maxComValue);
        _startPos = _dummyTrans.position;

        dir = (Vector2)_player.position - _startPos.normalized;
        _fishingDum.isMoving = true;
    }

    private void Update()
    {
        if(isFishing)
        {
            if(Time.frameCount % 20 == 0)
            {
                Shaking(-1);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Shaking(1);
            }
            if (Vector2.Distance(_player.position, _dummyTrans.position) < 0.5f)
            {
                _fishingEndEvent?.Invoke();
                isFishing = false;
                _fishingDum.isMoving = false;
                Debug.Log("³¡");
            }
            
        }
        FishingLogic();
        
    }
    Vector3 dir;
    private void Shaking(int val)
    {
        _dummyTrans.position += -dir * 0.1f * val;
    }

    private void FishingLogic()
    {
        #region Lock
        if (!isFIshingStart)
            return;
        isFIshingStart = false;
        #endregion
        StartCoroutine(WaitingFishing());
    }

    IEnumerator WaitingFishing()
    {
        yield return new WaitForSeconds(_waitingTime * 0.7f);
        Debug.Log("ÀâÈû!!");
        isFishing = true;
    }
}
