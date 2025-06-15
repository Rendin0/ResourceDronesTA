using _Game.Utils.UI;
using UnityEngine;

namespace _Game.Gameplay.UI
{
    public class GameplaySceneUIView : SceneUIView<GameplaySceneUIViewModel>
    {
        [field: SerializeField] public CameraMover LeftMover { get; private set; }
        [field: SerializeField] public CameraMover RightMover { get; private set; }
    }
}