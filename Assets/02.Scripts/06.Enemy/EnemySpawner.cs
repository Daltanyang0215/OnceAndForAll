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
    // (�� �̸�, ��ȯ ��, ��ȯ �� ������, ó�� ��ȯ������ ������) 
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
    private int _poolIndex; // Ǯ �ε���
    private int _spawnIndex; // Ǯ���� ���� �ε���

    private Vector3 _ranPos = new Vector3(0, 0, 90);
    // ��ȯ����Ʈ ������Ƽ
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

            // ������ ȸ���� ���� Ǯ����� ��ȯ ������ ũ�ų� ������
            if (_spawnIndex >= _spawners[_poolIndex].Num)
            {
                // ������ ����Ƚ�� �ʱ�ȭ �� Ǯ �ε��� �÷� ���� Ǯ�� ��ȯ�ǰ� ��
                _spawnIndex = 0;
                _poolIndex++;
                _timer = _spawners[0].SpawnDelay + _spawners[0].StartDelay;

                // Ǯ �ε����� ��ü Ǯ�� �� ���� ũ�ų� ������ Ȯ��
                if (_poolIndex >= _spawners.Count)
                {
                    // ������ ���� ����
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
