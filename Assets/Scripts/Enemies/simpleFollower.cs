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
	private SpriteRenderer sr;

	// physics simulations
	private Rigidbody2D rb;

	// player information
	private Vector2 target;
	private void Awake()
	{
		// setup basic enemy information
		Setup(size, true);

		// basic information
		body = bodies[0];
		speed = 100f;
		moveDir = Vector2.zero;
		vel = Vector2.zero;

		// visuals
		idleSprite = Resources.Load<Sprite>("Art/Enemies/simpleFollower/idle");
		sr = srs[0];
		sr.sprite = idleSprite;

		// physics
		rb = rbs[0];
	}

	private void FixedUpdate()
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
