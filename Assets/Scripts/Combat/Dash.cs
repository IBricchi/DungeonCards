﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dash : Combat
{
	private Rigidbody2D playerRB;
	private DashCombat di;

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

	public Dash(Settings _settings, Player _player, GameObject _canvas) :
		base(_settings, _player, _canvas) { }

	public override void Awake()
	{
		playerRB = player.rb;
		di = player.body.AddComponent<DashCombat>();
		di.dashing = false;
		di.dash = this;

		dashbar = new GameObject();
		dashbar.name = "Dash Bar";
		dashbar.transform.SetParent(canvasBody.transform);
		dashbarSprite = Resources.Load<Sprite>("Art/Combat/Dash/dashbar");
		dashbarImg = dashbar.AddComponent<Image>();
		dashbarImg.sprite = dashbarSprite;
		dashbarImg.color = Color.gray;
		dashbarImg.useSpriteMesh = true;
		dashbarRT = dashbar.GetComponent<RectTransform>();
		dashbarRT.localPosition = new Vector3(-Screen.width/2 + 105,0,0);
		dashbarRT.sizeDelta = new Vector2(90, 600);

		dashlevel = new GameObject();
		dashlevel.name = "Dash Level";
		dashlevel.transform.SetParent(dashbar.transform);
		dashlevelSprite = Resources.Load<Sprite>("Art/Combat/Dash/dashlevel");
		dashlevelImg = dashlevel.AddComponent<Image>();
		dashlevelImg.sprite = dashlevelSprite;
		dashlevelImg.color = Color.yellow;
		dashlevelImg.useSpriteMesh = true;
		dashlevelRT = dashlevel.GetComponent<RectTransform>();
		dashlevelRT.localPosition = new Vector3(0, -285, 0);
		dashlevelRT.sizeDelta = new Vector2(60, 0);

		pointer = new GameObject();
		pointer.name = "Dash Pointer";
		pointer.transform.parent = player.body.transform;
		
		pointerSprite = Resources.Load<Sprite>("Art/Combat/pointer");
		pointerSR = pointer.AddComponent<SpriteRenderer>();
		pointerSR.sprite = pointerSprite;
		pointerSR.color = Color.gray;

		pointerScale = 0.3f;
		pointer.transform.localScale = Vector3.one * pointerScale;

		pointerPos = Vector3.up;
		pointerDist = 1.3f;

		c = new InputMaster();

		dashSpeed = 100f;
		dashTime = 1f;
		remainingDash = 0f;
		dashCharge = 0.5f;
		dashDischarge = 13f;
		dashing = false;

		damage = 5;
	}

	public override void OnEnable()
	{
		c.Enable();
		c.Player.MousePoint.performed += ctx => GetMousePoint(ctx.ReadValue<Vector2>());
		c.Player.RJoyPoint.performed += ctx => GetRJoyPoint(ctx.ReadValue<Vector2>());
		c.Player.Attack.started += ctx => StartAttack();
		c.Player.Attack.canceled += ctx => StopAttack();
	}

	public override void OnDisable()
	{
		c.Disable();
	}

	public override void FixedUpdate()
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
				dashlevelImg.color = Color.green;
			}
		}

		dashlevelRT.localPosition = new Vector3(0, (remainingDash - dashTime) * 285, 0);
		dashlevelRT.sizeDelta = new Vector2(60, 570 * remainingDash);
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
			settings.StopEnemyPhysicsCollisions();
			player.DisableMovement();
			dashlevelImg.color = Color.yellow;
			dashing = true;
			di.dashing = true;
		}
	}

	private void StopAttack()
	{
		settings.StartEnemyPhysicsCollisions();
		player.EnableMovement();
		dashing = false;
		//di.dashing = false;
	}
}
