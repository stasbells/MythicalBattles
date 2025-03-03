using Reflex.Core;
using UnityEngine;

namespace MythicalBattles
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        //это DI контейнер от Reflex
        
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton("Hello");
            
            //сюда будем добавлять наши сервисы
        }
    }
}
