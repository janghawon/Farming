using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FishingSystem : MonoBehaviour
{
    private float _waitingTime;
    [SerializeField] [Range(1, 10)] private float _minWatingTime;
    [SerializeField] [Range(2, 11)] private float _maxWatingTime;

    [SerializeField] private float _completeValue;
    private float _currentValue;
    [SerializeField] [Range(30, 50)] private float _minComValue;
    [SerializeField] [Range(51, 80)] private float _maxComValue;

    public bool isFishing { get; set; }
    private bool isFIshingStart;

    [SerializeField] private DropTable _dropTable;
    [SerializeField] private UnityEvent _fishingStartEvent;
    [SerializeField] private UnityEvent _fishingEndEvent;

    public void FishingStart()
    {
        _waitingTime = Random.Range(_minWatingTime, _maxWatingTime);
        _completeValue = Random.Range(_minComValue, _maxComValue);
        _fishingStartEvent?.Invoke();
    }

    private void Update()
    {
        if(isFishing)
        {
            if(Time.frameCount % 5 == 0)
            {
                _completeValue--;
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                _completeValue++;
            }
        }
        if(_currentValue == _completeValue)
        {
            _fishingEndEvent?.Invoke();
        }
        FishingLogic();
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
    }
}
