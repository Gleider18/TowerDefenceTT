using Databases;
using Signals;
using Towers;
using Zenject;

namespace DefaultNamespace
{
    public class TowerPlacementController : IInitializable
    {
        private SignalBus _signalBus;
        private DiContainer _container;
        private ITowersDatabase _towersDatabase;

        public TowerPlacementController(
            SignalBus signalBus,
            DiContainer container,
            ITowersDatabase towersDatabase)
        {
            _signalBus = signalBus;
            _container = container;
            _towersDatabase = towersDatabase;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SignalTowerPlace>(OnTowerPLace);
        }

        private void OnTowerPLace(SignalTowerPlace signal)
        {
            var towerVo = _towersDatabase.GetTowerById(0);
            if (!GameController.SpendMoney(towerVo.price))
                return;
            var tower = _container.InstantiatePrefabForComponent<TowerController>(towerVo.towerPrefab);
            tower.SetParameters(towerVo.damage, towerVo.range, towerVo.attackCooldown);
            signal.place.z = 0;
            signal.place.y += 1.7f;
            tower.gameObject.transform.position = signal.place;
        }
    }
}