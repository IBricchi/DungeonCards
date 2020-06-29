using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	// ID of generated enemy, not unique for each instance but unique for each type of enemy
	public EnemyID ID;

	// basic references to player and settings
	protected Settings settings;
	protected Player player;

	// basic components used for 1 body components
	protected Sprite idleSprite;
	protected SpriteRenderer sr;
	protected Rigidbody2D rb;
	protected Collider2D col;

	protected float health;
	public bool alive;

	// awake function
	public void Awake()
	{
		alive = true;

		// sets up variables for the child
		ChildAwake();

		// setup
		SetupBody();
		SetupSprites();
		SetupPhysics();
	}
	protected virtual void ChildAwake() { }

	// start function
	public void Start()
	{
		// get settings player references
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
		player = settings.GetPlayer();

		// setup child start
		ChildStart();
	}
	protected virtual void ChildStart() { }

	// basic body setup with possible child extension
	protected void SetupBody(){
		gameObject.layer = 9;
		gameObject.tag = "Enemy";

		ChildSetupBody();
	}
	protected virtual void ChildSetupBody() { }
	// basic sprite setup for any enemy with only one body, virtual for child to overwrite
	protected virtual void SetupSprites()
	{
		SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = idleSprite;
	}
	// basic physics setup for any enemy with one body, virtual for child to overwrite
	protected virtual void SetupPhysics()
	{
		rb = gameObject.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		rb.freezeRotation = true;

		col = gameObject.AddComponent<PolygonCollider2D>();
	}
	protected virtual void SetupChild() { }

	// fixed update calls child fixed update overloaded by child if needed
	public void FixedUpdate()
	{
		ChildFixedUpdate();
	}
	protected virtual void ChildFixedUpdate() { }

	// simple position controls for components overloaded to allow for Vector2, and float float inputs
	public void SetPosition(Vector2 pos){
		SetPosition(pos.x, pos.y);
	}
	public virtual void SetPosition(float x, float y) // virtual, overload for any enemy with more than one body
	{
		Vector3 pos = new Vector3(x, y, 0);
		rb.transform.position = pos;
	}

	// simple collisions starting and stopping for single body enemies, required for certain combats
	// requires overload for more than one body enemies
	public virtual void StopPhysicsCollisions()
	{
		col.isTrigger = true;
	}
	public virtual void StartPhysicsCollisions()
	{
		col.isTrigger = false;
	}

	// basic damage for enemies, if more complex damange control is required overload
	public abstract void TakeDamage(Collider2D collider, float damage); // this one is used when isTrigger is on
	public abstract void TakeDamage(Collision2D collision, float damage); // this one is used when isTrigger is off
}
