using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
	private RoundSettings rs = new RoundSettings();
	public MovementInfo getMovementSettings(){
		return rs.moveInfo;
	}
}
