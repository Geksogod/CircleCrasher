using Core.ActorModel;
using UnityEngine;
using Zenject;

namespace Core.Factory
{
    public class ActorFactory : IFactory<ActorType,Vector2,Transform,Actor>
    {
        private readonly DiContainer _container;
        private readonly ActorContainer _actorContainer;

        public ActorFactory(DiContainer container,ActorContainer actorContainer)
        {
            _container = container;
            _actorContainer = actorContainer;
        }

        public Actor Create(ActorType actorType,Vector2 position,Transform parent)
        {
            var actorPrefab = _actorContainer.GetActorByType(actorType);
            
            var newActor = _container.InstantiatePrefab(actorPrefab,position, Quaternion.identity, parent)
                .GetComponent<Actor>();
            newActor.Configurate(actorType);
            return newActor;
        }
    }
    
    public class ActorFactoryHolder : PlaceholderFactory<ActorType,Vector2,Transform,Actor>
    {
        
    }
}