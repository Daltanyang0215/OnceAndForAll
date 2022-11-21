using System;
using System.Reflection;
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
        public string Buff;
        public SpawnElement(string name, int num, float spwanDelay, float startDelay, string buff)
        {
            Name = name;
            Num = num;
            SpawnDelay = spwanDelay;
            StartDelay = startDelay;
            Buff = buff;
        }
    }

    [SerializeField] private List<SpawnElement> _spawners = new List<SpawnElement>();

    private bool _isSpawning = false;
    public bool isSpawning => _isSpawning;

    private float _timer;
    private int _poolIndex; // Ǯ �ε���
    private int _spawnIndex; // Ǯ���� ���� �ε���

    private Vector3 _ranPos = new Vector3(0, 0, 205);
    // ��ȯ����Ʈ ������Ƽ
    private Vector3 RanPos
    {
        get
        {
            _ranPos.x = UnityEngine.Random.Range(-62.5f, 62.5f);
            return _ranPos;
        }
    }

    public void SpawnPoolAdd(string EnemyName, int SpawnCount, float spwanDelay, float startDelay, string buff = "") => _spawners.Add(new SpawnElement(EnemyName, SpawnCount, spwanDelay, startDelay, buff));

    public void SpawnStart()
    {
        _poolIndex = 0;
        _spawnIndex = 0;
        _timer = _spawners[0].SpawnDelay + _spawners[0].StartDelay;
        _isSpawning = true;
        // ���� ���۽� ��ȯ�� ���� ���� Ȯ�� ��
        foreach (var enemypool in _spawners)
        {
            MainGameManager.Instance.currentEnemyCount += enemypool.Num;
        }
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
            Enemy spwanEnemy = ObjectPool.Instance.Spawn(_spawners[_poolIndex].Name, RanPos, transform).GetComponent<Enemy>();
            spwanEnemy.target = _target;

            if (string.IsNullOrEmpty(_spawners[_poolIndex].Buff) == false
                && _spawners[_poolIndex].Buff != "None")
            {
                Type addbuff = Type.GetType(_spawners[_poolIndex].Buff);
                if (addbuff != null)
                {
                    ConstructorInfo constructorInfo = addbuff.GetConstructor(new Type[] { typeof(Enemy) });
                    EnemyBuffBase buff = constructorInfo.Invoke(new object[] { spwanEnemy }) as EnemyBuffBase;
                    spwanEnemy.AddBuff(buff);
                }
                else
                {
                    Debug.LogWarning("�߸��� ������ ��� �õ� �Ǿ����ϴ�");
                }
            }

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
