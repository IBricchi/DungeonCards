using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy
{
	// ID of generated enemy, not unique for each instance but unique for each type of enemy
	public EnemyID ID;

	// basic references to player and settings
	protected Settings settings;
	protected Player player;
	
	// basic enemy components
	protected GameObject body;
	protected EnemyInfo enemyInfo;

	// basic components used for 1 body components
	protected Sprite idleSprite;
	protected SpriteRenderer sr;
	protected Rigidbody2D rb;
	protected Collider2D col;

	protected float health;
	public bool alive;

	public Enemy(Settings _settings, Player _player)
	{
		settings = _settings;
		player = _player;
	}

	// awake function
	public void Awake()
	{
		// sets up variables for the child
		ChildAwake();

		// sets up enemy components
		Setup();
	}
	protected abstract void ChildAwake();

	// setup basic enemy settings
	public void Setup(){
		alive = true;
		SetupBody();
		SetupSprites();
		SetupPhysics();
	}

	// basic body setup with possible child extension
	protected void SetupBody(){
		body = new GameObject();
		body.layer = 9;
		body.tag = "Enemy";
		enemyInfo = body.AddComponent<EnemyInfo>();
		enemyInfo.enemy = this;

		ChildSetupBody();
	}
	protected virtual void ChildSetupBody() { }

	// basic sprite setup for any enemy with only one body, virtual for child to overwrite
	protected virtual void SetupSprites()
	{
		SpriteRenderer sr = body.AddComponent<SpriteRenderer>();
		sr.sprite = idleSprite;
	}

	// basic physics setup for any enemy with one body, virtual for child to overwrite
	protected virtual void SetupPhysics()
	{
		rb = body.AddComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		rb.freezeRotation = true;

		col = body.AddComponent<PolygonCollider2D>();
	}

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
	public virtual void TakeDamage(float damage)
	{
		health -= damage;
	}
}
