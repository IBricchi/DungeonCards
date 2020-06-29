using UnityEngine;
using System.Collections;

public class DashCollision : MonoBehaviour
{
	Player player;

	private void Awake()
	{
		player = gameObject.GetComponent<Player>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.name == "Simple Follower")
		{
			player.Attack(collision, collision.gameObject.GetComponent<Enemy>());
		}
	}
}
