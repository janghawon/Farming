using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingDummy : MonoBehaviour
{
    private SpriteRenderer _sr;
    private Rigidbody2D _rigid;
    Vector2 dirType;

    private void Awake()
    {
        _sr = (SpriteRenderer)GetComponent("SpriteRenderer");
        _rigid = (Rigidbody2D)GetComponent("Rigidbody2D");
    }

    private void Start()
    {
        _sr.enabled = false;
        _rigid.gravityScale = 0;
        _rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Flying(PlayerDirection _pdir, float force)
    {
        switch(_pdir)
        {
            case PlayerDirection.back:
                dirType = Vector2.up;
                break;
            case PlayerDirection.front:
                dirType = Vector2.down;
                break;
            case PlayerDirection.left:
                dirType = Vector2.left;
                break;
            case PlayerDirection.right:
                dirType = Vector2.right;
                break;
        }

        _sr.enabled = true;
        _rigid.bodyType = RigidbodyType2D.Dynamic;
        _rigid.AddForce(dirType * force, ForceMode2D.Impulse);
        StartCoroutine(Turm(force));
    }
    IEnumerator Turm(float time)
    {
        yield return new WaitForSeconds(time * 0.5f);
        _rigid.gravityScale = 0;
        _rigid.bodyType = RigidbodyType2D.Kinematic;
    }
}
