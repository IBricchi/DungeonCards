using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	public MovementID moveID;
	public CombatID combatID;
	public EnemyID enemyID;
	public TerrainID terrainID;
	
	private RoundSettings rs;

	public Player player;

	private Terrain terrain;

	private GameObject canvasBody;
	private Canvas canvas;
	private CanvasScaler canvasScaler;
	private GraphicRaycaster canvasGR;

	public List<Enemy> enemies;

	private Combat combat;

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
		terrain = rs.SetupTerrain(this, player);
		terrain.Awake();

		// setup overall canvas
		canvasBody = new GameObject();
		canvasBody.name = "Canvas";
		canvas = canvasBody.AddComponent<Canvas>();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		canvasScaler = canvasBody.AddComponent<CanvasScaler>();
		canvasGR = canvasBody.AddComponent<GraphicRaycaster>();

		// setup combat
		combat = rs.SetupCombat(this, player, canvasBody);
		combat.Awake();
		player.GiveCombat(combat);

		// setup enemies
		enemies = new List<Enemy>();
		for(int i = 0; i < terrain.GetEnemyCount(); i++){
			enemies.Add(rs.CreateEnemy());
		}
		terrain.PositionEnemies(enemies.ToArray());
	}
	private void OnEnable()
	{
		combat.OnEnable();
	}
	private void OnDisable()
	{
		combat.OnDisable();
	}

	public void StopEnemyPhysicsCollisions()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.StopPhysicsCollisions();
		}
	}

	public void StartEnemyPhysicsCollisions()
	{
		foreach(Enemy enemy in enemies)
		{
			enemy.StartPhysicsCollisions();
		}
	}
}
