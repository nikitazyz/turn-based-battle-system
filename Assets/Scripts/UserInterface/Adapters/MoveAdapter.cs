using System;
using Core;
using Dices;
using StateMachineSystem;
using StateMachineSystem.BattleStateMachine;
using UserInterface.Views;
using UserInterface.Views.MoveViewElements;

namespace UserInterface.Adapters
{
    public class MoveAdapter
    {
        private readonly StateMachine _stateMachine;
        private MoveView View { get; }
        private Battle Battle { get; }

        private bool _isMoveState;

        public MoveAdapter(MoveView view, Battle model)
        {
            _stateMachine = model.StateMachine;
            _stateMachine.StateChanged += OnStateChanged;
            View = view;
            Battle = model;

            model.Acted += UpdateActionView;
            UpdateActionView();

            View.EndMoveClicked += ViewOnEndMoveClicked;
            
            InitializeGuardianCells();
            
            view.DiceUsed += ViewOnDiceUsed;
            view.Rerolled += ViewOnRerolled;
        }

        private void OnStateChanged(Type state)
        {
            _isMoveState = state == typeof(UserMoveState);
            UpdateView();
        }

        private void InitializeGuardianCells()
        {
            var cellElements = View.GuardianCellElements;
            for (int i = 0; i < Battle.GuardianList.Count; i++)
            {
                cellElements[i].Avatar = Battle.GuardianList[i].Avatar;
                var dices = Battle.GuardianList[i].Dices;
                RegisterOnDiceEvents(dices, cellElements[i]);
            }
        }

        private static void RegisterOnDiceEvents(BattleDice[] dices, GuardianCellElement cellElement)
        {
            for (int j = 0; j < dices.Length; j++)
            {
                BattleDiceElement diceElement = cellElement.BattleDiceElements[j];
                BattleDice dice = dices[j];
                
                dices[j].Rerolled += _ => OnRerolled(dice, diceElement);
                dices[j].Used += () => OnUsed(diceElement);
                dices[j].Reseted += () => OnReseted(diceElement);
            }
        }

        #region Dice Events Handlers

        private static void OnRerolled(BattleDice dice, BattleDiceElement diceElement) => diceElement.Image = dice.DiceAbility().Image;

        private static void OnUsed(BattleDiceElement diceElement) => diceElement.Enabled = false;

        private static void OnReseted(BattleDiceElement diceElement) => diceElement.Enabled = true;

        #endregion

        #region View Update

        private void UpdateView()
        {
            View.EndMoveButtonActiveState = _isMoveState;
            foreach (var cellElement in View.GuardianCellElements)
            {
                foreach (var diceElement in cellElement.BattleDiceElements)
                {
                    diceElement.Enabled = _isMoveState;
                }

                cellElement.RerollButtonEnabled = _isMoveState;
            }
            UpdateActionView();
        }
        
        private void UpdateActionView()
        {
            View.SetActions(Battle.Actions, Battle.MaxActions);
        }

        #endregion

        #region View Events Handlers

        private void ViewOnRerolled(int guardianIndex)
        {
            var guardian = Battle.GuardianList[guardianIndex];
            Battle.RerollDices(guardian);
        }

        private void ViewOnDiceUsed(int guardian, int dice)
        {
            var battleDice = Battle.GuardianList[guardian].Dices[dice];
            Battle.UseDice(battleDice);
        }

        private void ViewOnEndMoveClicked()
        {
            Battle.EndMove();
        }

        #endregion
        
    }
}