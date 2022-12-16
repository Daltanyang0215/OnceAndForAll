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
    [Space]
    [Header("TowerBuildCircle")]
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
    [Space]
    [Header("TowerBuilder")]
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
            if (_isShowBuilder == false && 
                value)
            {
                _isShowBuilder = value;
                    ShowTowerBuilder(_isShowBuilder);
            }
            else
            {
                _isShowBuilder = false;
                ShowTowerBuilder(false);
            }
            CheckAllUIClose();
        }
    }

    public void ShowTowerBuilder(bool show, bool destroy = false)
    {
        _towerBuilder.gameObject.SetActive(show);
        _towerBuilder.isDestroy = destroy;
    }

    // 타워빌드에 타워 정보 전달 용
    // (ui -> 메인 ui(전달) -> 타워빌더)
    public void SetTowerBilder(TowerInfo towerInfo)
    {
        _towerBuilder.SetBuilderTower(towerInfo);
    }

    #endregion

    #region PlayerDataUI
    [Space]
    [Header("PlayerUI")]
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private GameObject _shotGunCircle;
    [SerializeField] private GameObject _sniperCircle;
    [SerializeField] private TMP_Text _sniperBulletCount;
    [SerializeField] private GameObject _addGunCircle;
    [SerializeField] private TMP_Text _addGunBulletCount;
    [SerializeField] private Image _addGunBulletImage;
    [SerializeField] private TMP_Text _towerInteraction;
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private TMP_Text _RoundText;
    [SerializeField] private TMP_Text _MonsterCountText;

    public void SetHealthText(int health)
    {
        _healthText.text = health.ToString();
    }
    public void SetMoneyText(int money)
    {
        _moneyText.text = money.ToString();
    }
    public void SetRounText(int round)
    {
        _RoundText.text = round.ToString();
    }
    public void SetMonsterCountText(int count)
    {
        _MonsterCountText.text = count.ToString();
    }

    public void OnPlayDataUIInit()
    {
        _healthText.text = MainGameManager.Instance.Health.ToString();
        _moneyText.text = MainGameManager.Instance.Money.ToString();
    }

    public void ShowWeaponCircle(WeaponType type , bool show)
    {
        switch (type)
        {
            case WeaponType.Rifle:
                break;
            case WeaponType.Shotgun:
                _shotGunCircle.SetActive(show);
                break;
            case WeaponType.Sniper:
                _sniperCircle.SetActive(show);
                break;
            case WeaponType.Add:
                _addGunCircle.SetActive(show);
                break;
            default:
                break;
        }
    }
    public void ShowInteractionPanel(bool show)
    {
        if(show != _interactionPanel.activeSelf)
            _interactionPanel.SetActive(show);
    }

    public void ShowSniperBulletCount(int count)
    {
        _sniperBulletCount.text = count.ToString();
    }

    public void ShowAddBulletCount(Element element)
    {
        switch (element)
        {
            case Element.Normal:
                _addGunBulletImage.color = new Color(0.5f,0.5f,0.5f,1f);
                break;
            case Element.Fire:
                _addGunBulletImage.color = new Color(1f, 0.5f, 0.5f, 1f);
                break;
            case Element.Ice:
                _addGunBulletImage.color = new Color(0.5f, 0.5f, 1f, 1f);
                break;
            case Element.Electricity:
                _addGunBulletImage.color = new Color(0.9f, 0.9f, 0.5f, 1f);
                break;
            default:
                break;
        }
        _addGunBulletCount.text = StatesEnforce.Instance.getElementCount(element).ToString();
    }

    public void ShowTowerInfoPanel(TowerBase tower =null)
    {
        if (tower == null)
            _towerInteraction.gameObject.SetActive(false);
        else
        {
            string upgrade = "";

            for (int i = 0; i < tower.GetElementLeath(); i++)
            {
                upgrade += $"{tower.GetElement(i)} \t";
            }

            _towerInteraction.text = $"{tower.GetTowerInfo.name} \n"+upgrade;
            _towerInteraction.gameObject.SetActive(true);
        }
    }
    #endregion

    #region Reward
    [Space]
    [Header("Reward")]
    [SerializeField] private Transform _reward;
    [SerializeField] private List<TMP_Text> _rewardText;
    [SerializeField] private GameObject _rewardList;
    [SerializeField] private TMP_Text _positiveText;
    [SerializeField] private TMP_Text _negativeText;
    [SerializeField] private GameObject _buffInfromtionList;
    [SerializeField] private GameObject _nextRound;
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
    public void ShowNextRound(bool show)
    {
        _nextRound.gameObject.SetActive(show);
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

    public void OnRewardList(bool show)
    {
        if (show)
        {
            _positiveText.text = StatesEnforce.Instance.GetPositiveList();
            _negativeText.text = StatesEnforce.Instance.GetNegativeList();
        }
            _rewardList.SetActive(show);
    }
    public void OnBuffInformation(bool show)
    {
        _buffInfromtionList.SetActive(show);
    }
    #endregion

    #region GameEnd
    [Header("GameEnd")]
    [SerializeField] private GameObject _gameEndPanel;
    [SerializeField] private TMP_Text _endRound;
    [SerializeField] private TMP_Text _endPos;
    [SerializeField] private TMP_Text _endNega;

    public void ShowGameEndPanel(int round, bool show = true)
    {
        _gameEndPanel.SetActive(show);
        if (show)
        {
            _endRound.text = round.ToString();
            _endPos.text = StatesEnforce.Instance.GetPositiveList();
            _endNega.text = StatesEnforce.Instance.GetNegativeList();
        }
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
