﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy
{
	protected GameObject[] bodies;
	protected Rigidbody2D[] rbs;
	protected PolygonCollider2D[] pcs;
	protected SpriteRenderer[] srs;

	protected GameObject player;

	public abstract void Awake();

	public void Setup(int size, bool getPlayerRef){
		SetupSize(size);
		SetupBodies();
		SetupSprites();
		SetupPhysics();
		if(getPlayerRef)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}

	protected void SetupSize(int size)
	{
		bodies = new GameObject[size];
		rbs = new Rigidbody2D[size];
		pcs = new PolygonCollider2D[size];
		srs = new SpriteRenderer[size];
	}

	protected void SetupBodies(){
		for(int i = 0; i < bodies.Length; i++)
		{
			bodies[i] = new GameObject();
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

	public abstract void FixedUpdate();

	public void SetPosition(Vector2 pos){
		SetPosition(pos.x, pos.y);
	}

	public virtual void SetPosition(float x, float y)
	{
		foreach (GameObject body in bodies)
		{
			body.transform.position.Set(x, y, 0);
		}
	}
}
