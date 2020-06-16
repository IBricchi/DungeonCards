using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovementInfo{
	public float maxSpeed;
	public float acceleration;
	public float deceleration;

	public MovementInfo(float _maxSpeed, float _acceleartion, float _deceleration){
		maxSpeed = _maxSpeed;
		acceleration = _acceleartion;
		deceleration = _deceleration;
	}
}
public static class MovementSettings
{
	private static MovementInfo def = new MovementInfo(20, 10, 20);
	private static MovementInfo fast = new MovementInfo(20, 10, 20);

	public static MovementInfo GetMovement(MovementID id){
		switch(id){
			case MovementID.def:
				return def;
				break;
			case MovementID.fast:
				return fast;
				break;
			default:
				Debug.LogWarning("Non supported MovmentID asked for movement type, default returned");
				return def;
				break;
		}
	}
}
