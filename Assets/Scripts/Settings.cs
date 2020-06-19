using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	private MovementID moveID;
	
	private EnemyID enemyID;
	private Enemy[] enemies;

	private Dash combat;

	private RoundSettings rs;
	public MovementInfo GetMovementSettings()
	{
		return rs.moveInfo;
	}
	private void Awake()
	{
		moveID = MovementID.walk;

		enemyID = EnemyID.simpleFollower;

		combat = new Dash();

		rs = new RoundSettings(moveID, enemyID);

		enemies = new Enemy[2];
		for(int i = 0; i < enemies.Length; i++){
			enemies[i] = rs.CreateEnemy();
			enemies[i].Awake();
		}

		combat.Awake();
	}

	private void Start()
	{
		enemies[0].SetPosition(10, 5);
		enemies[1].SetPosition(10, 0);
	}

	private void FixedUpdate()
	{
		foreach (Enemy enemy in enemies)
		{
			enemy.FixedUpdate();
		}

		combat.FixedUpdate();
	}
}
