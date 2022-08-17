using System;
using Databases;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Towers
{
    public class GameUiController : MonoBehaviour, IInitializable
    {
        [SerializeField] private GameObject selectMenu;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI livesText;
        private Vector3 _currentTowerPlace;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<SignalOpenSelectTowerMenu>(Open);
        }

        private void Update()
        {
            moneyText.text = GameController.money.ToString();
            livesText.text = GameController.health.ToString();
            waveText.text = "Wave: " + GameController.wave;
        }

        private void Open(SignalOpenSelectTowerMenu signal)
        {
            _currentTowerPlace = signal.place;
            var offsetPlace = signal.place;
            offsetPlace.y += 3f;
            selectMenu.transform.position = offsetPlace;
            selectMenu.SetActive(true);
        }

        public void OnSelectTowerClick()
        {
            _signalBus.Fire(new SignalTowerPlace(_currentTowerPlace));
            OnClose();
        }

        public void OnClose()
        {
            selectMenu.SetActive(false);
        }
    }
}