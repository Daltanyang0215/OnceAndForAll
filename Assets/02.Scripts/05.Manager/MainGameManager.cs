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

            return instance; }
    }

    [SerializeField] private int _money;
    public int Money
    {
        get { return _money; }
        set { _money = value; }
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
        LEVEL_SUCCESS,
        LEVEL_FAIL,
        WAITING_USER
    }


    private GameFlowState state;

    private void Update()
    {
        switch (state)
        {
            case GameFlowState.IDLE:
                {
                    ObjectPool.Instance.InstantiateAllPoolElement();
                    //state = GameFlowState.WAITING_START;
                    EnemySpawner.instance.SpawnPoolAdd("TestMonster",10,3f,1f);
                    EnemySpawner.instance.SpawnPoolAdd("TestMonster",5,0.1f,5f);
                    state = GameFlowState.ENEMYSPAWN;
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
}
