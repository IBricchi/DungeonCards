using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
	private MovementID moveID;
	private CombatID combatID;
	private EnemyID enemyID;
	
	private RoundSettings rs;

	private Player player;

	private Terrain terrain;

	private GameObject canvasBody;
	private Canvas canvas;
	private CanvasScaler canvasScaler;
	private GraphicRaycaster canvasGR;

	private Enemy[] enemies;

	private Combat combat;

	private void Awake()
	{
		// Get ID's for each component
		moveID = MovementID.walk;
		enemyID = EnemyID.simpleFollower;
		combatID = CombatID.dash;

		// setup round settings based on ID's
		rs = new RoundSettings(moveID, enemyID, combatID);
		
		// Setup Player
		player = new Player();
		player.Awake();
		player.UpdateMovementSettings(rs.moveInfo);

		// setup terrain
		terrain = new Maze(this, player);
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

		// setup enemies
		enemies = new Enemy[terrain.GetEnemyCount()];
		for(int i = 0; i < enemies.Length; i++){
			enemies[i] = rs.CreateEnemy();
			enemies[i].Awake();
		}
		terrain.PositionEnemies(enemies);
	}
	private void OnEnable()
	{
		player.OnEnable();
		combat.OnEnable();
	}
	private void OnDisable()
	{
		player.OnDisable();
		combat.OnDisable();
	}

	private void FixedUpdate()
	{
		player.FixedUpdate();
		combat.FixedUpdate();
		foreach (Enemy enemy in enemies)
		{
			enemy.FixedUpdate();
		}
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
