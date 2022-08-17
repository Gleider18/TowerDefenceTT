using System.Collections;
using System.Collections.Generic;
using Databases;
using Signals;
using UnityEngine;
using Zenject;

public class EnemySpawnerController : IInitializable
{
    private int _enemySpawnDelay = 1;
    private readonly SignalBus _signalBus;
    private readonly DiContainer _container;
    private readonly IEnemiesDatabase _enemiesDatabase;
    private readonly IWavesDatabase _wavesDatabase;
    private readonly AsyncProcessor _asyncProcessor;

    private List<WaveInfoVo> _waveInfos;
    public List<EnemyController> enemyPool;

    public EnemySpawnerController(
        SignalBus signalBus,
        DiContainer container,
        IEnemiesDatabase enemiesDatabase,
        IWavesDatabase wavesDatabase,
        AsyncProcessor asyncProcessor)
    {
        _signalBus = signalBus;
        _container = container;
        _enemiesDatabase = enemiesDatabase;
        _wavesDatabase = wavesDatabase;
        _asyncProcessor = asyncProcessor;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<SignalWaveStart>(OnSignalWaveStart);
        enemyPool = new List<EnemyController>();

        _waveInfos = _wavesDatabase.GetWaves();
        _signalBus.Fire(new SignalWaveStart(_waveInfos));
    }

    private void OnSignalWaveStart(SignalWaveStart signal)
    {
        _waveInfos = signal.WaveInfo;
        _asyncProcessor.StartCoroutine(LevelStart());
    }

    private IEnumerator LevelStart()
    {
        foreach (var _waveInfo in _waveInfos)
        {
            GameController.wave++;
            _asyncProcessor.StartCoroutine(WaveStart(_waveInfo));
            yield return new WaitForSeconds(_waveInfo.WaveTime);
        }

        GameController.isSpawnEnded = true;
    }

    private IEnumerator WaveStart(WaveInfoVo _waveInfo)
    {
        for(int i = 0; i < _waveInfo.Enemies.Count; i++)
        {
            for (int j = 0; j < _waveInfo.Enemies[i].amount; j++)
            {
                var enemyVo = _enemiesDatabase.GetEnemyById(_waveInfo.Enemies[i].id);
                var enemyClone = _container.InstantiatePrefabForComponent<EnemyController>(enemyVo.enemyPrefab);
                enemyClone.SetParameters(enemyVo.speed, enemyVo.health, enemyVo.moneyForDefeat);
                enemyPool.Add(enemyClone);
                yield return new WaitForSeconds(_enemySpawnDelay);
            }
        }
    }


    public void DefeatEnemy(EnemyController enemyController)
    {
        enemyPool.Remove(enemyController);
        if (enemyPool.Count <= 0 && GameController.isSpawnEnded)
            _asyncProcessor.StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(3);
        GameController.RestartGame();
    }

    public class AsyncProcessor : MonoBehaviour
    {
    }
}