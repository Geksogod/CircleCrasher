using Core.CameraModule;
using UnityEngine;
using Zenject;

namespace Core.Context
{
    public class GameCoreContext : MonoInstaller
    {
        [SerializeField] private GameCameraModule _camera;
        
        public override void InstallBindings()
        {
            Container.Bind<GameCameraModule>().FromComponentInNewPrefab(_camera).AsSingle().NonLazy();
        }
    }
}