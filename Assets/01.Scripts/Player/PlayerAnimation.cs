using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    [SerializeField] private Sprite _failSprite;
    [SerializeField] private AnimatorOverrideController _controller;
    [SerializeField] private AnimationClip[] clips;

    private readonly int _walkHash = Animator.StringToHash("isWalk");
    private readonly int _atkHash = Animator.StringToHash("isAttack");
    private readonly int _fishingHash = Animator.StringToHash("iSfishing");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Charging(Sprite sp)
    {
        _animator.enabled = false;
        _spriteRenderer.sprite = sp;
    }    

    public void FishingStart(AnimationClip clip)
    {
        _animator.enabled = true;
        Debug.Log(_animator.enabled);
        _controller["Back_Fishing_Burst"] = clip;
        _animator.SetBool(_fishingHash, true);
    }

    private void AnimationSet(int idle, int walk, int atk)
    {
        _controller["Front_Idle"] = clips[idle];
        _controller["Front_Walk"] = clips[walk];
        _controller["Front_Attack"] = clips[atk];
    }

    public void PFail()
    {
        _animator.enabled = false;
        _spriteRenderer.sprite = _failSprite;
    }

    public void PAttack()
    {
        _animator.SetBool(_atkHash, true);
        StartCoroutine(Turm());
    }

    IEnumerator Turm()
    {
        yield return new WaitForSeconds(clips[2].length - 0.7f);
        _animator.SetBool(_atkHash, false);
    }

    public void PAnimation(Vector3 dir, PlayerDirection _pDir)
    {
        _spriteRenderer.flipX = false;
        switch (_pDir)
        {
            case PlayerDirection.front:
                AnimationSet(0, 1, 2);
                break;
            case PlayerDirection.back:
                AnimationSet(3, 4, 5);
                break;
            case PlayerDirection.right:
                AnimationSet(6, 7, 8);
                break;
            case PlayerDirection.left:
                _spriteRenderer.flipX = true;
                AnimationSet(6, 7, 8);
                break;
        }
        if(dir.sqrMagnitude != 0)
        {
            _animator.SetBool(_walkHash, true);
        }
        else
        {
            _animator.SetBool(_walkHash, false);
        }
    }
}
