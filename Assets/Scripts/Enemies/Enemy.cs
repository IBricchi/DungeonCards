using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy
{
	public EnemyID ID;

	protected Player player;
	protected Settings settings;

	protected GameObject[] bodies;
	protected EnemyInfo[] eis;
	protected Rigidbody2D[] rbs;
	protected PolygonCollider2D[] pcs;
	protected SpriteRenderer[] srs;

	protected float health;
	public bool alive;


	public abstract void Awake();

	public Enemy(Settings _settings, Player _player)
	{
		settings = _settings;
		player = _player;
	}

	public void Setup(int size){
		SetupSize(size);
		SetupBodies();
		SetupSprites();
		SetupPhysics();
		SetupHealth();
	}

	protected void SetupSize(int size)
	{
		bodies = new GameObject[size];
		eis = new EnemyInfo[size];
		rbs = new Rigidbody2D[size];
		pcs = new PolygonCollider2D[size];
		srs = new SpriteRenderer[size];
	}

	protected void SetupBodies(){
		for(int i = 0; i < bodies.Length; i++)
		{
			bodies[i] = new GameObject();
			bodies[i].layer = 9;
			bodies[i].tag = "Enemy";
			eis[i] = bodies[i].AddComponent<EnemyInfo>();
			eis[i].enemy = this;
		}
	}

	protected abstract void SetupSprites();

	protected virtual void SetupPhysics()
	{
		for(int i = 0; i < bodies.Length; i++)
		{
			rbs[i] = bodies[i].AddComponent<Rigidbody2D>();
			rbs[i].gravityScale = 0;
			rbs[i].freezeRotation = true;

			pcs[i] = bodies[i].AddComponent<PolygonCollider2D>();
		}
	}

	protected virtual void SetupHealth()
	{
		alive = true;
	}

	public abstract void FixedUpdate();

	public void SetPosition(Vector2 pos){
		SetPosition(pos.x, pos.y);
	}

	public virtual void SetPosition(float x, float y)
	{
		Vector3 pos = new Vector3(x, y, 0);
		foreach (Rigidbody2D rb in rbs)
		{
			rb.transform.position = pos;
		}
	}

	public virtual void StopPhysicsCollisions()
	{
		foreach(Collider2D pc in pcs)
		{
			pc.isTrigger = true;
		}
	}

	public virtual void StartPhysicsCollisions()
	{
		foreach (Collider2D pc in pcs)
		{
			pc.isTrigger = false;
		}
	}

	public virtual void TakeDamage(float damage)
	{
		health -= damage;
	}
}
