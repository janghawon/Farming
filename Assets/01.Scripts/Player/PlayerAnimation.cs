using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator _animator;
    [SerializeField] private AnimatorOverrideController _controller;
    SpriteRenderer _sr;
    [SerializeField] private AnimationClip[] clips;

    private readonly int _walkHash = Animator.StringToHash("isWalk");
    private readonly int _atkHash = Animator.StringToHash("isAttack");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    private void AnimationSet(int idle, int walk, int atk)
    {
        _controller["Front_Idle"] = clips[idle];
        _controller["Front_Walk"] = clips[walk];
        _controller["Front_Attack"] = clips[atk];
    }

    public void PAnimation(Vector3 dir, PlayerDirection _pDir)
    {
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
                _sr.flipX = false;
                break;
            case PlayerDirection.left:
                AnimationSet(6, 7, 8);
                _sr.flipX = true;
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
