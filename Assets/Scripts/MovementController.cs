﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class MovementController : MonoBehaviour
{
	public int _horizontalRayCount, _verticalRayCount;
	public LayerMask _layerObstacles;
	float _skinWidth;
	BoxCollider2D _boxCollider;
	Vector2 _bottomLeft, _bottomRight, _topLeft, _topRight;
	float _verticalRaySpacing, _horizontalRaySpacing;
	public Collision _collision;

	public struct Collision
	{
		public bool top, bottom, left, right;
		public void Reset()
		{
			top = bottom = left = right = false;
		}
	}

	private void Start()
	{
		_boxCollider=GetComponent<BoxCollider2D>();
		_skinWidth = 1 / 16f;
		CalculateRaySpacing();
	}
	private void Update()
	{
		
	}
	public void Move(Vector2 velocity)
	{
		_collision.Reset();
		CalculateBounds();
		if (velocity.x !=0)
			HorizontalMove(ref velocity);
		if (velocity.y !=0)
			VerticalMove(ref velocity);
		transform.Translate(velocity);
	}
	public void HorizontalMove(ref Vector2 velocity)
	{
		float direction = Mathf.Sign(velocity.x);
		float distance = Mathf.Abs(velocity.x)+ _skinWidth;
		Vector2 baseOrigin = direction == 1 ? _bottomRight : _bottomLeft;
		for (int i = 0; i < _verticalRayCount; i++)
		{
			
			Vector2 origin = baseOrigin + new Vector2(0, _verticalRaySpacing * i);

			Debug.DrawLine(origin,origin + new Vector2(direction*distance,0));

			RaycastHit2D hit = Physics2D.Raycast(
			origin,
			new Vector2(direction,0),
			distance,
			_layerObstacles
			);

			if (hit)
			{
				velocity.x = (hit.distance-_skinWidth)*direction;
				distance = hit.distance - _skinWidth;
				if (direction > 0)
					_collision.right = true;
				if (direction < 0)
					_collision.left = true;

				Debug.Log(hit.point);
			}
		}
	}
	public void VerticalMove(ref Vector2 velocity)
	{
		float direction = Mathf.Sign(velocity.y);
		float distance = Mathf.Abs(velocity.y) + _skinWidth;
		Vector2 baseOrigin = direction == 1 ? _topLeft : _bottomLeft;
		for (int i = 0; i < _horizontalRayCount; i++) 
		{
			
			Vector2 origin = baseOrigin + new Vector2(_horizontalRaySpacing * i, 0);

			Debug.DrawLine(origin, origin + new Vector2(0, direction * distance));

			RaycastHit2D hit = Physics2D.Raycast(
			origin,
			new Vector2(0, direction),
			distance,
			_layerObstacles
			);

			if (hit)
			{
				velocity.y = (hit.distance - _skinWidth) * direction;
				distance = hit.distance - _skinWidth;
				if (direction > 0)
					_collision.top = true;
				if (direction < 0)
					_collision.bottom = true;
				Debug.Log(hit.point);
			}
		}
	}

	void CalculateRaySpacing()
	{
		Bounds bounds = _boxCollider.bounds;
		bounds.Expand(_skinWidth * -2f);
		_verticalRaySpacing = bounds.size.y / (_verticalRayCount - 1);
		_horizontalRaySpacing = bounds.size.x / (_horizontalRayCount - 1);
	}
	void CalculateBounds()
	{
		Bounds bounds = _boxCollider.bounds;
		bounds.Expand(_skinWidth * -2f);

		_bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
		_bottomRight = new Vector2(bounds.max.x, bounds.min.y);
		_topLeft = new Vector2(bounds.min.x, bounds.max.y);
		_topRight = new Vector2(bounds.max.x, bounds.max.y);
	}
}
