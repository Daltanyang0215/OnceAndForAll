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

                    EnemySpawner.instance.SpawnPoolAdd("기본몬스터", 5, 0.2f, 0.5f);
                    // 강화 적용
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
                    Debug.Log("라운드 종료");

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
                    Debug.Log("게임 종료. 미션 성공");
                }
                break;
            case GameFlowState.LEVEL_FAIL:
                {
                    Debug.Log("게임 종료. 미션 실패");
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

    // 라운드가 종료되었는지 확인
    public void RoundEndCheck()
    {
        // 적 소환이 종료 되었으며, 현재 남은 몬스터가 없을때 라운드 종료
        if (EnemySpawner.instance.isSpawning == false
            && EnemySpawner.instance.transform.childCount == 0
            && state == GameFlowState.WAITING_ENDROUND)
        {
            state = GameFlowState.ROUND_END;
        }
    }


    // 라운드 종료 후 보상 선택 선정
    private void SelectEffect()
    {
        // 긍정효과 선택
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

        // 부정효과 선택
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

    // 보상 선택이후 진행 사항 (UI에서 콜) / index -> 버튼 번호
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
