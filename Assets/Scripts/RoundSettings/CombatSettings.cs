using UnityEngine;
using System.Collections;

// static instance to create the right type of combat from an ID
public struct CombatSettings
{
	public static Combat PickCombat(CombatID id, Player player)
	{
		// switch statment returns the correct type of combat object based on the combat ID
		switch (id)
		{
			case CombatID.none:
				Debug.LogWarning("none combat ID should never call pick combat, null returned");
				return null;
			case CombatID.dash:
				return player.gameObject.AddComponent<Dash>();
			default:
				Debug.LogWarning("non supported Combat ID called to Create Enemy, null retunred");
				return null;
		}
	}
}
