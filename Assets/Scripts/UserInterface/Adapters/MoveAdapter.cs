using System;
using Core;
using EventBusSystem;
using UserInterface.Core;
using UserInterface.Views;

namespace UserInterface.Adapters
{
    public class MoveAdapter : IAdapter<MoveView, Battle>
    {
        private EventBus _endMove;
        private UseEventBus _useEventBus;
        public MoveView View { get; set; }
        public Battle Model { get; set; }

        public EventBus EndMove
        {
            get => _endMove;
            set
            {
                if (_endMove != null)
                {
                    _endMove.EnableStateChanged -= EndMoveOnEnableStateChanged;
                }

                _endMove = value;
                if (_endMove != null)
                {
                    _endMove.EnableStateChanged += EndMoveOnEnableStateChanged;
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
                }

                _useEventBus = value;

                if (_useEventBus != null)
                {
                    _useEventBus.EnableStateChanged += UseEventBusOnEnableStateChanged;
                }
            }
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
                cellElements[i].Avatar = model.GuardianList[i].Guardian.Avatar;
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