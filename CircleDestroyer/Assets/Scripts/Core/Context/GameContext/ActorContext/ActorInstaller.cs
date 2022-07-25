using Core.ActorModel;
using Core.Factory;
using Core.InputModule;
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
        Container.BindFactory<ActorType,Vector2,Actor, ActorFactoryHolder>().FromFactory<ActorFactory>();
    }
}