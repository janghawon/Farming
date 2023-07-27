using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI _guideTxt;
    private RectTransform _guideTxtRect;
    [SerializeField] private Canvas _canvas;
    private RectTransform _canvasRect;
    private Camera _worldCam;

    public bool canTexting;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!");
            return;
        }
        Instance = this;
        canTexting = true;
    }

    private void Start()
    {
        _canvasRect = _canvas.GetComponent<RectTransform>();
        _guideTxtRect = _guideTxt.GetComponent<RectTransform>();
        _worldCam = _canvas.worldCamera;

        _guideTxt.enabled = false;
    }

    Vector3 setPos;
    public void SetGuideText(string txtInfo, Vector2 pos, bool active)
    {
        if(!active || !canTexting)
        {
            _guideTxt.enabled = false;
            return;
        }
        _guideTxt.enabled = true;
        _guideTxt.SetText(txtInfo);

        setPos = GameManager.Instance.MainCam.WorldToScreenPoint(pos + new Vector2(0, 2));
        var localPos = new Vector2(0, 20);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, setPos, _worldCam, out localPos);

        _guideTxtRect.localPosition = localPos;
    }
}
