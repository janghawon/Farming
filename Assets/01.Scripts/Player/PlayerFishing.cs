using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerFishing : MonoBehaviour
{
    [SerializeField] private Sprite[] _front = new Sprite[4];
    [SerializeField] private Sprite[] _back = new Sprite[4];
    [SerializeField] private Sprite[] _side = new Sprite[4];
    [SerializeField] private AnimationClip[] _bursts = new AnimationClip[3];

    [SerializeField] private UnityEvent<Sprite> _chargingEvent;
    [SerializeField] private UnityEvent<AnimationClip> _burstEvent;

    [SerializeField] private int chargingValue;
    private int num;
    private SpriteRenderer _sr;

    private AnimationClip _burstAnimation;
    private Sprite _lateSprite;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        chargingValue = 0;
    }

    private int NumCalculator()
    {
        if (chargingValue >= 100 && chargingValue < 250)
        {
            num = 0;
        }
        else if (chargingValue >= 250 && chargingValue < 400)
        {
            num = 1;
        }
        else if (chargingValue >= 400)
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
                _lateSprite = _front[3];
                _chargingEvent?.Invoke(_front[NumCalculator()]);
                break;
            case PlayerDirection.back:
                _burstAnimation = _bursts[1];
                _lateSprite = _back[3];
                _chargingEvent?.Invoke(_back[NumCalculator()]);
                break;
            case PlayerDirection.right:
                _burstAnimation = _bursts[2];
                _lateSprite = _side[3];
                _chargingEvent?.Invoke(_side[NumCalculator()]);
                break;
            case PlayerDirection.left:
                _sr.flipX = true;
                _burstAnimation = _bursts[2];
                _lateSprite = _side[3];
                _chargingEvent?.Invoke(_side[NumCalculator()]);
                break;
        }
    }

    public void Charging(PlayerDirection pdir)
    {
        if(Input.GetMouseButton(0))
        {
            SetChargingTexture(pdir);
            chargingValue++;
        }
        if(Input.GetMouseButtonUp(0))
        {
            _burstEvent?.Invoke(_burstAnimation);
        }
    }
}
