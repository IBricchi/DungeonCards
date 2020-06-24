using UnityEngine;
using System.Collections;

public struct TerrainSettings
{
	public static Terrain PickTerrain(TerrainID id, Settings settings, Player player)
	{
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