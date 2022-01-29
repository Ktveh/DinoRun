using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private SpawnElement[] _spawnElements;
    [SerializeField] private int _chanceOfVoid;
    [SerializeField] private float _secondBetweenSpawn;

    private int _coutElenents;
    private float _elapsedTime;
    private int _countElements;

    private void Start()
    {
        _coutElenents = 0;
        _elapsedTime = 0;
        foreach (SpawnElement spawnElement in _spawnElements)
        {
            for(int i = 0; i < spawnElement.Chance; i++)
            {
                Initialize(spawnElement.Element);
                _coutElenents++;
            }
        }

        _countElements = _coutElenents + _chanceOfVoid;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _secondBetweenSpawn)
        {
            _elapsedTime = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        int randomNumber = Random.Range(0, _countElements);
        if (randomNumber > _chanceOfVoid)
        {
            if (TryGetObject(out GameObject gameObject))
            {
                Vector3 spawnPosition = gameObject.transform.position;
                spawnPosition.z = 100;
                gameObject.transform.position = spawnPosition;
                gameObject.SetActive(true);

                if (gameObject.TryGetComponent<Block>(out Block block))
                    block.Reset();
            }
        }
    }
}

[System.Serializable]
public class SpawnElement
{
    public GameObject Element;
    public int Chance;
}
