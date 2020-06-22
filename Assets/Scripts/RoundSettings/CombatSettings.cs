using UnityEngine;
using System.Collections;

public struct CombatSettings
{
	public static Combat PickCombat(CombatID id, Settings settings, Player player, GameObject canvasBody)
	{
		switch (id)
		{
			case CombatID.none:
				Debug.LogWarning("none combat ID should never call pick combat, null returned");
				return null;
			case CombatID.dash:
				return new Dash(settings, player, canvasBody);
			default:
				Debug.LogWarning("non supported Combat ID called to Create Enemy, null retunred");
				return null;
		}
	}
}
