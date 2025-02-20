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
            _money = 0;

            var equipmentItems = items.ToList();
            
            SelectedWeapon = equipmentItems.OfType<WeaponItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
            
            SelectedArmor =  equipmentItems.OfType<ArmorItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
            
            SelectedHelmet =  equipmentItems.OfType<HelmetItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
            
            SelectedBoots =  equipmentItems.OfType<BootsItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
            
            SelectedNecklace =  equipmentItems.OfType<NecklaceItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
            
            SelectedRing =  equipmentItems.OfType<RingItem>()
                .FirstOrDefault(item => item.EquipmentGrade == EquipmentGrades.Simple);
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
        }

        public int Money => _money;
        public ArmorItem SelectedArmor { get; private set; }
        public WeaponItem SelectedWeapon { get; private set; }
        public HelmetItem SelectedHelmet { get; private set; }
        public BootsItem SelectedBoots { get; private set; }
        public NecklaceItem SelectedNecklace { get; private set; }
        public RingItem SelectedRing { get; private set; }


        public void SelectWeapon(WeaponItem weapon)
        {
            SelectedWeapon = weapon;
        }
        
        public void SelectArmor(ArmorItem armor)
        {
            SelectedArmor = armor;
        }
        
        public void SelectHelmet(HelmetItem helmet)
        {
            SelectedHelmet = helmet;
        }
        
        public void SelectBoots(BootsItem boots)
        {
            SelectedBoots = boots;
        }
        
        public void SelectNecklace(NecklaceItem necklace)
        {
            SelectedNecklace = necklace;
        }
        
        public void SelectRing(RingItem ring)
        {
            SelectedRing = ring;
        }

        public void AddMoney(int money)
        {
            if(money < 0)
                throw new ArgumentOutOfRangeException(nameof(money));
            
            _money += money;
        }
        
        public void SpendMoney(int money)
        {
            if(money < 0 || money > _money)
                throw new ArgumentOutOfRangeException(nameof(money));
            
            _money -= money;
        }
    }
}
