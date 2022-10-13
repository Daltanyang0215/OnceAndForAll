using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private Transform _target;

    public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }
    // (적 이름, 소환 수, 소환 간 딜레이, 처음 소환까지의 딜레이) 
    struct SpawnElement
    {
        public string Name;
        public int Num;
        public float SpawnDelay;
        public float StartDelay;

        public SpawnElement(string name, int num, float spwanDelay, float startDelay)
        {
            Name = name;
            Num = num;
            SpawnDelay = spwanDelay;
            StartDelay = startDelay;
        }
    }

    [SerializeField] private List<SpawnElement> _spawners = new List<SpawnElement>();

    private bool _isSpawning = false;

    private float _timer;
    private int _poolIndex; // 풀 인덱스
    private int _spawnIndex; // 풀내에 스폰 인덱스

    private Vector3 _ranPos = new Vector3(0, 0, 90);
    // 소환포인트 프로퍼티
    private Vector3 RanPos
    {
        get
        {
            _ranPos.x = Random.Range(-17.5f, 17.5f);
            return _ranPos;
        }
    }

    public void SpawnPoolAdd(string EnemyName, int SpawnCount, float spwanDelay, float startDelay) => _spawners.Add(new SpawnElement(EnemyName, SpawnCount, spwanDelay, startDelay));

    public void SpawnStart()
    {
        _poolIndex = 0;
        _spawnIndex = 0;
        _timer = _spawners[0].SpawnDelay + _spawners[0].StartDelay;
        _isSpawning = true;
    }

    public void SpawnEnd()
    {
        _isSpawning = false;
    }


    private void Update()
    {
        if (!_isSpawning) return;

        if (_timer < 0)
        {
            ObjectPool.Instance.Spawn(_spawners[_poolIndex].Name, RanPos).GetComponent<Enemy>().target = _target;
            _spawnIndex++;

            // 스폰한 회수가 현재 풀요소의 소환 수보다 크거나 같은지
            if (_spawnIndex >= _spawners[_poolIndex].Num)
            {
                // 같은면 스폰횟수 초기화 및 풀 인덱스 늘려 다음 풀이 소환되게 함
                _spawnIndex = 0;
                _poolIndex++;
                _timer = _spawners[0].SpawnDelay + _spawners[0].StartDelay;

                // 풀 인덱스가 전체 풀의 수 보다 크거나 같은지 확인
                if (_poolIndex >= _spawners.Count)
                {
                    // 같으면 스폰 종료
                    SpawnEnd();
                }
            }
            else
            {
                _timer = _spawners[_poolIndex].SpawnDelay;
            }
        }
        _timer -= Time.deltaTime;
    }
}
