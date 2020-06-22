using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dash
{
	private Player player;
	private Rigidbody2D playerRB;

	private GameObject pointer;
	private Sprite pointerSprite;
	private SpriteRenderer pointerSR;
	private float pointerScale;
	private Vector2 pointerPos;
	private float pointerDist;

	private InputMaster controls;

	// dash variables
	private float dashSpeed;
	private float dashTime;
	private float remainingDash;
	private float dashCharge;
	private float dashDischarge;
	private bool dashing;

	public Dash(Player _player)
	{
		player = _player;
	}

	public void Awake()
	{
		playerRB = player.rb;

		pointer = new GameObject();
		pointer.transform.parent = player.body.transform;
		
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
		dashTime = 1f;
		remainingDash = 0f;
		dashCharge = 0.5f;
		dashDischarge = 13f;
		dashing = false;
	}

	public void OnEnable()
	{
		controls.Enable();
		controls.Player.MousePoint.performed += ctx => GetMousePoint(ctx.ReadValue<Vector2>());
		controls.Player.RJoyPoint.performed += ctx => GetRJoyPoint(ctx.ReadValue<Vector2>());
		controls.Player.Attack.started += ctx => StartAttack();
		controls.Player.Attack.canceled += ctx => StopAttack();
	}

	public void OnDisable()
	{
		controls.Disable();
	}

	public void FixedUpdate()
	{
		pointer.transform.localPosition = pointerPos * pointerDist;
		pointer.transform.up = pointerPos;

		if(dashing)
		{
			playerRB.velocity = pointerPos * dashSpeed;
			remainingDash -= dashDischarge * Time.fixedDeltaTime;
			if(remainingDash <= 0)
			{
				StopAttack();
			}
		}else
		{
			if(remainingDash < dashTime)
			{
				remainingDash += dashCharge * Time.fixedDeltaTime;
			}
			if(remainingDash > dashTime)
			{
				remainingDash = dashTime;
			}
		}
		Debug.Log(remainingDash);
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

	private void StartAttack()
	{
		if (remainingDash == dashTime)
		{
			player.DisableMovement();
			dashing = true;
		}
	}

	private void StopAttack()
	{
		player.EnableMovement();
		dashing = false;
	}
}
