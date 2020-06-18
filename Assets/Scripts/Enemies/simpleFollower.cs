using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class SimpleFollower : Enemy
{
	// basic information
	private int size = 1;
	
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

	public override void Awake()
	{
		// setup basic enemy information
		Setup(size, true);

		// basic information
		body = bodies[0];
		speed = 100f;
		moveDir = Vector2.zero;
		vel = Vector2.zero;

		// physics
		rb = rbs[0];
	}

	protected override void SetupSprites()
	{
		idleSprite = Resources.Load<Sprite>("Art/Enemies/simpleFollower/idle");
		srs[0] = bodies[0].AddComponent<SpriteRenderer>();
		srs[0].sprite = idleSprite;
	}

	public override void FixedUpdate()
	{
		// get player position
		target = player.transform.position;

		// get movement info and move
		moveDir = target - (Vector2) body.transform.position;
		vel = moveDir / moveDir.magnitude * speed;
		rb.velocity = vel * Time.fixedDeltaTime;

		// get direction info and turn
		body.transform.up = moveDir;
	}
}
