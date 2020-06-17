﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MovementInfo{
	public float maxSpeed;
	public float acceleration;
	public float deceleration;
	public float stopThreshold;

	public MovementInfo(float _maxSpeed, float _acceleartion, float _deceleration, float _stopThreshold){
		maxSpeed = _maxSpeed;
		acceleration = _acceleartion;
		deceleration = _deceleration;
		stopThreshold = _stopThreshold;
	}
}
public static class MovementSettings
{
	private static MovementInfo def = new MovementInfo(10f, 5f, 10f, 6f);
	private static MovementInfo fast = new MovementInfo(15f, 7f, 3f, 8f);

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
