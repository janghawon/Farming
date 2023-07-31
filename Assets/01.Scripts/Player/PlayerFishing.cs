using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFishing : MonoBehaviour
{
    [SerializeField] private Sprite[] _front = new Sprite[4];
    [SerializeField] private Sprite[] _back = new Sprite[4];
    [SerializeField] private Sprite[] _side = new Sprite[4];
    [SerializeField] private AnimationClip[] _bursts = new AnimationClip[6];

    [SerializeField] private UnityEvent<Sprite> _chargingEvent;
    [SerializeField] private UnityEvent<AnimationClip> _burstEvent;
    [SerializeField] private UnityEvent _fishingStartEvent;
    [SerializeField] private UnityEvent<AnimationClip> _unBurstEvent;
    [SerializeField] private UnityEvent<PlayerDirection, float> _dummyEvent;

    [SerializeField] private int chargingValue;
    private int num;
    private SpriteRenderer _sr;
    private AnimationClip _burstAnimation;
    private AnimationClip _unBurstAnimation;
    private bool canCharging;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        chargingValue = 0;
        canCharging = true;
    }

    public void CancleFishing() // ³¬½Ã ³¡
    {
        canCharging = true;
        chargingValue = 0;
        _unBurstEvent?.Invoke(_burstAnimation);
    }
    private int NumCalculator()
    {
        if (chargingValue >= 80 && chargingValue < 160)
        {
            num = 0;
        }
        else if (chargingValue >= 160 && chargingValue < 320)
        {
            num = 1;
        }
        else if (chargingValue >= 320)
        {
            num = 2;
        }
        return num;
    }
    private void SetChargingTexture(PlayerDirection _pDir)
    {
        _sr.flipX = false;
        switch(_pDir)
        {
            case PlayerDirection.front:
                _burstAnimation = _bursts[0];
                _unBurstAnimation = _bursts[3];
                _chargingEvent?.Invoke(_front[NumCalculator()]);
                break;
            case PlayerDirection.back:
                _burstAnimation = _bursts[1];
                _unBurstAnimation = _bursts[4];
                _chargingEvent?.Invoke(_back[NumCalculator()]);
                break;
            case PlayerDirection.right:
                _burstAnimation = _bursts[2];
                _unBurstAnimation = _bursts[5];
                _chargingEvent?.Invoke(_side[NumCalculator()]);
                break;
            case PlayerDirection.left:
                _sr.flipX = true;
                _burstAnimation = _bursts[2];
                _unBurstAnimation = _bursts[5];
                _chargingEvent?.Invoke(_side[NumCalculator()]);
                break;
        }
    }
    public void Charging(PlayerDirection pdir)
    {
        if (Input.GetMouseButton(0) && canCharging)
        {
            SetChargingTexture(pdir);
            chargingValue++;
        }
        if(Input.GetMouseButtonUp(0) && canCharging)
        {
            canCharging = false;
            _burstEvent?.Invoke(_burstAnimation);
            _dummyEvent?.Invoke(pdir, chargingValue * 0.01f);
            _fishingStartEvent?.Invoke();
        }
    }
}
