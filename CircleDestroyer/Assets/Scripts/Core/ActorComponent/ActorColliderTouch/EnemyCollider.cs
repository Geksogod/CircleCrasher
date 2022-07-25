using System;
using Core.Events;
using UnityEngine;
using Zenject;

namespace Core.ActorModel
{
    [RequireComponent(typeof(Collider2D))]
    public class EnemyCollider : ActorComponent
    {
        [Inject] private SignalBus _signalBus;
        private void OnDestroy()
        {
            _signalBus.Fire<OnEnemyDestroySignal>();
        }
    }
}