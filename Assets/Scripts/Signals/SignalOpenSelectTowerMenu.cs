using UnityEngine;

namespace Signals
{
    public class SignalOpenSelectTowerMenu
    {
        public Vector3 place;

        public SignalOpenSelectTowerMenu(Vector3 newPlace)
        {
            place = newPlace;
        }
    }
}