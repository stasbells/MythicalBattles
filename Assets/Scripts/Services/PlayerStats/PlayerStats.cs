using System;
using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Services.ItemSelector;
using R3;

namespace MythicalBattles.Assets.Scripts.Services.PlayerStats
{
    public class PlayerStats : IPlayerStats, IDisposable
    {
        private const int InitMaxHealth = 100;
        private const int InitDamage = 50;
        private const int InitAttackSpeed = 1;

        private PlayerData _playerData;
        private IItemSelector _itemSelector;

        public PlayerStats(IItemSelector itemSelector)
        {
            _itemSelector = itemSelector;
        }

        public ReactiveProperty<float> MaxHealth { get; } = new();
        public ReactiveProperty<float> Damage { get; } = new();
        public ReactiveProperty<float> AttackSpeed { get; } = new();

        public void UpdatePlayerData(PlayerData playerData)
        {
            if (_playerData != null)
                _itemSelector.SelectedItemChanged -= OnInventoryItemChanged;
            
            _playerData = playerData;
            
            _itemSelector.SelectedItemChanged += OnInventoryItemChanged;
            
            ResetStats();
        }
        
        public void Dispose()
        {
            _itemSelector.SelectedItemChanged -= OnInventoryItemChanged;
        }

        public void ResetStats()
        {
            MaxHealth.Value = InitMaxHealth;
            Damage.Value = InitDamage;
            AttackSpeed.Value = InitAttackSpeed;

            AcceptItemsStats();
        }

        private void OnInventoryItemChanged()
        {
            ResetStats();
        }

        private void AcceptItemsStats()
        {
            MaxHealth.Value += _playerData.GetSelectedArmor().AdditionalHealth;
            MaxHealth.Value += _playerData.GetSelectedHelmet().AdditionalHealth;
            Damage.Value += _playerData.GetSelectedWeapon().AdditionalDamage;
            Damage.Value += _playerData.GetSelectedNecklace().AdditionalDamage;
            AttackSpeed.Value += _playerData.GetSelectedBoots().AdditionalAttackSpeed;
            AttackSpeed.Value += _playerData.GetSelectedRing().AdditionalAttackSpeed;       
        }
    }
}