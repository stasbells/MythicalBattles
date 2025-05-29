using System.Collections;
using MythicalBattles.Assets._Developers.Stas.Scripts.UI.View.ScreenGameplay;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private float _levelPassTime;
        private int _score;
        private int _rewardMoney;

        private void Construct()
        {
            _wallet = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IWallet>();
            _dataProvider = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IDataProvider>();
            _persistentData = SceneManager.GetActiveScene().GetSceneContainer().Resolve<IPersistentData>();
        }

        private void Awake()
        {
            Construct();
            
            _levelStartTime = Time.time;
        }

        public void SetUiManager(GameplayUIManager gameplayUIManager)
        {
            _gameplayUIManager = gameplayUIManager;
        }

        public IEnumerator Run(int levelNumber, float baselevelReward)
        {
            _levelPassTime = Time.time - _levelStartTime;
            
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

                _score = CalculateScore(timeRatio);
            }
            
            yield return new WaitForSeconds(DelayBeforeShowUI);
            
            if (TryGetRewardMoney(levelNumber, _rewardMoney) == false)
                _rewardMoney = 0;
            
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
                _wallet.AddCoins(rewardMoney);
                
                return true;
            }
        }
    }
}
