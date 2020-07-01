using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dash : Combat
{
	private Rigidbody2D playerRB;

	private GameObject dashbar;
	private Sprite dashbarSprite;
	private Image dashbarImg;
	private CanvasRenderer dashbarCR;
	private RectTransform dashbarRT;

	private GameObject dashlevel;
	private Sprite dashlevelSprite;
	private Image dashlevelImg;
	private CanvasRenderer dashlevelCR;
	private RectTransform dashlevelRT;

	private GameObject pointer;
	private Sprite pointerSprite;
	private SpriteRenderer pointerSR;
	private float pointerScale;
	private Vector2 pointerPos;
	private float pointerDist;

	// dash variables
	private float dashSpeed;
	private float dashTime;
	private float remainingDash;
	private float dashCharge;
	private float dashDischarge;
	private bool dashing;

	protected override void Awake()
	{
		// call base awake function
		base.Awake();

		// attacking variables
		dashSpeed = 100f;
		dashTime = 1f;
		remainingDash = 0f;
		dashCharge = 0.5f;
		dashDischarge = 13f;
		dashing = false;
		damage = 5;

		// pointer variables
		pointerScale = 0.3f;
		pointerDist = 1.3f;

		// progress bar variables
		dashbarSprite = Resources.Load<Sprite>("Art/Combat/Dash/dashbar");
		dashlevelSprite = Resources.Load<Sprite>("Art/Combat/Dash/dashlevel");
	}

	protected override void Start()
	{
		// call base start function
		base.Start();

		// player info
		playerRB = player.rb;

		// add outer bar for dash loading bar
		dashbar = new GameObject();
		dashbar.name = "Dash Bar";
		dashbar.transform.SetParent(canvasBody.transform);
		dashbarImg = dashbar.AddComponent<Image>();
		dashbarImg.sprite = dashbarSprite;
		dashbarImg.color = Color.gray;
		dashbarImg.useSpriteMesh = true;
		dashbarRT = dashbar.GetComponent<RectTransform>();
		dashbarRT.localPosition = new Vector3(-Screen.width / 2 + 105, 0, 0);
		dashbarRT.sizeDelta = new Vector2(90, 600);

		// add inner bar for dash loading bar
		dashlevel = new GameObject();
		dashlevel.name = "Dash Level";
		dashlevel.transform.SetParent(dashbar.transform);
		dashlevelImg = dashlevel.AddComponent<Image>();
		dashlevelImg.sprite = dashlevelSprite;
		dashlevelImg.color = Color.yellow;
		dashlevelImg.useSpriteMesh = true;
		dashlevelRT = dashlevel.GetComponent<RectTransform>();
		dashlevelRT.localPosition = new Vector3(0, -285, 0);
		dashlevelRT.sizeDelta = new Vector2(60, 0);

		// add pointer for dash
		pointer = new GameObject();
		pointer.name = "Dash Pointer";
		pointer.transform.parent = player.gameObject.transform;

		// add sprite vor pointer
		pointerSprite = Resources.Load<Sprite>("Art/Combat/pointer");
		pointerSR = pointer.AddComponent<SpriteRenderer>();
		pointerSR.sprite = pointerSprite;
		pointerSR.color = Color.gray;

		// re-size pointer, position and turn
		pointer.transform.localScale = Vector3.one * pointerScale;
		pointerPos = Vector3.up;
	}

	protected override void OnEnable()
	{
		// call base OnEnable functions
		base.OnEnable();

		// setup bindings to controls
		c.Player.MousePoint.performed += ctx => GetMousePoint(ctx.ReadValue<Vector2>());
		c.Player.RJoyPoint.performed += ctx => GetRJoyPoint(ctx.ReadValue<Vector2>());
		c.Player.Attack.started += ctx => StartAttack();
		c.Player.Attack.canceled += ctx => StopAttack();
	}

	// phsyics updates
	protected override void FixedUpdate()
	{
		// move and turn pointer
		pointer.transform.localPosition = pointerPos * pointerDist;
		pointer.transform.up = pointerPos;

		// check if dashing or not
		if(dashing)
		{
			// move player and remove charge from dash meter
			playerRB.velocity = pointerPos * dashSpeed;
			remainingDash -= dashDischarge * Time.fixedDeltaTime;
			if(remainingDash <= 0)
			{
				StopAttack();
			}
		}else
		{
			// increase dash charge
			if(remainingDash < dashTime)
			{
				remainingDash += dashCharge * Time.fixedDeltaTime;
			}
			if(remainingDash > dashTime)
			{
				remainingDash = dashTime;
				dashlevelImg.color = Color.green;
			}
		}

		// resize dash charge bars
		dashlevelRT.localPosition = new Vector3(0, (remainingDash - dashTime) * 285, 0);
		dashlevelRT.sizeDelta = new Vector2(60, 570 * remainingDash);
	}

	// check for on trigger events and attack
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Enemy")
		{
			ColliderAttack(collider, collider.gameObject.GetComponent<Enemy>());
		}
	}

	// helper functions for each type of input type to setup pointerPos appropriately
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

	// attacking functions
	private void StartAttack()
	{
		if (remainingDash == dashTime)
		{
			settings.StopEnemyPhysicsCollisions();
			player.DisableMovement();
			dashlevelImg.color = Color.yellow;
			dashing = true;
		}
	}
	private void StopAttack()
	{
		settings.StartEnemyPhysicsCollisions();
		player.EnableMovement();
		dashing = false;
		//di.dashing = false;
	}
	// dash uses collider attack since it turns all enemies into a trigger
	public override void ColliderAttack(Collider2D collider, Enemy enemy)
	{
		enemy.TakeDamage(collider, damage);
	}
}
