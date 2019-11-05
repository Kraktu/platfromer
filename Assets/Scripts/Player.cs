using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
	[Tooltip("Number of meter by second")]
	public float _speed;
	public float _gravity;
	public float _jumpForce;
	public int _maxJump;
	int _jumpCount=0;

	Vector2 _velocity;
	MovementController _movementController;
	private void Start()
	{
		_movementController = GetComponent<MovementController>();
	}
	private void Update()
	{
		
		int horizontal = 0;
		if (_movementController._collision.bottom || _movementController._collision.top)
		{
			_velocity.y = 0;
			if(_movementController._collision.bottom)
				_jumpCount = 0;
		}


		if (Input.GetKey(KeyCode.Q))
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontal += 1;
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_movementController._collision.left || _movementController._collision.right&&(Input.GetKey(KeyCode.Q)||Input.GetKey(KeyCode.D)))
			{
				_jumpCount = _maxJump - 1;
				Jump();
				if (_movementController._collision.left)
					horizontal = 30;
				if (_movementController._collision.right)
					horizontal = -30;
			}
			else if (_jumpCount < _maxJump)
			{
				Jump();
			}

		}

		_velocity.x = horizontal * _speed;


		_velocity.y += _gravity * Time.deltaTime * -1f;

		_movementController.Move(_velocity*Time.deltaTime);
	}
	public void Jump()
	{
		_velocity.y = _jumpForce;
		_jumpCount++;
	}
}
