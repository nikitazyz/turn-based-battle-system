using Dices;
using Enemies;
using Guardians;
using PlayerSystem;

namespace Core
{
    public class BattleStatus
    {
        public GameSettings GameSettings { get; set; }
        public Guardian[] Guardians { get; set; }
        public Enemy[] Enemies { get; set; }
        public Player Player { get; set; }
        public int PlayerHealth { get; set; }

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