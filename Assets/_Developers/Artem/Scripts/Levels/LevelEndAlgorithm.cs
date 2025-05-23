using System;
using System.Collections;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using Reflex.Attributes;
using UnityEngine;

namespace MythicalBattles
{
    public class LevelEndAlgorithm : MonoBehaviour
    {
        private const float MaxTimeInSecondsForBonus = 1200f;
        private const float MaxPoints = 2400f;
        private const float AdditionalGoldFactor = 0.08f;
        private const float DelayBeforeShowUI = 2f;
        
        private IWallet _wallet;
        private IDataProvider _dataProvider;
        private IPersistentData _persistentData;
        private GameplayUIManager _gameplayUIManager;
        private float _levelStartTime;
        private float _maxAdditionalGold; 
        
        [Inject]
        private void Construct(IWallet wallet, IPersistentData persistentData, IDataProvider dataProvider)
        {
            _wallet = wallet;
            _dataProvider = dataProvider;
            _persistentData = persistentData;
        }

        private void Awake()
        {
            _levelStartTime = Time.time;
        }

        public void SetUiManager(GameplayUIManager gameplayUIManager)
        {
            _gameplayUIManager = gameplayUIManager;
        }

        public IEnumerator Run(int levelNumber, float baselevelReward)
        {
            float levelPassTime = Time.time - _levelStartTime;
            int score;
            int rewardMoney;

            _maxAdditionalGold = baselevelReward * AdditionalGoldFactor;

            if (levelPassTime >= MaxTimeInSecondsForBonus)
            {
                rewardMoney = (int)baselevelReward;
                score = 1;
            }
            else
            {
                float timeRatio = 1 - (levelPassTime / MaxTimeInSecondsForBonus);
                
                rewardMoney = CalculateRewardMoney(timeRatio, baselevelReward);

                score = CalculateScore(timeRatio);
            }
            
            yield return new WaitForSeconds(DelayBeforeShowUI);

            if (_persistentData.GameProgressData.TryUpdateLevelRecord(levelNumber, score, levelPassTime))  
            {
                _dataProvider.SaveGameProgressData();
            }

            float bestTime = _persistentData.GameProgressData.LevelsResults[levelNumber].Time;

            if (TryGetRewardMoney(levelNumber, rewardMoney) == false)
                rewardMoney = 0;
            
            _gameplayUIManager.OpenScreenLevelComplete(levelPassTime, bestTime, score, rewardMoney);
        }

        private int CalculateRewardMoney(float timeRatio, float baselevelReward)
        {
            return (int)(baselevelReward + Mathf.RoundToInt(timeRatio * _maxAdditionalGold));
        }

        private int CalculateScore(float timeRatio)
        {
            return Mathf.RoundToInt(timeRatio * MaxPoints);
        }

        private bool TryGetRewardMoney(int levelNumber, int rewardMoney)
        {
            if (Mathf.Approximately(_persistentData.GameProgressData.GetLevelRecordPoints(levelNumber), 0f) == false)
                return false;
            else
            {
                _wallet.AddCoins((int)rewardMoney);
                
                return true;
            }
        }
    }
}
