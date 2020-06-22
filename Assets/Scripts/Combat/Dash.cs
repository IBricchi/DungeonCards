﻿using UnityEngine;
using System.Collections;

public class Dash
{
	private GameObject player;
	private Rigidbody2D playerRB;

	private GameObject pointer;
	private Sprite pointerSprite;
	private SpriteRenderer pointerSR;
	private float pointerScale;
	private Vector2 pointerPos;
	private float pointerDist;

	private InputMaster controls;

	private float dashSpeed;

	public void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerRB = player.GetComponent<Rigidbody2D>();

		pointer = new GameObject();
		pointer.transform.parent = player.transform;
		
		pointerSprite = Resources.Load<Sprite>("Art/Combat/pointer");
		pointerSR = pointer.AddComponent<SpriteRenderer>();
		pointerSR.sprite = pointerSprite;
		pointerSR.color = Color.gray;

		pointerScale = 0.3f;
		pointer.transform.localScale = Vector3.one * pointerScale;

		pointerPos = Vector3.up;
		pointerDist = 1.3f;

		controls = new InputMaster();

		dashSpeed = 100f;
	}

	public void OnEnable()
	{
		controls.Enable();
		controls.Player.MousePoint.performed += ctx => GetMousePoint(ctx.ReadValue<Vector2>());
		controls.Player.RJoyPoint.performed += ctx => GetRJoyPoint(ctx.ReadValue<Vector2>());
		controls.Player.Attack.performed += ctx => Attack();
	}

	public void OnDisable()
	{
		controls.Disable();
	}

	public void FixedUpdate()
	{
		pointer.transform.localPosition = pointerPos * pointerDist;
		pointer.transform.up = pointerPos;
	}

	private void GetMousePoint(Vector2 pos)
	{
		pos.x -= Screen.width / 2;
		pos.y -= Screen.height / 2;
		pointerPos = pos / pos.magnitude;
	}
	private void GetRJoyPoint(Vector2 pos)
	{
		pointerPos = pos / pos.magnitude;
	}

	private void Attack()
	{
		playerRB.velocity = pointerPos * dashSpeed;
	}
}
