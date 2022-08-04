using System;
using Core.ActorModel;
using Core.Factory;
using Core.SaveLoad;
using UnityEngine;
using Zenject;

namespace Core.GameInitializer
{
    public class GameplayRunner : MonoBehaviour
    {
        [Inject]private ActorFactoryHolder _actorFactory;
        [Inject]private SaveLoadModel _saveLoadModel;
        
        //TODO: Create LevelContainer.cs and move
        [SerializeField] private Transform _actorContainer;
        public Transform ActorContainer => _actorContainer;
        
        private void Start()
        {
            GameStart();
        }

        private void GameStart()
        {
            _actorFactory.Create(ActorType.Player,Vector2.zero,_actorContainer);
            _saveLoadModel.Load();
            _saveLoadModel.Save();
        }

        private void OnDestroy()
        {
            _saveLoadModel.Save();
        }
    }
}