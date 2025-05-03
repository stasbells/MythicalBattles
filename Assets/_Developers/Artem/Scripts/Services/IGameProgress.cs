namespace MythicalBattles
{
    public interface IGameProgress
    {
        public float GetLevelRecordTime(int levelNumber);
        public float GetLevelRecordPoints(int levelNumber);
        public float GetAllPoints();
        public bool TryUpdateLevelRecord(int levelNumber, float results, float time);
    }
}
