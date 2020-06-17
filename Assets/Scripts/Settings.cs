using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
	private MovementID moveId = MovementID.fast;

	private RoundSettings rs;
	public MovementInfo GetMovementSettings()
	{
		return rs.moveInfo;
	}
	private void Awake()
	{
		rs = new RoundSettings(moveId);
	}
}
