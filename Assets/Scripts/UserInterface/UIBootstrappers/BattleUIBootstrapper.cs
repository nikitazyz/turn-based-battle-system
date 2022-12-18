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
    public class BattleUIBootstrapper : UIBootstrapper, IInitializable<Battle, EndMoveEventBus, UseEventBus>
    {
        [SerializeField] private MoveView _moveView;

        private MoveAdapter _moveAdapter;

        public void Initialize(Battle battle, EndMoveEventBus endMoveEvent, UseEventBus useEventBus)
        {
            _moveAdapter = new MoveAdapter(_moveView, battle)
            {
                EndMove = endMoveEvent,
                UseEvent = useEventBus
            };
        }
    }
}