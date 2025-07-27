using System;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles.Shop
{
    public class ShopTestButton : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private Button _button;

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);
        private void OnClick() => Clicked?.Invoke();
    }
}
