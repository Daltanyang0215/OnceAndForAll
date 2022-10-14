using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        LEVEL_SUCCESS,
        LEVEL_FAIL,
        WAITING_USER
    }

    private GameFlowState state;

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

    private void Update()
    {
        switch (state)
        {
            case GameFlowState.IDLE:
                {
                    MainUIManager.instance.OnPlayDataUIInit();

                    ObjectPool.Instance.InstantiateAllPoolElement();
                    //state = GameFlowState.WAITING_START;
                    EnemySpawner.instance.SpawnPoolAdd("TestMonster", 10, 3f, 1f);
                    EnemySpawner.instance.SpawnPoolAdd("TestMonster", 5, 0.1f, 5f);

                    Player.Instance.enabled = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    state = GameFlowState.ROUND_START;
                }
                break;
            case GameFlowState.WAITING_START:
                {

                }
                break;
            case GameFlowState.LEVEL_START:
                {

                }
                break;
            case GameFlowState.ROUND_START:
                {
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
                    Debug.Log("라운드 종료");
                }
                break;
            case GameFlowState.LEVEL_SUCCESS:
                {

                }
                break;
            case GameFlowState.LEVEL_FAIL:
                {

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

    public void LevelEndCheck()
    {
        if (EnemySpawner.instance.isSpawning == false
            && EnemySpawner.instance.transform.childCount == 0)
        {
            state = GameFlowState.ROUND_END;
        }
    }

    private void LevelFail()
    {
        state = GameFlowState.LEVEL_FAIL;
    }
}
