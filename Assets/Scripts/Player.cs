using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
	[Tooltip("Number of meter by second")]
	public float _speed;
	public float _gravity;

	Vector2 _velocity;
	MovementController _movementController;
	private void Start()
	{
		_movementController = GetComponent<MovementController>();
	}
	private void Update()
	{
		int horizontal = 0;
		if (Input.GetKey(KeyCode.Q))
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontal += 1;
		}

		_velocity.x = horizontal * _speed;

		if (_movementController._collision.bottom)
			_velocity.y = 0;
		_velocity.y += _gravity * Time.deltaTime * -1f;

		_movementController.Move(_velocity*Time.deltaTime);
	}
}
