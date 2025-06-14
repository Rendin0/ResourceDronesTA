using UnityEngine;

namespace _Game.Utils.UI
{
    public interface IWindowView
    {
        Transform Transform { get; }

        void Bind(WindowViewModel viewModel);
        void Close();

    }
}