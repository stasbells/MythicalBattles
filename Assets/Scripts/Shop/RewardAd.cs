using MythicalBattles.Services.Wallet;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using YG;

namespace MythicalBattles
{
    public class RewardAd : MonoBehaviour, IPointerClickHandler
    {
        private readonly string _rewardId;

        [SerializeField] private int _moneyReward = 1000;
        
        private IWallet _wallet;

        private void Construct()
        {
            _wallet = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IWallet>();
        }

        private void Awake()
        {
            Construct();
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            YG2.RewardedAdvShow(_rewardId, () =>
            {
                _wallet.AddCoins(_moneyReward);
            });
        }
    }
}
