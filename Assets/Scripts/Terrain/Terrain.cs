using UnityEngine;
using System.Collections;

public abstract class Terrain
{
	protected Settings settings;
	protected Player player;

	protected int enemyCount;

	public Terrain(Settings _settings, Player _player)
	{
		settings = _settings;
		player = _player;
	}

	public abstract void Awake();
	public abstract int GetEnemyCount();
	public abstract void PositionEnemies(Enemy[] enemies);

}
