using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RoundSettings{
	readonly MovementID moveId;
	public MovementInfo moveInfo;

	public RoundSettings(MovementID _moveId = MovementID.def){
		moveId = _moveId;
		moveInfo = MovementSettings.GetMovement(moveId);
	}
}
