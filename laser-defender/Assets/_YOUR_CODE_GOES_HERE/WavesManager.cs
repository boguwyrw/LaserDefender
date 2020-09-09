using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WavesManager : MonoBehaviour
{
    public delegate void WaveSpawned(int spawnedWaveNo);
    public event WaveSpawned E_WaveSpawned;

    public int CurrentWave { get; private set; }

    private float _maxPositionX;
    private float _minPositionX;

    [SerializeField]
    private float positionY = 4.3f;
    
    public WavesConfiguration[] wavesConfigurations;

    private void Start()
    {
        float playSpaceWidth = FindObjectOfType<Playspace>().Width;
        _maxPositionX = playSpaceWidth / 2f - 0.5f;
        _minPositionX = (playSpaceWidth / 2f - 0.5f) * -1f;
        CurrentWave = 1;
        SetupWavesAndSpawn();
    }

    public void SetupWavesAndSpawn()
    {
        StartCoroutine(CreateEnemy());

        SpawnNextWave();
    }

    public void SpawnNextWave()
    {
        E_WaveSpawned?.Invoke(CurrentWave);
    }

    private void Update()
    {
        FindingEnemies();
    }

    IEnumerator CreateEnemy()
    {
        for (int i = 0; i < wavesConfigurations[CurrentWave - 1].numberOfEnemyTypesList.Length; i++)
        {
            for (int j = 0; j < wavesConfigurations[CurrentWave - 1].numberOfEnemies[i]; j++)
            {
                Instantiate(wavesConfigurations[CurrentWave - 1].numberOfEnemyTypesList[i], new Vector3(Random.Range(_minPositionX, _maxPositionX), positionY, 0.0f), Quaternion.identity);
                yield return new WaitForSeconds(0.2f);
            }
        }
        yield break;
    }

    private void FindingEnemies()
    {
        List<GameObject> enemiesList = new List<GameObject>();
        GameObject[] enemies = FindObjectsOfType<GameObject>();
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].layer == 10)
            {
                enemiesList.Add(enemies[i]);
            }
        }

        if (enemiesList.Count == 0)
        {
            if (CurrentWave == wavesConfigurations.Length)
            {
                GameScene gameScene = FindObjectOfType<GameScene>();
                GameScore.UpdateTitle("YOU WON !");
                gameScene.EndGame();
            }
            else
            {
                CurrentWave++;
                GameScore.UpdateWaves(CurrentWave);
                SetupWavesAndSpawn();
            }
        }
    }
}
