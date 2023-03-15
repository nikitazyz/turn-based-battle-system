using System;
using Core;
using StateMachineSystem.BattleStateMachine;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class EndGameAdapter
    {
        private EndGameView _view;
        private Battle _battle;

        public EndGameAdapter(EndGameView view, Battle battle)
        {
            _view = view;
            _battle = battle;
            
            _battle.StateMachine.StateChanged += StateMachineOnStateChanged;
            
            _view.RestartClicked += ViewOnRestartClicked;
            _view.MainMenuClicked += ViewOnMainMenuClicked;
        }

        private void ViewOnMainMenuClicked()
        {
            _battle.Exit();
        }

        private void ViewOnRestartClicked()
        {
            _battle.Restart();
        }

        private void StateMachineOnStateChanged(Type obj)
        {
            _view.Enabled = obj == typeof(EndState);
        }
    }
}