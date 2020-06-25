using UnityEngine;
using System.Collections;

public class DashCombat: MonoBehaviour
{
	public bool dashing;
	public Dash dash;

	private Enemy enemy;
	
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(dashing && collision.gameObject.tag == "Enemy")
		{	
			enemy = collision.gameObject.GetComponent<EnemyInfo>().enemy;
			dash.DamageEnemy(enemy);	
		}
	}
}
