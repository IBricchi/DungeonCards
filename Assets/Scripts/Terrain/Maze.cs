using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : Terrain
{
	private GameObject mazeBody;
	private List<GameObject> walls;
	private Sprite wallSprite;

	private float corridorWidth;
	private float wallWidth;

	private int sizex;
	private int sizey;

	private MazeGen maze;

	protected override void Awake()
	{
		base.Awake();	

		// setup enemy count
		enemyCount = 10;

		// setup size of maze
		sizex = 10;
		sizey = 10;

		// setup dimentions of walls and cooridors
		corridorWidth = 7;
		wallWidth = 3;

		// create new maze object
		mazeBody = new GameObject();
		mazeBody.name = "Maze";

		// initialise wall informaton
		walls = new List<GameObject>();
		wallSprite = Resources.Load<Sprite>("Art/Terrain/Maze/basic");

		// generate maze information using helper class
		maze = new MazeGen(sizex, sizey);

		// turn maze information into game objects
		GameObject lastWall;
		for (int y = 0; y < sizey; y++)
		{
			for (int x = 0; x < sizex; x++)
			{
				// checks if there is a wall at the bottom position
				if (maze[x, y].HasFlag(CellState.Top))
				{
					lastWall = new GameObject();
					lastWall = SetupWall(lastWall, corridorWidth + wallWidth, wallWidth, corridorWidth * x, corridorWidth * y - corridorWidth / 2);
					walls.Add(lastWall);
				}
				// checks if ther is a wall on the right posision
				if (maze[x, y].HasFlag(CellState.Left))
				{
					lastWall = new GameObject();
					lastWall = SetupWall(lastWall, wallWidth, corridorWidth + wallWidth, corridorWidth * x - corridorWidth / 2, corridorWidth * y);
					walls.Add(lastWall);
				}
			}
		}

		// generates top and right outer walls
		lastWall = new GameObject();
		lastWall = SetupWall(lastWall, corridorWidth * sizex + wallWidth, wallWidth, corridorWidth * sizex / 2 - corridorWidth / 2, corridorWidth * sizey - corridorWidth / 2);
		walls.Add(lastWall);

		lastWall = new GameObject();
		lastWall = SetupWall(lastWall, wallWidth, corridorWidth * sizey + wallWidth, corridorWidth * sizex - corridorWidth / 2, corridorWidth * sizey / 2 - corridorWidth / 2);
		walls.Add(lastWall);
	}

	// setup wall helper function for generating wall game objects
	// TODO! replace with prefabs
	private GameObject SetupWall(GameObject wall, float width, float height, float posx, float posy)
	{
		wall.transform.SetParent(mazeBody.transform);
		wall.transform.localScale = new Vector3(width, height, 0);
		wall.transform.localPosition = new Vector3(posx, posy, 0);
		SpriteRenderer sr = wall.AddComponent<SpriteRenderer>();
		sr.sprite = wallSprite;
		sr.color =	new Color(0.6509434f, 0.4831124f, 0.3408241f); // light brown
		wall.AddComponent<BoxCollider2D>();
		return wall;
	}

	protected override void Start()
	{
		base.Start();
	}

	// sets position of all enemis
	public override void InitialPositionEnemies(List<Enemy> enemies)
	{
		// randomly allocates a position for each enemy
		foreach(Enemy enemy in enemies)
		{
			enemy.SetPosition(
				Random.Range(wallWidth, sizex * corridorWidth - wallWidth),
				Random.Range(wallWidth, sizey * corridorWidth - wallWidth)
			);
		}
	}
}
