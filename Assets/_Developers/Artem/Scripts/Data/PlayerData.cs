using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace MythicalBattles
{
    public class PlayerData
    {
        private int _money;

        public PlayerData(IEnumerable<EquipmentItem> items)
        {
            _money = 10000;

            var equipmentItems = items.ToList();

            SelectedWeapon = equipmentItems.OfType<WeaponItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);

            SelectedArmor = equipmentItems.OfType<ArmorItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);

            SelectedHelmet = equipmentItems.OfType<HelmetItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);

            SelectedBoots = equipmentItems.OfType<BootsItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);

            SelectedNecklace = equipmentItems.OfType<NecklaceItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);

            SelectedRing = equipmentItems.OfType<RingItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
            
            List<EquipmentItem> selectedItems = new List<EquipmentItem>
                {SelectedWeapon, SelectedArmor, SelectedHelmet, SelectedBoots, SelectedNecklace, SelectedRing};

            foreach (EquipmentItem item in selectedItems)
                item.ApplyStats();
        }

        [JsonConstructor]
        public PlayerData(int money, WeaponItem selectedSelectedWeapon, ArmorItem selectedArmor,
            HelmetItem selectedSelectedHelmet, BootsItem selectedSelectedBoots, NecklaceItem selectedNecklace,
            RingItem selectedSelectedRing)
        {
            _money = money;
            SelectedWeapon = selectedSelectedWeapon;
            SelectedArmor = selectedArmor;
            SelectedHelmet = selectedSelectedHelmet;
            SelectedBoots = selectedSelectedBoots;
            SelectedNecklace = selectedNecklace;
            SelectedRing = selectedSelectedRing;

            List<EquipmentItem> items = new List<EquipmentItem>
                {SelectedWeapon, SelectedArmor, SelectedHelmet, SelectedBoots, SelectedNecklace, SelectedRing};

            foreach (EquipmentItem item in items)
                item.ApplyStats();
        }

        public int Money => _money;
        public ArmorItem SelectedArmor { get; private set; }
        public WeaponItem SelectedWeapon { get; private set; }
        public HelmetItem SelectedHelmet { get; private set; }
        public BootsItem SelectedBoots { get; private set; }
        public NecklaceItem SelectedNecklace { get; private set; }
        public RingItem SelectedRing { get; private set; }

        public event Action SelectedItemChanged;

        public void SelectWeapon(WeaponItem weapon)
        {
            ChangeSelectedItem(SelectedWeapon, weapon);
        }

        public void SelectArmor(ArmorItem armor)
        {
            ChangeSelectedItem(SelectedArmor, armor);
        }

        public void SelectHelmet(HelmetItem helmet)
        {
            ChangeSelectedItem(SelectedHelmet, helmet);
        }

        public void SelectBoots(BootsItem boots)
        {
            ChangeSelectedItem(SelectedBoots, boots);
        }

        public void SelectNecklace(NecklaceItem necklace)
        {
            ChangeSelectedItem(SelectedNecklace, necklace);
        }

        public void SelectRing(RingItem ring)
        {
            ChangeSelectedItem(SelectedRing, ring);
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

        private void ChangeSelectedItem(EquipmentItem currentItem, EquipmentItem newItem)
        {
            currentItem.CancelStats();

            currentItem = newItem;
            
            currentItem.ApplyStats();
            
            SelectedItemChanged?.Invoke();
        }
    }
}