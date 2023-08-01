using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishingSystem : MonoBehaviour
{
    [SerializeField] private Transform _dummyTrans;
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

    public void FishingStart()
    {
        _waitingTime = Random.Range(_minWatingTime, _maxWatingTime);
        _completeValue = Random.Range(_minComValue, _maxComValue);
        _startPos = _dummyTrans.position;
    }

    private void Update()
    {
        if(isFishing)
        {
            if(Time.frameCount % 20 == 0)
            {
                _currentValue--;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _currentValue++;
            }
            if (_currentValue == _completeValue)
            {
                _fishingEndEvent?.Invoke();
                isFishing = false;
                Debug.Log("≥°");
            }
        }
        FishingLogic();
        Shaking();
    }

    private void Shaking()
    {

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
        yield return new WaitForSeconds(_waitingTime);
        Debug.Log("¿‚»˚!!");
        isFishing = true;
    }
}
