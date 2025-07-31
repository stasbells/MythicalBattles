using MythicalBattles;
using MythicalBattles.Assets.Scripts.Services.AudioPlayback;
using MythicalBattles.Assets.Scripts.Services.Data;
using MythicalBattles.Assets.Scripts.Services.ItemSelector;
using MythicalBattles.Assets.Scripts.Services.LevelCompletionStopwatch;
using MythicalBattles.Assets.Scripts.Services.LevelSelection;
using MythicalBattles.Assets.Scripts.Services.PlayerStats;
using MythicalBattles.Assets.Scripts.Services.Wallet;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(typeof(PersistentData), typeof(IPersistentData));
        builder.AddSingleton(typeof(DataLocalProvider), typeof(IDataProvider));
        builder.AddSingleton(typeof(ItemSelector), typeof(IItemSelector));
        builder.AddSingleton(typeof(PlayerStats), typeof(IPlayerStats));
        builder.AddSingleton(typeof(AudioPlayback), typeof(IAudioPlayback));
        builder.AddSingleton(typeof(Wallet), typeof(IWallet));
        builder.AddSingleton(typeof(LevelSelectionService), typeof(ILevelSelectionService));
        builder.AddSingleton(typeof(LevelCompletionStopwatch), typeof(ILevelCompletionStopwatch));
    }
}