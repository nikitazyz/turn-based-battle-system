namespace Core
{
    public class DebugParameters
    {
        private readonly IGame _game;

        public DebugParameters(IGame game)
        {
            _game = game;
        }

        public void StartGame()
        {
            _game.StartBattle();
        }
    }
}