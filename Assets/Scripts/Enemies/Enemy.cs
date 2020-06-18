using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy
{
	protected GameObject[] bodies;
	protected Rigidbody2D[] rbs;
	protected PolygonCollider2D[] pcs;
	protected SpriteRenderer[] srs;

	protected GameObject player;

	protected void Setup(int size, bool getPlayerRef){
		SetupSize(size);
		SetupBodies();
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
	}

	protected void SetupBodies(){
		for(int i = 0; i < bodies.Length; i++)
		{
			bodies[i] = new GameObject();

			rbs[i] = bodies[i].AddComponent<Rigidbody2D>();
			rbs[i].gravityScale = 0;
			rbs[i].freezeRotation = true;

			pcs[i] = bodies[i].AddComponent<PolygonCollider2D>();

			srs[i] = bodies[i].AddComponent<SpriteRenderer>();
		}
	}
}
