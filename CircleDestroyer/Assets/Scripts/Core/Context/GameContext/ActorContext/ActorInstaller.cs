using Core.ActorModel;
using Core.Events;
using Core.Factory;
using Core.InputModule;
using Core.Spawner;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ActorInstaller", menuName = "Installers/ActorInstaller")]
public class ActorInstaller : ScriptableObjectInstaller<ActorInstaller>
{
    //Can move to attribute
    private const string ActorContainerPath = "Containers/Actor/ActorContainer";
        
    public override void InstallBindings()
    {
        Container.Bind<ActorContainer>().FromResource(ActorContainerPath).AsSingle();
        Container.Bind<InputOnActor>().FromNew().AsSingle().NonLazy();
        Container.Bind<EnemySpawner>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindFactory<ActorType,Vector2,Transform,Actor,ActorFactoryHolder>().FromFactory<ActorFactory>();
        Container.DeclareSignal<OnEnemyDestroySignal>();
    }
}