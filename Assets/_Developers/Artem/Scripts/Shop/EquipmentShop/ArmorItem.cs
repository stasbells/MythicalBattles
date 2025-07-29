using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "ArmorItem", menuName = "Shop/EquipmentItems/ArmorItem")]
    public class ArmorItem : EquipmentItem
    {
        [SerializeField] private float _additionalHealth;
        
        public float AdditionalHealth => _additionalHealth;
        protected override string ItemName => Constants.Armor;
        protected override string ImprovementType => Constants.Health;
        protected override string DisplayValue => $"{Prefix}{_additionalHealth}";

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);
    }
}