namespace MythicalBattles.Assets.Scripts.UI
{
    public interface IScreenBinder
    {
        void Bind(ScreenViewModel viewModel);

        void Close();
    }
}