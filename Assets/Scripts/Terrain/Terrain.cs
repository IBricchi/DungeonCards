using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Terrain : MonoBehaviour
{
	// reference main game objects
	protected Settings settings;
	protected Player player;

	// basic terrain variables
	protected int enemyCount;

	// virtual awake method
	protected virtual void Awake() { }

	// virtual start method
	protected virtual void Start()
	{
		// get settings player refferences
		settings = GameObject.FindGameObjectWithTag("Settings").GetComponent<Settings>();
		player = settings.GetPlayer();
	}

	// returns enemy count
	public int GetEnemyCount()
	{
		return enemyCount;
	}

	// Positiion enemies should position given list of enemies
	public abstract void InitialPositionEnemies(List<Enemy> enemies);
}
