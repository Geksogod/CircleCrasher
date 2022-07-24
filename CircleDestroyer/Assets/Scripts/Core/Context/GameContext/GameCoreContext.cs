using Core.CameraModule;
using Core.GameInitializer;
using Core.InputModule;
using UnityEngine;
using Zenject;

namespace Core.Context
{
    public class GameCoreContext : MonoInstaller
    {
        [SerializeField] private GameCameraModule _camera;
        [SerializeField] private InputProcess _inputProcess;
        
        public override void InstallBindings()
        {
            Container.Bind<GameCameraModule>().FromComponentInNewPrefab(_camera).AsSingle().NonLazy();
            Container.Bind<GameplayRunner>().FromNew().AsSingle().NonLazy();
            Container.Bind<InputProcess>().FromComponentInNewPrefab(_inputProcess).AsSingle().NonLazy();
        }
    }
}