using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MythicalBattles
{
    public class ShopCategoryButton : MonoBehaviour
    {
        public event Action Clicked;

        [SerializeField] private Button _button;

        private void OnEnable() => _button.onClick.AddListener(OnClick);
        private void OnDisable() => _button.onClick.RemoveListener(OnClick);
        private void OnClick() => Clicked?.Invoke();
    }
}
