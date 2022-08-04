using System;
using Core.Events;
using Core.UI.Counters;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [Inject] private SignalBus _signalBus;
        [SerializeField]private Counter[] _counters;

        private void Start()
        {
            _signalBus.Subscribe<PlayerDataUpdateSignal>(OnDataUpdate);
        }

        private void OnDataUpdate()
        {
            foreach (var counter in _counters)
            {
                counter.UpdateValue();
            }
        }
    }
}