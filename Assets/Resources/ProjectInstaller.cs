using MythicalBattles;
using Reflex.Core;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    private IDataProvider _dataProvider;
    private IPersistentData _persistentData;
    
    public void InstallBindings(ContainerBuilder builder)
    {
        builder.AddSingleton(typeof(PersistentData), typeof(IPersistentData));
        builder.AddSingleton(typeof(DataLocalProvider), typeof(IDataProvider));
        builder.AddSingleton(typeof(Wallet), typeof(IWallet));
        builder.AddSingleton(typeof(SpawnPointGenerator), typeof(ISpawnPointGenerator));
        builder.AddSingleton(typeof(PlayerStats), typeof(IPlayerStats));
    }
}