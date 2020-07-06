using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// round setting keeps the current settings for the round
public struct RoundSettings{
	// id's for the round
	readonly MovementID moveID;
	readonly EnemyID enemyID;
	readonly CombatID combatID;
	readonly TerrainID terrainID;

	// functions to create and resturn instances of game objects
	public MovementInfo moveInfo; // TODO! replace with class creation later on
	public Enemy CreateEnemy()
	{
		return EnemySettings.CreateEnemy(enemyID);
	}
	public Combat GiveCombat(Player player)
	{
		return CombatSettings.PickCombat(combatID, player);
	}
	public Terrain SetupTerrain()
	{
		return TerrainSettings.PickTerrain(terrainID);

	}

	// instanciate the round settings
	public RoundSettings(MovementID _moveID = MovementID.walk, EnemyID _enemyID = EnemyID.none, CombatID _combatID = CombatID.none, TerrainID _terrainID = TerrainID.open){
		moveID = _moveID;
		enemyID = _enemyID;
		combatID = _combatID;
		terrainID = _terrainID;

		moveInfo = MovementSettings.GetMovement(moveID);
	}
}
