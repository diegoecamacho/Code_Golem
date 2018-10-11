﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CodeGolem.StateController
{
	public interface IState {
	    void Enter();
	    void Execute();
	    void Exit();
	}

}
