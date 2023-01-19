using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private LobbyEnemy[] _enemies;
    [SerializeField] private float _spawntime;
    private float _timer;


    private void Update()
    {
        if (_timer < 0)
        {
            Instantiate(_enemies[Random.Range(0, _enemies.Length)], new Vector3(Random.Range(-40, 40), 0, 220f), Quaternion.Euler(0f, -180f, 0f));
            _timer = _spawntime;
        }
        _timer -= Time.deltaTime;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene(1);
        MainGameManager.Instance.isloabby = false;
    }
    public void GameEnd()
    {
        Application.Quit();
    }

}
