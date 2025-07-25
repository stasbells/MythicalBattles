using UnityEngine;
using YG;

namespace MythicalBattles
{
    public class SaveDataResseter : MonoBehaviour
    {
        public void ResetSaveData()
        {
            YG2.SetDefaultSaves();
            YG2.SaveProgress();

            Debug.Log("Save data has been reset.");
        }
    }
}