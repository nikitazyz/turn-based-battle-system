using System;
using Core;
using EventBusSystem;
using Initialization;
using UnityEngine;
using UserInterface.Adapters;
using UserInterface.Core;
using UserInterface.Views;

namespace UserInterface.UIBootstrappers
{
    public class BattleUIBootstrapper : UIBootstrapper, IInitializable<Battle, EndMoveEventBus, UseEventBus, RerollEventBus>
    {
        [SerializeField] private MoveView _moveView;
        [SerializeField] private StatusView _statusView;

        private MoveAdapter _moveAdapter;
        private StatusAdapter _statusAdapter;

        public void Initialize(Battle battle, EndMoveEventBus endMoveEvent, UseEventBus useEventBus, RerollEventBus rerollEventBus)
        {
            _moveAdapter = new MoveAdapter(_moveView, battle)
            {
                EndMove = endMoveEvent,
                UseEvent = useEventBus,
                RerollEvent = rerollEventBus
            };

            _statusAdapter = new StatusAdapter(_statusView, battle.Player);
        }
    }
}