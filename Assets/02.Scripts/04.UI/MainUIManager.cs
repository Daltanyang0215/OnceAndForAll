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
            // �Է��� �ٲ� �̹��� ����
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
            // �Է��� �ٲ� �̹��� ����
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

    // Ÿ�����忡 Ÿ�� ���� ���� ��
    // (ui -> ���� ui(����) -> Ÿ������)
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
            default:
                break;
        }
    }
    public void ShowInteractionPanel(bool show)
    {
        if(show != _interactionPanel.activeSelf)
            _interactionPanel.SetActive(show);
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
            // �Է��� �ٲ� �̹��� ����
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
