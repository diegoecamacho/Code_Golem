using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem.StateController
{
	public class StateMachine : MonoBehaviour {

        public IState currentState;
        public IState previousState;

       public void ChangeState(IState newState)
        {
            if (this.currentState != null)
            {
                currentState.Exit();
            }
            this.previousState = this.currentState;
            this.currentState = newState;
            this.currentState.Enter();
        }

        public void ExecuteStateUpdate()
        {
          
            var runningState = this.currentState;
            if (runningState != null)
            {
                runningState.Execute();
            }
        }

        public void ReturnToPreviousState()
        {
                this.currentState.Exit();
                this.currentState = this.previousState;
                this.currentState.Enter();
        }
	}
}
