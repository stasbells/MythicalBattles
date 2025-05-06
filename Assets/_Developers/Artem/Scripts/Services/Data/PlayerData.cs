using Newtonsoft.Json;
using System;
using UnityEngine;

namespace MythicalBattles
{
    [Serializable]
    public class PlayerData
    {
        private const int InitMoney = 100000;
        
        [JsonIgnore] private EquipmentsShopContent _shopContent;
        
        public PlayerData()
        {
            Money = InitMoney;

            SelectedWeaponID = DefaultItemsID.Weapon;

            SelectedArmorID = DefaultItemsID.Armor;

            SelectedHelmetID = DefaultItemsID.Helmet;

            SelectedBootsID = DefaultItemsID.Boots;

            SelectedNecklaceID = DefaultItemsID.Necklace;

            SelectedRingID = DefaultItemsID.Ring;
        }

        [JsonConstructor]
        public PlayerData(int money, string selectedWeaponID, string selectedArmorID, string selectedHelmetID,
            string selectedBootsID, string selectedNecklaceID, string selectedRingID)
        {
            Money = money;
            SelectedWeaponID = selectedWeaponID;
            SelectedArmorID = selectedArmorID;
            SelectedHelmetID = selectedHelmetID;
            SelectedBootsID = selectedBootsID;
            SelectedNecklaceID = selectedNecklaceID;
            SelectedRingID = selectedRingID;
        }

        public int Money { get; private set; }
        public string SelectedWeaponID { get; private set; }
        public string SelectedArmorID { get; private set; }
        public string SelectedHelmetID { get; private set; }
        public string SelectedBootsID { get; private set; }
        public string SelectedNecklaceID { get; private set; }
        public string SelectedRingID { get; private set; }

        public event Action SelectedItemChanged;

        public void Initialize(EquipmentsShopContent shopContent)
        {
            _shopContent = shopContent;
        }

        public WeaponItem GetSelectedWeapon()
        {
            return _shopContent.GetItem<WeaponItem>(SelectedWeaponID);
        }

        public ArmorItem GetSelectedArmor()
        {
            return _shopContent.GetItem<ArmorItem>(SelectedArmorID);
        }

        public HelmetItem GetSelectedHelmet()
        {
            return _shopContent.GetItem<HelmetItem>(SelectedHelmetID);
        }

        public BootsItem GetSelectedBoots()
        {
            return _shopContent.GetItem<BootsItem>(SelectedBootsID);
        }

        public NecklaceItem GetSelectedNecklace()
        {
            return _shopContent.GetItem<NecklaceItem>(SelectedNecklaceID);
        }

        public RingItem GetSelectedRing()
        {
            return _shopContent.GetItem<RingItem>(SelectedRingID);
        }

        public void SelectWeapon(WeaponItem weapon)
        {
            SelectedWeaponID = weapon.ItemID;
            SelectedItemChanged?.Invoke();
        }

        public void SelectArmor(ArmorItem armor)
        {
            SelectedArmorID = armor.ItemID;
            SelectedItemChanged?.Invoke();
        }

        public void SelectHelmet(HelmetItem helmet)
        {
            SelectedHelmetID = helmet.ItemID;
            SelectedItemChanged?.Invoke();
        }

        public void SelectBoots(BootsItem boots)
        {
            SelectedBootsID = boots.ItemID;
            SelectedItemChanged?.Invoke();
        }

        public void SelectNecklace(NecklaceItem necklace)
        {
            SelectedNecklaceID = necklace.ItemID;
            SelectedItemChanged?.Invoke();
        }

        public void SelectRing(RingItem ring)
        {
            SelectedRingID = ring.ItemID;
            SelectedItemChanged?.Invoke();
        }

        public void AddMoney(int money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            Money += money;
        }

        public void SpendMoney(int money)
        {
            if (money < 0 || money > Money)
                throw new ArgumentOutOfRangeException(nameof(money));

            Money -= money;
        }

        public void Reset()
        {
            Money = InitMoney;

            SelectedWeaponID = DefaultItemsID.Weapon;

            SelectedArmorID = DefaultItemsID.Armor;

            SelectedHelmetID = DefaultItemsID.Helmet;

            SelectedBootsID = DefaultItemsID.Boots;

            SelectedNecklaceID = DefaultItemsID.Necklace;

            SelectedRingID = DefaultItemsID.Ring;
            
            SelectedItemChanged?.Invoke();
        }
    }
}