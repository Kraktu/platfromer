using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
	[Tooltip("Number of meter by second")]
	public float _speed;
	[Tooltip("Time in seconds to reach the jump height")]
	public float _timeToMaxJump;
	[Tooltip("Unity value of max jump height")]
	public float _jumpHeight;

	float _gravity;
	float _jumpForce;
	float _horizontalModifier=1;
	public int _maxJump;
	int _jumpCount=0;
	bool _jumpedOnLeftWall = false, _jumpOnRightWall = false,_isJumping=false,_wallJumped=false;

	Vector2 _velocity;
	MovementController _movementController;
	private void Start()
	{
		_movementController = GetComponent<MovementController>();
		_gravity = (2 * _jumpHeight) / Mathf.Pow(_timeToMaxJump, 2);
		_jumpForce = Mathf.Abs(_gravity) * _timeToMaxJump;
	}
	private void Update()
	{
		float horizontal = 0;

		if (_movementController._collision.bottom || _movementController._collision.top)
		{
			_velocity.y = 0;
			if (_movementController._collision.bottom)
			{
				_jumpCount = 0;
				_horizontalModifier = 1;
				_isJumping = _jumpOnRightWall=_jumpedOnLeftWall=_wallJumped= false;
			}
		}
		if (_wallJumped == false && _isJumping == true && (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.D)))
		{
			_horizontalModifier = 0.3f;
		}
		
		if (_wallJumped == true&& (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.D)))
		{
			_wallJumped = false;
		}

		if (Input.GetKey(KeyCode.Q))
		{

				horizontal -= 1*_horizontalModifier;

		}
		if (Input.GetKey(KeyCode.D))
		{

				horizontal += 1*_horizontalModifier;


		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_movementController._collision.left&&_jumpedOnLeftWall==false)
			{
				WallJump();
				_jumpedOnLeftWall = true;
				_jumpOnRightWall = false;

			}
			if (_movementController._collision.right&&_jumpOnRightWall==false)
			{
				WallJump();
				_jumpedOnLeftWall = false;
				_jumpOnRightWall = true;
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
		_isJumping = true;
	}
	public void WallJump()
	{
		_jumpCount = _maxJump - 1;
		Jump();
		
		_jumpedOnLeftWall =!_jumpedOnLeftWall;
		_jumpOnRightWall = !_jumpOnRightWall;
		_wallJumped = true;
	}

}
