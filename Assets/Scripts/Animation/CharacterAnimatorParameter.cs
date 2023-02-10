using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Animation
{
    [Serializable]
    public class CharacterAnimatorParameter
    {
        [SerializeField] private bool _useManualAnimatorController;

        [SerializeField] private AnimationClip _idleClip;
        [SerializeField] private AnimationClip _attackClip;

        [SerializeField] private AnimatorController _manualAnimatorController;

        public AnimatorController GetAnimatorController()
        {
            if (_useManualAnimatorController)
            {
                return _manualAnimatorController;
            }

            var animatorController = new AnimatorController();

            var baseLayer = CreateBaseLayer();
            var baseStateMachine = baseLayer.stateMachine;

            var idleState = CreateState(baseStateMachine, "Idle", _idleClip);
            var attackState = CreateState(baseStateMachine, "Attack", _attackClip);

            baseStateMachine.defaultState = idleState;

            AddOneShot(animatorController, idleState, attackState, "Attack");

            animatorController.AddLayer(baseLayer);
            return animatorController;
        }

        private AnimatorControllerLayer CreateBaseLayer()
        {
            var baseStateMachine = new AnimatorStateMachine();
            var baseLayer = new AnimatorControllerLayer
            {
                name = "Base Layer",
                stateMachine = baseStateMachine,
                defaultWeight = 1f
            };

            return baseLayer;
        }
        private AnimatorState CreateState(AnimatorStateMachine stateMachine, string name, Motion motion)
        {
            AnimatorState state = new AnimatorState() { motion = motion, name = name };
            stateMachine.AddState(state, new Vector3());
            return state;
        }

        private void AddOneShot(AnimatorController animatorController, AnimatorState from, AnimatorState to, string trigger)
        {
            animatorController.AddParameter(trigger, AnimatorControllerParameterType.Trigger);
            AddOneShotTransitions(from, to, trigger);
        }
        private void AddOneShotTransitions(AnimatorState from, AnimatorState to, string trigger)
        {
            var fromTo = from.AddTransition(to);
            fromTo.hasExitTime = false;
            fromTo.AddCondition(AnimatorConditionMode.If, 0f, trigger);

            var toFrom = to.AddTransition(from);
            toFrom.hasExitTime = true;
        }
    }
}