using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SimpleFollower : Enemy
{
	// basic variables
	public float speed;
	private Vector2 moveDir;
	private Vector2 pos;
	private Vector2 vel;

	// player information
	private GameObject playerGO;
	private Vector2 target;

	protected override void ChildAwake()
	{
		// setup basic enemy information
		ID = EnemyID.simpleFollower;

		// basic information
		speed = 100f;
		moveDir = Vector2.zero;
		vel = Vector2.zero;

		// visuals information
		idleSprite = Resources.Load<Sprite>("Art/Enemies/SimpleFollower/idle");

		// game information
		health = 12f;
	}

	protected override void ChildSetupBody()
	{
		gameObject.name = "Simple Follower";
	}

	protected override void ChildStart()
	{
		playerGO = player.body;
	}

	protected override void ChildFixedUpdate()
	{
		// check if dead
		if(health <= 0)
		{
			alive = false;
			UnityEngine.Object.Destroy(gameObject);
			settings.enemies.Remove(this);
		}

		// get player position
		target = playerGO.transform.position;

		// get movement info and move
		moveDir = target - (Vector2) gameObject.transform.position;
		vel = moveDir / moveDir.magnitude * speed;
		rb.velocity = vel * Time.fixedDeltaTime;

		// get direction info and turn
		gameObject.transform.up = moveDir;
	}
}
