using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitJump : MonoBehaviour
{
	MovementController _movementController;
	PlayerAccel _playerAccel;
    void Start()
    {
		_movementController = GetComponentInParent<MovementController>();
		_playerAccel = GetComponentInParent<PlayerAccel>();
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
		Enemy enemy = collision.gameObject.GetComponent<Enemy>();

		if (enemy != null)
		{
			HitEnemy(enemy);
		}
	}
	void HitEnemy(Enemy enemy)
	{
		if (!_movementController._collision.bottom&& !_playerAccel._freeze)
		{
			_playerAccel.JumpA();
			enemy.Die();
		}
	}
}
