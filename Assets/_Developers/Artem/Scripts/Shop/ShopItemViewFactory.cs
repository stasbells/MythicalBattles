using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
    public class ShopItemViewFactory : ScriptableObject
    {
        [SerializeField] private ShopItemView _equipmentItemPrefab;

        public ShopItemView Get(ShopItem shopItem, Transform parent)
        {
            ShopItemView shopItemView = Instantiate(_equipmentItemPrefab, parent);
            
            shopItemView.Initialize(shopItem);

            return shopItemView;
        }
    }
}
