using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Transform _towerBuildCircle;
    private bool _isShowBuildCircle;
    public bool isShowBuildCircle
    {
        get
        {
            return _isShowBuildCircle;
        }
        set
        {
            // 입력이 바뀔때 이미지 띄우기
            if (_isShowBuildCircle != value)
            {
                _isShowBuildCircle = value;
                if (_isShowBuildCircle)
                {
                    ShowTowerBuildCircle(true);
                }
                else
                {
                    ShowTowerBuildCircle(false);
                }
            }
        }
    }

    public void ShowTowerBuildCircle(bool show)
    {
        _towerBuildCircle.gameObject.SetActive(show);
    }
}
