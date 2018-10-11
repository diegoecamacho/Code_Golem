using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeGolem.StateController;

public class AttackState : IState
{
    float timeElapsed = 0;
    float attackTime;

    private System.Action attackCallBack;

    public AttackState(System.Action AttackCall, float timeBetweenAttack)
    {
        this.attackCallBack = AttackCall;
        attackTime = timeBetweenAttack;
    }

    public void Enter()
    {
        return;
    }

    public void Execute()
    {
        timeElapsed += Time.deltaTime;
        Debug.Log(timeElapsed);
        if (timeElapsed >= attackTime)
        {
            this.attackCallBack();
            timeElapsed = 0;
        }
    }

    public void Exit()
    {
        return;
    }
}
