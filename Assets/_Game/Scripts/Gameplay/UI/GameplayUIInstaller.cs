using UnityEngine;
using Zenject;

namespace _Game.Gameplay.UI
{
    public class GameplayUIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameplaySceneUIViewModel>().AsSingle();
            Container.Bind<GameplaySceneUIView>().FromComponentInNewPrefabResource("UI/GameplaySceneUI").AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameplayUIManager>().AsSingle().NonLazy();
        }

    }
}