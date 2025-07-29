using System.Globalization;
using MythicalBattles.Services.PlayerStats;
using R3;
using Reflex.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Shop
{
    public class StatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _damage;
        [SerializeField] private TMP_Text _health;
        [SerializeField] private TMP_Text _attackSpeed;

        private IPlayerStats _playerStats;
        private readonly CompositeDisposable _disposable = new ();
        
        private void Construct()
        {
            _playerStats = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPlayerStats>();
        }

        private void OnEnable()
        {
            Construct();
            
            DisplayStats();

            _playerStats.Damage.Subscribe(_ => OnStatsChanged()).AddTo(_disposable);
            
            _playerStats.MaxHealth.Subscribe(_ => OnStatsChanged()).AddTo(_disposable);
            
            _playerStats.AttackSpeed.Subscribe(_ => OnStatsChanged()).AddTo(_disposable);

        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }

        private void OnStatsChanged()
        {
            DisplayStats();
        }

        private void DisplayStats()
        {
            _damage.text = _playerStats.Damage.Value.ToString(CultureInfo.InvariantCulture);
            
            _health.text = _playerStats.MaxHealth.Value.ToString(CultureInfo.InvariantCulture);
            
            _attackSpeed.text = _playerStats.AttackSpeed.Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
