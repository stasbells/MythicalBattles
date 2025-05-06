using System;
using UnityEngine;

namespace MythicalBattles
{
    public class GameProgress : IGameProgress
    {
        private const int LevelsCount = 9;
        private LevelResultData[] _levelsResults;

        public GameProgress()
        {
            _levelsResults = new LevelResultData[LevelsCount];
        }

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

            for (int i = 0; i < _levelsResults.Length; i++)
            {
                resultsSum += _levelsResults[i].Points;
            }

            return resultsSum;
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
            _levelsResults[levelNumber - 1].Points = resultPoints;
            _levelsResults[levelNumber - 1].Time = resultTime;
        }
    }
}
