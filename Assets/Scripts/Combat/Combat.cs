using UnityEngine;
using System.Collections;

public abstract class Combat
{
	protected Settings settings;

	protected Player player;

	protected GameObject canvasBody;

	protected InputMaster c;

	protected float damage;

	protected Combat(Settings _settings, Player _player, GameObject _canvasBody)
	{
		settings = _settings;
		player = _player;
		canvasBody = _canvasBody;
	}

	public abstract void Awake();
	public abstract void OnEnable();
	public abstract void OnDisable();
	public abstract void FixedUpdate();

	public virtual void ColliderAttack(Collider2D collider, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}
	public virtual void CollisionAttack(Collision2D collision, Enemy enemy)
	{
		throw new System.NotImplementedException();
	}
}
