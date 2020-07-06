using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static instance to create the right type of enemy from an ID
public struct EnemySettings
{
	public static Enemy CreateEnemy(EnemyID id)
	{
		// switch statment creates the right type of enemy based on the combat ID
		switch (id)
		{
			case EnemyID.none:
				Debug.LogWarning("none enemy ID should never call Create Enemy, null returned");
				return null;
			case EnemyID.simpleFollower:
				GameObject go = new GameObject();
				return go.AddComponent<SimpleFollower>();
			default:
				Debug.LogWarning("non supported enemy ID called to Create Enemy, null retunred");
				return null;
		}
	}
}
