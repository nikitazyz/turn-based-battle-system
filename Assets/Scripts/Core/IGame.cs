namespace Core
{
    public interface IGame
    {
        BattleStatus BattleStatus { get; set; }
        void StartBattle();
        void OpenMap();
        void OpenBattleEditor();
    }
}