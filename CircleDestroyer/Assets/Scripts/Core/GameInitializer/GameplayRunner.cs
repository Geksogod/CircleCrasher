using Core.ActorModel;
using Core.Factory;
using Core.InputModule;
using UnityEngine;

namespace Core.GameInitializer
{
    public class GameplayRunner
    {
        private ActorFactoryHolder _actorFactory;
        
        public GameplayRunner(ActorFactoryHolder actorFactory,InputProcess _input)
        {
            _actorFactory = actorFactory;
            Debug.LogError(_input);
            GameStart();
        }

        private void GameStart()
        {
            _actorFactory.Create(ActorType.Player,Vector2.zero);
        }
    }
}