using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoundSettings{
	readonly MovementID moveID;
	readonly EnemyID enemyID;
	readonly CombatID combatID;

	public MovementInfo moveInfo; // TODO! replace with class creation later on
	public Enemy CreateEnemy()
	{
		return EnemySettings.CreateEnemy(enemyID);
	}
	public Combat SetupCombat(Settings settings, Player player, GameObject canvasBody)
	{
		return CombatSettings.PickCombat(combatID, settings, player, canvasBody);
	}

	public RoundSettings(MovementID _moveID = MovementID.walk, EnemyID _enemyID = EnemyID.none, CombatID _combatID = CombatID.none){
		moveID = _moveID;
		enemyID = _enemyID;
		combatID = _combatID;

		moveInfo = MovementSettings.GetMovement(moveID);
	}
}
