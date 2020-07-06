﻿using UnityEngine;
using System.Collections;

// static instance to create the right type of terrain from an ID
public struct TerrainSettings
{
	public static Terrain PickTerrain(TerrainID id, Settings settings, Player player)
	{
		// switch statment returns the correct type of terrain object based on the combat ID
		switch (id)
		{
			case TerrainID.open: // TODO! need to implement open (I'm thinking a simple rectangle)
				Debug.Log("Terrain ID Open called, not yet implemented, null retunred");
				return null;
			case TerrainID.maze:
				return new Maze(settings, player);
			default:
				Debug.Log("Uknown Terrain ID called, null retunred");
				return null;
		}
	}
}