using Databases;
using Databases.Impls;
using DefaultNamespace;
using PathCreation;
using Towers;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameSettingsInstaller", menuName = "Installers/GameSettingsInstaller")]
    public class GameSettings : ScriptableObjectInstaller
    {
        [SerializeField] private EnemiesDatabase enemiesDatabase;
        [SerializeField] private TowersDatabase towersDatabase;
        [SerializeField] private WavesDatabase wavesDatabase;
        [SerializeField] private PathCreator pathCreator;
        [SerializeField] private LevelCreator levelCreator;
    
        public override void InstallBindings()
        {
            //Databases bind
            Container.Bind<IEnemiesDatabase>().FromInstance(enemiesDatabase);
            Container.Bind<ITowersDatabase>().FromInstance(towersDatabase);
            Container.Bind<IWavesDatabase>().FromInstance(wavesDatabase);

            Container.Bind<PathCreator>().FromInstance(pathCreator);
            Container.Bind<LevelCreator>().FromInstance(levelCreator);

            Container.InstantiatePrefab(pathCreator);
            Container.InstantiatePrefab(levelCreator);
        }
    }
}
