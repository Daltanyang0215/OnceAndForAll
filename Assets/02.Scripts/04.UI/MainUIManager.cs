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

    #region TowerBuildCircle
    [SerializeField] private QuickWheel _towerBuildCircle;
    private bool _isShowBuildCircle;
    public bool isShowBuildCircle
    {
        get
        {
            return _isShowBuildCircle;
        }
        set
        {
            // �Է��� �ٲ� �̹��� ����
            if (_isShowBuildCircle != value)
            {
                _isShowBuildCircle = value;
                if (_isShowBuildCircle)
                {
                    ShowTowerBuildCircle(true);
                    isShowBuilder=false;
                }
                else
                {
                    ShowTowerBuildCircle(false);
                }
            }
            CheckAllUIClose();
        }
    }
    public void ShowTowerBuildCircle(bool show)
    {
        _towerBuildCircle.gameObject.SetActive(show);
    }
    #endregion

    #region TowerBuilder
    [SerializeField] private TowerBuilder _towerBuilder;
    private bool _isShowBuilder;
    public bool isShowBuilder
    {
        get
        {
            return _isShowBuilder;
        }
        set
        {
            // �Է��� �ٲ� �̹��� ����
            if (_isShowBuilder != value)
            {
                _isShowBuilder = value;
                if (_isShowBuilder)
                {
                    ShowTowerBuilder(true);
                }
                else
                {
                    ShowTowerBuilder(false);
                }
            }
            CheckAllUIClose();
        }
    }

    public void ShowTowerBuilder(bool show)
    {
        _towerBuilder.gameObject.SetActive(show);
    }

    // Ÿ�����忡 Ÿ�� ���� ���� ��
    // (ui -> ���� ui(����) -> Ÿ������)
    public void SetTowerBilder(TowerInfo towerInfo)
    {

        _towerBuilder.SetBuilderTower(towerInfo);
    }

    #endregion
    // �÷��̾ ui �� �����ִ� �������� ���� Ȯ�ο� �Լ�
    // ui �� �߰� �ʿ� ���� ����� ����. Ȥ�� �� ���� ������ �������� ���� ����
    public void CheckAllUIClose()
    {
        if (_isShowBuilder
            || _isShowBuildCircle)
        {
            Player.Instance.isShowUI = true;
        }
        else
        {
            Player.Instance.isShowUI = false;
        }
    }
}
