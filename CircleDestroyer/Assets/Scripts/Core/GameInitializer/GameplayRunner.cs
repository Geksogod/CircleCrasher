using Core.ActorModel;
using Core.Factory;
using UnityEngine;

namespace Core.GameInitializer
{
    public class GameplayRunner
    {
        private ActorFactoryHolder _actorFactory;
        
        public GameplayRunner(ActorFactoryHolder actorFactory)
        {
            _actorFactory = actorFactory;
            GameStart();
        }

        private void GameStart()
        {
            _actorFactory.Create(ActorType.Player,Vector2.zero);
        }
    }
}