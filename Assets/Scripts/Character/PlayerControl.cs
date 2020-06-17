using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
	// basic requirements for control
	private InputMaster c;
	private Rigidbody2D rb;

	// reference to settings
	private Settings settings;
	private MovementInfo moveSettings;

	// variables for movement
	public float maxSpeed;
	public float acceleration;
	public float deceleration;
	public float stopThreshold;

	// variable for movement calcualttion
	private Vector2 dir;
	private Vector2 lastDir;
	private bool moving;
	private Vector2 vel;
	private float speed;


	private void Awake()
	{
		c = new InputMaster();
		rb = GetComponent<Rigidbody2D>();
		
		// speed claculation variables
		dir = Vector2.zero;
		vel = Vector2.zero;
		speed = 0f;
		moving = false;
	}

	private void OnEnable()
	{
		c.Enable();
		c.Player.Move.performed += ctx => StartMoving(ctx.ReadValue<Vector2>());
		c.Player.Move.canceled += ctx => StopMoving(ctx.ReadValue<Vector2>());
	}

	private void OnDisable()
	{
		c.Disable();
	}

	private void Start()
	{
		// get settings information
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
		moveSettings = settings.GetMovementSettings();

		// use settings to setup movement variables
		maxSpeed = moveSettings.maxSpeed;
		acceleration = moveSettings.acceleration;
		deceleration = moveSettings.deceleration;
		stopThreshold = moveSettings.stopThreshold;
	}
	private void FixedUpdate()
	{
			// change speed depending on movement type
		if (moving)
		{
			speed += (maxSpeed - speed) * acceleration * Time.fixedDeltaTime;
			if (speed > maxSpeed - 0.1) speed = maxSpeed;
			lastDir = dir;
		}
		else
		{
			speed -= speed * deceleration * Time.fixedDeltaTime;
			if (speed < stopThreshold) speed = 0;	
		}

		// update velocity
		vel = lastDir * speed;
		rb.velocity = vel;
	}

	private void StartMoving(Vector2 _dir)
	{
		moving = true;
		dir = _dir;
	}
	private void StopMoving(Vector2 _dir)
	{
		moving = false;
		dir = _dir;
	}
}
