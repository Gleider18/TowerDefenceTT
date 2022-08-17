using DefaultNamespace;
using Signals;
using Towers;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        //Signals declaration
        Container.DeclareSignal<SignalWaveStart>();
        Container.DeclareSignal<SignalTowerPlace>();
        Container.DeclareSignal<SignalOpenSelectTowerMenu>();
        
        //Controllers bind
        Container.BindInterfacesAndSelfTo<EnemySpawnerController>().AsSingle();
        Container.BindInterfacesAndSelfTo<TowerPlacementController>().AsSingle();
        
        Container.Bind<EnemySpawnerController.AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle();
    }
    
}
