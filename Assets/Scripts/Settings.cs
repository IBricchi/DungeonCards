using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	// basic id's for the game
	public MovementID moveID;
	public CombatID combatID;
	public EnemyID enemyID;
	public TerrainID terrainID;
	
	// round settings
	private RoundSettings rs;

	// player
	private Player player;
	private Combat combat;
	
	// terrain
	private Terrain terrain;

	// canvas
	private GameObject canvasBody;
	private Canvas canvas;
	private CanvasScaler canvasScaler;
	private GraphicRaycaster canvasGR;
	
	// enemies list
	public List<Enemy> enemies;

	private void Awake()
	{
		// Get ID's for each component
		//moveID = MovementID.walk;
		//enemyID = EnemyID.simpleFollower;
		//combatID = CombatID.dash;
		//terrainID = TerrainID.maze;

		// setup round settings based on ID's
		rs = new RoundSettings(moveID, enemyID, combatID, terrainID);

		// Setup Player
		GameObject playerGO = new GameObject();
		player = playerGO.AddComponent<Player>();
		player.UpdateMovementSettings(rs.moveInfo);

		// setup terrain
		terrain = rs.SetupTerrain();

		// setup overall canvas
		canvasBody = new GameObject();
		canvasBody.name = "Canvas";
		canvas = canvasBody.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvasScaler = canvasBody.AddComponent<CanvasScaler>();
		canvasGR = canvasBody.AddComponent<GraphicRaycaster>();

		// setup combat
		combat = rs.SetupCombat(this, player, canvasBody);
		player.GiveCombat(combat);

		// setup enemies
		enemies = new List<Enemy>();
		for(int i = 0; i < terrain.GetEnemyCount(); i++){
			enemies.Add(rs.CreateEnemy());
		}
		terrain.InitialPositionEnemies(enemies);
	}

	// calls stopPhysicsCollisions on all enemies
	// used for physical attacks
	public void StopEnemyPhysicsCollisions()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.StopPhysicsCollisions();
		}
	}

	// calls startPhysicsCOllisions on all enemies
	// used for physical attacks
	public void StartEnemyPhysicsCollisions()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.StartPhysicsCollisions();
		}
	}

	// access important game objects
	public Player GetPlayer()
	{
		return player;
	}
	public GameObject GetCanvas()
	{
		return canvasBody;
	}
}
