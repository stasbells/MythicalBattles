using UnityEngine;
using YG;

namespace MythicalBattles.Assets.Scripts.Utils
{
    public class SaveDataResseter : MonoBehaviour
    {
        public void ResetSaveData()
        {
            YG2.SetDefaultSaves();
            YG2.SaveProgress();
        }
    }
}