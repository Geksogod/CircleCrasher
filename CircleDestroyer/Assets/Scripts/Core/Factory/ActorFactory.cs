using System;
using Core.ActorModel;
using UnityEngine;
using Zenject;

namespace Core.Factory
{
    public class ActorFactory : IFactory<ActorType,Vector2,Actor>
    {
        private readonly DiContainer _container;
        private readonly ActorContainer _actorContainer;

        public ActorFactory(DiContainer container,ActorContainer actorContainer)
        {
            _container = container;
            _actorContainer = actorContainer;
        }

        public Actor Create(ActorType actorType,Vector2 position)
        {
            var actorPrefab = _actorContainer.GetActorByType(actorType);
            
            if (actorPrefab == null)
            {
                throw new NullReferenceException($"Can't find actor by type - {actorType}");
            }
            
            var newActor = _container.InstantiatePrefab(actorPrefab,position, Quaternion.identity, new GameObject().transform).GetComponent<Actor>();
            return newActor;
        }
    }
    
    public class ActorFactoryHolder : PlaceholderFactory<ActorType,Vector2,Actor>
    {
        
    }
}