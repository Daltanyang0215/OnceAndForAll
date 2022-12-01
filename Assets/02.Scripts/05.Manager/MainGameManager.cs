using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    private static MainGameManager instance;
    public static MainGameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.Find("MainGameManager").GetComponent<MainGameManager>();

            return instance;
        }
    }

    public StatesEnforce Enforce = new StatesEnforce();

    private enum GameFlowState
    {
        IDLE,
        WAITING_START,
        LEVEL_START,
        ROUND_START,
        ENEMYSPAWN,
        WAITING_ENDROUND,
        ROUND_END,
        ROUND_REWARD,
        WAITING_NEXTROUND,
        LEVEL_SUCCESS,
        LEVEL_FAIL,
        WAITING_USER
    }

    private GameFlowState state;

    [SerializeField] private int _currentRound;
    public int currentRound
    {
        get { return _currentRound; }
        set
        {
            _currentRound = value;
            MainUIManager.instance.SetRounText(_currentRound);
        }
    }

    [SerializeField] private int _currentEnemyCount;
    public int currentEnemyCount
    {
        get { return _currentEnemyCount; }
        set
        {
            _currentEnemyCount = value;
            MainUIManager.instance.SetMonsterCountText(_currentEnemyCount);
        }
    }

    [SerializeField] private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            MainUIManager.instance.SetHealthText(_health);
            if (_health <= 0)
            {
                LevelFail();
            }
        }
    }

    [SerializeField] private int _money;
    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            MainUIManager.instance.SetMoneyText(_money);
        }
    }


    [SerializeField] private List<AddEffect> _positiveEffectList = new List<AddEffect>();
    [SerializeField] private List<AddEffect> _negativeEffectList = new List<AddEffect>();
    private AddEffect[] _positiveEffect = new AddEffect[3];
    private AddEffect[] _negativeEffect = new AddEffect[3];

    private void Update()
    {
        switch (state)
        {
            case GameFlowState.IDLE:
                {
                    MainUIManager.instance.OnPlayDataUIInit();
                    ObjectPool.Instance.InstantiateAllPoolElement();
                    MainUIManager.instance.ShowTutorial(true);

                    state = GameFlowState.WAITING_START;
                }
                break;
            case GameFlowState.WAITING_START:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        MainUIManager.instance.ShowTutorial(false);

                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                        Player.Instance.enabled = true;

                        state = GameFlowState.ROUND_START;
                    }
                }
                break;
            case GameFlowState.LEVEL_START:
                {

                }
                break;
            case GameFlowState.ROUND_START:
                {
                    currentRound++;

                    EnemySpawner.instance.SpawnPoolAdd("�⺻����", 5, 0.2f, 0.5f);
                    // ��ȭ ����
                    Player.Instance.EnforceApply();
                    TowerManager.instance.OnStatesEnforce();
                    state = GameFlowState.ENEMYSPAWN;
                }
                break;
            case GameFlowState.ENEMYSPAWN:
                {
                    EnemySpawner.instance.SpawnStart();
                    state = GameFlowState.WAITING_ENDROUND;
                }
                break;
            case GameFlowState.WAITING_ENDROUND:
                {
                    // not to do
                }
                break;
            case GameFlowState.ROUND_END:
                {
                    Debug.Log("���� ����");

                    SelectEffect();
                    MainUIManager.instance.IsShowReward = true;

                    Player.Instance.PlayerStop();
                    Player.Instance.enabled = false;

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    state = GameFlowState.ROUND_REWARD;
                }
                break;
            case GameFlowState.ROUND_REWARD:
                {
                }
                break;
            case GameFlowState.WAITING_NEXTROUND:
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        MainUIManager.instance.ShowNextRound(false);
                        state = GameFlowState.ROUND_START;
                    }
                }
                break;
            case GameFlowState.LEVEL_SUCCESS:
                {
                    Debug.Log("���� ����. �̼� ����");
                }
                break;
            case GameFlowState.LEVEL_FAIL:
                {
                    Debug.Log("���� ����. �̼� ����");
                    MainUIManager.instance.ShowGameEndPanel(currentRound);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    state = GameFlowState.WAITING_USER;
                }
                break;
            case GameFlowState.WAITING_USER:
                {

                }
                break;
            default:
                break;
        }
    }

    // ���尡 ����Ǿ����� Ȯ��
    public void RoundEndCheck()
    {
        // �� ��ȯ�� ���� �Ǿ�����, ���� ���� ���Ͱ� ������ ���� ����
        if (EnemySpawner.instance.isSpawning == false
            && EnemySpawner.instance.transform.childCount == 0
            && state == GameFlowState.WAITING_ENDROUND)
        {
            state = GameFlowState.ROUND_END;
        }
    }


    // ���� ���� �� ���� ���� ����
    private void SelectEffect()
    {
        // ����ȿ�� ����
        List<int> selectindex = new List<int>();
        int tmpindex = 0;
        for (int i = 0; i < _positiveEffectList.Count; i++)
        {
            selectindex.Add(i);
        }
        for (int i = 0; i < _positiveEffect.Length; i++)
        {
            tmpindex = selectindex[Random.Range(0, selectindex.Count)];
            selectindex.Remove(tmpindex);
            _positiveEffect[i] = _positiveEffectList[tmpindex];
            MainUIManager.instance.SettingRewardText(i * 2, _positiveEffect[i].GetInfomation());
        }

        // ����ȿ�� ����
        selectindex.Clear();
        for (int i = 0; i < _negativeEffectList.Count; i++)
        {
            selectindex.Add(i);
        }
        for (int i = 0; i < _negativeEffect.Length; i++)
        {
            tmpindex = selectindex[Random.Range(0, selectindex.Count)];
            selectindex.Remove(tmpindex);
            _negativeEffect[i] = _negativeEffectList[tmpindex];
            MainUIManager.instance.SettingRewardText(i * 2 + 1, _negativeEffect[i].GetInfomation());
        }
    }

    // ���� �������� ���� ���� (UI���� ��) / index -> ��ư ��ȣ
    public void RewardSelectAfter(int index)
    {
        _positiveEffect[index].OnApply();
        _negativeEffect[index].OnApply();

        Player.Instance.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        MainUIManager.instance.ShowNextRound(true);
        state = GameFlowState.WAITING_NEXTROUND;
    }

    private void LevelFail()
    {
        state = GameFlowState.LEVEL_FAIL;
    }

    public void GameReStart()
    {
        SceneManager.LoadScene(0);
    }
    public void GamEnd()
    {
        Application.Quit();
    }
}
