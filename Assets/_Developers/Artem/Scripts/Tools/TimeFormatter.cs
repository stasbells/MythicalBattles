using UnityEngine;

namespace MythicalBattles
{
    public static class TimeFormatter 
    {
        public static string GetTimeInString(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60f);
            
            int seconds = Mathf.FloorToInt(time % 60f);
            
            string formattedTime = $"{minutes:00}:{seconds:00}";
            
            return formattedTime;
        }
    }
}
