using System.Collections;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using MythicalBattles.Services.Data;
using MythicalBattles.Services.LevelCompletionStopwatch;
using MythicalBattles.Services.Wallet;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Levels
{
    public class LevelEndAlgorithm : MonoBehaviour
    {
        private const float MaxTimeInSecondsForBonus = 1200f;
        private const float AdditionalGoldFactor = 0.08f;
        private const float DelayBeforeShowUI = 2f;
        
        private IWallet _wallet;
        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private ILevelCompletionStopwatch _levelCompletionStopwatch;
        private GameplayUIManager _gameplayUIManager;
        private float _levelStartTime;
        private float _maxAdditionalGold;
        private float _levelPassTime;
        private int _score;
        private int _rewardMoney;

        private void Construct()
        {
            var container = SceneManager.GetActiveScene().GetSceneContainer();
            
            _wallet = container.Resolve<IWallet>();
            _dataProvider = container.Resolve<IDataProvider>();
            _persistentData = container.Resolve<IPersistentData>();
            _levelCompletionStopwatch = container.Resolve<ILevelCompletionStopwatch>();
        }

        private void Awake()
        {
            Construct();
            
            _levelCompletionStopwatch.Reset();
            _levelCompletionStopwatch.Start();
        }

        public void SetUiManager(GameplayUIManager gameplayUIManager)
        {
            _gameplayUIManager = gameplayUIManager;
        }

        public IEnumerator Run(int levelNumber, float baselevelReward, float maxScore)
        {
            _levelCompletionStopwatch.Stop();
            
            _levelPassTime = _levelCompletionStopwatch.ElapsedTime;
            
            _maxAdditionalGold = baselevelReward * AdditionalGoldFactor;

            if (_levelPassTime >= MaxTimeInSecondsForBonus)
            {
                _rewardMoney = (int)baselevelReward;
                _score = 1;
            }
            else
            {
                float timeRatio = 1 - (_levelPassTime / MaxTimeInSecondsForBonus);
                
                _rewardMoney = CalculateRewardMoney(timeRatio, baselevelReward);

                _score = CalculateScore(timeRatio, maxScore);
            }
            
            yield return new WaitForSeconds(DelayBeforeShowUI);
            
            _wallet.AddCoins(_rewardMoney);
            
            if (_persistentData.GameProgressData.TryUpdateLevelRecord(levelNumber, _score, _levelPassTime))  
            {
                _dataProvider.SaveGameProgressData();
            }

            float bestTime = _persistentData.GameProgressData.LevelsResults[levelNumber - 1].Time;

            _gameplayUIManager.OpenScreenLevelComplete(_levelPassTime, bestTime, _score, _rewardMoney);
        }

        private int CalculateRewardMoney(float timeRatio, float baselevelReward)
        {
            float reward = baselevelReward + Mathf.RoundToInt(timeRatio * _maxAdditionalGold);
            
            return (int)reward;
        }

        private int CalculateScore(float timeRatio, float maxScore)
        {
            return Mathf.RoundToInt(timeRatio * maxScore);
        }
    }
}
