using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TextMeshProUGUI _guideTxt;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("!!!");
            return;
        }
        Instance = this;
    }

    public void SetGuideText(string txtInfo, Vector2 pos)
    {
        _guideTxt.SetText(txtInfo);
        _guideTxt.transform.localPosition = GameManager.Instance.MainCam.ScreenToWorldPoint(pos);
    }
}
