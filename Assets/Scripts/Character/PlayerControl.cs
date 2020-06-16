using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
	private InputMaster c;
	private Rigidbody2D rb;
	public float speed = 10f;
	Vector2 dir = Vector2.zero;

	private void Awake()
	{
		c = new InputMaster();
		rb = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		c.Enable();
		c.Player.Move.performed += ctx => UpdateDir(ctx.ReadValue<Vector2>());
		c.Player.Move.canceled += ctx => UpdateDir(ctx.ReadValue<Vector2>());
	}

	private void OnDisable()
	{
		c.Disable();
	}

	private void FixedUpdate()
	{
		rb.velocity = dir * speed;
	}

	private void UpdateDir(Vector2 _dir)
	{
		dir = _dir;
	}
}
