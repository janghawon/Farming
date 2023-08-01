using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingDummy : MonoBehaviour
{
    LineRenderer _lineRenderer;
    [SerializeField] Transform _fishingPos;
    private SpriteRenderer _sr;
    private Rigidbody2D _rigid;
    Vector2 dirType;
    Camera _cam;
    Transform _player;
    public bool isMoving { get; set; }

    private void Awake()
    {
        _lineRenderer = (LineRenderer)GetComponent("LineRenderer");
        _sr = (SpriteRenderer)GetComponent("SpriteRenderer");
        _rigid = (Rigidbody2D)GetComponent("Rigidbody2D");
    }

    private void Start()
    {
        _sr.enabled = false;
        _rigid.gravityScale = 0;
        _rigid.bodyType = RigidbodyType2D.Kinematic;
        _cam = Camera.main;
        _player = GameManager.Instance.Player;
    }

    public void SetPos()
    {
        _sr.enabled = false;
        transform.localPosition = new Vector3(0.4f, 0.65f);
        _cam.transform.parent = _player;
    }

    public void Flying(PlayerDirection _pdir, float force)
    {
        switch(_pdir)
        {
            case PlayerDirection.back:
                dirType = Vector2.up;
                _fishingPos.localPosition = new Vector2(-1, -0.35f);
                break;
            case PlayerDirection.front:
                dirType = Vector2.down;
                _fishingPos.localPosition = new Vector2(0, -0.85f);
                break;
            case PlayerDirection.left:
                dirType = Vector2.left;
                _fishingPos.localPosition = new Vector2(1, -0.35f);
                break;
            case PlayerDirection.right:
                dirType = Vector2.right;
                _fishingPos.localPosition = new Vector2(0, 0.85f);
                break;
        }
        _lineRenderer.SetPosition(0, _fishingPos.position);
        isMoving = true;
        _cam.transform.parent = transform;

        _sr.enabled = true;
        _rigid.bodyType = RigidbodyType2D.Dynamic;
        if (force < 3)
            force = 3;
        if (force > 5f)
            force = 5f;
        _rigid.AddForce(dirType * force * 2, ForceMode2D.Impulse);
        StartCoroutine(Turm(force));
    }

    IEnumerator Turm(float time)
    {
        yield return new WaitForSeconds(time);
        isMoving = false;
        _rigid.gravityScale = 0;
        _rigid.bodyType = RigidbodyType2D.Kinematic;
    }

    private void Update()
    {
        if(isMoving)
            _lineRenderer.SetPosition(1, transform.position);
    }
}
