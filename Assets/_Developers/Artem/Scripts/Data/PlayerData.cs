using System;
using Newtonsoft.Json;
using UnityEngine;

namespace MythicalBattles
{
    public class PlayerData
    {
        private EquipmentItem _selectedWeapon;
        private EquipmentItem _selectedArmor;
        private EquipmentItem _selectedHelmet;
        private EquipmentItem _selectedBoots;
        private EquipmentItem _selectedNecklace;
        private EquipmentItem _selectedRing;

        private int _money;
        
        public PlayerData()
        {
    
        }

        [JsonConstructor]
        public PlayerData(int money, EquipmentItem selectedWeapon, EquipmentItem selectedArmor,
            EquipmentItem selectedHelmet, EquipmentItem selectedBoots, EquipmentItem selectedNecklace,
            EquipmentItem selectedRing)
        {
            _money = money;
            _selectedWeapon = selectedWeapon;
            _selectedArmor = selectedArmor;
            _selectedHelmet = selectedHelmet;
            _selectedBoots = selectedBoots;
            _selectedNecklace = selectedNecklace;
            _selectedRing = selectedRing;
        }

        public int Money => _money;

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
