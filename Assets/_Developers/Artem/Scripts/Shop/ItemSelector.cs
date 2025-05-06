using UnityEngine;

namespace MythicalBattles
{
    public class ItemSelector : IShopItemVisitor
    {
        private IPersistentData _persistentData;

        public ItemSelector(IPersistentData persistentData) => _persistentData = persistentData;

        //public void Visit(ShopItem shopItem) => Visit((dynamic) shopItem);

        public void Visit(ShopItem shopItem)
        {
            if (shopItem is WeaponItem weapon)
                Visit(weapon);
            else if (shopItem is ArmorItem armor)
                Visit(armor);
            else if (shopItem is HelmetItem helmet)
                Visit(helmet);
            else if (shopItem is BootsItem boots)
                Visit(boots);
            else if (shopItem is NecklaceItem necklace)
                Visit(necklace);
            else if (shopItem is RingItem ring)
                Visit(ring);
            else
                Debug.LogError("Unknown ShopItem type");
        }

        public void Visit(WeaponItem weaponItem)
        {
            _persistentData.PlayerData.SelectWeapon(weaponItem);
        }

        public void Visit(ArmorItem armorItem)
        {
            _persistentData.PlayerData.SelectArmor(armorItem);
        }

        public void Visit(HelmetItem helmetItem)
        {
            _persistentData.PlayerData.SelectHelmet(helmetItem);
        }

        public void Visit(BootsItem bootsItem)
        {
            _persistentData.PlayerData.SelectBoots(bootsItem);
        }

        public void Visit(NecklaceItem necklaceItem)
        {
            _persistentData.PlayerData.SelectNecklace(necklaceItem);
        }

        public void Visit(RingItem ringItem)
        {
            _persistentData.PlayerData.SelectRing(ringItem);
        }
    }
}
