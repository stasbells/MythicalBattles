namespace MythicalBattles
{
    internal interface IScreenBinder
    {
        void Bind(ScreenViewModel viewModel);

        void Close();
    }
}