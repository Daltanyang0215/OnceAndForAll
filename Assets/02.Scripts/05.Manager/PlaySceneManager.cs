using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneManager : MonoBehaviour
{

    public void ResetScene()
    {
        MainGameManager.Instance.LevelReset();
        SceneManager.LoadScene(1);
    }

    public void QuitScene()
    {
        MainGameManager.Instance.LevelReset();
        MainGameManager.Instance.isloabby = true;
        SceneManager.LoadScene(0);
    }
}
