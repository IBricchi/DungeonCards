using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoundSettings{
	readonly MovementID moveID;
	readonly EnemyID enemyID;
	readonly CombatID combatID;
	readonly TerrainID terrainID;

	public MovementInfo moveInfo; // TODO! replace with class creation later on
	public Enemy CreateEnemy(Settings settings, Player player)
	{
		return EnemySettings.CreateEnemy(enemyID, settings, player);
	}
	public Combat SetupCombat(Settings settings, Player player, GameObject canvasBody)
	{
		return CombatSettings.PickCombat(combatID, settings, player, canvasBody);
	}
	public Terrain SetupTerrain(Settings settings, Player player)
	{
		return TerrainSettings.PickTerrain(terrainID, settings, player);

	}

	public RoundSettings(MovementID _moveID = MovementID.walk, EnemyID _enemyID = EnemyID.none, CombatID _combatID = CombatID.none, TerrainID _terrainID = TerrainID.open){
		moveID = _moveID;
		enemyID = _enemyID;
		combatID = _combatID;
		terrainID = _terrainID;

		moveInfo = MovementSettings.GetMovement(moveID);
	}
}
