using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour
{
	private InputMaster c;
	private CharacterController cc;
	private float speed = 10f;
	Vector2 dir = Vector2.zero;

	private void Awake()
	{
		c = new InputMaster();
		cc = GetComponent<CharacterController>();
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
		cc.Move(dir * speed * Time.fixedDeltaTime);
	}

	private void UpdateDir(Vector2 _dir)
	{
		dir = _dir;
	}
}
