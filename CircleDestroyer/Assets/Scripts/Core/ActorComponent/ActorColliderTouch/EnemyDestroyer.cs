using UnityEngine;

namespace Core.ActorModel
{
    public class EnemyDestroyer : ActorComponent
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            var actor = col.gameObject.GetComponentInParent<Actor>();
            if (actor == null) return;
            
            if (actor.ActorType == ActorType.Enemy)
            {
                Destroy(actor.gameObject);
            }
        }
    }
}