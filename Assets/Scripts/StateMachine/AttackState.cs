using System;
using UnityEngine;

namespace CodeGolem.StateController
{
    public class AttackState : IState
    {
        private float _timeElapsed = 0;

        private readonly float _attackTime;
        private readonly Action _attackCallBack;
        private readonly Action _animationAction;

        private bool _animActive;

        public AttackState(Action attackCall, Action animationAction, float timeBetweenAttack)
        {
            this._attackCallBack = attackCall;
            _attackTime = timeBetweenAttack;
            _animationAction = animationAction;
        }

        public void Enter()
        {

        }

        public void Execute()
        {
            Debug.Log("Attack State");
            _timeElapsed += Time.deltaTime;

            if (_timeElapsed >= _attackTime /2  && !_animActive)
            {
                Debug.Log("AttackAnim");
                _animationAction();
                _animActive = true;
            }

            if (_timeElapsed >= _attackTime)
            {
                _attackCallBack();
                _timeElapsed = 0;
                _animActive = false;
            }

            
        }

        public void Exit()
        {
        }
    }
}