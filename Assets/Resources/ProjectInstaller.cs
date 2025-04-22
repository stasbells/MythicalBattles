using MythicalBattles;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(typeof(PersistentData), typeof(IPersistentData));
        builder.AddSingleton(typeof(PlayerStats), typeof(IPlayerStats));
        builder.AddSingleton(typeof(DataLocalProvider), typeof(IDataProvider));
        builder.AddSingleton(typeof(Wallet), typeof(IWallet));
        builder.AddSingleton(typeof(LevelSelectionService), typeof(ILevelSelectionService));
        builder.AddSingleton(typeof(SpawnPointGenerator), typeof(ISpawnPointGenerator));
    }
}