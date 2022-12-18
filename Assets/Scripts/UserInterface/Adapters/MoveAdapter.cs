using System;
using Core;
using EventBusSystem;
using UserInterface.Core;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class MoveAdapter : IAdapter<MoveView, Battle>
    {
        private IEventBus _endMove;
        private UseEventBus _useEventBus;
        private RerollEventBus _rerollEventBus;
        public MoveView View { get; set; }
        public Battle Model { get; set; }

        public IEventBus EndMove
        {
            get => _endMove;
            set
            {
                if (_endMove != null)
                {
                    _endMove.EnableStateChanged -= EndMoveOnEnableStateChanged;
                    EndMoveOnEnableStateChanged(false);
                }

                _endMove = value;
                if (_endMove != null)
                {
                    _endMove.EnableStateChanged += EndMoveOnEnableStateChanged;
                    EndMoveOnEnableStateChanged(_endMove.Enabled);
                }
            }
        }

        public UseEventBus UseEvent
        {
            get => _useEventBus;
            set
            {
                if (_useEventBus != null)
                {
                    _useEventBus.EnableStateChanged -= UseEventBusOnEnableStateChanged;
                    UseEventBusOnEnableStateChanged(false);
                }

                _useEventBus = value;

                if (_useEventBus != null)
                {
                    _useEventBus.EnableStateChanged += UseEventBusOnEnableStateChanged;
                    UseEventBusOnEnableStateChanged(_useEventBus.Enabled);
                }
            }
        }
        
        public RerollEventBus RerollEvent
        {
            get => _rerollEventBus;
            set
            {
                if (_rerollEventBus != null)
                {
                    _rerollEventBus.EnableStateChanged -= RerollEventBusOnEnableStateChanged;
                    RerollEventBusOnEnableStateChanged(false);
                }

                _rerollEventBus = value;

                if (_rerollEventBus != null)
                {
                    _rerollEventBus.EnableStateChanged += RerollEventBusOnEnableStateChanged;
                    RerollEventBusOnEnableStateChanged(_rerollEventBus.Enabled);
                }
            }
        }

        private void RerollEventBusOnEnableStateChanged(bool obj)
        {
            
        }

        private void UseEventBusOnEnableStateChanged(bool obj)
        {
            
        }

        public MoveAdapter(MoveView view, Battle model)
        {
            View = view;
            Model = model;

            model.Acted += () => view.SetActions(model.Actions, model.MaxActions);

            View.EndMoveClicked += ViewOnEndMoveClicked;
            
            var cellElements = view.GuardianCellElements;
            for (int i = 0; i < model.GuardianList.Count; i++)
            {
                cellElements[i].Avatar = model.GuardianList[i].Avatar;
                var dices = model.GuardianList[i].Dices;
                for (int j = 0; j < dices.Length; j++)
                {
                    var i1 = i;
                    var j1 = j;
                    dices[j].Rerolled += rerollSide =>
                        cellElements[i1].BattleDiceElements[j1].Image = dices[j1].DiceAbility().Image;
                    dices[j].Used += () => cellElements[i1].BattleDiceElements[j1].Enabled = false;
                    dices[j].Reseted += () => cellElements[i1].BattleDiceElements[j1].Enabled = true;
                }
            }
            view.DiceUsed += ViewOnDiceUsed;
            view.Rerolled += ViewOnRerolled;
        }

        private void ViewOnRerolled(int guardianIndex)
        {
            var guardian = Model.GuardianList[guardianIndex];
            RerollEvent?.Invoke(guardian);
        }

        private void ViewOnDiceUsed(int guardian, int dice)
        {
            var battleDice = Model.GuardianList[guardian].Dices[dice];
            _useEventBus?.Invoke(battleDice);
        }

        private void EndMoveOnEnableStateChanged(bool state)
        {
            View.EndMoveButtonActiveState = state;
        }

        private void ViewOnEndMoveClicked()
        {
            EndMove?.Invoke();
        }
    }
}