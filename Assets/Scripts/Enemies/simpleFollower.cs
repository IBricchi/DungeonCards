using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class simpleFollower : MonoBehaviour
{
	// basic information
	private float speed;
	private Vector2 moveDir;

	// visusals
	public Sprite idleSprite;
	public SpriteRenderer sr;

	// physics simulations
	private GameObject body;
	private Rigidbody2D rb;
	private Collider2D bc;

	// player information
	GameObject player;
	private Vector2 target;
	private void Awake()
	{
		body = new GameObject();

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
		target = player.transform.position;

		// setup some physics
		rb.gravityScale = 0;	
	}

	private void FixedUpdate()
	{
		moveDir = target - (Vector2) transform.position;
		
	}
}
