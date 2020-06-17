using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class simpleFollower : MonoBehaviour
{
	// basic information
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
	private Collider2D bc;

	// player information
	GameObject player;
	private Vector2 target;
	private void Awake()
	{
		// basic information
		body = new GameObject();
		speed = 100f;
		moveDir = Vector2.zero;
		vel = Vector2.zero;

		// visuals
		idleSprite = Resources.Load<Sprite>("Art/Enemies/simpleFollower/idle");
		sr = body.AddComponent<SpriteRenderer>();
		sr.sprite = idleSprite;

		// physics
		rb = body.AddComponent<Rigidbody2D>();
		bc = body.AddComponent<PolygonCollider2D>();
	}
	private void Start()
	{
		// get player
		player = GameObject.FindGameObjectWithTag("Player");

		// setup some physics
		rb.gravityScale = 0;
		rb.freezeRotation = true;
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
