using MythicalBattles;
using MythicalBattles.Services.AudioPlayback;
using MythicalBattles.Services.Data;
using MythicalBattles.Services.ItemSelector;
using MythicalBattles.Services.LevelCompletionStopwatch;
using MythicalBattles.Services.LevelSelection;
using MythicalBattles.Services.PlayerStats;
using MythicalBattles.Services.Wallet;
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