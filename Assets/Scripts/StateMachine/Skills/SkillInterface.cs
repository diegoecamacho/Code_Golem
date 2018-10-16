

using CodeGolem.Actor;

namespace CodeGolem.StateController.Skills
{
    internal interface ISkillState : IState 
    {
        void RegisterSkill();
        void Use();
    }
}
