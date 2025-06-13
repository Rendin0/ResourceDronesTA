
using Zenject;

namespace _Game.Utils.UI
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<UIRootView>().FromComponentInNewPrefabResource("UI/UIRoot").AsSingle().NonLazy();
        }
    }
}