using Core.Events;
using UnityEngine;
using Zenject;

namespace Core.ActorModel
{
    public class EnemyDestroyer : ActorComponent
    {
        [Inject] private SignalBus _signalBus;
        private void OnTriggerEnter2D(Collider2D col)
        {
            var actor = col.gameObject.GetComponent<EnemyCollider>();
            if (actor == null) return;
            
            Destroy(actor.gameObject);
            _signalBus.Fire<OnEnemyDestroySignal>();
        }
    }
}