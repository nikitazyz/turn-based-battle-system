using System;
using Core;
using Dices;
using Guardians;
using StateMachineSystem;
using StateMachineSystem.BattleStateMachine;
using UnityEngine;
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
                GuardianCell guardianCell = Battle.GuardianList[i];
                int currentIndex = i;
                cellElements[i].Avatar = guardianCell.Avatar;
                var dices = guardianCell.Dices;
                RegisterOnDiceEvents(dices, cellElements[currentIndex]);

                Battle.GuardianList[i].RerollAmountChanged += () => OnRerollAmountChanged(guardianCell, currentIndex);
                View.SetRerollAmount(i, guardianCell.RerollAmount);
            }
        }

        private void OnRerollAmountChanged(GuardianCell guardianCell, int index)
        {
            View.SetRerollAmount(index, guardianCell.RerollAmount);
        }

        private static void RegisterOnDiceEvents(BattleDice[] dices, GuardianCellElement cellElement)
        {
            for (int j = 0; j < dices.Length; j++)
            {
                BattleDiceElement diceElement = cellElement.BattleDiceElements[j];
                BattleDice dice = dices[j];
                
                dice.Rerolled += _ => OnRerolled(dice, diceElement);
                dice.Used += () => OnUsed(diceElement);
                dice.Reseted += () => OnReseted(diceElement);
                dice.FlameSet += side => OnFlameSet(side, dice, diceElement);
                dice.FlameReset += () => OnFlameReset(diceElement);
            }
        }

        private static void OnFlameReset(BattleDiceElement diceElement)
        {
            diceElement.FireFrameEnabled = false;
        }

        private static void OnFlameSet(int side, BattleDice battleDice, BattleDiceElement battleDiceElement)
        {
            battleDiceElement.FireFrameEnabled = battleDice.IsFlameOnActiveSide;
        }

        #region Dice Events Handlers

        private static void OnRerolled(BattleDice dice, BattleDiceElement diceElement)
        {
            diceElement.Image = dice.DiceAbility.Image;
            diceElement.FireFrameEnabled = dice.IsFlameOnActiveSide;
        }

        private static void OnUsed(BattleDiceElement diceElement) => diceElement.IsUsed = false;

        private static void OnReseted(BattleDiceElement diceElement) => diceElement.IsUsed = true;

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