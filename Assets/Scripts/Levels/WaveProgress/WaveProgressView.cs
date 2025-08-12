using MythicalBattles.Assets.Scripts.Controllers.Boosts;
using TMPro;
using UnityEngine;

namespace MythicalBattles.Assets.Scripts.Levels.WaveProgress
{
    public class WaveProgressView : MonoBehaviour
    {
        [field: SerializeField] public GameObject ProgressBar { get; private set; }
        [field: SerializeField] public TMP_Text NextWaveText { get; private set; }
        [field: SerializeField] public BoostsDescription BoostsDescription { get; private set; }
    }
}
