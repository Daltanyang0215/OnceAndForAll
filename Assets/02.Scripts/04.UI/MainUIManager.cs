using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainUIManager : MonoBehaviour
{
    public static MainUIManager instance;
    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private Transform _tutorial;

    public void ShowTutorial(bool show)
    {
        _tutorial.gameObject.SetActive(show);
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
            // 입력이 바뀔때 이미지 띄우기
            if (_isShowBuildCircle != value)
            {
                _isShowBuildCircle = value;
                if (_isShowBuildCircle)
                {
                    ShowTowerBuildCircle(true);
                    isShowBuilder = false;
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
            // 입력이 바뀔때 이미지 띄우기
            if (_isShowBuilder != value)
            {
                _isShowBuilder = value;
                    ShowTowerBuilder(_isShowBuilder);
            }
            CheckAllUIClose();
        }
    }

    public void ShowTowerBuilder(bool show)
    {
        _towerBuilder.gameObject.SetActive(show);
    }

    // 타워빌드에 타워 정보 전달 용
    // (ui -> 메인 ui(전달) -> 타워빌더)
    public void SetTowerBilder(TowerInfo towerInfo)
    {
        _towerBuilder.SetBuilderTower(towerInfo);
    }

    #endregion

    #region PlayerDataUI
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _moneyText;

    public void SetHealthText(int health)
    {
        _healthText.text = health.ToString();
    }
    public void SetMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }

    public void OnPlayDataUIInit()
    {
        _healthText.text = MainGameManager.Instance.Health.ToString();
        _moneyText.text = MainGameManager.Instance.Money.ToString();
    }
    #endregion

    #region Reward
    [SerializeField] private Transform _reward;
    [SerializeField] private List<TMP_Text> _rewardText;
    private bool _isShowReward;
    public bool IsShowReward
    {
        get { return _isShowReward; }
        set
        {
            // 입력이 바뀔때 이미지 띄우기
            if (_isShowReward != value)
            {
                _isShowReward = value;

                ShowReward(_isShowReward);
            }
            CheckAllUIClose();
        }

    }
    public void ShowReward(bool show)
    {
        _reward.gameObject.SetActive(show);
    }

    public void SettingRewardText(int index , string infomation)
    {
        _rewardText[index].text = infomation;
    }

    public void OnSelectReward(int index)
    {
        IsShowReward = false;
        MainGameManager.Instance.RewardSelectAfter(index);
    }
    #endregion

    // 플레이어가 ui 가 열려있는 상태인지 상태 확인용 함수
    // ui 가 추가 됨에 따라 길어질 예정. 혹은 더 좋은 구조가 떠오르면 수정 예정
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
