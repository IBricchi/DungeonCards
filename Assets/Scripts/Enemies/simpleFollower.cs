using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SimpleFollower : Enemy
{
	// basic information
	private int size;

	private GameObject playerGO;

	private GameObject body;
	public float speed;
	private Vector2 moveDir;
	private Vector2 pos;
	private Vector2 vel;

	// visusals
	private Sprite idleSprite;

	// physics simulations
	private Rigidbody2D rb;

	// player information
	private Vector2 target;

	public SimpleFollower(Settings settings, Player player):
		base(settings, player) { }

	public override void Awake()
	{
		// setup basic enemy information
		ID = EnemyID.simpleFollower;
		size = 1;
		Setup(size);

		// player reference
		playerGO = player.body;

		// basic information
		body = bodies[0];
		body.name = "Simple Follower";
		speed = 100f;
		moveDir = Vector2.zero;
		vel = Vector2.zero;

		// physics
		rb = rbs[0];

		// game information
		health = 12f;
	}

	protected override void SetupSprites()
	{
		idleSprite = Resources.Load<Sprite>("Art/Enemies/simpleFollower/idle");
		srs[0] = bodies[0].AddComponent<SpriteRenderer>();
		srs[0].sprite = idleSprite;
	}

	public override void FixedUpdate()
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
