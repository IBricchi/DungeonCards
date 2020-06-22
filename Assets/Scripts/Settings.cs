using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	private Player player;

	private MovementID moveID;
	
	private EnemyID enemyID;
	private Enemy[] enemies;

	private Dash combat;

	private RoundSettings rs;

	private void Awake()
	{
		moveID = MovementID.walk;

		enemyID = EnemyID.simpleFollower;

		rs = new RoundSettings(moveID, enemyID);
		
		player = new Player();
		player.Awake();
		player.UpdateMovementSettings(rs.moveInfo);

		combat = new Dash(player);

		enemies = new Enemy[2];
		for(int i = 0; i < enemies.Length; i++){
			enemies[i] = rs.CreateEnemy();
			enemies[i].Awake();
		}

		combat.Awake();
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
	private void Start()
	{
		enemies[0].SetPosition(10, 5);
		enemies[1].SetPosition(10, 0);
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
}
