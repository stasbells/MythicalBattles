using System.Collections;
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
        private GameProgressData _gameProgressData;
        private float _levelStartTIme;
        private float _maxAdditionalGold; 
        
        [Inject]
        private void Construct(IWallet wallet, IPersistentData persistentData, IDataProvider dataProvider)
        {
            _wallet = wallet;
            _dataProvider = dataProvider;
            _gameProgressData = persistentData.GameProgressData;
        }

        private void Awake()
        {
            _levelStartTIme = Time.time;
        }

        public IEnumerator Run(int levelNumber, float baselevelReward)
        {
            float levelPassTime = Time.time - _levelStartTIme;

            _maxAdditionalGold = baselevelReward * AdditionalGoldFactor;

            float rewardMoney;
            float victoryPoints;

            if (levelPassTime >= MaxTimeInSecondsForBonus)
            {
                rewardMoney = baselevelReward;
                victoryPoints = 1f;
            }
            else
            {
                float timeRatio = 1 - (levelPassTime / MaxTimeInSecondsForBonus);
                
                rewardMoney = CalculateRewardMoney(timeRatio, baselevelReward);

                victoryPoints = CalculateVictoryPoints(timeRatio);
            }
            
            yield return new WaitForSeconds(DelayBeforeShowUI);

            if (_gameProgressData.TryUpdateLevelRecord(levelNumber, victoryPoints, levelPassTime))  
            {
                _dataProvider.SaveGameProgressData();
                
                Debug.Log("new record!"); //вывести PopUp с указанием нового рекорда
            }
            else
            {
                Debug.Log("base points view");  //вывести PopUp с обычным указанием счёта
            }

            if (TryGetRewardMoney(levelNumber, rewardMoney))   
            {
                Debug.Log("collect money!"); //вывести PopUp с указанием заработанных монет
            }
            else
            {
                Debug.Log("only points and time view");  //вывести PopUp без указания заработанных монет
            }
        }

        private float CalculateRewardMoney(float timeRatio, float baselevelReward)
        {
            float rewardMoney = baselevelReward + Mathf.RoundToInt(timeRatio * _maxAdditionalGold);
            
            return rewardMoney;
        }

        private float CalculateVictoryPoints(float timeRatio)
        {
            return Mathf.RoundToInt(timeRatio * MaxPoints);
        }

        private bool TryGetRewardMoney(int levelNumber, float rewardMoney)
        {
            if (Mathf.Approximately(_gameProgressData.GetLevelRecordPoints(levelNumber), 0f) == false)
                return false;
            else
            {
                _wallet.AddCoins((int)rewardMoney);
                
                return true;
            }
        }
    }
}
