using UnityEngine;
using System.Collections;

public class Dash
{
	private GameObject player;
	private Rigidbody2D rb;

	private GameObject pointer;
	private Sprite pointerSprite;
	private SpriteRenderer pointerSR;
	private float pointerScale;
	private Vector2 pointerPos;
	private float pointerDist;

	public void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		rb = player.GetComponent<Rigidbody2D>();

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
	}

	public void FixedUpdate()
	{
		pointer.transform.localPosition = pointerPos * pointerDist;
	}
}
