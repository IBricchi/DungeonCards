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
		sizex = 20;
		sizey = 20;

		corridorWidth = 5;
		wallWidth = 1;
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
				if(maze[x,y].HasFlag(CellState.Top))
				{
					lastWall = new GameObject();
					lastWall = SetupWall(lastWall, corridorWidth+wallWidth, wallWidth, x+ww2, y);
					walls.Add(lastWall);
				}
				if(maze[x,y].HasFlag(CellState.Left))
				{
					lastWall = new GameObject();
					lastWall = SetupWall(lastWall, wallWidth, corridorWidth+wallWidth, x, y+ww2);
					walls.Add(lastWall);
				}
				
			}
		}
	}

	private GameObject SetupWall(GameObject wall, float width, float height, float posx, float posy)
	{
		wall.transform.SetParent(mazeBody.transform);
		wall.transform.localScale = new Vector3(width, height, 0);
		wall.transform.localPosition = new Vector3(corridorWidth * posx, corridorWidth * posy, 0);
		SpriteRenderer sr = wall.AddComponent<SpriteRenderer>();
		sr.sprite = wallSprite;
		sr.color = new Color(166, 123, 87);
		wall.AddComponent<BoxCollider2D>();
		return wall;
	}
}
