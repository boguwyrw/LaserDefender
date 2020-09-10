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
        StartCoroutine(CoR_CreateEnemy());
        
        SpawnNextWave();
    }

    public void SpawnNextWave()
    {
        E_WaveSpawned?.Invoke(CurrentWave);
    }

    private void Update()
    {
        EnemyWavesSpawningMechanism();
    }

    private IEnumerator CoR_CreateEnemy()
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

    private void EnemyWavesSpawningMechanism()
    {
        if (GameObject.FindGameObjectWithTag("Enemies") == null)
        {
            if (CurrentWave == wavesConfigurations.Length)
            {
                YouWonEndGame();
            }
            else
            {
                NextWaveMechanism();
            }
        }
    }

    private void YouWonEndGame()
    {
        GameScene gameScene = FindObjectOfType<GameScene>();
        string successTitle = "YOU WON !";
        GameScore.UpdateTitle(successTitle);
        gameScene.EndGame();
    }

    private void NextWaveMechanism()
    {
        CurrentWave++;
        GameScore.UpdateWaves(CurrentWave);
        SetupWavesAndSpawn();
    }
}
