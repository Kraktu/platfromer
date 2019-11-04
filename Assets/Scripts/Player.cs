using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
	[Tooltip("Number of meter by second")]
	public float _speed;
	Vector2 _velocity;
	MovementController _movementController;
	private void Start()
	{
		_movementController = GetComponent<MovementController>();
	}
	private void Update()
	{
		int horizontal = 0;
		int vertical = 0;
		if (Input.GetKey(KeyCode.Q))
		{
			horizontal -= 1;
		}
		if (Input.GetKey(KeyCode.D))
		{
			horizontal += 1;
		}
		if (Input.GetKey(KeyCode.Z))
		{
			vertical += 1;
		}
		if (Input.GetKey(KeyCode.S))
		{
			vertical -= 1;
		}
		_velocity= new Vector2(horizontal,vertical) * Time.deltaTime * _speed;
		_movementController.Move(_velocity);
	}
}
