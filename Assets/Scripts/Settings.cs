using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	private MovementID moveId;
	private Enemy enemy;

	private RoundSettings rs;
	public MovementInfo GetMovementSettings()
	{
		return rs.moveInfo;
	}
	private void Awake()
	{
		moveId = MovementID.def;
		rs = new RoundSettings(moveId);
		enemy = new SimpleFollower();
		enemy.Awake();
	}

	private void Start()
	{
		enemy.SetPosition(100, 50);
	}

	private void FixedUpdate()
	{
		enemy.FixedUpdate();
	}
}
