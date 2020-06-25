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

	public SimpleFollower(Settings settings, Player player):
		base(settings, player) { }

	protected override void ChildAwake()
	{
		// setup basic enemy information
		ID = EnemyID.simpleFollower;

		// player reference
		playerGO = player.body;

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
		body.name = "Simple Follower";
	}

	protected override void ChildFixedUpdate()
	{
		// check if dead
		if(health <= 0)
		{
			alive = false;
			UnityEngine.Object.Destroy(body);
			settings.enemies.Remove(this);
		}

		// get player position
		target = playerGO.transform.position;

		// get movement info and move
		moveDir = target - (Vector2) body.transform.position;
		vel = moveDir / moveDir.magnitude * speed;
		rb.velocity = vel * Time.fixedDeltaTime;

		// get direction info and turn
		body.transform.up = moveDir;
	}
}
