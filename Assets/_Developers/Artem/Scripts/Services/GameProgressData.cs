using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace MythicalBattles
{
    [Serializable]
    public class GameProgressData
    {
        private const int LevelsCount = 9;

        private readonly List<LevelResultData> _levelsResults;
        
        public GameProgressData()
        {
            _levelsResults = new List<LevelResultData>( new LevelResultData[LevelsCount]);
        }
        
        [JsonConstructor]
        public GameProgressData(IEnumerable<LevelResultData> levelsResults)
        {
            _levelsResults = new List<LevelResultData>(levelsResults);
        }
        
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

        public int GetLastOpenedLevelNumber()
        {
            for (int i = 0; i < _levelsResults.Count; i++)
            {
                if (Mathf.Approximately(_levelsResults[i].Points, 0f))
                    return i + 1;
            }

            return _levelsResults.Count;
        }

        public bool TryUpdateLevelRecord(int levelNumber, float resultPoints, float resultTime)
        {
            if (resultPoints > _levelsResults[levelNumber - 1].Points)
            {
                SetLevelResults(levelNumber, resultPoints, resultTime);

                return true;
            }
            else
            {
                return false;
            }
        }

        private void SetLevelResults(int levelNumber, float resultPoints, float resultTime)
        {
            var levelResult = _levelsResults[levelNumber - 1];
            levelResult.Points = resultPoints;
            levelResult.Time = resultTime;
            _levelsResults[levelNumber - 1] = levelResult;
        }
    }
}
