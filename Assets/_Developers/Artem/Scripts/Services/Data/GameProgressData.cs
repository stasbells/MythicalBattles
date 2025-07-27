using System;
using System.Collections.Generic;
using MythicalBattles.Assets._Developers.Stas.Scripts.Constants;
using MythicalBattles.Levels;
using Newtonsoft.Json;
using UnityEngine;
using YG;

namespace MythicalBattles.Services.Data
{
    [Serializable]
    [JsonObject(MemberSerialization.Fields)]
    public class GameProgressData
    {
        private const int LevelsCount = 9;

        [SerializeField] private List<LevelResultData> _levelsResults = new List<LevelResultData>( new LevelResultData[LevelsCount]);
        
        // public GameProgressData()
        // {
        //     _levelsResults = new List<LevelResultData>( new LevelResultData[LevelsCount]);
        // }
        //
        // [JsonConstructor]
        // public GameProgressData(IEnumerable<LevelResultData> levelsResults)
        // {
        //     _levelsResults = new List<LevelResultData>(levelsResults);
        // }
        
        public IReadOnlyList<LevelResultData> LevelsResults => _levelsResults.AsReadOnly();

        public float GetLevelRecordTime(int levelNumber)
        {
            return _levelsResults[levelNumber - 1].Time;
        }

        public float GetLevelRecordPoints(int levelNumber)
        {
            return _levelsResults[levelNumber - 1].Points;
        }

        public float GetAllPoints()
        {
            float resultsSum = 0;

            for (int i = 0; i < _levelsResults.Count; i++)
            {
                resultsSum += _levelsResults[i].Points;
            }

            return resultsSum;
        }

        public int GetLastUnlockedLevelNumber()
        {
            for (int i = 0; i < _levelsResults.Count; i++)
            {
                if (Mathf.Approximately(_levelsResults[i].Points, 0f))
                    return i + 1;
            }

            return _levelsResults.Count;
        }

        public bool TryUpdateLevelRecord(int levelNumber, int resultPoints, float resultTime)
        {
            if (resultPoints > _levelsResults[levelNumber - 1].Points)
            {
                SetLevelResults(levelNumber, resultPoints, resultTime);

                YG2.SetLeaderboard(Constants.TestLeaderboard, (int)GetAllPoints());

                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            _levelsResults = new List<LevelResultData>( new LevelResultData[LevelsCount]);
        }

        private void SetLevelResults(int levelNumber, int resultPoints, float resultTime)
        {
            var levelResult = _levelsResults[levelNumber - 1];
            levelResult.Points = resultPoints;
            levelResult.Time = resultTime;
            _levelsResults[levelNumber - 1] = levelResult;
        }      
    }
}
