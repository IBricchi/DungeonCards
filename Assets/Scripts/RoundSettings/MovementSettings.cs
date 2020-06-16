using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovementType{
	public float maxSpeed;
	public float acceleration;
	public float deceleration;

	public MovementType(float _maxSpeed, float _acceleartion, float _deceleration){
		maxSpeed = _maxSpeed;
		acceleration = _acceleartion;
		deceleration = _deceleration;
	}
}
public class MovementSettings
{
	private static MovementType def = new MovementType(20, 10, 20);
	private static MovementType fast = new MovementType(20, 10, 20);

	public MovementType GetMovement(MovementID id){
		MovementType outMovement;
		switch(id){
			case MovementID.def:
				outMovement = def;
				break;
			case MovementID.fast:
				outMovement = fast;
				break;
			default:
				Debug.LogWarning("Non supported MovmentID asked for movement type, default returned");
				outMovement = def;
				break;
		}
		return outMovement;
	}
}
