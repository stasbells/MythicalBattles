using MythicalBattles.Assets._Developers.Stas.Scripts.Building.Utils;
using UnityEngine;

namespace MythicalBattles.Shop.EquipmentShop
{
    [CreateAssetMenu(fileName = "NecklaceItem", menuName = "Shop/EquipmentItems/NecklaceItem")]
    public class NecklaceItem : EquipmentItem
    {
        [SerializeField] private float _additionalDamage;

        public float AdditionalDamage => _additionalDamage;
        protected override string ItemName => Constants.Necklace;
        protected override string ImprovementType => Constants.Damage;
        protected override string DisplayValue => $"{Prefix}{_additionalDamage}";

        public override void Accept(IShopItemVisitor visitor) => visitor.Visit(this);
    }
}