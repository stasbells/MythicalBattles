using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "WeaponItem", menuName = "Shop/EquipmentItems/WeaponItem")]
    public class WeaponItem : EquipmentItem
    {
        [SerializeField] private float _additionalDamage;

        public float AdditionalDamage => _additionalDamage;
        protected override string ItemName => Constants.Bow;
        protected override string ImprovementType => Constants.Damage;
        protected override string DisplayValue => $"{Prefix}{_additionalDamage}";

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);
    }   
}