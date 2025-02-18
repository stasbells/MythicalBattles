using System;
using Newtonsoft.Json;

namespace MythicalBattles
{
    public class PlayerData
    {
        private WeaponItem _selectedWeapon;
        private ArmorItem _selectedArmor;
        private HelmetItem _selectedHelmet;
        private BootsItem _selectedBoots;
        private NecklaceItem _selectedNecklace;
        private RingItem _selectedRing;

        private int _money;
        
        public PlayerData()
        {
    
        }

        [JsonConstructor]
        public PlayerData(int money, WeaponItem selectedWeapon, ArmorItem selectedArmor,
            HelmetItem selectedHelmet, BootsItem selectedBoots, NecklaceItem selectedNecklace,
            RingItem selectedRing)
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
