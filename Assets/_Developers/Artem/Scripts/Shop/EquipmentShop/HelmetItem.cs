using UnityEngine;

namespace MythicalBattles
{
    [CreateAssetMenu(fileName = "HelmetItem", menuName = "Shop/EquipmentItems/HelmetItem")]
    public class HelmetItem : EquipmentItem
    {
        [SerializeField] private float _additionalHealth;

        public float AdditionalHealth => _additionalHealth;
        public override string DisplayText => $"Item effect: +{_additionalHealth} Health";
        public override string TypeText => $"{EquipmentGrade} Helmet";
        
        public override void Accept(IShopItemVisitor visitor)  => visitor.Visit(this);
    }
}
