using System;
using MythicalBattles.Shop.EquipmentShop;
using Newtonsoft.Json;
using UnityEngine;

namespace MythicalBattles.Services.Data
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class PlayerData
    {
        private const int InitMoney = 0;
        
        [JsonIgnore]private EquipmentsShopContent _shopContent;

        [SerializeField] private int _money = InitMoney;
        [SerializeField] private string _selectedWeaponID = DefaultItemsID.Weapon;
        [SerializeField] private string _selectedArmorID = DefaultItemsID.Armor;
        [SerializeField] private string _selectedHelmetID = DefaultItemsID.Helmet;
        [SerializeField] private string _selectedBootsID = DefaultItemsID.Boots;
        [SerializeField] private string _selectedNecklaceID = DefaultItemsID.Necklace;
        [SerializeField] private string _selectedRingID = DefaultItemsID.Ring;

        // public PlayerData()
        // {
        //     Money = InitMoney;
        //
        //     SelectedWeaponID = DefaultItemsID.Weapon;
        //
        //     SelectedArmorID = DefaultItemsID.Armor;
        //
        //     SelectedHelmetID = DefaultItemsID.Helmet;
        //
        //     SelectedBootsID = DefaultItemsID.Boots;
        //
        //     SelectedNecklaceID = DefaultItemsID.Necklace;
        //
        //     SelectedRingID = DefaultItemsID.Ring;
        // }

        // [JsonConstructor]
        // public PlayerData(int money, string selectedWeaponID, string selectedArmorID, string selectedHelmetID,
        //     string selectedBootsID, string selectedNecklaceID, string selectedRingID)
        // {
        //     Money = money;
        //     SelectedWeaponID = selectedWeaponID;
        //     SelectedArmorID = selectedArmorID;
        //     SelectedHelmetID = selectedHelmetID;
        //     SelectedBootsID = selectedBootsID;
        //     SelectedNecklaceID = selectedNecklaceID;
        //     SelectedRingID = selectedRingID;
        // }

        public int Money => _money;
        
        // public string SelectedWeaponID => _selectedWeaponID;
        // public string SelectedArmorID => _selectedArmorID;
        // public string SelectedHelmetID => _selectedHelmetID;
        // public string SelectedBootsID => _selectedBootsID;
        // public string SelectedNecklaceID => _selectedNecklaceID;
        // public string SelectedRingID => _selectedRingID;


        public void Initialize(EquipmentsShopContent shopContent)
        {
            _shopContent = shopContent;
        }

        public WeaponItem GetSelectedWeapon()
        {
            return _shopContent.GetItem<WeaponItem>(_selectedWeaponID);
        }

        public ArmorItem GetSelectedArmor()
        {
            return _shopContent.GetItem<ArmorItem>(_selectedArmorID);
        }

        public HelmetItem GetSelectedHelmet()
        {
            return _shopContent.GetItem<HelmetItem>(_selectedHelmetID);
        }

        public BootsItem GetSelectedBoots()
        {
            return _shopContent.GetItem<BootsItem>(_selectedBootsID);
        }

        public NecklaceItem GetSelectedNecklace()
        {
            return _shopContent.GetItem<NecklaceItem>(_selectedNecklaceID);
        }

        public RingItem GetSelectedRing()
        {
            return _shopContent.GetItem<RingItem>(_selectedRingID);
        }

        public void SelectWeapon(WeaponItem weapon)
        {
            _selectedWeaponID = weapon.ItemID;
        }

        public void SelectArmor(ArmorItem armor)
        {
            _selectedArmorID = armor.ItemID;
        }

        public void SelectHelmet(HelmetItem helmet)
        {
            _selectedHelmetID = helmet.ItemID;
        }

        public void SelectBoots(BootsItem boots)
        {
            _selectedBootsID = boots.ItemID;
        }

        public void SelectNecklace(NecklaceItem necklace)
        {
            _selectedNecklaceID = necklace.ItemID;
        }

        public void SelectRing(RingItem ring)
        {
            _selectedRingID = ring.ItemID;
        }

        public void AddMoney(int money)
        {
            if (money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));

            _money += money;
        }

        public void SpendMoney(int money)
        {
            if (money < 0 || money > _money)
                throw new ArgumentOutOfRangeException(nameof(money));

            _money -= money;
        }

        public void Reset()
        {
            _money = InitMoney;

            _selectedWeaponID = DefaultItemsID.Weapon;

            _selectedArmorID = DefaultItemsID.Armor;

            _selectedHelmetID = DefaultItemsID.Helmet;

            _selectedBootsID = DefaultItemsID.Boots;

            _selectedNecklaceID = DefaultItemsID.Necklace;

            _selectedRingID = DefaultItemsID.Ring;
        }
    }
}