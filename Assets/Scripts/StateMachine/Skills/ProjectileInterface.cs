using CodeGolem.Actor;
using CodeGolem.StateController.Skills;
using UnityEngine;

public class MeleeAttack : ISkillState
{
    private readonly ActorStats actor;
    private readonly Transform weaponSocket;
    private readonly System.Action attackAction;

    private readonly GameObject swordPrefab;

    public MeleeAttack(ActorStats actor, Transform weaponSocket, GameObject swordPrefab, System.Action attackAction)
    {
        this.actor = actor;
        this.weaponSocket = weaponSocket;
        this.swordPrefab = swordPrefab;
        this.attackAction = attackAction;
    }


    public void Enter()
    {

        Object.Instantiate(swordPrefab, weaponSocket);
    }

    public void Execute()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void RegisterSkill()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        attackAction();
    }
}