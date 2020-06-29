using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	// camera information
	private GameObject camBody;
	private Camera cam;

	// visual information
	private Sprite idleSprite;
	private SpriteRenderer sr;

	// phsyics
	public Rigidbody2D rb;
	private PolygonCollider2D pc;

	// controls
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

	// combat information
	Combat combat;

	// overrides for external control
	private bool disableMovment = false;

	// awake function
	private void Awake()
	{
		// setup basic variables
		// setup basic game object information
		gameObject.tag = "Player";
		gameObject.name = "Player";
		gameObject.layer = 8;
		// speed claculation variables
		dir = Vector2.zero;
		vel = Vector2.zero;
		speed = 0f;
		moving = false;
		// get control data
		c = new InputMaster();

		// basic setup
		SetupCamera();
		setupVisuals();
		setupPhysics();
	}
	// setup camera
	private void SetupCamera()
	{
		// create main camera and position
		camBody = new GameObject();
		camBody.name = "Main Camera";
		camBody.transform.parent = gameObject.transform;
		camBody.transform.localPosition = new Vector3(0, 0, -30);
		camBody.tag = "MainCamera";

		// add actual camera and setup attributes
		cam = camBody.AddComponent<Camera>();
		cam.orthographic = true;
	}
	// setup visuals
	private void setupVisuals()
	{
		idleSprite = Resources.Load<Sprite>("Art/Player/idle");
		sr = gameObject.AddComponent<SpriteRenderer>();
		sr.sprite = idleSprite;
		sr.color = Color.red;
	}
	// setup physics
	private void setupPhysics()
	{
		rb = gameObject.AddComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		rb.gravityScale = 0;
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		pc = gameObject.AddComponent<PolygonCollider2D>();
	}

	private void OnEnable()
	{
		// enable controls and link to appropriate functions
		c.Enable();
		c.Player.Move.performed += ctx => StartMoving(ctx.ReadValue<Vector2>());
		c.Player.Move.canceled += ctx => StopMoving(ctx.ReadValue<Vector2>());
	}

	private void OnDisable()
	{
		// disable controls
		c.Disable();
	}

	private void FixedUpdate()
	{
		// change speed depending on movement type
		if (moving)
		{
			// if moving accelerate
			speed += (maxSpeed - speed) * acceleration * Time.fixedDeltaTime;
			if (speed > maxSpeed - 0.1) speed = maxSpeed;
			lastDir = dir;
		}
		else
		{
			// if not moving decelerate
			speed -= speed * deceleration * Time.fixedDeltaTime;
			if (speed < stopThreshold) speed = 0;
		}

		// update velocity
		if (!disableMovment)
		{
			vel = lastDir * speed;
			rb.velocity = vel;
		}

		combat.FixedUpdate();
	}

	public void UpdateMovementSettings(MovementInfo _moveSettings) // this is going to be replaced later when player is turned into a base class which will have specific movement classes inherit from it
	{
		// setup movement settings
		moveSettings = _moveSettings;

		// use settings to setup movement variables
		maxSpeed = moveSettings.maxSpeed;
		acceleration = moveSettings.acceleration;
		deceleration = moveSettings.deceleration;
		stopThreshold = moveSettings.stopThreshold;
	}

	// movement information updates depending on inputs
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

	// combat
	public void GiveCombat(Combat _combat)
	{
		combat = _combat;
	}
	public void Attack(Collider2D collider, Enemy enemy)
	{
		combat.ColliderAttack(collider, enemy);
	}
	public void Attack(Collision2D collision, Enemy enemy)
	{
		combat.CollisionAttack(collision, enemy);
	}

	// override functions for other components to eddit
	// this is mainly important for dashing right now, but will also allow for stun attacks later on in the development
	public void DisableMovement()
	{
		disableMovment = true;
	}
	public void EnableMovement()
	{
		disableMovment = false;
	}
}
