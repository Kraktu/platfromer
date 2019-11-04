using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Tooltip("Number of meter by second")]
	public float _speed;
	Vector2 _velocity;
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
		_velocity= new Vector2(horizontal,vertical);
		transform.Translate(_velocity * Time.deltaTime * _speed);
	}
}
