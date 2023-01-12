using Dices;
using Enemies;
using Guardians;
using PlayerSystem;

namespace Core
{
    public class BattleStatus
    {
        public GameSettings GameSettings { get; }

        public Guardian[] Guardians { get; }
        public Enemy[] Enemies { get; }
        public Player Player { get; }
        public int PlayerHealth { get; }

        public BattleStatus(GameSettings gameSettings, Guardian[] guardians, Enemy[] enemies, Player player, int playerHealth)
        {
            Guardians = guardians;
            Enemies = enemies;
            Player = player;
            PlayerHealth = playerHealth;
            GameSettings = gameSettings;
        }
    }
}