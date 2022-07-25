using System;
using System.Collections.Generic;
using Core.ActorModel;
using Core.CameraModule;
using Core.Events;
using Core.Factory;
using Core.GameInitializer;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Core.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private ActorFactoryHolder _actorFactory;
        [Inject] private GameCameraModule _cameraModule;
        [Inject] private GameplayRunner _runner;
        [Inject] private SignalBus _signalBus;
        
        private const int MaxEnemy = 2;
        private const float MinSpawnTime = 1f;
        private const float MaxSpawnTime = 4f;
        
        private int _currentEnemyCount;
        private float _currentEnemySpawnTimer;

        private void Start()
        {
            _signalBus.Subscribe<OnEnemyDestroySignal>(OnEnemyDestroy);
        }

        public void Update()
        {
            if (_currentEnemyCount >= MaxEnemy)
            {
                return;
            }
            _currentEnemySpawnTimer -= Time.deltaTime;
            
            if (!(_currentEnemySpawnTimer <= 0)) return;
            
            Spawn();
            _currentEnemySpawnTimer = Random.Range(MinSpawnTime,MaxSpawnTime);
        }

        private void Spawn()
        {
            _actorFactory.Create(ActorType.Enemy, GetNextPos(), _runner.ActorContainer);
            _currentEnemyCount++;
        }

        private Vector2 GetNextPos()
        {
            var screenX = Random.Range(100f, _cameraModule.Camera.pixelWidth - 100f);
            var screenY = Random.Range(100f, _cameraModule.Camera.pixelHeight- 100f);
            return _cameraModule.Camera.ScreenToWorldPoint(new Vector2(screenX, screenY));
        }

        private void OnEnemyDestroy()
        {
            _currentEnemyCount--;
        }
    }
}