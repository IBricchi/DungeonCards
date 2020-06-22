using UnityEngine;
using System.Collections;

public class Player
{
	public GameObject body;

	private GameObject camBody;
	private Camera cam;

	private Sprite idleSprite;
	private SpriteRenderer sr;

	public Rigidbody2D rb;
	private PolygonCollider2D pc;

	private InputMaster c;

	// reference to settings
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

	// overrides
	private bool disableMovment = false;

	public void Awake()
	{
		body = new GameObject();
		body.tag = "Player";
		body.name = "Player";

		c = new InputMaster();

		camBody = new GameObject();
		camBody.name = "Main Camera";
		camBody.transform.parent = body.transform;
		camBody.transform.localPosition = new Vector3(0, 0, -30);
		camBody.tag = "MainCamera";

		cam = camBody.AddComponent<Camera>();
		cam.orthographic = true;

		idleSprite = Resources.Load<Sprite>("Art/Player/idle");
		sr = body.AddComponent<SpriteRenderer>();
		sr.sprite = idleSprite;
		sr.color = Color.red;
	
		rb = body.AddComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		rb.gravityScale = 0;
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		pc = body.AddComponent<PolygonCollider2D>();

		// speed claculation variables
		dir = Vector2.zero;
		vel = Vector2.zero;
		speed = 0f;
		moving = false;
	}

	public void OnEnable()
	{
		c.Enable();
		c.Player.Move.performed += ctx => StartMoving(ctx.ReadValue<Vector2>());
		c.Player.Move.canceled += ctx => StopMoving(ctx.ReadValue<Vector2>());
	}

	public void OnDisable()
	{
		c.Disable();
	}

	public void FixedUpdate()
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
		if (!disableMovment)
		{
			vel = lastDir * speed;
			rb.velocity = vel;
		}
	}

	public void UpdateMovementSettings(MovementInfo _moveSettings)
	{
		// setup movement settings
		moveSettings = _moveSettings;

		// use settings to setup movement variables
		maxSpeed = moveSettings.maxSpeed;
		acceleration = moveSettings.acceleration;
		deceleration = moveSettings.deceleration;
		stopThreshold = moveSettings.stopThreshold;
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

	public void DisableMovement()
	{
		disableMovment = true;
	}
	public void EnableMovement()
	{
		disableMovment = false;
	}
}
