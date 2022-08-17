using System.Collections.Generic;
using Databases;
using Towers;
using UnityEngine;
using Zenject;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> towerPlaces;
    [SerializeField] private GameUiController gameUiController;
    private DiContainer _container;
    private ITowersDatabase _towersDatabase;

    [Inject]
    public void Construct(
        DiContainer container,
        ITowersDatabase towersDatabase)
    {
        _container = container;
        _towersDatabase = towersDatabase;
    }

    private void Start()
    {
        var controller = _container.InstantiatePrefabForComponent<GameUiController>(gameUiController);
        controller.Initialize();
        _container.BindInterfacesAndSelfTo<GameUiController>().FromInstance(controller);
        foreach (var towerPlace in towerPlaces)
        {
            var newTowerPlace = _container.InstantiatePrefabForComponent<TowerPlaceController>(_towersDatabase.GetTowerPlace());
            newTowerPlace.gameObject.transform.position = towerPlace.transform.position;
        }
    }
}
