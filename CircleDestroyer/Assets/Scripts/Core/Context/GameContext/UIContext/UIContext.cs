using Core.UI;
using Core.UI.Counters;
using Zenject;

namespace Core.Context
{
    public class UIContext : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplayUI>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PointCounter>().FromComponentInHierarchy().AsSingle();
        }
    }
}