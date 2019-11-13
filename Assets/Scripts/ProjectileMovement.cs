using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
	public float _speed=10;
	public string[] _opaqueTags;
	public bool _destroyedOnImpactWithEnemy;
	public bool _bouncing;
	public int _bounceCount;

	private void Update()
	{
		transform.Translate(Vector3.right*Time.deltaTime*_speed);
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();

		for (int i = 0; i < _opaqueTags.Length; i++)
		{
			if (collision.gameObject.tag==_opaqueTags[i])
			{
				if (_bouncing)
				{
					int sign = (int)Random.Range(-1, 1);
					transform.Rotate(0, 0, 90*sign);
					_bounceCount--;
				}
				if (!_bouncing||_bounceCount==0)
				{
					Destroy(gameObject);
				}
			}
		}

		if (enemy != null && !enemy._isTrap)
		{
			HitEnemy(enemy);
			if (_destroyedOnImpactWithEnemy==true)
			{
				Destroy(gameObject);
			}
		}
	}
	void HitEnemy(Enemy enemy)
	{
			enemy.Die();
	}

}
