using System.Collections;
using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Services.LevelCompletionStopwatch;
using MythicalBattles.Assets.Scripts.Services.Wallet;
using MythicalBattles.Assets.Scripts.UI.View.ScreenGameplay;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MythicalBattles.Assets.Scripts.Levels
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
            _levelPassTime = GetLevelPassTime();

            _score = GetScore(maxScore);

            _rewardMoney = GetRewardMoney(baselevelReward);

            yield return new WaitForSeconds(DelayBeforeShowUI);

            float bestTime = GetBestTime(levelNumber);

            _gameplayUIManager.OpenScreenLevelComplete(_levelPassTime, bestTime, _score, _rewardMoney);
        }

        private float GetLevelPassTime()
        {
            _levelCompletionStopwatch.Stop();
            
            return _levelCompletionStopwatch.ElapsedTime;
        }

        private int GetScore(float maxScore)
        {
            if (_levelPassTime >= MaxTimeInSecondsForBonus)
            {
                return 1;
            }

            float timeRatio = 1 - _levelPassTime / MaxTimeInSecondsForBonus;

            return CalculateScore(timeRatio, maxScore);
        }

        private int GetRewardMoney(float baselevelReward)
        {
            _maxAdditionalGold = baselevelReward * AdditionalGoldFactor;

            int rewardMoney;

            if (_levelPassTime >= MaxTimeInSecondsForBonus)
            {
                rewardMoney = (int)baselevelReward;
            }
            else
            {
                float timeRatio = 1 - _levelPassTime / MaxTimeInSecondsForBonus;
                
                rewardMoney = CalculateRewardMoney(timeRatio, baselevelReward);
            }
            
            _wallet.AddCoins(rewardMoney);

            return rewardMoney;
        }

        private float GetBestTime(int levelNumber)
        {
            if (_persistentData.GameProgressData.
                TryUpdateLevelRecord(levelNumber, _score, _levelPassTime))  
            {
                _dataProvider.SaveGameProgressData();
            }

            return _persistentData.GameProgressData.LevelsResults[levelNumber - 1].Time;
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
