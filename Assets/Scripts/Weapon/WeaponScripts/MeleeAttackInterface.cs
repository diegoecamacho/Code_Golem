using System.Collections;
using System.Collections.Generic;
using CodeGolem.UI;
using UnityEngine;
using System;

namespace CodeGolem.Combat
{
    public class MeleeAttack : SkillComponent
    {
        public override ISkillInterface GetBehaviour()
        {
            throw new ArgumentNullException("No Behavior")
        }

        public override void Use(SkillParam skillParam)
        {
            throw new System.NotImplementedException();
        }
    }
}
