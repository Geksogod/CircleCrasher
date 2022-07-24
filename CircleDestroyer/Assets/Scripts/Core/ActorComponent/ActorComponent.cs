using UnityEngine;

namespace Core.ActorModel
{
    public class ActorComponent : MonoBehaviour
    {
        protected Actor ComponentActor;

        public virtual void Configurate(Actor actor)
        {
            ComponentActor = actor;
        }
    }
}