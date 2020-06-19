using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoundSettings{
	readonly MovementID moveID;
	public MovementInfo moveInfo;

	readonly EnemyID enemyID;
	public Enemy CreateEnemy()
	{
		return EnemySettings.CreateEnemy(enemyID);
	}

	public RoundSettings(MovementID _moveID = MovementID.walk, EnemyID _enemyID = EnemyID.none){
		moveID = _moveID;
		moveInfo = MovementSettings.GetMovement(moveID);

		enemyID = _enemyID;
	}
}
