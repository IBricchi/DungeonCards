using UnityEngine;
using System.Collections;

public abstract class Combat : MonoBehaviour
{
	// referebces to main game obects
	protected Settings settings;
	protected Player player;
	protected GameObject canvasBody;

	// control scheme
	protected InputMaster c;

	// basic damagae variable (probably will be used as max damage in most occasions)
	protected float damage;

	// setsup variables on initialisation
	protected virtual void Awake()
	{
		// setup new controls
		c = new InputMaster();
	}

	protected virtual void Start()
	{
		// get settings player references
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
		player = settings.GetPlayer();
		canvasBody = settings.GetCanvas();
	}

	// generally setup for controls
	protected virtual void OnEnable()
	{
		// enable control scheme
		c.Enable();
	}
	
	protected virtual void OnDisable()
	{
		// disable control scheme
		c.Disable();
	}

	// all physics should go in here
	protected virtual void FixedUpdate() { }

	// information for attacking, used to pass infomration onto enemy
	public virtual void ColliderAttack(Collider2D collider, Enemy enemy) // used when is trigger = true
	{
		throw new System.NotImplementedException();
	}
	public virtual void CollisionAttack(Collision2D collision, Enemy enemy) // used when is trigger = false
	{
		throw new System.NotImplementedException();
	}
}
