using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
	private Settings settings;
	private Player player;

	private GameObject mazeBody;
	private List<GameObject> walls;
	private Sprite wallSprite;

	private int enemyCount;

	private float corridorWidth;
	private float wallWidth;
	private float ww2;

	private int sizex;
	private int sizey;

	public MazeGen maze;

	public Maze(Settings _settings, Player _player)
	{
		settings = _settings;
		player = _player;
	}

	public void Awake()
	{
		sizex = 30;
		sizey = 20;

		corridorWidth = 7;
		wallWidth = 2;
		ww2 = wallWidth / 2;

		mazeBody = new GameObject();
		mazeBody.name = "Maze";

		walls = new List<GameObject>();

		wallSprite = Resources.Load<Sprite>("Art/Terrain/Maze/basic");

		maze = new MazeGen(sizex, sizey);
		GameObject lastWall;
		for (int y = 0; y < sizey; y++)
		{
			for (int x = 0; x < sizex; x++)
			{
				if (maze[x, y].HasFlag(CellState.Top))
				{
					lastWall = new GameObject();
					lastWall = SetupWall(lastWall, corridorWidth + wallWidth, wallWidth, corridorWidth * x, corridorWidth * y - corridorWidth / wallWidth);
					walls.Add(lastWall);
				}
				if (maze[x, y].HasFlag(CellState.Left))
				{
					lastWall = new GameObject();
					lastWall = SetupWall(lastWall, wallWidth, corridorWidth + wallWidth, corridorWidth * x - corridorWidth / wallWidth, corridorWidth * y);
					walls.Add(lastWall);
				}
			}
		}

		lastWall = new GameObject();
		lastWall = SetupWall(lastWall, corridorWidth * sizex + wallWidth, wallWidth, corridorWidth* (sizex) / 2 - corridorWidth/wallWidth, corridorWidth*sizey - corridorWidth/wallWidth);
		walls.Add(lastWall);

		lastWall = new GameObject();
		lastWall = SetupWall(lastWall, wallWidth, corridorWidth * sizey + wallWidth, corridorWidth * sizex - corridorWidth / wallWidth, corridorWidth*(sizey)/2 - corridorWidth/wallWidth);
		walls.Add(lastWall);
	}

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
}
