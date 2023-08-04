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
    }

    private void Update()
    {
        if(isFishing)
        {
            if(Time.frameCount % 70 == 0)
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
    }
    Vector3 dir;
    private void Shaking(int val)
    {
        _dummyTrans.position += dir * val;
    }

    public void FishingLogic()
    {
        StartCoroutine(WaitingFishing());
    }

    IEnumerator WaitingFishing()
    {
        yield return new WaitForSeconds(_waitingTime);
        Debug.Log("ÀâÈû!!");
        _startPos = _dummyTrans.position;
        dir = (Vector2)_player.position - _startPos;
        dir.Normalize();

        _fishingDum.isMoving = true;
        isFishing = true;
    }
}
