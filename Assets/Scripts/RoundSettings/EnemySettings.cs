﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemySettings
{
	public static Enemy CreateEnemy(EnemyID id)
	{
		switch(id)
		{
			case EnemyID.none:
				Debug.LogWarning("none enemy ID should never call Create Enemy, null returned");
				return null;
			case EnemyID.simpleFollower:
				return new SimpleFollower();
			default:
				Debug.LogWarning("non supported enemy ID called to Create Enemy, null retunred");
				return null;
		}
	}
}
