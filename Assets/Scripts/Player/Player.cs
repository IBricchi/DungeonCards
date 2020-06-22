using UnityEngine;
using System.Collections;

public class Player
{
	private GameObject body;

	private GameObject camBody;
	private Camera cam;

	private Sprite idleSprite;
	private SpriteRenderer sr;

	private Rigidbody2D rb;
	private PolygonCollider2D pc;

	public void Awake()
	{
		body = new GameObject();
		body.tag = "Player";

		camBody = new GameObject();
		cam = camBody.AddComponent<Camera>();
		camBody.transform.parent = body.transform;
		camBody.transform.localPosition = new Vector3(0, 0, -30);
		camBody.tag = "MainCamera";

		cam.orthographic = true;

		idleSprite = Resources.Load<Sprite>("Art/Player/idle");
		sr = body.AddComponent<SpriteRenderer>();
		sr.sprite = idleSprite;
		sr.color = Color.red;
	
		rb = body.AddComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		rb.gravityScale = 0;
		pc = body.AddComponent<PolygonCollider2D>();
	}

	public void FixedUpdate()
	{
		
	}
}
