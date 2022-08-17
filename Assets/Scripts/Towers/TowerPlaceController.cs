using System;
using Signals;
using UnityEngine;
using Zenject;

namespace Towers
{
    public class TowerPlaceController : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnMouseDown()
        {
            _signalBus.Fire(new SignalOpenSelectTowerMenu(gameObject.transform.position));
        }

    }
}
