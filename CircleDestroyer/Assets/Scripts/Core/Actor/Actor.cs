using System.Collections.Generic;
using UnityEngine;

namespace Core.ActorModel
{
    public class Actor : MonoBehaviour , IActorTouch
    {
        private ActorComponent[] _actorComponents;
        public IEnumerable<ActorComponent> ActorComponents => _actorComponents;

        private void Awake()
        {
            _actorComponents = gameObject.GetComponentsInChildren<ActorComponent>();
        }

        private void Start()
        {
            foreach (var actorComponent in _actorComponents)
            {
                actorComponent.Configurate(this);
            }
        }

        public void OnTouch()
        {
            foreach (var actorComponent in _actorComponents)
            {
                if (actorComponent is IActorComponentTouchHandler touchHandler)
                {
                    touchHandler.OnActorTouch();
                }
            }
        }
    }
}