namespace MythicalBattles.Assets.Scripts.UI
{
    public interface IScreenBinder
    {
        public void Bind(ScreenViewModel viewModel);

        public void Close();
    }
}