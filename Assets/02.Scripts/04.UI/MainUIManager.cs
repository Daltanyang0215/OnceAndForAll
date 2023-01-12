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

    public void ShowWeaponCircle(WeaponType type, bool show)
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
        if (show != _interactionPanel.activeSelf)
            _interactionPanel.SetActive(show);
    }


    public void ShowAddBulletCount(Element element)
    {
        switch (element)
        {
            case Element.Normal:
                _addGunBulletImage.color = new Color(0.5f, 0.5f, 0.5f, 1f);
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

    public void ShowTowerInfoPanel(TowerBase tower = null)
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

            _towerInteraction.text = $"{tower.GetTowerInfo.TowerName} \n" + upgrade;
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

    public void SettingRewardText(int index, string infomation)
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

    #region BaseUI
    [Header("BaseUI")]
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _Round;
    [SerializeField] private TMP_Text _enemyCount;
    private int _maxEnemyCount;

    public void ShowHealthText(int health) => _health.text = health.ToString();
    public void ShowRoundText(int round) => _Round.text = round.ToString();
    public void ShowEnemyCountText(int enemyCount)
    {
        if (enemyCount > _maxEnemyCount)
            _maxEnemyCount = enemyCount;

        _enemyCount.text = $"{enemyCount} / {_maxEnemyCount}";
        _enemyCount.GetComponentInParent<Image>().fillAmount = (float)enemyCount / _maxEnemyCount;
    }
    public void ResetEnemyCountText()
    {
        _maxEnemyCount = 0;
    }
    #endregion

    #region WeaponUI
    [Header("WeaponUI")]
    [SerializeField] private GameObject _weaponUI;
    [SerializeField] private RectTransform[] _weapons;
    [SerializeField] private Image _bulletFill;
    [SerializeField] private TMP_Text _bulletCount;

    public void ShowBullet(int currentBullet, int maxBullet)
    {
        _bulletCount.text = $"{currentBullet} / {maxBullet}";
        _bulletFill.fillAmount = (float)currentBullet / maxBullet;
    }

    public void ShowWeaponIcon(int index , Sprite icon)
    {
        _weapons[index].GetChild(0).GetComponent<Image>().sprite = icon;
    }

    #endregion

    #region UpgradeUI
    [Header("UpgradeUI")]
    [SerializeField] private GameObject _upgradeUI;
    [SerializeField] private GameObject _orbUI;
    [SerializeField] private RectTransform _orbCircle;
    [SerializeField] private TMP_Text _orbCount;
    [Header("-TowerBuildUI")]
    [SerializeField] private GameObject _towerBuildUI;
    [SerializeField] private RectTransform _towerCircle;
    [SerializeField] private TMP_Text _towerCount;
    [Header("-OrbUI")]
    [SerializeField] private GameObject _towerinfoPanel;
    [SerializeField] private Image _towerImage;
    [SerializeField] private Transform _currentOrb;
    [SerializeField] private TMP_Text _towerName;
    [SerializeField] private GameObject _towerDamageFill;
    [SerializeField] private GameObject _towerRangeFill;
    [SerializeField] private GameObject _towerSpeedFill;
    [SerializeField] private TMP_Text _towerAddEffect;
    private bool _isUpgrade;
    private TowerBase _beforeData;
    public void ShowUpgrade(bool show, bool _isUpgrade)
    {
        _upgradeUI.SetActive(show);
        if (show)
        {
            if (_isUpgrade)
            {
                _orbUI.SetActive(true);
                _towerBuildUI.SetActive(false);
            }
            else
            {
                _orbUI.SetActive(false);
                _towerBuildUI.SetActive(true);
            }
            this._isUpgrade = _isUpgrade;
        }
    }
    public void SelectedOrb(Element element)
    {
        _orbCircle.eulerAngles = Vector3.forward * (-90 * (int)element);
        _addGunBulletCount.text = StatesEnforce.Instance.getElementCount(element).ToString();
    }
    public void SelectedTower(TowerType tower)
    {
        _towerCircle.eulerAngles = Vector3.forward * (-90 * (int)tower);
    }
    public void SetTowerInfoPanel(TowerBase tower = null)
    {
        if (_beforeData == tower) return;

        if (tower == null)
            _towerinfoPanel.gameObject.SetActive(false);
        else
        {
            _towerinfoPanel.gameObject.SetActive(true);
            //_towerImage = tower.GetTowerInfo.ico
            _towerName.text = tower.GetTowerInfo.TowerName;

            // 타워의 강화 설명
            string upgrade = "";
            for (int i = 0; i < tower.GetElementLeath(); i++)
            {
                upgrade += $"{tower.GetElement(i)} \t";
            }
            _towerAddEffect.text = upgrade;

            // 오브의 이미지 출력
            for (int i = 0; i < _currentOrb.childCount; i++)
            {
                if (i >= tower.GetElementLeath())
                {
                    _currentOrb.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    _currentOrb.GetChild(i).GetChild(0).gameObject.SetActive(true);
                    switch (tower.GetElement(i))
                    {
                        case Element.Normal:
                            _currentOrb.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                            break;
                        case Element.Fire:
                            _currentOrb.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(1f, 0.5f, 0.5f, 1f);
                            break;
                        case Element.Ice:
                            _currentOrb.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f, 1f);
                            break;
                        case Element.Electricity:
                            _currentOrb.GetChild(i).GetChild(0).GetComponent<Image>().color = new Color(0.9f, 0.9f, 0.5f, 1f);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        // 게이지 관련 작업 필요

        _beforeData = tower;
    }
    #endregion


    public void ShowUI(int inputKeyValue)
    {
        if (inputKeyValue == 0)
        {
            ShowUpgrade(true, _isUpgrade);
            _weaponUI.SetActive(false);
        }
        else if (inputKeyValue == 1)
        {
            _weaponUI.SetActive(true);
            ShowUpgrade(false, _isUpgrade);
            _weapons[0].localPosition = new Vector3(850, -200);
            _weapons[1].localPosition = new Vector3(950, -315);
        }
        else if (inputKeyValue == 2)
        {
            _weaponUI.SetActive(true);
            ShowUpgrade(false, _isUpgrade);
            _weapons[0].localPosition = new Vector3(950, -200);
            _weapons[1].localPosition = new Vector3(850, -315);
        }
    }

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

    #region Setting
    [Header("SettingUI")]
    [SerializeField] private GameObject _settingPanel;

    public void ShowSettingUI()
    {
        _settingPanel.SetActive(!_settingPanel.activeSelf);
        CheckAllUIClose();
    }

    #endregion
    // 플레이어가 ui 가 열려있는 상태인지 상태 확인용 함수
    // ui 가 추가 됨에 따라 길어질 예정. 혹은 더 좋은 구조가 떠오르면 수정 예정
    public void CheckAllUIClose()
    {
        if (_isShowBuilder
            || _isShowBuildCircle
            || _isShowReward
            || _settingPanel.activeSelf)
        {
            Player.Instance.isShowUI = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Player.Instance.isShowUI = false;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
